using HRMS.Core.Models.General;
using System;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveApplicationCommentModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeLeaveApplicationId { get; set; }
        public string Comments { get; set; }
        public EmployeeOutlineModel CommentBy { get; set; }
        public DateTime? CommentDate { get; set; }
    }
}
