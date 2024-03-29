using Microsoft.AspNetCore.Mvc;
using HRMS.Core.Consts;
using HRMS.Api.Controllers.Base;
using HRMS.Services.Interfaces;
using HRMS.Api.Models;
using HRMS.Core.Models.Overtime;
using HRMS.Resources;
using HRMS.Core.Utilities.Auth;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/employee/overtime"), Tags("Overtime")]
    public class EmployeeOvertimeController : ApiControllerBase
    {
        private readonly IEmployeeOvertimeService _employeeOvertimeService;
        private readonly IAppResourceAccessor _appResourceAccessor;
        private readonly IUserContextAccessor _userContextAccessor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeOvertimeService"></param>
        /// <param name="appResourceAccessor"></param>
        /// <param name="userContextAccessor"></param>
        public EmployeeOvertimeController(IEmployeeOvertimeService employeeOvertimeService, IAppResourceAccessor appResourceAccessor, IUserContextAccessor userContextAccessor)
        {
            _employeeOvertimeService = employeeOvertimeService;
            _appResourceAccessor = appResourceAccessor;
            _userContextAccessor = userContextAccessor;
        }

        /// <summary>
        /// Add/Update employee overtime
        /// </summary>
        /// <returns></returns>
        [HttpPost, Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> AddorUpdateEmployeeOvertime([FromBody] EmployeeOvertimeRequest request)
        {
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                request.EmployeeCode = _userContextAccessor.EmployeeCode;
            }
            var result = await _employeeOvertimeService.AddorUpdateEmployeeOvertime(request);
            return (result == 0 ? Success(_appResourceAccessor.GetResource("EmployeeOvertime:EmployeeOrContractNoExists")) : Success(result));
        }
        /// <summary>
        /// Get employee overtime
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("{employeeCode}/list"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<EmployeeOvertimeListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeOvertime([FromRoute] string employeeCode, [FromBody] EmployeeOvertimeFilterModel filter)
        {
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                employeeCode = _userContextAccessor.EmployeeCode;
            }
            var result = await _employeeOvertimeService.GetEmployeeOvertime(employeeCode, _userContextAccessor.UserRole, filter);
            return (result == null && result?.TotalRecords == 0 ? Warning<string>(_appResourceAccessor.GetResource("EmployeeOvertime:NoOvertimeDataExists")) : Success(result?.EmployeeOvertimeRecords, filter.Pagination, result?.TotalRecords ?? 0));
        }
        /// <summary>
        /// Get employee overtime
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeOvertimeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeOvertimeById([FromRoute] int id)
        {
            string employeeCode = "";
            if (_userContextAccessor.UserRole == RoleTypes.Employee)
            {
                employeeCode = _userContextAccessor.EmployeeCode;
            }
            var result = await _employeeOvertimeService.GetEmployeeOvertimeById(id, employeeCode, _userContextAccessor.UserRole);
            return (result == null ? Warning<string>(_appResourceAccessor.GetResource("EmployeeOvertime:NoOvertimeDataExists")) : Success(result));
        }
        /// <summary>
        /// Get employee overtime
        /// </summary>
        /// <returns></returns>
        [HttpPut("status"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> SetOvertimeStatus([FromBody] EmployeeOverTimeStatusModel model)
        {
            model.ApprovedBy = _userContextAccessor.EmployeeId;
            var result = await _employeeOvertimeService.SetOvertimeStatus(model);
            return (result == null ? Warning<string>(_appResourceAccessor.GetResource("EmployeeOvertime:NoOvertimeDataExists")) : Success(result));
        }
        /// <summary>
        /// Get all employee overtimes list
        /// </summary>
        /// <returns></returns>
        [HttpPost("list"), Base.AuthorizeAttribute(RoleTypes.HRManager, RoleTypes.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<EmployeeOvertimeListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PagedResponse<object>))]
        public async Task<IActionResult> GetAllEmployeeOvertimes([FromBody] EmployeeOvertimeFilterModel filter)
        {
            var result = await _employeeOvertimeService.GetAllEmployeeOvertimes(filter, _userContextAccessor.UserRole, _userContextAccessor.EmployeeId);
            return (result == null && result?.TotalRecords == 0 ? Warning<string>(_appResourceAccessor.GetResource("EmployeeOvertime:NoOvertimeDataExists")) : Success(result?.EmployeeOvertimeRecords, filter.Pagination, result?.TotalRecords ?? 0));
        }
        /// <summary> 
        /// Delete employee overtime record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> DeleteEmployeeOvertime([FromRoute] int id)
        {
            string employeeCode = "";
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                employeeCode = _userContextAccessor.EmployeeCode;
            }
            return Success(await _employeeOvertimeService.DeleteEmployeeOvertime(id, employeeCode));
        }
        /// <summary>
        /// Remove employee overtime
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("remove/{id}"), Base.AuthorizeAttribute(RoleTypes.SuperAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> RemoveEmployeeOvertime([FromRoute] int id)
        {
            if (_userContextAccessor.UserRole != RoleTypes.SuperAdmin)
            {
                return Warning<string>(_appResourceAccessor.GetResource("User:InsufficientPermissions"));
            }
            var result = await _employeeOvertimeService.RemoveEmployeeOvertime(id);
            return !result ? Warning<string>(_appResourceAccessor.GetResource("General:ErrorUpsertRecords")) : Success(result);
        }
    }
}
