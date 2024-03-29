using System;
using System.Collections.Generic;
using System.ComponentModel;
using HRMS.Core.Models.General;

namespace HRMS.Core.Models.Employee
{
    public class EmployeeFilterModel
    {
        [DefaultValue("")]
        public string SearchString { get; set; } = "";
        [DefaultValue(null)]
        public IEnumerable<int?> Branch { get; set; } = null;
        [DefaultValue(null)]
        public IEnumerable<int?> Status { get; set; } = null;
        [DefaultValue(null)]
        public bool? HasExited { get; set; } = null;
        [DefaultValue(null)]
        public bool? HasFNFSettled { get; set; } = null;
        public DateTime? SettlementDate { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
