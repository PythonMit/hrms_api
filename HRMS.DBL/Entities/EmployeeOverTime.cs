using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.DBL.Entities
{
    public class EmployeeOverTime : TrackableEntity
    {
        public EmployeeOverTime()
        {
            EmployeeOverTimeManagers = new List<EmployeeOverTimeManager>();
        }
        public int Id { get; set; }
        public int EmployeeContractId { get; set; }
        public string ProjectName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime? OverTimeDate { get; set; }
        public TimeSpan? OverTimeMinutes { get; set; }
        public decimal? OverTimeAmount { get; set; }
        public int? EmployeeOverTimeStatusId { get; set; }
        [ForeignKey("Employee")]
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public TimeSpan? ApprovedMinutes { get; set; }
        public string Remarks { get; set; }
        public virtual EmployeeContract EmployeeContract { get; set; }
        public virtual EmployeeOverTimeStatus EmployeeOverTimeStatus { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<EmployeeOverTimeManager> EmployeeOverTimeManagers { get; set; }
    }
}
