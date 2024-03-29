using System;

namespace HRMS.DBL.Entities
{
    public class Holiday : TrackableEntity
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Event { get; set; }
        public string Description { get; set; }
    }
}
