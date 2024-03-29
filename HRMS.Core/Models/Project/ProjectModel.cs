using HRMS.Core.Consts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Project
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public IEnumerable<ProjectManagerModel> ProjectManagers { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
