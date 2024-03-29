namespace HRMS.Core.Models.Contract
{
    public class EmployeeFixGrossModel
    {
        public int Id { get; set; }
        public int EmployeeContractId { get; set; }
        public decimal CostToCompany { get; set; }
        public double StipendAmount { get; set; }
        public double Basic { get; set; }
        public double DA { get; set; }
        public double LTA { get; set; }
        public double HRA { get; set; }
        public double ConveyanceAllowance { get; set; }
        public double OtherAllowance { get; set; }
        public double MedicalAllowance { get; set; }
        public double ChildEducation { get; set; }
        public FixIncentiveModel FixIncentiveDetail { get; set; }
        public int? DesignationTypeId { get; set; }
    }
}
