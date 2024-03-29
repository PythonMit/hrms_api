using System;
using HRMS.Core.Models.Branch;
using HRMS.Core.Models.DesignationType;
using HRMS.Core.Models.General;

namespace HRMS.Core.Models.Contract
{
    public class EmployeeContractViewModel : EmployeeOutlineModel
    {
        public DateTime? CurrentContractEndDate { get; set; }
        public DateTime? LastContractEndDate { get; set; }
        public string ContractStatus { get; set; }
        public int TotalContract { get; set; }
    }
}
