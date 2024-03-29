namespace HRMS.DBL.Entities
{
    public class EmployeeFixGross : TrackableEntity
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
        public bool IsFixIncentive { get; set; }
        public int FixIncentiveDuration { get; set; }
        public string FixIncentiveRemarks { get; set; }
        public string Remarks { get; set; }
        public bool? IsDelete { get; set; }
        public virtual EmployeeContract EmployeeContract { get; set; }
    }
}
