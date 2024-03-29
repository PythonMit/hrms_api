using System;

namespace HRMS.Core.Models.Contract
{
    public class EmployeeCurrentContractViewModel
    {
        public ContractResponseModel EmployeeContractDetail { get; set; }
        public ContractPeriod ContractPeriod { get; set; }
    }

    public class ContractPeriod
    {
        public int ContractEndDaysLeft { get; set; }
        public DateTime? IncentiveDate { get; set; }
        public DateTime? ProbationPeriodLastDate { get; set; }
        public int ProbationPeriodDaysLeft { get; set; }
        public DateTime? TrainingPeriodLastDate { get; set; }
        public int TrainingPeriodDaysLeft { get; set; }
    }
}
