using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HRMS.Core.Models.Employee.ExitProccess
{
    public class EmployeeFNFDetailsRequestModel
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string Remarks { get; set; }
        public bool HasCertificateIssued { get; set; }
        public bool HasSalaryProceed { get; set; }
        [Required]
        public DateTime? SettlementDate { get; set; }
        [JsonIgnore]
        public string SettlementBy { get; set; }
    }
}
