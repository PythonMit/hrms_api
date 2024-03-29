using Microsoft.AspNetCore.Mvc;
using HRMS.Api.Controllers.Base;
using HRMS.Services.Interfaces;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Branch;
using HRMS.Core.Models.DesignationType;
using HRMS.Core.Models.Document;
using HRMS.Core.Models.Contract;
using HRMS.Core.Models.Overtime;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.User;
using HRMS.Core.Models.Leave;
using HRMS.Core.Models.General;
using HRMS.Resources;
using HRMS.Core.Models.Salary;
using HRMS.Core.Utilities.Auth;
using HRMS.DBL.Entities;
using HRMS.Core.Models.Resource;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Tags("Master Data")]
    public class MasterDataController : ApiControllerBase
    {
        private readonly IMasterDataService _masterData;
        private readonly IAppResourceAccessor _appResourceAccessor;
        private readonly IUserContextAccessor _userContextAccessor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterData"></param>
        /// <param name="appResourceAccessor"></param>
        /// <param name="userContextAccessor"></param>
        public MasterDataController(IMasterDataService masterData, IAppResourceAccessor appResourceAccessor, IUserContextAccessor userContextAccessor)
        {
            _masterData = masterData;
            _appResourceAccessor = appResourceAccessor;
            _userContextAccessor = userContextAccessor;
        }

        /// <summary>
        /// Get employee status
        /// </summary>
        /// <returns></returns>
        [HttpGet("employee-status"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeStatusModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeStatus()
        {
            var result = await _masterData.GetEmployeeStatus();
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get employee contract status
        /// </summary>
        /// <returns></returns>
        [HttpGet("employee-contract-status"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeContractStatusModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeContractStatus()
        {
            var result = await _masterData.GetEmployeeContractStatus();
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get document types
        /// </summary>
        /// <returns></returns>
        [HttpGet("document-types"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<DocumentTypeModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetDocumentTypes()
        {
            var result = await _masterData.GetDocumentTypes();
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get employee overtime status
        /// </summary>
        /// <returns></returns>
        [HttpGet("employee-overtime-status"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeOverTimeStatusModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeOverTimeStatus()
        {
            var result = await _masterData.GetEmployeeOverTimeStatus();
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get employee leave status
        /// </summary>
        /// <returns></returns>
        [HttpGet("employee-leave-status"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeLeaveStatusModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeLeaveStatus()
        {
            var result = await _masterData.GetEmployeeLeaveStatus();
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get designation types
        /// </summary>
        /// <returns></returns>
        [HttpGet("designation-types"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<DesignationTypeModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetDesignationTypes()
        {
            var result = await _masterData.GetDesignationTypes();
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get leave types
        /// </summary>
        /// <returns></returns>
        [HttpGet("leave-types"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<LeaveTypeModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetLeaveTypes()
        {
            var result = await _masterData.GetLeaveTypes();
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get branches
        /// </summary>
        /// <returns></returns>
        [HttpGet("branches"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<BranchModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetBranches()
        {
            var result = await _masterData.GetBranches();
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("roles"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<RoleModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> RolesTypes([FromQuery] bool onPriority)
        {
            var result = await _masterData.RoleTypes(_userContextAccessor.UserRole, onPriority);
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get countries
        /// </summary>
        /// <returns></returns>
        [HttpGet("countries"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _masterData.GetCountries();
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get states
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        [HttpGet("states"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<StateModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetStates([FromQuery] string countryName = "")
        {
            var result = await _masterData.GetStates(countryName);
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get cities
        /// </summary>
        /// <param name="stateCode"></param>
        /// <param name="stateName"></param>
        /// <returns></returns>
        [HttpGet("cities"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<CityModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetCities([FromQuery] string stateCode = "", [FromQuery] string stateName = "")
        {
            var result = await _masterData.GetCities(stateCode, stateName);
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get cities
        /// </summary>
        /// <param name="stateCode"></param>
        /// <param name="stateName"></param>
        /// <returns></returns>
        [HttpGet("metro-cities"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetMetroCities([FromQuery] string stateCode = "", [FromQuery] string stateName = "")
        {
            var result = await _masterData.GetMetroCities(stateCode, stateName);
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Postal code list
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="metroCity"></param>
        /// <returns></returns>
        [HttpGet("postalcode"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetPostalCode([FromQuery] string cityName = "", [FromQuery] string metroCity = "")
        {
            var result = await _masterData.GetPostalCode(cityName, metroCity);
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get employee salary status
        /// </summary>
        /// <returns></returns>
        [HttpGet("employee-salary-status"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeSalaryStatusModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeSalaryStatus()
        {
            var result = await _masterData.GetEmployeeSalaryStatus();
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get Leave Category Model
        /// </summary>
        /// <returns></returns>
        [HttpGet("leave-category-types"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<LeaveCategoryModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetLeaveCategoryTypes()
        {
            var result = await _masterData.GetLeaveCategoryTypes();
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
        /// <summary>
        /// Get resource types
        /// </summary>
        /// <returns></returns>
        [HttpGet("resource-types"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<ResourceTypeModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetResourceTypes()
        {
            var result = await _masterData.GetResourceTypes();
            return result != null && result.Any() ? Success(result) : Success(_appResourceAccessor.GetResource("MasterData:NoAvailable"));
        }
    }
}
