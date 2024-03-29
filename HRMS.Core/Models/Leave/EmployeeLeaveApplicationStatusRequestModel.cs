using HRMS.Core.Consts;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveApplicationStatusRequestModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public EmployeeLeaveStatusType Status { get; set; }
        [Required]
        public string Remark { get; set; }
        [JsonIgnore]
        public int ApprovedBy { get; set; }
        [Required]
        public DateTime? ApprovedFromDate { get; set; }
        [Required]
        public DateTime? ApprovedToDate { get; set; }
        [Required]
        public double ApprovedDays { get; set; }
        [Required]
        public double LWPDays { get; set; }
        [Required]
        public double DeclineDays { get; set; } = 0;
    }
}
