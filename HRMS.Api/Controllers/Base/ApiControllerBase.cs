using HRMS.Api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using HRMS.Api.Extensions;
using HRMS.Core.Models.General;

namespace HRMS.Api.Controllers.Base
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ApiExceptionFilter]
    //[ApiVersion("1.0")]
    [EnableCors]
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IActionResult Success()
        {
            return Ok(new ApiResponse<object>());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected IActionResult Success<T>(T data)
        {
            return Ok(new ApiResponse<T>
            {
                Data = data,
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        protected IActionResult Success<T>(string message)
        {
            return Ok(new ApiResponse<T>
            {
                Message = message
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="pagination"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        protected IActionResult Success<T>(T data, PaginationModel pagination, int totalRecords)
        {
            int roundedTotalPages = 0, pageNumber = 0, pageSize = 0;
            if (pagination != null)
            {
                pageNumber = pagination.PageNumber;
                pageSize = pagination.PageSize;

                double totalPages = totalRecords / (double)pageSize;
                roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            }

            return Ok(new PagedResponse<T>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = roundedTotalPages,
                TotalRecords = totalRecords
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        protected IActionResult Warning<T>(string message)
        {
            return Ok(new ApiResponse<T>
            {
                Message = message,
                IsWarning = true
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        protected IActionResult Error(string message, ModelStateDictionary modelState = null)
        {
            return BadRequest(new ApiResponse<object>
            {
                IsError = true,
                Message = message,
                Errors = modelState?.ToErrorDictionary()
            });
        }
    }
}
