using Microsoft.Extensions.Options;
using HRMS.Core.Settings;
using HRMS.Core.Consts;

namespace HRMS.Api.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class MaintananceModeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtCookieMiddleware> _logger;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        /// <param name="appSettings"></param>
        public MaintananceModeMiddleware(RequestDelegate next, ILogger<JwtCookieMiddleware> logger, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _logger = logger;
            _appSettings = appSettings.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.StartsWith("/api"))
            {
                if (context.Request.Path.Value.ToLower().Contains("api/auth/set-demo-mode"))
                {
                    await _next(context);
                }
                else
                {
                    if (_appSettings.DemoModeEnabled)
                    {
                        var demoModeCookie = context.Request.Cookies[AuthOptions.DemoModeEnabled];
                        if (!string.IsNullOrEmpty(demoModeCookie) && demoModeCookie == "True")
                        {
                            await _next(context);
                        }
                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                        }

                    }
                    else
                    {
                        await _next(context);
                    }
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
