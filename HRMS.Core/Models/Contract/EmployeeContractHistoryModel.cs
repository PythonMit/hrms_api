using System;
using HRMS.Core.Models.Branch;
using HRMS.Core.Models.DesignationType;

namespace HRMS.Core.Models.Contract
{
    public class EmployeeContractHistoryModel
    {
        public int EmployeeContractId { get; set; }
        public ContractEmployeeDetailModel Employee { get; set; }
        public BranchModel Branch { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractendDate { get; set; }
        public decimal CostToCompany { get; set; }
        public double StipendAmount { get; set; }
        public string FixIncentiveRemarks { get; set; }
        public bool? IsProjectTrainee { get; set; }
        public int? TrainingPeriod { get; set; }
        public string Status { get; set; }
        public DesignationTypeModel Designation { get; set; }
    }
}
