using System;

namespace HRMS.Core.Models.Contract
{
    public class EmployeeIncentiveDetailModel
    {
        public int Id { get; set; }
        public int EmployeeContractId { get; set; }
        public DateTime? IncentiveDate { get; set; }
        public double? IncentiveAmount { get; set; }
        public int EmployeeIncentiveStatusId { get; set; }
        public string Remarks { get; set; }
        public EmployeeIncentiveStatusModel Status { get; set; }
    }
}
