using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Leave;
using HRMS.Core.Utilities.Auth;
using HRMS.Resources;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/employee/leave"), Tags("Leave Application")]
    public class EmployeeLeaveApplicationController : ApiControllerBase
    {
        private readonly IEmployeeLeaveApplicationService _employeeApplicationService;
        private readonly IUserContextAccessor _userContextAccessor;
        private readonly IAppResourceAccessor _appResourceAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeLeaveService"></param>
        /// <param name="userContextAccessor"></param>
        /// <param name="appResourceAccessor"></param>
        public EmployeeLeaveApplicationController(IEmployeeLeaveApplicationService employeeApplicationService, IUserContextAccessor userContextAccessor, IAppResourceAccessor appResourceAccessor)
        {
            _employeeApplicationService = employeeApplicationService;
            _userContextAccessor = userContextAccessor;
            _appResourceAccessor = appResourceAccessor;
        }

        #region Leave Application
        /// <summary>
        /// Add or Update employee leave
        /// </summary>
        /// <returns></returns>
        [HttpPost("application"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Guid>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<int>))]
        public async Task<IActionResult> AddorUpdateEmployeeLeaveApplication([FromBody] EmployeeLeaveApplicationRequestModel model)
        {
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                model.EmployeeCode = _userContextAccessor.EmployeeCode;
            }

            if (string.IsNullOrEmpty(model.EmployeeCode))
            {
                return Warning<string>(_appResourceAccessor.GetResource("EmployeeLeave:InvalidApplicationForLeave"));
            }

            var result = await _employeeApplicationService.AddorUpdateEmployeeLeaveApplication(model);
            return (result == Guid.Empty ? Warning<string>(_appResourceAccessor.GetResource("EmployeeLeave:InvalidApplicationForLeave")) : Success(result));
        }
        /// <summary>
        /// Get all employee leave list
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("application/list"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<EmployeeLeaveApplicationListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PagedResponse<object>))]
        public async Task<IActionResult> GetEmployeeLeaveApplications([FromBody] EmployeeLeaveApplicationFilterModel filter)
        {
            int? employeeId = null;
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                employeeId = _userContextAccessor.EmployeeId;
            }
            var result = await _employeeApplicationService.GetEmployeeLeaveApplications(filter, employeeId, _userContextAccessor.UserRole);
            return Success(result, filter.Pagination, result.TotalRecords);
        }
        /// <summary>
        /// Remove employee leaves
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete("application"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteEmployeeLeaveApplications([FromBody] IEnumerable<Guid> ids)
        {
            return Success(await _employeeApplicationService.DeleteEmployeeLeaveApplications(ids));
        }
        /// <summary>
        /// Update employee leave(s) status
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("application/status"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> SetEmployeeLeaveApplicationsStatus([FromBody] EmployeeLeaveApplicationStatusRequestModel model)
        {
            model.ApprovedBy = _userContextAccessor?.EmployeeId ?? 0;
            var result = await _employeeApplicationService.SetEmployeeLeaveApplicationsStatus(model);
            return (result ? Success(result) : Warning<string>(_appResourceAccessor.GetResource("EmployeeLeave:StatusRequest")));
        }
        /// <summary>
        /// Get employee leave application details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("application/{id}"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeLeaveApplicationModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeLeaveApplications([FromRoute] Guid id)
        {
            return Success(await _employeeApplicationService.GetEmployeeLeaveApplications(id));
        }
        /// <summary>
        /// Get sandwich days for leave application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("application/sandwich-days"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int?>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetLeaveApplicationSandwichDays([FromBody] SandwichDateRequestModel model)
        {
            return Success(await _employeeApplicationService.GetLeaveApplicationSandwichDays(model));
        }
        /// <summary>
        /// Get total contract for selected employee
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpPost("application/{employeeCode}/contracts"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int?>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetTotalContract([FromRoute] string employeeCode)
        {
            return Success(await _employeeApplicationService.GetTotalRunningContract(employeeCode));
        }
        #endregion Leave Application
        #region Leave Application Comment
        /// <summary>
        /// Add or Update Employee Leave Application Comment 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("application/comment"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Guid>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<int>))]
        public async Task<IActionResult> AddorUpdateEmployeeLeaveApplicationComment(EmployeeLeaveApplicationCommentRequestModel model)
        {
            return Success(await _employeeApplicationService.AddorUpdateEmployeeLeaveApplicationComment(model));
        }
        /// <summary>
        /// Get Employee Leave Application Comments 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("application/comment/list"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<EmployeeLeaveApplicationCommentListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PagedResponse<int>))]
        public async Task<IActionResult> GetEmployeeLeaveApplicationComments(EmployeeLeaveApplicationCommentFilterModel filter)
        {
            var result = await _employeeApplicationService.GetEmployeeLeaveApplicationComments(filter);
            return Success(result, filter.Pagination, result.TotalRecords);
        }
        /// <summary>
        /// Remove employee leaves
        /// </summary>
        /// <param name="commentIds"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("application/{id}/comments"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteEmployeeLeaveApplicationComments([FromBody] IEnumerable<Guid> commentIds, [FromRoute] Guid id)
        {
            return Success(await _employeeApplicationService.DeleteEmployeeLeaveApplicationComments(commentIds, id));
        }
        #endregion Leave Application Comment        
    }
}
