using HRMS.Core.Models.Branch;
using HRMS.Core.Models.DesignationType;
using System;
using System.Collections.Generic;

namespace HRMS.Core.Models.Contract
{
    public class EmployeeContractListModel
    {
        public IEnumerable<ContractDetailList> ContractList { get; set; }
        public int TotalRecords { get; set; }
    }

    public class ContractDetailList
    {
        public int Id { get; set; }
        public ContractEmployeeDetailModel Employee { get; set; }
        public BranchModel Branch { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProbationPeriod { get; set; }
        public int TrainingPeriod { get; set; }
        public bool HasFixIncentive { get; set; }
        public string Designation { get; set; }
        public EmployeeContractStatusModel Status { get; set; }
    }
}
