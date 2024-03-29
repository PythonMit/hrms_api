using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Employee;
using HRMS.Core.Utilities.Auth;
using HRMS.Resources;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/employee"), Tags("Security Information")]
    public class EmployeeSecurityInformationController : ApiControllerBase
    {
        private readonly IEmployeeSecurityInformationService _employeeSecurityInformationService;
        private readonly IAppResourceAccessor _appResourceAccessor;
        private readonly IUserContextAccessor _userContextAccessor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeSecurityInformationService"></param>
        /// <param name="appResourceAccessor"></param>
        public EmployeeSecurityInformationController(IEmployeeSecurityInformationService employeeSecurityInformationService, IAppResourceAccessor appResourceAccessor, IUserContextAccessor userContextAccessor)
        {
            _employeeSecurityInformationService = employeeSecurityInformationService;
            _appResourceAccessor = appResourceAccessor;
            _userContextAccessor = userContextAccessor;
        }

        /// <summary>
        /// Get employee security information using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/security-information"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<SecurityInformationModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeSecurityInfomation([FromRoute] int id)
        {
            var result = await _employeeSecurityInformationService.GetEmployeeSecurityInfomation(id);
            return (result == null ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(result));
        }
        /// <summary>
        /// Add employee security information using employeecode
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("security-information"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> ManageSecurityInformation([FromBody] SecurityInformationModel model)
        {
            var result = await _employeeSecurityInformationService.ManageSecurityInformation(model);
            return Success(result);
        }
        /// <summary>
        /// Active or Inactive employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost("{id}/status/{status}"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> ActiveOrInactiveEmployee([FromRoute] int id, [FromRoute] bool status)
        {
            var result = await _employeeSecurityInformationService.ActiveOrInactiveEmployee(id, status);
            return Success(result);
        }
        /// <summary>
        /// Reveal passcode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/reveal-password"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> DecryptPassword([FromRoute] int id)
        {
            var result = await _employeeSecurityInformationService.DecryptPassword(id);
            return Success(result);
        }
        /// <summary>
        /// Verify passcode is correct
        /// </summary>
        /// <param name="passcode"></param>
        /// <returns></returns>
        [HttpGet("verify/{passcode}"), Base.AuthorizeAttribute(RoleTypes.SuperAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> VerifyCurrentPassword([FromRoute] string passcode)
        {
            var result = await _employeeSecurityInformationService.DecryptPassword(_userContextAccessor.EmployeeId ?? 0);
            return Success(result == passcode);
        }
        /// <summary>
        /// Enable or disable employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost("{id}/incapacitate/{status}"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> EnableOrDisableEmployeeUser([FromRoute] int id, [FromRoute] bool status)
        {
            var result = await _employeeSecurityInformationService.EnableOrDisableEmployeeUser(id, status);
            return Success(result);
        }
    }
}
