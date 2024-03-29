using System;

namespace HRMS.DBL.Entities
{
    public class ActivityLog : TrackableEntity
    {
        public Guid Id { get; set; }
        public string EventType { get; set; }
        public string ActivityJson { get; set; }
        public string EventLocation { get; set; }
        public string IPAddress { get; set; }
    }
}
