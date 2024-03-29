using HRMS.Core.Models.General;
using HRMS.Core.Models.Project;
using HRMS.DBL.Entities;
using System.Linq;

namespace HRMS.DBL.Extensions
{
    public static class ProjectMappingExtension
    {
        public static IQueryable<ProjectModel> ToProjectDetails(this IQueryable<Project> data)
        {
            return data.Select(x => new ProjectModel
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                RecordStatus = x.RecordStatus,
                Type = x.Type,
                ProjectManagers = x.ProjectManagers == null ? null : x.ProjectManagers.Select(y => new ProjectManagerModel
                {
                    Id = y.Id,                    
                    Name = $"{y.Employee.FirstName} {y.Employee.LastName}",
                    Designation = y.Employee.DesignationType.Name,
                    Code = y.Employee.EmployeeCode,
                    Branch = y.Employee.Branch.Name,
                    EmployeeId =  y.EmployeeId
                })
            });
        }
    }
}
