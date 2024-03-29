using HRMS.Core.Consts;
using System.ComponentModel;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveBalanceModel
    {
        public int ContractId { get; set; }
        public LeaveTypes? LeaveType { get; set; }
        [DefaultValue(null)]
        public string Month { get; set; }
        [DefaultValue(null)]
        public int? Year { get; set; }
        [DefaultValue(null)]
        public double? TotalLeaves { get; set; }
        [DefaultValue(0)]
        public double? TotalLeavesTaken { get; set; } = 0;
        [DefaultValue(null)]
        public double? LeaveBalance { get; set; }
        [DefaultValue(null)]
        public double? ApprovedDays { get; set; }
        [DefaultValue(null)]
        public double? LWPDays { get; set; }
        [DefaultValue(null)]
        public double? DeclineDays { get; set; }
        public LeaveCategoryType? LeaveCategoryType { get; set; }
        public RecordStatus? RecordStatus { get; set; }
    }
}
