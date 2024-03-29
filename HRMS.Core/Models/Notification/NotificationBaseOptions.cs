using System.Collections.Generic;
using HRMS.Core.Models.WebPush;

namespace HRMS.Core.Models.Notification
{
    public class NotificationBaseOptions
    {
        public NotificationBaseOptions()
        {
            Parameters = new Dictionary<string, string>();
        }

        public string ToMail { get; set; }
        public string ToPhoneNumber { get; set; }
        public IEnumerable<WebPushSubscription> ToPushSubscriptions { get; set; }
        public string Url { get; set; }
        public string MessageType { get; set; }
        public IDictionary<string, string> CustomData { get; set; }
        public IDictionary<string, string> Parameters { get; set; }
        public bool EnableWebSocket { get; set; } = true;
        public string ChannelId { get; set; }
        public int? EmployeeId { get; set; }
    }
}
