using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Holiday
{
    public class HolidayStatusRequestModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
