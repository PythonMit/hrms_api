using Microsoft.AspNetCore.Mvc;
using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Services.Interfaces;
using HRMS.Core.Consts;
using HRMS.Core.Models.Leave;
using HRMS.Core.Utilities.Auth;
using HRMS.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using HRMS.Core.Utilities.Notification.Slack;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/employee/leave"), Tags("Leave")]
    public class EmployeeLeaveController : ApiControllerBase
    {
        private readonly IEmployeeLeaveService _employeeLeaveService;
        private readonly IUserContextAccessor _userContextAccessor;
        private readonly IAppResourceAccessor _appResourceAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeLeaveService"></param>
        /// <param name="userContextAccessor"></param>
        /// <param name="appResourceAccessor"></param>
        public EmployeeLeaveController(IEmployeeLeaveService employeeLeaveService, IUserContextAccessor userContextAccessor, IAppResourceAccessor appResourceAccessor)
        {
            _employeeLeaveService = employeeLeaveService;
            _userContextAccessor = userContextAccessor;
            _appResourceAccessor = appResourceAccessor;
        }
        #region Leave Balance
        /// <summary>
        /// Generate employee leave
        /// </summary>
        /// <returns></returns>
        [HttpPost("{employeeCode}/generate"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<int>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GenerateEmployeeLeave([FromBody] IEnumerable<EmployeeLeaveRequestModel> model, [FromRoute, DisallowNull] string employeeCode)
        {
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                employeeCode = _userContextAccessor.EmployeeCode;
            }
            var result = await _employeeLeaveService.GenerateEmployeeLeave(model, employeeCode);
            return (result == null || (result != null && result.Count() == 0) ? Warning<string>(_appResourceAccessor.GetResource("EmployeeLeave:NoValidContract")) : Success(result));
        }
        /// <summary>
        /// Get employee leave balance
        /// </summary>
        /// <returns></returns>
        [HttpPost("balance"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeLeaveListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<int>))]
        public async Task<IActionResult> GetEmployeeLeave([FromBody] EmployeeLeaveFilterModel filter)
        {
            int? employeeId = null;
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                employeeId = _userContextAccessor.EmployeeId;
            }

            // Depricated
            //var result = await _employeeLeaveService.GetEmployeeLeaveBalance(filter, employeeId);
            var result = await _employeeLeaveService.GetEmployeeLeaveBalanceV2(filter, employeeId, _userContextAccessor.UserRole);
            return (result == null ? Warning<string>(_appResourceAccessor.GetResource("EmployeeLeave:NoValidLeaveBalance")) : Success(result, filter.Pagination, result.TotalRecords));
        }
        /// <summary>
        /// View leave balance for applied type
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <param name="leaveFor"></param>
        /// <returns></returns>
        [HttpGet("{employeeCode}/balance/view"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<double?>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeLeave([FromRoute, DisallowNull] string employeeCode, [FromQuery] string? leaveFor = null)
        {
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                employeeCode = _userContextAccessor.EmployeeCode;
            }
            var result = await _employeeLeaveService.GetCurrentEmployeeLeaveBalance(employeeCode, (leaveFor ?? HttpUtility.UrlDecode(leaveFor)));
            return (result == null ? Warning<string>(_appResourceAccessor.GetResource("EmployeeLeave:NoValidContract")) : Success(result));
        }
        /// <summary>
        /// Get all employee without generation of leave balance
        /// </summary>
        /// <returns></returns>
        [HttpPost("generate/remaining"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<LeaveEmployeeDetailModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetRaminingEmployeeForLeaves()
        {
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                //employeeCode = _userContextAccessor.EmployeeCode;
            }
            var result = await _employeeLeaveService.GetRaminingEmployeeForLeaves();
            return (result == null || (result != null && result.Count() == 0) ? Warning<string>(_appResourceAccessor.GetResource("EmployeeLeave:NoRemainingEmployeeLeave")) : Success(result));
        }
        /// <summary>
        /// Get employee who have acquired leaves
        /// </summary>
        /// <returns></returns>
        [HttpPost("acquired"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<LeaveEmployeeDetailModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<int>))]
        public async Task<IActionResult> GetLeaveAcquiredEmployee()
        {
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                //employeeCode = _userContextAccessor.EmployeeCode;
            }
            return Success(await _employeeLeaveService.GetLeaveAcquiredEmployee());
        }
        /// <summary>
        /// Get leave balance re-calculate for current month
        /// </summary>
        /// <returns></returns>
        [HttpPost("balance/calibrate"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<string>?>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<int>))]
        public async Task<IActionResult> GetLeaveBalanceCalibrate()
        {
            return Success(await _employeeLeaveService.GetLeaveBalanceCalibrate());
        }
        /// <summary>
        /// Delete leave balance informations
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpDelete("balance/{id}"), Base.AuthorizeAttribute(RoleTypes.SuperAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<int>))]
        public async Task<IActionResult> DeleteLeaveBalanceInformations([FromRoute] string employeeCode)
        {
            if (_userContextAccessor.UserRole != RoleTypes.SuperAdmin)
            {
                return Warning<string>(_appResourceAccessor.GetResource("User:InsufficientPermissions"));
            }
            return Success(await _employeeLeaveService.DeleteLeaveBalanceInformations(employeeCode));
        }
        #endregion Leave Balance        
        #region Leave Application Detail
        /// <summary>
        /// Get Employee Leave Details and other information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("application/{id}/request/details"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeLeaveDetailModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeLeaveDetails([FromRoute] Guid id)
        {
            //return Success(await _employeeLeaveService.GetEmployeeLeaveDetails(id));
            return Success(await _employeeLeaveService.GetEmployeeLeaveDetailsV2(id));
        }
        /// <summary>
        /// Get Employee Leave balance information and Details
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpGet("application/{employeeCode}/balance/details"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeLeaveBalanceDetailModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeLeaveBalanceDetails([FromRoute] string employeeCode)
        {
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                employeeCode = _userContextAccessor.EmployeeCode;
            }
            //Note: Depricated
            //return Success(await _employeeLeaveService.GetEmployeeLeaveBalanceDetails(employeeCode));
            return Success(await _employeeLeaveService.GetEmployeeLeaveBalanceDetailsV2(employeeCode));
        }
        #endregion Leave Application Detail        
    }
}
