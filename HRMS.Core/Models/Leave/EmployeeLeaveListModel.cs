using System.Collections.Generic;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveListModel
    {
        public IEnumerable<EmployeeLeaveBalanceListModel> Leaves { get; set; }
        public int TotalRecords { get; set; }
    }
}
