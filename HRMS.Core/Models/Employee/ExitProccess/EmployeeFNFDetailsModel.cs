using HRMS.Core.Models.General;
using System;

namespace HRMS.Core.Models.Employee.ExitProccess
{
    public class EmployeeFNFDetailsModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? FNFDueDate { get; set; }
        public string ExitNote { get; set; }
        public string Remarks { get; set; }
        public bool HasCertificateIssued { get; set; }
        public bool HasSalaryProceed { get; set; }
        public DateTime? SettlementDate { get; set; }
        public string SettlementBy { get; set; }
        public EmployeeOutlineModel Employee { get; set; }
        public string ContractStatus { get; set; }
    }
}
