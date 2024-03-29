using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Contract
{
    public class RunningContractRequestModel
    {
        [Required]
        public string employeeCode { get; set; }
        [DefaultValue(null)]
        public DateTime? StartDate { get; set; } = null;
        [DefaultValue(null)]
        public DateTime? EndDate { get; set; } = null;
    }
}
