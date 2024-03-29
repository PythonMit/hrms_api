using HRMS.Core.Models.Contract;
using System;

namespace HRMS.Core.Models.Salary
{
    public class EmployeeSalaryIncentiveModel
    {
        public double? IncentiveAmount { get; set; }
        public DateTime? IncentiveDate { get; set; }
        public EmployeeIncentiveStatusModel Status { get; set; }

    }
}
