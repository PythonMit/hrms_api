using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using HRMS.Core.Exstensions;
using HRMS.Core.Consts;

namespace HRMS.Core.Utilities.Auth
{
    public class UserContextAccessor : IUserContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    return -1;
                }
                var userId = _httpContextAccessor.HttpContext.User.GetUserId();
                if (userId.HasValue && userId <= 0)
                {
                    throw new Exception("User id is empty");
                }
                return userId;
            }
        }

        public bool? IsAuthorizedUser => _httpContextAccessor.HttpContext.User.GetUserId() != null;

        public RoleTypes? UserRole
        {
            get
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    return null;
                }
                var userRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value;
                if (string.IsNullOrEmpty(userRole))
                {
                    throw new Exception("User role is empty");
                }
                return GeneralExtensions.ToEnum<RoleTypes>(userRole);
            }
        }

        public bool? IsAdmin => RoleTypes.Admin.GetEnumDescriptionAttribute().Contains(UserRole.GetEnumDescriptionAttribute());

        public int? EmployeeId
        {
            get
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    return -1;
                }
                var employeeId = _httpContextAccessor.HttpContext.User.FindFirst(AuthOptions.EmployeeId)?.Value;
                if (string.IsNullOrEmpty(employeeId))
                {
                    throw new Exception("Employee id is empty");
                }
                return int.Parse(employeeId);
            }
        }

        public string EmployeeCode
        {
            get
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    return string.Empty;
                }
                var employeeCode = _httpContextAccessor.HttpContext.User.FindFirst(AuthOptions.EmployeeCode)?.Value;
                if (string.IsNullOrEmpty(employeeCode))
                {
                    throw new Exception("Employee code is empty");
                }
                return employeeCode;
            }
        }

        public int? BranchId
        {
            get
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    return -1;
                }
                var branchId = _httpContextAccessor.HttpContext.User.FindFirst(AuthOptions.BranchId)?.Value;
                if (string.IsNullOrEmpty(branchId))
                {
                    throw new Exception("Branch id is empty");
                }
                return string.IsNullOrEmpty(branchId) ? 0 : int.Parse(branchId);
            }
        }

        public int? Priority
        {
            get
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    return -1;
                }
                var priority = _httpContextAccessor.HttpContext.User.FindFirst(AuthOptions.Priority)?.Value;
                if (string.IsNullOrEmpty(priority))
                {
                    throw new Exception("Priority id is empty");
                }
                return string.IsNullOrEmpty(priority) ? 0 : int.Parse(priority);
            }
        }
    }
}
