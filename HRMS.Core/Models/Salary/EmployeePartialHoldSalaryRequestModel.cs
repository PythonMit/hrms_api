using HRMS.Core.Consts;
using System;

namespace HRMS.Core.Models.Salary
{
    public class EmployeeHoldSalaryRequestModel
    {
        public Guid? Id { get; set; }
        public int? EmployeeContractId { get; set; }
        public double? HoldAmount { get; set; }
        public EmployeeSalaryStatusType Status { get; set; }
        public string Remarks { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}
