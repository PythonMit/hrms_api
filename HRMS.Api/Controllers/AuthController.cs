using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HRMS.Services.Interfaces;
using HRMS.Api.Models;
using HRMS.Api.Controllers.Base;
using HRMS.Core.Models.Auth;
using Microsoft.Extensions.Options;
using HRMS.Core.Settings;
using HRMS.Core.Consts;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Tags("Authentication")]
    public class AuthController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly SlackSettings _slackSettings;
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="authService"></param>
        /// <param name="httpClient"></param>
        /// <param name="slackSettings"></param>
        /// <param name="apiSettings"></param>
        public AuthController(IUserService userService, IAuthService authService, HttpClient httpClient, IOptions<SlackSettings> slackSettings, IOptions<ApiSettings> apiSettings)
        {
            _userService = userService;
            _authService = authService;
            _slackSettings = slackSettings.Value;
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
        }

        /// <summary>
        /// Sign up
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> SignUp(SignUpRequestModel model)
        {
            var userDetails = await _authService.SignUp(model);
            return Success(userDetails);
        }
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("signin")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<SignInResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var result = await _authService.SignIn(model);
            return Success(result);
        }
        /// <summary>
        /// Check useremail already exists
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        [HttpGet("validate-existing-useremail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> CheckUserAlreadyExists([FromQuery] string userEmail)
        {
            var result = await _userService.CheckUserByUserEmail(userEmail);
            return Success(result);
        }
        /// <summary>
        /// Validate username and password 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("valid-user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> ValidUser(SignInModel model)
        {

            var result = await _authService.ValidUser(model);
            return Success(result);
        }
        /// <summary>
        /// Check username already exists
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("validate-existing-username")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> CheckUserNameAlreadyExists([FromQuery] string userName)
        {
            var result = await _userService.CheckUserByUserEmail(userName);
            return Success(result);
        }
        /// <summary>
        /// Slack authorization
        /// </summary>
        /// <returns></returns>
#if DEBUG
        [HttpGet("slack/authorize")]
#else
        [HttpGet("slack/authorize"), ApiExplorerSettings(IgnoreApi = true)]
#endif
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> SlackAuthorization()
        {
            var url = $"{SlackEndpoints.Authorize.Replace("{{scope}}", _slackSettings.Scope)}{_slackSettings.ClientID}&redirect_uri={_apiSettings.AppUrl}{_slackSettings.RedirectUrl}";
            return Success(url);
        }
        /// <summary>
        /// Slack authorization callback
        /// </summary>
        /// <param name="state"></param>
        /// <param name="code"></param>
        /// <param name="error"></param>
        /// <returns></returns>
#if DEBUG
        [HttpGet("slack/callback")]
#else
        [HttpGet("slack/callback"), ApiExplorerSettings(IgnoreApi = true)]
#endif
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> SlackCallBack([FromQuery] string code = "", [FromQuery] string state = "", [FromQuery] string error = "")
        {
            await _authService.GenerateSlackToken(code, state, error);
            return Success();
        }
    }
}