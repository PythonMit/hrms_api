using System;
using System.Collections.Generic;
using System.ComponentModel;
using HRMS.Core.Models.Branch;

namespace HRMS.Core.Models.Overtime
{
    public class EmployeeOvertimeModel
    {
        public int Id { get; set; }
        public OvertimeEmployeeDetailModel Employee { get; set; }
        public BranchModel Branch { get; set; }
        public int EmployeeId { get; set; }
        public string ProjectName { get; set; }
        public DateTime? OverTimeDate { get; set; }
        public TimeSpan? OverTimeMinutes { get; set; }
        public IEnumerable<OvertimeEmployeeDetailModel> ProjectManagers { get; set; }
        public EmployeeOverTimeStatusModel Status { get; set; }
        public string TaskDescription { get; set; }
        [DefaultValue(null)]
        public EmployeeOvertimeApprovedModel ApprovedOvertime { get; set; }
        [DefaultValue(false)]
        public bool? hasRequested { get; set; } = false;
    }
}
