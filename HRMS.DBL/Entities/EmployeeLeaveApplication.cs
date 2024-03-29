using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.DBL.Entities
{
    public class EmployeeLeaveApplication : TrackableEntity
    {
        public EmployeeLeaveApplication()
        {
            EmployeeLeaveApplicationManagers = new List<EmployeeLeaveApplicationManager>();
            EmployeeLeaveApplicationComments = new List<EmployeeLeaveApplicationComment>();
        }
        public Guid Id { get; set; }
        public int EmployeeContractId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime? ApplyDate { get; set; }
        public DateTime? LeaveFromDate { get; set; }
        public DateTime? LeaveToDate { get; set; }
        public double NoOfDays { get; set; }
        public string PurposeOfLeave { get; set; }
        public int EmployeeLeaveStatusId { get; set; }
        [ForeignKey("Employee")]
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedRemark { get; set; }
        public DateTime? ApprovedFromDate { get; set; }
        public DateTime? ApprovedToDate { get; set; }
        public double ApprovedDays { get; set; }
        public double LWPDays { get; set; }
        public double DeclineDays { get; set; }
        public int? LeaveCategoryId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual LeaveType LeaveType { get; set; }
        public virtual EmployeeContract EmployeeContract { get; set; }
        public virtual EmployeeLeaveStatus EmployeeLeaveStatus { get; set; }
        public virtual LeaveCategory LeaveCategory { get; set; }
        public virtual ICollection<EmployeeLeaveApplicationManager> EmployeeLeaveApplicationManagers { get; set; }
        public virtual ICollection<EmployeeLeaveApplicationComment> EmployeeLeaveApplicationComments { get; set; }
    }
}
