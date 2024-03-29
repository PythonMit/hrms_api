using HRMS.Core.Models.General;
using System.Collections.Generic;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveTransactionDetailModel 
    {
        public EmployeeOutlineModel Employee { get; set; }
        public IEnumerable<EmployeeLeaveTransactionModel> Transactions { get; set; }
    }
}
