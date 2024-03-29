using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Holiday;
using HRMS.Resources;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/holiday"), Tags("Holiday"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
    public class HolidayController : ApiControllerBase
    {
        private readonly IHolidayService _employeeHolidayService;
        private readonly IAppResourceAccessor _appResourceAccessor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeHolidayService"></param>
        /// <param name="appResourceAccessor"></param>
        public HolidayController(IHolidayService employeeHolidayService, IAppResourceAccessor appResourceAccessor)
        {
            _employeeHolidayService = employeeHolidayService;
            _appResourceAccessor = appResourceAccessor;
        }
        /// <summary>
        /// Add or update employee holiday
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<HolidayModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> AddorUpdateEmployeeHolidays([FromBody] HolidayRequestModel model)
        {
            var result = await _employeeHolidayService.AddOrUpdateEmployeeHoliday(model);
            return (result == null ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(result));
        }
        /// <summary>
        /// Get employee holiday details using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<HolidayModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeHoliday([FromRoute] int id)
        {
            var result = await _employeeHolidayService.GetEmployeeHoliday(id);
            return (result == null ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(result));
        }
        /// <summary>
        /// Get employee holiday list
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<HolidatListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeHolidays([FromBody] HolidayFilterModel filter)
        {
            var result = await _employeeHolidayService.GetEmployeeHolidays(filter);
            return result == null && result?.TotalRecords == 0 ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(result?.HolidayRecords, filter.Pagination, result?.TotalRecords ?? 0);
        }
        /// <summary>
        /// Remove employee holiday details
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete, Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteEmployeeSalary([FromBody] IEnumerable<int> ids)
        {
            return Success(await _employeeHolidayService.DeleteEmployeeHoliday(ids));
        }
        /// <summary>
        /// Change employee holiday status
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut, Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> SetEmployeeHolidayStatus([FromBody] HolidayStatusRequestModel model)
        {
            return Success(await _employeeHolidayService.SetEmployeeHolidayStatus(model));
        }
    }
}
