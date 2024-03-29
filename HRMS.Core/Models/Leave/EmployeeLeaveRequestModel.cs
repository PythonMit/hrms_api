using System.Collections.Generic;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveRequestModel
    {
        public int Id { get; set; }
        public int LeaveTypeId { get; set; }
        public double TotalLeaves { get; set; }
    }
}
