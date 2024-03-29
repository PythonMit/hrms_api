using HRMS.Core.Models.Branch;
using HRMS.Core.Models.DesignationType;
using System;
using System.Collections.Generic;

namespace HRMS.Core.Models.Contract
{
    public class ContractResponseModel
    {
        public int Id { get; set; }
        public ContractEmployeeDetailModel Employee { get; set; }
        public BranchModel Branch { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProbationPeriod { get; set; }
        public int TrainingPeriod { get; set; }
        public bool IsProjectTrainee { get; set; }
        public string Remarks { get; set; }
        public string ContractDocument { get; set; }
        public DropInformationModel DropInformation { get; set; }
        public NoticePeriodModel NoticePeriod { get; set; }
        public DesignationTypeModel Designation { get; set; }
        public EmployeeContractStatusModel Status { get; set; }
        public EmployeeFixGrossModel FixGross { get; set; }
        public IEnumerable<EmployeeIncentiveDetailModel> Incentives { get; set; }
        public TerminateModel TerminateInformation { get; set; }
    }
}
