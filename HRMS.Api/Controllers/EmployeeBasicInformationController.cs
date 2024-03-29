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
    [Route("api/employee"), Tags("Basic Information"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
    public class EmployeeBasicInformationController : ApiControllerBase
    {
        private readonly IEmployeeBasicInformationService _employeeBasicInformationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeBasicInformationService"></param>
        public EmployeeBasicInformationController(IEmployeeBasicInformationService employeeBasicInformationService)
        {
            _employeeBasicInformationService = employeeBasicInformationService;
        }
        /// <summary>
        /// Get employee basic information using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/basic-information")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<BasicInformationModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeBasicInformation([FromRoute] int id)
        {
            var result = await _employeeBasicInformationService.GetEmployeeBasicInformation(id);
            return Success(result);
        }
        /// <summary>
        /// Add employee basic information
        /// </summary>
        /// <param name="model"></param>
        /// <param name="publicRead"></param>
        /// <returns></returns>
        [HttpPost("basic-information")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> AddOrUpdateEmployeeBasicInformation([FromForm] BasicInformationModel model, [FromRoute] bool publicRead = false)
        {
            byte[]? fileStream = null;
            if (model.formFile != null && !string.IsNullOrEmpty(model.formFile?.FileName))
            {
                MemoryStream stream = new MemoryStream();
                await model.formFile?.CopyToAsync(stream);
                fileStream = stream?.ToArray();
            }
            
            var result = await _employeeBasicInformationService.AddorUpdateEmployeeBasicInformation(model, fileStream, model.folderPath?.ToLower(), publicRead, model.formFile?.FileName);
            return Success(result);
        }
    }
}
