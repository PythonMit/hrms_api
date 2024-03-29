using System.Collections.Generic;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveApplicationCommentListModel
    {
        public IEnumerable<EmployeeLeaveApplicationCommentModel> Comments { get; set; }
        public int TotalRecords { get; set; }
    }
}
