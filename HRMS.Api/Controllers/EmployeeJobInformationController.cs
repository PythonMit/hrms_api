using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Employee;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/employee"), Tags("Job Information"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]

    public class EmployeeJobInformationController : ApiControllerBase
    {
        private readonly IEmployeeJobInformationService _employeeJobInformationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeJobInformationService"></param>
        public EmployeeJobInformationController(IEmployeeJobInformationService employeeJobInformationService)
        {
            _employeeJobInformationService = employeeJobInformationService;
        }
        /// <summary>
        ///  Get employee job information using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/job-information")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeJobInformationModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeJobInformation([FromRoute] int id)
        {
            var result = await _employeeJobInformationService.GetEmployeeJobInformation(id);
            return Success(result);
        }
        /// <summary>
        /// Add employee job information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("job-information")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> AddOrUpdateEmployeeJobInformation(EmployeeJobInformationModel model)
        {
            var result = await _employeeJobInformationService.AddOrUpdateEmployeeJobInformation(model);
            return Success(result);
        }
    }
}
