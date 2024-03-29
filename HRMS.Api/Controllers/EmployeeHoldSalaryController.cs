using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Salary;
using HRMS.Core.Utilities.Auth;
using HRMS.Core.Utilities.General;
using HRMS.Resources;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// Employee hold salary
    /// </summary>
    [Route("api/employee/salary"), Tags("Salary - Hold")]
    [ApiController]
    public class EmployeeHoldSalaryController : ApiControllerBase
    {
        private readonly IEmployeeSalaryService _employeeSalaryService;
        private readonly IAppResourceAccessor _appResourceAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeSalaryService"></param>
        /// <param name="appResourceAccessor"></param>
        /// <param name="userContextAccessor"></param>
        /// <param name="systemFlagService"></param>
        /// <param name="generalUtilities"></param>
        public EmployeeHoldSalaryController(IEmployeeSalaryService employeeSalaryService, IAppResourceAccessor appResourceAccessor)
        {
            _employeeSalaryService = employeeSalaryService;
            _appResourceAccessor = appResourceAccessor;
        }

        #region Partial Hold Salary
        /// <summary>
        /// Add or Update Employee Partial Hold Salary
        /// </summary>
        /// <returns></returns>
        [HttpPost("hold"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Guid?>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> AddorUpdateEmployeeHoldSalary([FromBody] EmployeeHoldSalaryRequestModel model)
        {
            var result = await _employeeSalaryService.AddorUpdateEmployeeHoldSalary(model);
            return result.HasValue ? Success(result) : Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable"));
        }
        /// <summary>
        /// Get Employee Hold Salary
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost("hold/list"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeHoldSalaryListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeHoldSalary([FromBody] EmployeeHoldSalaryFilterModel filters)
        {
            var result = await _employeeSalaryService.GetEmployeeHoldSalary(filters);
            return result?.TotalRecords > 0 ? Success(result) : Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable"));
        }
        /// <summary>
        /// Get history employee hold salary
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost("hold/history"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeHoldSalaryListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetHistoryEmployeeHoldSalary([FromBody] EmployeeHoldSalaryHistoryFilterModel filters)
        {
            var result = await _employeeSalaryService.GetHistoryEmployeeHoldSalary(filters);
            return (result?.TotalRecords > 0 ? Success(result) : Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")));
        }
        /// <summary>
        /// Remove employee hold salary
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete("hold/remove"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> RemoveEmployeeHoldSalary([FromBody] IEnumerable<Guid> ids)
        {
            var result = await _employeeSalaryService.RemoveEmployeeHoldSalary(ids);
            return (result ? Success(result) : Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")));
        }
        #endregion Partial Hold Salary
    }
}
