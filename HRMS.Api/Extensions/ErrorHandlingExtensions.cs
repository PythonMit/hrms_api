using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HRMS.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ErrorHandlingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static IDictionary<string, string[]> ToErrorDictionary(this ModelStateDictionary modelState)
        {
            var errorList = modelState.ToDictionary(
                kvp => char.ToLower(kvp.Key[0]) + kvp.Key.Substring(1),
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            return errorList;
        }
    }
}
