using System.ComponentModel;
using System;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveApplicationApproveModel
    {
        public LeaveEmployeeDetailModel ApprovedBy { get; set; }
        [DefaultValue(null)]
        public DateTime? ApprovedDate { get; set; }
        [DefaultValue(null)]
        public DateTime? ApprovedFromDate { get; set; }
        [DefaultValue(null)]
        public DateTime? ApprovedToDate { get; set; }
        [DefaultValue(0)]
        public double ApprovedDays { get; set; }
        [DefaultValue(0)]
        public double LWPDays { get; set; }
        [DefaultValue(0)]
        public double DeclineDays { get; set; }
    }
}
