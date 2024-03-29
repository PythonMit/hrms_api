using HRMS.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveApplicationRequestModel
    {
        [DefaultValue(null)]
        public Guid? Id { get; set; } = null;
        [DefaultValue("Active")]
        public RecordStatus? RecordStatus { get; set; }
        [DefaultValue(null)]
        public DateTime? LeaveFromDate { get; set; }
        [DefaultValue(null)]
        public DateTime? LeaveToDate { get; set; }
        public double NoOfDays { get; set; }
        public string PurposeOfLeave { get; set; }
        [DefaultValue("")]
        public string LeaveType { get; set; }
        [DefaultValue(null)]
        public IEnumerable<int> ProjectManagerIds { get; set; }
        [DefaultValue("")]
        public string EmployeeCode { get; set; }
    }
}
