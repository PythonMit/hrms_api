using System.Reflection;

namespace HRMS.Core.Models.Notification
{
    public class NotificationSettings
    {
        public string InAppNamespace { get; set; }
        public string SmsNamespace { get; set; }
        public string EmailNamespace { get; set; }
        public string WebPushNamespace { get; set; }
        public string SlackNamespace { get; set; }
        public Assembly Assembly { get; set; }
    }
}
