using HRMS.Core.Models.General;
using System;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveModel
    {
        public int Id { get; set; }
        public int? EmployeeContractId { get; set; }
        public DateTime? LeaveStartDate { get; set; }
        public DateTime? LeaveEndDate { get; set; }
        public LeaveTypeModel LeaveType { get; set; }
        public double TotalLeaves { get; set; }
        public double TotalLeavesTaken { get; set; }
        public EmployeeOutlineModel Employee { get; set; }
    }
}
