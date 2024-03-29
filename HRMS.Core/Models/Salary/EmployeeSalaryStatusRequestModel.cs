using HRMS.Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Salary
{
    public class EmployeeSalaryStatusRequestModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public EmployeeEarningGrossStatusType Status { get; set; }
        public string Remarks { get; set; }
    }
}
