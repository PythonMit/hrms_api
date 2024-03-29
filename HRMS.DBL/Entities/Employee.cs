using System;
using System.Collections.Generic;

namespace HRMS.DBL.Entities
{
    public class Employee : TrackableEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string EmployeeCode { get; set; }
        public int? BranchId { get; set; }
        public int? DesignationTypeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public Guid? ImagekitDetailId { get; set; }
        public string SlackUserId { get; set; }
        public virtual User User { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual DesignationType DesignationType { get; set; }
        public virtual ImagekitDetail ImagekitDetail { get; set; }
        public virtual ICollection<EmployeeDocument> EmployeeDocuments { get; set; }
        public virtual ICollection<EmployeeContract> EmployeeContracts { get; set; }
        public virtual ICollection<EmployeeOverTimeManager> EmployeeOverTimeManagers { get; set; }
        public virtual ICollection<EmployeeLeaveApplicationManager> EmployeeLeaveApplicationManagers { get; set; }
        public virtual ICollection<ProjectManager> ProjectManagers { get; set; }
    }
}
