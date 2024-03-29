using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HRMS.Api.Models;
using HRMS.Api.Extensions;

namespace HRMS.Api.ActionFilters
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidatorActionFilter : IActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var responese = new ApiResponse<object>();
                responese.IsError = true;
                responese.Errors = context.ModelState.ToErrorDictionary();
                context.Result = new BadRequestObjectResult(responese);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Method intentionally left empty.
        }
    }
}
