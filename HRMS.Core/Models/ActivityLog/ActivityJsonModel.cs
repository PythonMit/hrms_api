using System;

namespace HRMS.Core.Models.ActivityLog
{
    public class ActivityJsonModel
    {
        public string ActivityId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ActivityDescription { get; set; }
    }
}
