using System.Collections.Generic;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveApplicationListModel
    {
        public IEnumerable<EmployeeLeaveApplicationModel> Applications { get; set; }
        public int TotalRecords { get; set; }
    }
}
