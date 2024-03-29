using System;

namespace HRMS.DBL.Entities
{
    public class EmployeeIncentiveDetails : TrackableEntity
    {
        public int Id { get; set; }
        public int EmployeeContractId { get; set; }
        public DateTime? IncentiveDate { get; set; }
        public double? IncentiveAmount { get; set; }
        public int EmployeeIncentiveStatusId { get; set; }
        public string Remarks { get; set; }
        public virtual EmployeeIncentiveStatus EmployeeIncentiveStatus { get; set; }
        public virtual EmployeeContract EmployeeContract { get; set; }        
    }
}
