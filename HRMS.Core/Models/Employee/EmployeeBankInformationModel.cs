using HRMS.Core.Consts;
using System.Text.Json.Serialization;

namespace HRMS.Core.Models.Employee
{
    public class EmployeeBankInformationModel
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string TransactionType { get; set; }
        public string BeneficiaryACNumber { get; set; }
        public string BeneficiaryName { get; set; }
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string BeneficiaryEmail { get; set; }
        [JsonIgnore]
        public RecordStatus RecordStatus { get; internal set; }
    }
}
