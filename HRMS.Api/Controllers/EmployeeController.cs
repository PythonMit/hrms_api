using Microsoft.AspNetCore.Mvc;
using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Services.Interfaces;
using HRMS.Core.Consts;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.General;
using HRMS.Resources;
using HRMS.Core.Utilities.Auth;
using HRMS.Core.Models.Employee.ExitProccess;
using HRMS.Core.Models.Document;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeController : ApiControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAppResourceAccessor _appResourceAccessor;
        private readonly IUserContextAccessor _userContextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="appResourceAccessor"></param>
        /// <param name="userContextAccessor"></param>
        public EmployeeController(IEmployeeService employeeService, IAppResourceAccessor appResourceAccessor, IUserContextAccessor userContextAccessor)
        {
            _employeeService = employeeService;
            _appResourceAccessor = appResourceAccessor;
            _userContextAccessor = userContextAccessor;
        }
        /// <summary>
        /// Get all employee list
        /// </summary>
        /// <returns></returns>
        [HttpPost("list"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<EmployeeListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PagedResponse<object>))]
        public async Task<IActionResult> GetAllEmployees([FromBody] EmployeeFilterModel filter)
        {
            var results = await _employeeService.GetAllEmployees(filter, _userContextAccessor.UserRole);
            return results == null ? Warning<string>(_appResourceAccessor.GetResource("Employee:NoInformationExists")) : Success(results.EmployeeRecords, filter.Pagination, results.TotalRecords);
        }
        /// <summary> 
        /// Delete employee record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            return Success(result);
        }
        /// <summary> 
        /// Remove employee record
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpDelete("remove/{employeeCode}"), Base.AuthorizeAttribute(RoleTypes.SuperAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> RemoveEmployee([FromRoute] string employeeCode)
        {
            if (_userContextAccessor.UserRole != RoleTypes.SuperAdmin)
            {
                return Warning<string>(_appResourceAccessor.GetResource("User:InsufficientPermissions"));
            }

            var result = await _employeeService.RemoveEmployeeInformation(employeeCode);
            if (result == null)
            {
                return Warning<string>(_appResourceAccessor.GetResource("Settings:PermenantRemove"));
            }
            return result == false ? Warning<string>(_appResourceAccessor.GetResource("EmployeeContract:AvailableContract")) : Success(result);
        }
        /// <summary>
        /// Get Employee details using employee id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeInformationModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeInformation([FromRoute] int id)
        {
            var result = await _employeeService.GetEmployeeInformation(id);
            return result == null ? Success(_appResourceAccessor.GetResource("Employee:NoInformationExists")) : Success(result);
        }
        /// <summary>
        /// Get employee information using role type
        /// </summary>
        /// <param name="roleTypeId"></param>
        /// <returns></returns>
        [HttpGet("roles/{roleTypeId}"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeOutlineModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeInformationByRoleType([FromRoute] int roleTypeId)
        {
            var result = await _employeeService.GetEmployeeInformationByRoleType(roleTypeId);
            return (result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("Employee:NoAvailableData")?.Replace("{term}", "role type")));
        }
        /// <summary>
        /// Get employee information using designations
        /// </summary>
        /// <param name="designationIds"></param>
        /// <param name="hasContract"></param>
        /// <returns></returns>
        [HttpPost("designation"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeOutlineModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeInformationByDesignation([FromBody] IEnumerable<int?> designationIds, [FromQuery] bool hasContract)
        {
            var result = await _employeeService.GetEmployeeInformationByDesignation(designationIds, hasContract, _userContextAccessor.UserRole, _userContextAccessor.EmployeeId);
            return (result != null && result.Any() ? Success(result) : Warning<string>(_appResourceAccessor.GetResource("Employee:NoAvailableData")?.Replace("{term}", "requested designations")));
        }
        /// <summary>
        /// Check employee code exists
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpGet("{employeeCode}/exists"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> CheckEmployeeCodeExists([FromRoute] string employeeCode)
        {
            var result = await _employeeService.CheckEmployeeCodeExists(employeeCode);
            return Success(result);
        }
        /// <summary>
        /// Get employees currenly having exit process
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("exit-process/list"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<EmployeeExitProcessListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PagedResponse<object>))]
        public async Task<IActionResult> GetExitProcessEmployees([FromBody] EmployeeFilterModel filter)
        {
            var results = await _employeeService.GetExitProcessEmployees(filter, _userContextAccessor.UserRole);
            return results == null ? Warning<string>(_appResourceAccessor.GetResource("Employee:NoInformationExists")) : Success(results.Records, filter.Pagination, results.TotalRecords);
        }
        /// <summary>
        /// Update employees exit process status
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("exit-process/status"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> AddorUpdateExitProcessEmployee([FromBody] EmployeeExitRequestModel model)
        {
            var results = await _employeeService.AddorUpdateExitProcessEmployee(model, _userContextAccessor.UserRole);
            return Success(results);
        }
        /// <summary>
        /// Add or update employees exit process data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("exit-process"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> AddorUpdateFNFProcessEmployee([FromBody] EmployeeFNFDetailsRequestModel model)
        {
            var results = await _employeeService.AddorUpdateFNFProcessEmployee(model, _userContextAccessor.UserRole);
            return Success(results);
        }
        /// <summary>
        /// Add or Update Employee Image
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("picture"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> AddorUpdateEmployeeImage([FromForm] EmployeeImageModel model)
        {
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                model.EmployeeCode = _userContextAccessor.EmployeeCode;
            }

            byte[]? fileStream = null;
            if (model.FormFile != null && !string.IsNullOrEmpty(model.FormFile?.FileName))
            {
                MemoryStream stream = new MemoryStream();
                await model.FormFile?.CopyToAsync(stream);
                fileStream = stream?.ToArray();
            }
            var result = await _employeeService.AddorUpdateEmployeeImage(model, fileStream, model.FolderPath?.ToLower(), model.FormFile?.FileName);
            return Success(result);
        }
        /// <summary>
        /// Get exit process notes
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpGet("exit-process/{employeeCode}"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeFNFDetailsModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetExitProcessNotes(string employeeCode)
        {
            var results = await _employeeService.GetExitProcessNotes(employeeCode);
            return results == null ? Warning<string>(_appResourceAccessor.GetResource("Employee:NoInformationExists")) : Success(results);
        }
    }
}
