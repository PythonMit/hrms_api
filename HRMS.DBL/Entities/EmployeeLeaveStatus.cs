namespace HRMS.DBL.Entities
{
    public class EmployeeLeaveStatus : RecordStatusEntity
    {
        public int Id { get; set; }
        public string StatusType { get; set; }
        public string Description { get; set; }
    }
}
