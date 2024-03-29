namespace HRMS.DBL.Entities
{
    public class EmployeeOverTimeManager
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? EmployeeOvertimeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual EmployeeOverTime EmployeeOverTime { get; set; }
    }
}
