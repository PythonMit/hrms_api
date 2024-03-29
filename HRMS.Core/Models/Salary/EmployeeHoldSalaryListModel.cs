using System.Collections.Generic;

namespace HRMS.Core.Models.Salary
{
    public class EmployeeHoldSalaryListModel
    {
        public IEnumerable<EmployeeHoldSalaryModel> HoldSalaries { get; set; }
        public int TotalRecords { get; set; }
    }
}
