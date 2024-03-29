namespace HRMS.DBL.Entities
{
    public class Branch : RecordStatusEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
