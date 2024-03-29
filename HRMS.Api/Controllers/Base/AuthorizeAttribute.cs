using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRMS.Api.Controllers.Base
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<RoleTypes> _roles;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        public AuthorizeAttribute(params RoleTypes[] roles)
        {
            _roles = roles ?? new RoleTypes[] { };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization
            var user = context.HttpContext.User;
            var role = context.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value;
            if (role == RoleTypes.SuperAdmin.GetEnumDescriptionAttribute())
                return;

            if (user == null || role == null || (_roles != null && !_roles.Any(x => x.GetEnumDescriptionAttribute() == role)))
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
