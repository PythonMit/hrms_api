using HRMS.Core.Models.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HRMS.Core.Models.Salary
{
    public class EmployeeHoldSalaryFilterModel
    {
        [DefaultValue(null)]
        public Guid? Id { get; set; } = null;
        [DefaultValue("")]
        public string SearchString { get; set; } = "";
        [DefaultValue(null)]
        public IEnumerable<int?> Branch { get; set; } = null;
        [DefaultValue(null)]
        public IEnumerable<int?> Status { get; set; } = null;
        [DefaultValue(null)]
        public string SalaryMonth { get; set; } = null;
        public int? SalaryYear { get; set; } = null;
        public PaginationModel Pagination { get; set; }
    }
}
