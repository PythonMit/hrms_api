namespace HRMS.DBL.Entities
{
    public class LeaveCategory : RecordStatusEntity
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
