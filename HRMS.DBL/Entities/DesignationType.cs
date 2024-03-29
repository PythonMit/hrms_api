namespace HRMS.DBL.Entities
{
    public class DesignationType : RecordStatusEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
    }
}
