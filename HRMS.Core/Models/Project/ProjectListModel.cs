using System.Collections.Generic;

namespace HRMS.Core.Models.Project
{
    public class ProjectListModel
    {
        public IEnumerable<ProjectModel> ProjectRecords { get; set; }
        public int TotalRecords { get; set; }
    }
}
