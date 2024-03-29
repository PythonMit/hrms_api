using System.Collections.Generic;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveMonthlyBalanceModel
    {
        public EmployeeLeaveMonthlyBalanceModel()
        {
            Balances = new List<EmployeeLeaveBalanceModel>();
        }
        public string MonthYear { get; set; }
        public List<EmployeeLeaveBalanceModel> Balances { get; set; }
    }
}
