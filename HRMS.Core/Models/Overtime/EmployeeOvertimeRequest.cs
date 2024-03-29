using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HRMS.Core.Models.Overtime
{
    public class EmployeeOvertimeRequest
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime? OverTimeDate { get; set; }
        public TimeSpan? OverTimeMinutes { get; set; }
        public IEnumerable<int> ProjectManagerIds { get; set; }
        public string EmployeeCode { get; set; }
    }
}
