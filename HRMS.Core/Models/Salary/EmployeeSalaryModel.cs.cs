using HRMS.Core.Models.General;
using System;
using System.ComponentModel;

namespace HRMS.Core.Models.Salary
{
    public class EmployeeSalaryModel
    {
        public int Id { get; set; }
        public int EmployeeContractId { get; set; }
        public EmployeeOutlineModel Employee { get; set; }
        public EarningGrossCalculationModel EarningGross { get; set; }
        [DefaultValue(null)]
        public EmployeeSalaryIncentiveModel FixIncentive { get; set; } = null;
        public OtherAllowanceModel OtherAllowance { get; set; }
        public string Remarks { get; set; }
        public string CalculationType { get; set; }
        public bool IsPartlyPaid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public EmployeeOutlineModel CreatedBy { get; set; }
        public virtual EmployeeEarningGrossStatusModel Status { get; set; }
    }
}
