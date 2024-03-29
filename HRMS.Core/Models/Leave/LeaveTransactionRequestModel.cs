using HRMS.Core.Consts;
using System;
using System.ComponentModel;

namespace HRMS.Core.Models.Leave
{
    public class LeaveTransactionRequestModel
    {
        public int Id { get; set; }
        [DefaultValue(null)]
        public DateTime? TransactionDate { get; set; }
        [DefaultValue(null)]
        public int? LeaveTypeId { get; set; }
        [DefaultValue(null)]
        public Guid? LeaveApplicationId { get; set; }
        [DefaultValue(0)]
        public double? Added { get; set; } = 0;
        [DefaultValue(0)]
        public double? LWP { get; set; } = 0;
        [DefaultValue(0)]
        public double? Used { get; set; } = 0;
        [DefaultValue(false)]
        public bool IsReCalculate { get; set; } = false;
        [DefaultValue("")]
        public string EmployeeCode { get; set; } = "";
        [DefaultValue(false)]
        public bool HasProbationPeriod { get; set; } = false;
        [DefaultValue(0)]
        public double LeavePeriod { get; set; } = 0;
    }
}
