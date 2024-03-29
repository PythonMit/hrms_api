using System;

namespace HRMS.DBL.Entities
{
    public class EmployeeLeaveTransaction : TrackableEntity
    {
        public int Id { get; set; }
        public int EmployeeContractId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public double? Added { get; set; }
        public double? Used { get; set; }
        public double? Balance { get; set; }
        public double? LWP { get; set; }
        public virtual EmployeeContract EmployeeContract { get; set; }
    }
}
