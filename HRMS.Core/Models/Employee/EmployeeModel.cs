using HRMS.Core.Consts;
using HRMS.Core.Models.Branch;
using System;

namespace HRMS.Core.Models.Employee
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string EmployeeCode { get; set; }
        public BranchModel Branch { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ProfilePhoto { get; set; }
        public string Designation { get; set; }
        public string MobileNumber { get; set; }
        public string ContractStatus { get; set; }
        public DateTime? JoiningDate { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
