using System.Collections.Generic;

namespace HRMS.Core.Models.Salary
{
    public class EmployeeSalaryListModel
    {
        public IEnumerable<EmployeeSalaryModel> Salaries { get; set; }
        public int TotalRecords { get; set; }
        public double TotalNetSalary { get; set; }
        public double TotalOnHoldSalary { get; set; }
    }
}
