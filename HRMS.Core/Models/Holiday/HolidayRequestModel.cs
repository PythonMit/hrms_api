using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Holiday
{
    public class HolidayRequestModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Event { get; set; }
        public string Description { get; set; }
    }
}
