namespace HRMS.Core.Models.Contract
{
    public class FixGrossCalculationModel
    {
        public double Basic { get; set; }
        public double DA { get; set; }
        public double LTA { get; set; }
        public double HRA { get; set; }
        public double ConveyanceAllowance { get; set; }
        public double OtherAllowance { get; set; }
        public double MedicalAllowance { get; set; }
        public double ChildEducation { get; set; }
    }
}
