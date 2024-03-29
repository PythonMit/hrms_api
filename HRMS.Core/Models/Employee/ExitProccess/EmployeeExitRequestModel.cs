using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Employee.ExitProccess
{
    public class EmployeeExitRequestModel
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string ExitNote { get; set; }
        [Required]
        public DateTime? Exitdate { get; set; }
        public DateTime? FNFDueDate { get; set; }
    }
}
