using Microsoft.AspNetCore.Mvc;
using HRMS.Core.Consts;
using HRMS.Api.Controllers.Base;
using HRMS.Services.Interfaces;
using HRMS.Api.Models;
using HRMS.Core.Models.Contract;
using HRMS.Resources;
using HRMS.Core.Utilities.Auth;
using HRMS.Core.Models.Salary;
using HRMS.Services.Common;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/employee"), Tags("Contract"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
    public class EmployeeContractController : ApiControllerBase
    {
        private readonly IEmployeeContractService _employeeContractService;
        private readonly IAppResourceAccessor _appResourceAccessor;
        private readonly IUserContextAccessor _userContextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeContractService"></param>
        /// <param name="appResourceAccessor"></param>
        public EmployeeContractController(IEmployeeContractService employeeContractService, IAppResourceAccessor appResourceAccessor, IUserContextAccessor userContextAccessor)
        {
            _employeeContractService = employeeContractService;
            _appResourceAccessor = appResourceAccessor;
            _userContextAccessor = userContextAccessor;
        }
        /// <summary>
        ///  Get all employee contract information List
        /// </summary>
        /// <returns></returns>
        [HttpPost("contracts/list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<EmployeeContractListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PagedResponse<object>))]
        public async Task<IActionResult> GetAllEmployeeContractList([FromBody] EmployeeContractFilterModel filter)
        {
            var result = await _employeeContractService.GetAllEmployeeContract(filter, _userContextAccessor.UserRole);
            return Success(result.ContractList, filter.Pagination, result.TotalRecords);
        }
        /// <summary>
        ///  Get employee contract history using employeecode
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <param name="recordStatus"></param>
        /// <returns></returns>
        [HttpGet("{employeeCode}/contract/history")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeContractHistoryModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeContractHistoryByEmployeeCode(string employeeCode, [FromQuery] RecordStatus recordStatus = RecordStatus.Active)
        {
            var result = await _employeeContractService.GetEmployeeContractHistoryByEmployeeCode(employeeCode, recordStatus);
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("Employee:NoAvailableData").Replace("{term}", "contract history"));
        }
        /// <summary>
        ///  Get employee details using employee code
        /// </summary>
        /// <returns></returns>
        [HttpGet("{employeeCode}/contract/employee-information")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeContractViewModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetRemainingEmployeeDetails([FromRoute] string employeeCode)
        {
            var result = await _employeeContractService.GetRemainingEmployeeDetails(employeeCode);
            return result == null ? Success(_appResourceAccessor.GetResource("Employee:NoAvailableData").Replace("{term}", "employee")) : Success(result);
        }
        /// <summary>
        ///  Get employee list using employee name whos contract not yet generated
        /// </summary>
        /// <returns></returns>
        [HttpGet("contract/remaining")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<ContractEmployeeDetailModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetRemainingEmployees([FromQuery] string employeeName = "")
        {
            var result = await _employeeContractService.GetRemainingEmployees(employeeName);
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("Employee:NoAvailableData").Replace("{term}", "employee"));
        }
        /// <summary>
        ///  Get current employee contract details using employee contract Id
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet("{contractId}/contract/information")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeCurrentContractViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeCurrentContractDetails([FromRoute] int contractId)
        {
            var result = await _employeeContractService.GetEmployeeCurrentContractDetails(contractId);
            return (result == null ? Success(_appResourceAccessor.GetResource("EmployeeContract:NoContractInformation")) : Success(result));
        }
        /// <summary>
        /// Add or Update or Renew employee contract information
        /// </summary>
        /// <param name="model"></param>
        /// <param name="publicRead"></param>
        /// <returns></returns>
        [HttpPost("contract")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> AddOrUpdateEmployeeContractDetail([FromForm] EmployeeContractRequestModel model, [FromRoute] bool publicRead = false)
        {
            byte[]? fileStream = null;
            if (model.FormFile != null && !string.IsNullOrEmpty(model.FormFile?.FileName))
            {
                MemoryStream stream = new MemoryStream();
                await model.FormFile?.CopyToAsync(stream);
                fileStream = stream?.ToArray();
            }

            var result = await _employeeContractService.AddOrUpdateEmployeeContractDetail(model, fileStream, model.FolderPath, publicRead, model.FormFile?.FileName);
            return result == null ? Success(_appResourceAccessor.GetResource("EmployeeContract:ManageContractError")) : Success(result);
        }
        /// <summary>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
        /// Delete employee contract details
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpDelete("contract/{contractId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> DeleteEmployeeContractDetails([FromRoute] int contractId)
        {
            return Success(await _employeeContractService.DeleteEmployeeContractDetails(contractId));
        }
        /// <summary>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
        /// Get employee running contract if any available 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("contract/running")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> GetRunningContract([FromBody] RunningContractRequestModel model)
        {
            return Success(await _employeeContractService.GetRunningContract(model));
        }
        /// <summary>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
        /// Change employee contract status
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="statusType"></param>
        /// <returns></returns>
        [HttpPut("contract/{contractId}/status/{statusType}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> SetEmployeeContractStatus([FromRoute] int contractId, [FromRoute] int statusType)
        {
            return Success(await _employeeContractService.SetEmployeeContractStatus(contractId, statusType));
        }
        /// <summary>
        /// Get employee incentives details
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpGet("{employeeCode}/incentives"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeIncentiveDataModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeIncentivesDetails([FromRoute] string employeeCode)
        {
            var result = await _employeeContractService.GetEmployeeIncentivesDetails(employeeCode);
            return (result == null ? Success(_appResourceAccessor.GetResource("EmployeeSalary:NoEmployeeInformation")) : Success(result));
        }
        /// <summary>
        /// Remove employee contract information permenantly
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{contractId}/remove"), Base.AuthorizeAttribute(RoleTypes.SuperAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeSalaryIncentiveModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> RemoveContractInformations([FromRoute] int contractId)
        {
            if (_userContextAccessor.UserRole != RoleTypes.SuperAdmin)
            {
                return Warning<string>(_appResourceAccessor.GetResource("User:InsufficientPermissions"));
            }

            var result = await _employeeContractService.RemoveContractInformations(contractId);
            if (result == null)
            {
                return Warning<string>(_appResourceAccessor.GetResource("Settings:PermenantRemove"));
            }
            return result == false ? Warning<string>(_appResourceAccessor.GetResource("General:ErrorUpsertRecords")) : Success(result);
        }
    }
}
