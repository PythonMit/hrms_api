using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using HRMS.DBL.Stores;
using HRMS.Core.Exceptions;
using HRMS.DBL.Entities;
using HRMS.Core.Settings;
using HRMS.Core.Consts;
using HRMS.Resources;
using HRMS.Services.Interfaces;
using HRMS.Core.Models.Auth;
using HRMS.Core.Models.User;
using HRMS.Core.Utilities.Cipher;
using System.Net.Http;
using HRMS.Core.Models.Slack;
using Newtonsoft.Json;
using HRMS.Core.Models.SystemFlag;

namespace HRMS.Services.Common
{
    public class AuthService : IAuthService
    {
        private readonly ApiSettings _apiSettings;
        private readonly UserStore _userStore;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppResourceAccessor _appResourceAccessor;
        private readonly SlackSettings _slackSettings;
        private readonly ISystemFlagService _systemFlagService;

        public AuthService(IOptions<ApiSettings> apiSettings, IHttpContextAccessor httpContextAccessor, UserStore userStore, IAppResourceAccessor appResourceAccessor, IOptions<SlackSettings> slackSettings, ISystemFlagService systemFlagService)
        {
            _apiSettings = apiSettings.Value;
            _userStore = userStore;
            _httpContextAccessor = httpContextAccessor;
            _appResourceAccessor = appResourceAccessor;
            _slackSettings = slackSettings.Value;
            _systemFlagService = systemFlagService;
        }
        public async Task<UserDetailModel> SignUp(SignUpRequestModel model)
        {
            //Validate
            var hasUser = await _userStore.HasUser(model.Emailaddress);
            if (hasUser)
            {
                throw new ApiException(_appResourceAccessor.GetResource("SignIn:UserAlreadyExists"));
            }

            //Genrate hash and add user
            var (hash, salt) = CipherUtils.GenerateHash(model.Password);

            var user = new User
            {
                Username = model.Username,
                Emailaddress = model.Emailaddress,
                Password = hash,
                Salt = salt,
                RecordStatus = RecordStatus.Active,
                RoleId = !model.RoleId.HasValue ? (int)RoleTypes.Employee : model.RoleId.Value,
            };

            var newUserId = await _userStore.AddUser(user);

            var employee = new Employee
            {
                UserId = newUserId,
                FirstName = model.Firstname,
                LastName = model.Lastname,
            };
            await _userStore.AddEmployeeDetails(employee);
            return new UserDetailModel
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                PhoneNumber = model.Mobilenumber,
            };
        }
        public async Task<SignInResponseModel> SignIn(SignInModel model)
        {
            var user = new User();
            bool isEmail = Regex.IsMatch(model.Username, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isEmail)
            {
                user = await _userStore.GetUserByEmail(model.Username);
            }
            else
            {
                user = await _userStore.GetUserByUsername(model.Username);
            }

            if (user == null)
            {
                throw new ApiException(_appResourceAccessor.GetResource("SignIn:InvalidEmailPassword"));
            }

            bool isValid = CipherUtils.CheckHash(model.Password, user.Salt, user.Password);

            if (!isValid)
            {
                throw new ApiException(_appResourceAccessor.GetResource("SignIn:InvalidEmailPassword"));
            }

            if (user.IsVerified == false)
            {
                throw new ApiException(_appResourceAccessor.GetResource("SignIn:AccountNotConfirmed"));
            }

            if (user.RecordStatus == RecordStatus.InActive)
            {
                throw new ApiException(_appResourceAccessor.GetResource("SignIn:AccountInActive"));
            }

            if (user.Disabled == true)
            {
                throw new ApiException(_appResourceAccessor.GetResource("SignIn:AccountIsDisabled"));
            }

            var token = SetJwtToken(_httpContextAccessor.HttpContext, user.Role?.Name, user.Emailaddress, user.Id, user.Employee?.Id, user.Employee.EmployeeCode, user.Employee?.BranchId, user.Role?.Priority);

            var joinDate = await _userStore.GetJoinDateByEmployeeId(user.Employee?.Id);

            var result = new SignInResponseModel
            {
                UserId = user.Id,
                EmailAddress = user.Emailaddress,
                UserName = user.Username,
                Role = new RoleModel { Id = user.Role.Id, Name = user.Role?.Name, Priority = user.Role?.Priority ?? 0 },
                FirstName = user.Employee?.FirstName,
                LastName = user.Employee?.LastName,
                Token = token,
                RecordStatus = user.RecordStatus,
                ProfileImage = user.Employee?.ImagekitDetail?.Url ?? "",
                JoinDate = joinDate,
                EmployeeCode = user.Employee?.EmployeeCode
            };

            return result;
        }
        public async Task<bool> ValidUser(SignInModel model)
        {
            var user = await _userStore.GetUserByEmail(model.Username);
            if (user == null)
            {
                return false;
            }
            var isvalid = CipherUtils.CheckHash(model.Password, user.Salt, user.Password);
            return isvalid;
        }
        public async Task<bool> GenerateSlackToken(string code, string state = "", string error = "")
        {
            var @params = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", _slackSettings.ClientID),
                new KeyValuePair<string, string>("client_secret", _slackSettings.ClientSecret),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", $"{_slackSettings.RedirectUrl}")
            };

            using (HttpClient httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{SlackEndpoints.OAuthAccess}")
                {
                    Content = new FormUrlEncodedContent(@params)
                };
                var result = await httpClient.SendAsync(request);
                var resultJsonString = await result.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<SlackAuthResponse>(resultJsonString);
                if (tokenResponse == null)
                {
                    return false;
                }

                var flag = await _systemFlagService.GetFlagDetailsByName("Sys_Slack_Access_Token");
                await _systemFlagService.AddorUpdateSystemFlag(new SystemFlagModel
                {
                    Id = (flag == null ? 0 : flag.Id),
                    Name = "Sys_Slack_Access_Token",
                    Description = "Slack api access token",
                    Value = tokenResponse.access_token,
                    Tags = "slackaccesstoken",
                    RecordStatus = RecordStatus.Active
                });
            }
            return true;
        }
        private TokenModel SetJwtToken(HttpContext httpContext, string role, string email, int userId, int? employeeId, string employeeCode, int? branchId, int? priority)
        {
            //_httpContextAccessor.HttpContext.Response.Cookies.Delete(AuthOptions.CookieKey);
            var now = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            };

            if (!string.IsNullOrEmpty(role))
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }

            if (employeeId.HasValue)
            {
                claims.Add(new Claim(AuthOptions.EmployeeId, employeeId?.ToString()));
            }

            if (!string.IsNullOrEmpty(employeeCode))
            {
                claims.Add(new Claim(AuthOptions.EmployeeCode, employeeCode));
            }

            if (branchId.HasValue)
            {
                claims.Add(new Claim(AuthOptions.BranchId, branchId?.ToString()));
            }

            if (priority.HasValue)
            {
                claims.Add(new Claim(AuthOptions.Priority, priority?.ToString()));
            }

            claims.Add(new Claim(AuthOptions.Environment, (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")));

            var experationTime = now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime));

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            var jwt = new JwtSecurityToken(
                AuthOptions.Issuer,
                _apiSettings.AppUrl,
                claimsIdentity.Claims,
                notBefore: now,
                experationTime,
                signingCredentials: new SigningCredentials(CipherUtils.GetSymmetricSecurityKey(AuthOptions.SecurityKey), SecurityAlgorithms.HmacSha256)
            );

            return new TokenModel
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                ExpirationTime = experationTime
            };
        }
    }
}
