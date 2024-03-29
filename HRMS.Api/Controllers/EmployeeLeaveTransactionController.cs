using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Leave;
using HRMS.Core.Utilities.Auth;
using HRMS.Resources;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// Employee Leave Transaction
    /// </summary>
    [Route("api/employee/leave"), Tags("Leave Transaction")]
    public class EmployeeLeaveTransactionController : ApiControllerBase
    {
        private readonly IEmployeeLeaveService _employeeLeaveService;
        private readonly IAppResourceAccessor _appResourceAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeLeaveService"></param>
        /// <param name="appResourceAccessor"></param>
        public EmployeeLeaveTransactionController(IEmployeeLeaveService employeeLeaveService, IAppResourceAccessor appResourceAccessor)
        {
            _employeeLeaveService = employeeLeaveService;
            _appResourceAccessor = appResourceAccessor;
        }

        #region Leave Transactions
        /// <summary>
        /// Get Employee Leave Transactions
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpGet("{employeeCode}/transactions"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeLeaveTransactionDetailModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeLeaveTransactions([FromRoute] string employeeCode)
        {
            return Success(await _employeeLeaveService.GetEmployeeLeaveTransactions(employeeCode));
        }
        /// <summary>
        /// Update employee leave transaction
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("transactions"), Base.AuthorizeAttribute(RoleTypes.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeLeaveTransactionModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> UpdateEmployeeLeaveTransactions([FromBody] LeaveTransactionUpdateRequestModel model)
        {
            var result = await _employeeLeaveService.UpdateEmployeeLeaveTransactions(model);
            return (result == null ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(result));
        }
        /// <summary>
        /// Add new employee leave transaction
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("transactions"), Base.AuthorizeAttribute(RoleTypes.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeLeaveTransactionModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> AddEmployeeLeaveTransactions([FromBody] LeaveTransactionUpdateRequestModel model)
        {
            var result = await _employeeLeaveService.AddEmployeeLeaveTransactions(model);
            return (result == null ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(result));
        }
        #endregion Leave Transactions
    }
}
