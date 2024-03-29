using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HRMS.Core.Models.Salary
{
    public class EmployeeSalaryQueryModel
    {
        [AllowNull, DefaultValue(null)]
        public int? Id { get; set; } = null;
        [Required]
        public string EmployeeCode { get; set; }
        [Required]
        public string Month { get; set; }
        [Required]
        public int? Year { get; set; }
    }
}
