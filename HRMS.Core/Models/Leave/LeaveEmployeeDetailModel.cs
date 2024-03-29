using HRMS.Core.Models.General;
using System;

namespace HRMS.Core.Models.Leave
{
    public class LeaveEmployeeDetailModel : EmployeeOutlineModel
    {
        public string ApprovedRemark { get; set; }
        public int ProbationPeriod { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set;}
    }
}
