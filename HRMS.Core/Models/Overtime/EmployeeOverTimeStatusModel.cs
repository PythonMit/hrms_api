using HRMS.Core.Consts;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace HRMS.Core.Models.Overtime
{
    public class EmployeeOverTimeStatusModel
    {
        public int Id { get; set; }
        [DefaultValue(""), DisallowNull]
        public string EmployeeCode { get; set; }
        public EmployeeOverTimeStatusType StatusType { get; set; }
        [DisallowNull]
        public DateTime? OverTimeDate { get; set; }
        [DisallowNull]
        public TimeSpan? OverTimeMinutes { get; set; }
        [DisallowNull]
        public decimal? OverTimeAmount { get; set; }
        [DefaultValue(null)]
        public int? ApprovedBy { get; set; } = null;
        [DisallowNull]
        public TimeSpan? ApprovedMinutes { get; set; }
        [DefaultValue("")]
        public string Description { get; set; } = "";
    }
}
