using HRMS.Core.Models.General;
using HRMS.Core.Models.Leave;
using System.Collections.Generic;

namespace HRMS.Core.Models.Salary
{
    public class SalaryEmployeeDetailModel
    {
        public double? CostToCompany { get; set; }
        public double? StipendAmount { get; set; }
        public string IncentiveRemarks { get; set; }
        public double? IncentiveDuration { get; set; }
        public double? FixIncentive { get; set; }
        public bool? HasFixIncentive { get; set; }
    }

    public class SalaryEmployeeInformationModel
    {
        public EmployeeOutlineModel Employee { get; set; }
        public SalaryEmployeeDetailModel Salary { get; set; }
        public IEnumerable<EmployeeSalaryIncentiveModel> Incentives { get; set; }
        public EmployeeContractOutlineModel Contract { get; set; }
    }
}
