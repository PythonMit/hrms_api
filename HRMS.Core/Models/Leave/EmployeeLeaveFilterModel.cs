using HRMS.Core.Consts;
using HRMS.Core.Models.General;
using System.Collections.Generic;
using System.ComponentModel;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveFilterModel
    {
        [DefaultValue("")]
        public string SearchString { get; set; } = "";
        [DefaultValue(null)]
        public IEnumerable<int?> Branch { get; set; } = null;
        public PaginationModel Pagination { get; set; }
    }
}
