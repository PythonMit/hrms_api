using HRMS.Core.Consts;
using System;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveTransactionModel
    {
        public int Id { get; set; }
        public int EmployeeContractId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Description { get; set; }
        public double? Added { get; set; }
        public double? Used { get; set; }
        public double? Balance { get; set; }
        public double? LWP { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
