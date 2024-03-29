using System;

namespace HRMS.Core.Models.Overtime
{
    public class EmployeeOvertimeApprovedModel
    {
        public TimeSpan? ApprovedMinutes { get; set; }
        public decimal? OverTimeAmount { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string Remarks { get; set; }
    }
}
