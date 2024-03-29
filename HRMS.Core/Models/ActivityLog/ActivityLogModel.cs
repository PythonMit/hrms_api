using System;

namespace HRMS.Core.Models.ActivityLog
{
    public class ActivityLogModel
    {
        public Guid? Id { get; set; } = null;
        public string EventType { get; set; } = "";
        public string ActivityJson { get; set; } = "";
        public string EventLocation { get; set; } = "";
        public string IPAddress { get; set; } = "";
    }
}
