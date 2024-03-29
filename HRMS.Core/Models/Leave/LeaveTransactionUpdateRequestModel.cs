using HRMS.Core.Consts;
using System;
using System.ComponentModel;

namespace HRMS.Core.Models.Leave
{
    public class LeaveTransactionUpdateRequestModel
    {
        [DefaultValue(null)]
        public int Id { get; set; }
        [DefaultValue(0)]
        public double? Added { get; set; } = 0;
        [DefaultValue(0)]
        public double? LWP { get; set; } = 0;
        [DefaultValue(0)]
        public double? Used { get; set; } = 0;
        [DefaultValue("")]
        public string EmployeeCode { get; set; } = "";
        [DefaultValue(0)]
        public double? Balance { get; set; } = 0;
        [DefaultValue("")]
        public string Description { get; set; } = "";
    }
}
