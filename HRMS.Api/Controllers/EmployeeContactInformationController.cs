using Microsoft.AspNetCore.Mvc;
using HRMS.Core.Consts;
using HRMS.Api.Controllers.Base;
using HRMS.Services.Interfaces;
using HRMS.Api.Models;
using HRMS.Core.Models.Employee;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/employee"), Tags("Contact Information"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
    public class EmployeeContactInformationController : ApiControllerBase
    {
        private readonly IEmployeeContactInformationService _employeeContactInformationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeContactInformationService"></param>
        public EmployeeContactInformationController(IEmployeeContactInformationService employeeContactInformationService)
        {
            _employeeContactInformationService = employeeContactInformationService;
        }

        /// <summary>
        ///  Get employee contact information using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/contact-information")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeContactInformationModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeContactInformation([FromRoute] int id)
        {
            var result = await _employeeContactInformationService.GetEmployeeContactInformation(id);
            return Success(result);
        }

        /// <summary>
        /// manage Employee contract Information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("contact-information")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeContactInformationModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> ManageContactInformation([FromBody] EmployeeContactInformationModel model)
        {
            var result = await _employeeContactInformationService.ManageContactInformation(model);
            return Success(result);
        }
    }
}
