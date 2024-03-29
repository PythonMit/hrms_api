using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using HRMS.Core.Settings;
using HRMS.Core.Exstensions;

namespace HRMS.Api.Controllers.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class AnonymousOnlyAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public AnonymousOnlyAttribute() : base(typeof(AnonymousOnlyFilter))
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public class AnonymousOnlyFilter : IAuthorizationFilter
        {
            private readonly ApiSettings _apiSettings;
            private readonly ILogger<AnonymousOnlyFilter> _logger;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="apiSettings"></param>
            /// <param name="logger"></param>
            public AnonymousOnlyFilter(IOptions<ApiSettings> apiSettings, ILogger<AnonymousOnlyFilter> logger)
            {
                _apiSettings = apiSettings.Value;
                _logger = logger;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var userId = context.HttpContext.User.GetUserId();
                if (userId > 0)
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                }
            }
        }
    }
}
