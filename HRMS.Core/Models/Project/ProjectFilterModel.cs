using HRMS.Core.Models.General;
using System.ComponentModel;

namespace HRMS.Core.Models.Project
{
    public class ProjectFilterModel
    {
        [DefaultValue("")]
        public string SearchString { get; set; } = "";
        public PaginationModel Pagination { get; set; }
    }
}
