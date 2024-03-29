using HRMS.Core.Consts;
using HRMS.Core.Models.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveApplicationFilterModel
    {
        [DefaultValue("")]
        public string SearchString { get; set; } = "";
        [DefaultValue(null)]
        public IEnumerable<int?> Branch { get; set; } = null;
        [DefaultValue(null)]
        public IEnumerable<int?> Status { get; set; } = null;
        [DefaultValue(null)]
        public DateTime? LeaveFrom { get; set; } = null;
        [DefaultValue(null)]
        public DateTime? LeaveTo { get; set; } = null;
        public PaginationModel Pagination { get; set; }
    }
}
