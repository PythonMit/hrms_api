using HRMS.Core.Models.General;
using System.ComponentModel;

namespace HRMS.Core.Models.SystemFlag
{
    public class SystemFlagFilterModel
    {
        [DefaultValue("")]
        public string byTag { get; set; } = "";
        [DefaultValue("")]
        public string SearchString { get; set; } = "";
        public PaginationModel  Pagination { get; set; }    
    }
}
