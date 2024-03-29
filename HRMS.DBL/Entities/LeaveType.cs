namespace HRMS.DBL.Entities
{
    public class LeaveType : RecordStatusEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalLeaves { get; set; }
    }
}
