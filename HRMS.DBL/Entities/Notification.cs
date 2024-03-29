using System;

namespace HRMS.DBL.Entities
{
    public class Notification : TrackableEntity
    {
        public Guid Id { get; set; }
        public int? EmployeeId {  get; set; }    
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool HasRead { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
