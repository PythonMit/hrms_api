using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveApplicationCommentRequestModel
    {
        [DefaultValue(null)]
        public Guid? Id { get; set; }
        [Required]
        public Guid EmployeeLeaveApplicationId { get; set; }
        [Required]
        public string Comments { get; set; }
    }
}
