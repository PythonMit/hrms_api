using HRMS.Core.Consts;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveBalanceInfoModel
    {
        public string LeaveType { get; set; }
        public double Total { get; set; }
        public int Order { get; set; }
    }
}
