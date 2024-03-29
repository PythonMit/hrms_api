using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Salary;
using HRMS.Core.Utilities.Auth;
using HRMS.Resources;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LinqKit;
using HRMS.Core.Models.General;
using System.Net.Mime;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using Org.BouncyCastle.Utilities;
using HRMS.DBL.Entities;
using HRMS.Core.Utilities.General;
using System.Diagnostics.CodeAnalysis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/employee/salary"), Tags("Salary")]
    public class EmployeeSalaryController : ApiControllerBase
    {
        private readonly IEmployeeSalaryService _employeeSalaryService;
        private readonly IAppResourceAccessor _appResourceAccessor;
        private readonly IUserContextAccessor _userContextAccessor;
        private readonly ISystemFlagService _systemFlagService;
        private readonly IGeneralUtilities _generalUtilities;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeSalaryService"></param>
        /// <param name="appResourceAccessor"></param>
        /// <param name="userContextAccessor"></param>
        /// <param name="systemFlagService"></param>
        /// <param name="generalUtilities"></param>
        public EmployeeSalaryController(IEmployeeSalaryService employeeSalaryService, IAppResourceAccessor appResourceAccessor, IUserContextAccessor userContextAccessor, ISystemFlagService systemFlagService, IGeneralUtilities generalUtilities)
        {
            _employeeSalaryService = employeeSalaryService;
            _appResourceAccessor = appResourceAccessor;
            _userContextAccessor = userContextAccessor;
            _systemFlagService = systemFlagService;
            _generalUtilities = generalUtilities;
        }
        /// <summary>
        /// Generate employee salary
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("generate"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<int>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> AddorUpdateEmployeeSalary([FromBody] IEnumerable<EmployeeSalaryRequestModel> model)
        {
            model.ForEach(x => x.CreatedBy = _userContextAccessor.EmployeeId);
            var result = await _employeeSalaryService.AddorUpdateEmployeeSalary(model);
            return (result == null ? Success(_appResourceAccessor.GetResource("EmployeeSalary:NoContractOrEarningGross")) : Success(result));
        }
        /// <summary>
        /// Get employee salary list
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("list"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<EmployeeSalaryListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PagedResponse<object>))]
        public async Task<IActionResult> GetEmployeeSalary(EmployeeSalaryFilterModel filter)
        {
            int? employeeId = null;
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                employeeId = _userContextAccessor.EmployeeId;
            }
            var result = await _employeeSalaryService.GetEmployeeSalary(filter, employeeId);
            return (result == null ? Warning<string>(_appResourceAccessor.GetResource("EmployeeSalary:NoContractOrEarningGross")) : Success(result, filter.Pagination, result.TotalRecords));
        }
        /// <summary>
        /// Get employee salary details
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("details"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeSalaryModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeSalaryDetails(EmployeeSalaryQueryModel query)
        {
            if (_userContextAccessor.UserRole == RoleTypes.Manager || _userContextAccessor.UserRole == RoleTypes.Employee)
            {
                query.EmployeeCode = _userContextAccessor.EmployeeCode;
            }
            var result = await _employeeSalaryService.GetEmployeeSalaryDetailsAsync(query);
            return (result == null ? Success(_appResourceAccessor.GetResource("EmployeeSalary:NoContractOrEarningGrossSlip")) : Success(result));
        }
        /// <summary>
        /// Get employee salary details
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("partly/details"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeSalaryModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeSalaryInformation(EmployeeSalaryQueryModel query)
        {
            var result = await _employeeSalaryService.GetEmployeePartlySalaryDetails(query);
            return (result == null ? Success(_appResourceAccessor.GetResource("EmployeeSalary:NoContractOrEarningGross")) : Success(result));
        }
        /// <summary>
        /// Remove employee salary details
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete, Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteEmployeeSalary([FromBody] IEnumerable<int> ids)
        {
            return Success(await _employeeSalaryService.DeleteEmployeeSalary(ids));
        }
        /// <summary>
        /// Change employee salary status
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>-
        [HttpPut, Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> SetEmployeeSalaryStatus([FromBody] IEnumerable<EmployeeSalaryStatusRequestModel> model)
        {
            return Success(await _employeeSalaryService.SetEmployeeSalaryStatus(model));
        }
        /// <summary>
        /// Get actual leave witout pay for given month and year
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("leave-witout-pay"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<double>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetActualLeaveWitoutPay(EmployeeSalaryQueryModel query)
        {
            return Success(await _employeeSalaryService.GetActualLeaveWitoutPay(query));
        }
        /// <summary>
        /// Get employee details
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpGet("{employeeCode}/information"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<SalaryEmployeeInformationModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeDetails([FromRoute] string employeeCode)
        {
            var result = await _employeeSalaryService.GetEmployeeDetails(employeeCode);
            return (result == null ? Success(_appResourceAccessor.GetResource("EmployeeSalary:NoEmployeeInformation")) : Success(result));
        }
        /// <summary>
        /// Get actual leave witout pay for given month and year
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("overtime"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<double>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetActualApprovedOvertime(EmployeeSalaryQueryModel query)
        {
            return Success(await _employeeSalaryService.GetActualApprovedOvertime(query));
        }
        /// <summary>
        /// Check for existing salary for given month and year
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("exists"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool?>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> CheckMonthlySalaryExist(EmployeeSalaryQueryModel query)
        {
            return Success(await _employeeSalaryService.CheckMonthlySalaryExist(query));
        }
        /// <summary>
        /// Get current running or notice period contract years 
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        [HttpPost("{employeeCode}/years"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<int>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetContractYears(string employeeCode)
        {
            return Success(await _employeeSalaryService.GetContractYears(employeeCode));
        }
        /// <summary>
        /// Get list of employee without generated salary
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("remaining-salaried-employees"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeOutlineModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetAlreadySalariedEmployee(AlreadySalariedEmployeeRequestModel model)
        {
            var result = await _employeeSalaryService.GetAlreadySalariedEmployee(model, _userContextAccessor.UserRole, _userContextAccessor.EmployeeId);
            return (result != null && result.Any() ? Success(result) : Warning<string>(_appResourceAccessor.GetResource("Employee:NoAvailableData")?.Replace("{term}", "requested designations")));
        }
        #region Others
        /// <summary>
        /// Download bulk salary sheet for upload
        /// </summary>
        /// <param name="month">Curent month</param>
        /// <param name="year">Curent year</param>
        /// <param name="employeeCodes">This must be comma seperated if providede</param>
        /// <returns></returns>
        [HttpGet("sheet"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        public async Task<IActionResult> GetEmployeeSalarySheet([FromQuery] string month, [FromQuery] int year, [FromQuery, AllowNull] string employeeCodes = "")
        {
            if (!string.IsNullOrEmpty(employeeCodes) && employeeCodes?.Contains(",") != true)
            {
                return Warning<string>(_appResourceAccessor.GetResource("General:ErrorUpsertRecords"));
            }
            var result = await _employeeSalaryService.GetEmployeeSalarySheet(employeeCodes, month, year);
            if (result != null && result?.Any() == true)
            {
                var stream = _generalUtilities.CreateCSV(result);
                var filename = await _systemFlagService.GetSystemFlagsByTag("bulkpaymentfile");
                return File(stream, MediaTypeNames.Application.Octet, $"{filename?.Value}{DateTime.Now.ToString("ddMM")}.002");
            }
            else
            {
                return Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable"));
            }
        }
        #endregion Others
    }
}