using Microsoft.AspNetCore.Localization;
using System.Security.Claims;

namespace HRMS.Api.Utility.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public class AccessTokenRequestCultureProvider : RequestCultureProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var locale = httpContext.User.FindFirst(ClaimTypes.Locality)?.Value;
            if (locale == null)
            {
                Task.FromResult<ProviderCultureResult>(null);
            }
            return Task.FromResult(new ProviderCultureResult(locale));
        }
    }
}
