using HRMS.Core.Consts;
using System.Collections.Generic;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveHistoryModel
    {
        public LeaveTypes LeaveType { get; set; }
        public IEnumerable<EmployeeLeaveBalanceModel> MonthlyBalance { get; set; }
    }
}
