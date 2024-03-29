using HRMS.Core.Consts;
using HRMS.Core.Models.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveApplicationModel
    {
        [DefaultValue(null)]
        public Guid Id { get; set; }
        public EmployeeOutlineModel Employee { get; set; }
        public int EmployeeContractId { get; set; }
        [DefaultValue(null)]
        public LeaveTypeModel Type { get; set; }
        [DefaultValue(null)]
        public DateTime? ApplyDate { get; set; }
        [DefaultValue(null)]
        public DateTime? LeaveFromDate { get; set; }
        [DefaultValue(null)]
        public DateTime? LeaveToDate { get; set; }
        public double NoOfDays { get; set; }
        public string PurposeOfLeave { get; set; }
        [DefaultValue(null)]
        public EmployeeLeaveStatusModel Status { get; set; }
        public EmployeeLeaveApplicationApproveModel ApprovedInformation { get; set; }
        [DefaultValue(null)]
        public LeaveCategoryModel Category { get; set; }
        [DefaultValue(null)]
        public IEnumerable<LeaveEmployeeDetailModel> ProjectManagers { get; set; }
        [DefaultValue(null)]
        public bool? HasRequested { get; set; }
        [DefaultValue(null)]
        public bool? HasProbationPeriod { get; set; }
        [DefaultValue(null)]
        public bool? HasTrainingPeriod { get; set; }
        [DefaultValue(null)]
        public bool? HasNoticePeriod { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
