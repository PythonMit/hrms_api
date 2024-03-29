using System;
using System.Collections.Generic;

namespace HRMS.DBL.Entities
{
    public class EmployeeContract : TrackableEntity
    {
        public EmployeeContract()
        {
            EmployeeIncentiveDetails = new List<EmployeeIncentiveDetails>();
        }
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public int ProbationPeriod { get; set; }
        public int TrainingPeriod { get; set; }
        public int DesignationTypeId { get; set; }
        public int EmployeeContractStatusId { get; set; }
        public bool IsProjectTrainee { get; set; }
        public string Remarks { get; set; }
        public DateTime DropDate { get; set; }
        public string DropRemarks { get; set; }
        public DateTime NoticePeriodStartDate { get; set; }
        public DateTime NoticePeriodEndDate { get; set; }
        public string NoticeRemarks { get; set; }
        public Guid? ImagekitDetailId { get; set; }
        public DateTime? TerminateDate { get; set; }
        public string TerminateRemarks { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual DesignationType DesignationType { get; set; }
        public virtual EmployeeContractStatus EmployeeContractStatus { get; set; }
        public virtual EmployeeFixGross EmployeeFixGross { get; set; }
        public virtual ImagekitDetail ImagekitDetail { get; set; }
        public virtual ICollection<EmployeeLeaveApplication> EmployeeLeaveApplications { get; set; }
        public virtual ICollection<EmployeeLeave> EmployeeLeaves { get; set; }
        public virtual ICollection<EmployeeIncentiveDetails> EmployeeIncentiveDetails { get; set; }
        public virtual ICollection<EmployeeLeaveTransaction> EmployeeLeaveTransactions { get; set; }
    }
}
