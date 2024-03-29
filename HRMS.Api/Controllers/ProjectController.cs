using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Project;
using HRMS.Resources;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/project"), Tags("Project")]
    public class ProjectController : ApiControllerBase
    {
        private readonly IAppResourceAccessor _appResourceAccessor;
        private readonly IProjectService _projectService;
        /// <summary>
        /// 
        /// </summary>
        public ProjectController(IProjectService projectService, IAppResourceAccessor appResourceAccessor)
        {
            _projectService = projectService;
            _appResourceAccessor = appResourceAccessor;
        }

        /// <summary>
        /// Add or Update project details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int?>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> AddOrUpdateProjectDetails([FromBody] ProjectRequestModel model)
        {
            return Success(await _projectService.AddOrUpdateProjectDetails(model));
        }
        /// <summary>
        /// Get Project Detail
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<int?>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> GetProjectDetail([FromRoute] int projectId)
        {
            var result = await _projectService.GetProjectDetail(projectId);
            return result == null ? Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")) : Success(result);
        }
        /// <summary>
        /// Get Project Details
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("list"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Employee, RoleTypes.Manager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<ProjectListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> GetProjectDetails([FromBody] ProjectFilterModel filter)
        {
            var result = await _projectService.GetProjectDetails(filter);
            return result != null && result.TotalRecords > 0 ? Success(result.ProjectRecords, filter.Pagination, result.TotalRecords) : Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable"));
        }
        /// <summary>
        /// Remove employee holiday details
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete, Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteProjects([FromBody] IEnumerable<int> ids)
        {
            return Success(await _projectService.DeleteProjects(ids));
        }
        /// <summary>
        /// Change employee holiday status
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut, Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> SetProjectsStatus([FromBody] IEnumerable<ProjectsStatusModel> model)
        {
            return Success(await _projectService.SetProjectsStatus(model));
        }
    }
}
