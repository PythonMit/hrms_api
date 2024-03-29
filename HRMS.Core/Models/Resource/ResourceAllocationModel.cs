using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Resource
{
    public class ResourceAllocationModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string PhysicalLocation { get; set; }
    }
}
