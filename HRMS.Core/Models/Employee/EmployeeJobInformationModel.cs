using System;

namespace HRMS.Core.Models.Employee
{
    public class EmployeeJobInformationModel
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public string EmployeeCode { get; set; }
        public int? DesignationTypeId { get; set; }
        public DateTime? JoinDate { get; set; }
        public string PreviousEmployeer { get; set; }
        public decimal? Experience { get; set; }
        public string WorkingFormat { get; set;}
    }
}

