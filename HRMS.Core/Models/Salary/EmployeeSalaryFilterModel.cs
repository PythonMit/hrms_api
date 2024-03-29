using HRMS.Core.Consts;
using HRMS.Core.Models.General;
using System.Collections.Generic;
using System.ComponentModel;

namespace HRMS.Core.Models.Salary
{
    public class EmployeeSalaryFilterModel
    {
        [DefaultValue(null)]
        public int? Id { get; set; }
        [DefaultValue("")]
        public string SearchString { get; set; } = "";
        [DefaultValue(null)]
        public IEnumerable<int> Branch { get; set; } = null;
        [DefaultValue(null)]
        public IEnumerable<int> Status { get; set; } = null;
        [DefaultValue(null)]
        public IEnumerable<string> SalaryMonth { get; set; } = null;
        [DefaultValue(null)]
        public IEnumerable<int> Year { get; set; } = null;
        public PaginationModel Pagination { get; set; }
    }
}
