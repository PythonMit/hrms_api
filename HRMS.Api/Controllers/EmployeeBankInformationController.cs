using Microsoft.AspNetCore.Mvc;
using HRMS.Core.Consts;
using HRMS.Api.Controllers.Base;
using HRMS.Services.Interfaces;
using HRMS.Api.Models;
using HRMS.Core.Models.Employee;
using HRMS.Resources;
using System.IO;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/employee"), Tags("Bank Information"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
    public class EmployeeBankInformationController : ApiControllerBase
    {
        private readonly IEmployeeBankInformationService _employeeBankInformationService;
        private readonly IAppResourceAccessor _appResourceAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeBankInformationService"></param>
        /// <param name="appResourceAccessor"></param>
        public EmployeeBankInformationController(IEmployeeBankInformationService employeeBankInformationService, IAppResourceAccessor appResourceAccessor)
        {
            _employeeBankInformationService = employeeBankInformationService;
            _appResourceAccessor = appResourceAccessor;
        }

        /// <summary>
        ///  Get employee bank information using id
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpGet("{employeeCode}/bank-information")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeBankInformationModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeContactInformation([FromRoute] string employeeCode)
        {
            var result = await _employeeBankInformationService.GetEmployeeBankInformation(employeeCode);
            return (result == null ? Warning<string>(_appResourceAccessor.GetResource("General:ErrorUpsertRecords")) : Success(result));
        }
        /// <summary>
        /// manage employee bank Information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("bank-information")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> ManageBankInformation([FromBody] EmployeeBankInformationModel model)
        {
            return Success(await _employeeBankInformationService.ManageBankInformation(model));
        }
    }
}
