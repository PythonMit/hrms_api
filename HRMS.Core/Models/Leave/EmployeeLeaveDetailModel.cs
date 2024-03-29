using System.Collections.Generic;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveDetailModel
    {
        public EmployeeLeaveApplicationModel Application { get; set; }
        public IEnumerable<EmployeeLeaveBalanceModel> BalanceV1 { get; set; }
        public IEnumerable<EmployeeLeaveBalanceInfoModel> Balance { get; set; }
        public IEnumerable<EmployeeLeaveMonthlyBalanceModel> MonthlyBalance { get; set; }
    }
}
