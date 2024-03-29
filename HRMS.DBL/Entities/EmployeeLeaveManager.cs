using System;

namespace HRMS.DBL.Entities
{
    public class EmployeeLeaveApplicationManager
    {
        public Guid Id { get; set; }
        public int? EmployeeId { get; set; }
        public Guid? EmployeeLeaveApplicationId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual EmployeeLeaveApplication EmployeeLeaveApplication { get; set; }
    }
}
