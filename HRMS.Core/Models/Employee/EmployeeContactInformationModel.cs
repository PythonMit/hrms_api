namespace HRMS.Core.Models.Employee
{
    public class EmployeeContactInformationModel
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public int? UserId { get; set; }
        public string WorkEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateMobileNumber { get; set; }
        public EmployeeAddressModel PermanentAddress { get; set; }
        public EmployeeAddressModel PresentAddress { get; set; }
    }
}
