using HRMS.Core.Models.Salary;
using System.Collections.Generic;

namespace HRMS.Core.Models.Contract
{
    public class EmployeeIncentiveDataModel
    {
        public string Remarks { get; set; }
        public IEnumerable<EmployeeSalaryIncentiveModel> Incentives { get; set;}
    }
}
