using System;

namespace HRMS.DBL.Entities
{
    public class EmployeeFNFDetails : TrackableEntity
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
        public virtual Employee Employee { get; set; }
    }
}
