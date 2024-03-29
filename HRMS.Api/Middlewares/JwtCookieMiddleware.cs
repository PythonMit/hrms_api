using HRMS.Core.Consts;

namespace HRMS.Api.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtCookieMiddleware> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public JwtCookieMiddleware(RequestDelegate next, ILogger<JwtCookieMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies[AuthOptions.CookieKey];
            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }
            else
            {
                _logger.LogError($"No auth cookie or auth token for {AuthOptions.CookieKey}");
            }
            await _next(context);
        }
    }
}
