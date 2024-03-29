using HRMS.Core.Models.Project;
using HRMS.DBL.Extensions;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectStore _projectStore;
        public ProjectService(ProjectStore projectStore)
        {
            _projectStore = projectStore;
        }

        public async Task<int?> AddOrUpdateProjectDetails(ProjectRequestModel model)
        {
            return await _projectStore.AddOrUpdateProjectDetails(model);
        }
        public async Task<ProjectModel> GetProjectDetail(int? projectId)
        {
            var data = await _projectStore.GetProjectDetail(projectId);
            if (data == null)
            {
                return null;
            }
            return data.ToProjectDetails().FirstOrDefault();
        }
        public async Task<ProjectListModel> GetProjectDetails(ProjectFilterModel filter)
        {
            var data = await _projectStore.GetProjectDetails(filter);
            if (data == null)
            {
                return null;
            }

            var result = data.ToProjectDetails().ToList();
            var totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = result?.Count() ?? 0;
                result = result?.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize).ToList();
            }

            return new ProjectListModel
            {
                ProjectRecords = result,
                TotalRecords = totalRecords
            };
        }
        public async Task<bool> DeleteProjects(IEnumerable<int> projectIds)
        {
            return await _projectStore.DeleteProjects(projectIds);
        }
        public async Task<bool> SetProjectsStatus(IEnumerable<ProjectsStatusModel> model)
        {
            return await _projectStore.SetProjectsStatus(model);
        }
    }
}
