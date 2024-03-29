namespace HRMS.DBL.Entities
{
    public class EmployeeBankDetail : TrackableEntity
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public string BeneficiaryACNumber { get; set; }
        public string BeneficiaryName { get; set; }
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string BeneficiaryEmail { get; set; }
    }
}
