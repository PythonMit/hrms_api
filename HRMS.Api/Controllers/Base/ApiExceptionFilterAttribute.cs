using HRMS.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HRMS.Api.Controllers.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiExceptionFilterAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public ApiExceptionFilterAttribute() : base(typeof(ApiExceptionFilter))
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public class ApiExceptionFilter : IActionFilter, IOrderedFilter
        {
            /// <summary>
            /// 
            /// </summary>
            public int Order { get; set; } = int.MaxValue - 10;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public void OnActionExecuting(ActionExecutingContext context) { }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public void OnActionExecuted(ActionExecutedContext context)
            {
                var exception = context.Exception;
                if (exception != null)
                {
                    var loggerFactory = context.HttpContext.RequestServices.GetService<ILoggerFactory>();
                    if (loggerFactory != null)
                    {
                        var logMessage = $"Error when processing request. QueryString: {context.HttpContext.Request.QueryString}";
                        loggerFactory.CreateLogger("ExceptionHandler").LogError(exception, logMessage);
                    }

                    var message = exception.Message; //exception is ApiException ? exception.Message : "An error has occurred";

                    var apiResponse = new ApiResponse<object>
                    {
                        IsError = true,
                        Message = message,
                    };

                    context.Result = new BadRequestObjectResult(apiResponse);
                    context.ExceptionHandled = true;
                }
            }
        }
    }
}
