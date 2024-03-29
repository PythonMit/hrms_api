using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.SystemFlag;
using HRMS.Resources;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Tags("System Flag"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
    public class SystemFlagController : ApiControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISystemFlagService _systemFlagService;
        private readonly IAppResourceAccessor _appResourceAccessor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemFlagService"></param>
        /// <param name="appResourceAccessor"></param>
        public SystemFlagController(ISystemFlagService systemFlagService, IAppResourceAccessor appResourceAccessor)
        {
            _systemFlagService = systemFlagService;
            _appResourceAccessor = appResourceAccessor;
        }
        /// <summary>
        /// Add or Update system flag
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> AddorUpdateSystemFlag([FromBody] SystemFlagModel model)
        {
            var result = await _systemFlagService.AddorUpdateSystemFlag(model);
            return Success(result);
        }
        /// <summary>
        /// Delete system flag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> DeleteSystemFlag([FromRoute] int id)
        {
            var result = await _systemFlagService.DeleteSystemFlag(id);
            return Success(result);
        }
        /// <summary>
        /// List of All System Flag
        /// </summary>
        /// <returns></returns>
        [HttpPost("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<SystemFlagListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetAllSystemFlags([FromBody] SystemFlagFilterModel filter)
        {
            var result = await _systemFlagService.GetAllSystemFlags(filter);             
            return (result == null && result?.TotalRecords == 0 ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(result?.SystemFlagRecords, filter.Pagination, (result?.TotalRecords ?? 0)));
        }
        /// <summary>
        /// Change flag status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut("{id}/status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> StatusChange([FromRoute] int id, [FromRoute] bool status)
        {
            var result = await _systemFlagService.StatusChange(id, status);
            return Success(result);
        }
        /// <summary>
        /// Get flag details by flag name
        /// </summary>
        /// <param name="flagName"></param>
        /// <returns></returns>
        [HttpGet("{flagName}/details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<SystemFlagModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetFlagDetailsByName([FromRoute] string flagName)
        {
            var result = await _systemFlagService.GetFlagDetailsByName(flagName);
            return result == null ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(result);
        }
        /// <summary>
        /// Check whether flag is exist or not
        /// </summary>
        /// <param name="flagName"></param>
        /// <returns></returns>
        [HttpGet("{flagName}/exists")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> CheckFlagExists([FromRoute] string flagName)
        {
            var result = await _systemFlagService.CheckFlagExists(flagName);
            return Success(result);
        }
    }
}

