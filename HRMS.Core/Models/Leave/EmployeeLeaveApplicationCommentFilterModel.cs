using HRMS.Core.Consts;
using HRMS.Core.Models.General;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveApplicationCommentFilterModel
    {
        [Required]
        public Guid EmployeeLeaveApplicationId { get; set; }
        [Required]
        public RecordStatus? RecordStatus { get; set; }
        public PaginationModel  Pagination { get; set; }
    }
}
