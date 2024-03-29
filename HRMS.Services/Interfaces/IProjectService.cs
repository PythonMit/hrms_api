using HRMS.Core.Models.Project;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IProjectService
    {
        Task<int?> AddOrUpdateProjectDetails(ProjectRequestModel model);
        Task<ProjectModel> GetProjectDetail(int? projectId);
        Task<ProjectListModel> GetProjectDetails(ProjectFilterModel filter);
        Task<bool> DeleteProjects(IEnumerable<int> projectIds);
        Task<bool> SetProjectsStatus(IEnumerable<ProjectsStatusModel> model);
    }
}
