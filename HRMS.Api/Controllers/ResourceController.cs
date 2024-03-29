using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Resource;
using HRMS.Resources;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// Resources
    /// </summary>
    [Tags("Resource"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
    public class ResourceController : ApiControllerBase
    {
        private readonly IResourceService _resourceService;
        private readonly IAppResourceAccessor _appResourceAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceService"></param>
        /// <param name="appResourceAccessor"></param>
        public ResourceController(IResourceService resourceService, IAppResourceAccessor appResourceAccessor)
        {
            _resourceService = resourceService;
            _appResourceAccessor = appResourceAccessor;
        }

        /// <summary>
        /// Add Or Update Recource Details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> AddOrUpdateRecourceDetails([FromBody] ResourceRequestModel model)
        {
            var result = await _resourceService.AddOrUpdateRecourceDetails(model);
            return (result == 0 ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(result));
        }
        /// <summary>
        /// Add Recource Allocation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("allocation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> AddRecourceAllocation([FromBody] ResourceAllocationModel model)
        {
            var result = await _resourceService.AddRecourceAllocation(model);
            return (result == 0 ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(result));
        }
        /// <summary>
        /// Get Recource Details
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<ResourceListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PagedResponse<object>))]
        public async Task<IActionResult> GetRecourceDetails([FromBody] ResourceFilterModel filter)
        {
            var results = await _resourceService.GetRecourceDetails(filter);
            return results == null ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(results.Records, filter.Pagination, results.TotalRecords);
        }
        /// <summary>
        /// Get Recources
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ResourceModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetRecourceDetail([FromRoute] int id, [FromQuery] RecordStatus status = RecordStatus.Active)
        {
            var results = await _resourceService.GetRecourceDetail(id, status);
            return results == null ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(results);
        }
        /// <summary>
        /// Get Recource User History
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        [HttpGet("{resourceId}/history")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<ResourceUserHistoryModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetRecourceHistory([FromRoute] int resourceId)
        {
            var results = await _resourceService.GetRecourceDetails(resourceId);
            return results == null ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(results);
        }
        /// <summary>
        /// Remove resources
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<ResourceUserHistoryModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteRecource([FromRoute] int id)
        {
            var results = await _resourceService.DeleteResources(id);
            return results == null ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(results);
        }
    }
}