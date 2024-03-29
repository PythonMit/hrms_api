using System;

namespace HRMS.Core.Models.Employee
{
    public class EmployeeInformationModel
    {
        public int Id { get; set; }
        public EmployeeModel EmployeeDetail { get; set; }
        public string WorkEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateMobileNumber { get; set; }
        public EmployeeAddressModel PresentAddress { get; set; }
        public EmployeeAddressModel PermanentAddress { get; set; }
        public string PreviousEmployeer { get; set; }
        public decimal? Experience { get; set; }
        public string InstituteName { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? FullNFinalSatelementDate { get; set; }
        public string FullNFinalSatelementBy { get; set; }
        public bool AllowEditPersonalDetails { get; set; }
    }
}
