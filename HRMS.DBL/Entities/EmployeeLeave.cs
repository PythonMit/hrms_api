using System;

namespace HRMS.DBL.Entities
{
    public class EmployeeLeave : TrackableEntity
    {
        public int Id { get; set; }
        public int EmployeeContractId { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public int LeaveTypeId { get; set; }
        public double TotalLeaves { get; set; }
        public double TotalLeavesTaken { get; set; }
        public virtual EmployeeContract EmployeeContract { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}
