using HRMS.Core.Consts;
using HRMS.Core.Models.General;
using System;

namespace HRMS.Core.Models.Salary
{
    public class EmployeeHoldSalaryModel
    {
        public Guid? Id { get; set; }
        public int EmployeeContractId { get; set; }
        public int EmployeeEarningGrossId { get; set; }
        public double? HoldAmount { get; set; }
        public double? PaidAmount { get; set; }
        public double? NetSalary { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateTime? PaidDate { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public string SalaryMonth { get; set; }
        public int SalaryYear { get; set; }
        public EmployeeOutlineModel Employee { get; set; }
    }
}
