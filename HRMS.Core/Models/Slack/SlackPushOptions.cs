using HRMS.Core.Models.Notification;
using System.Collections.Generic;

namespace HRMS.Core.Models.Slack
{
    public class SlackPushOptions
    {
        public SlackPushOptions()
        {
            MessageOptions = new List<NotificationOptions>();
        }
        public string ChannelId { get; set; }
        public string MessageTemplate { get; set; }
        public string Token { get; set; }
        public IEnumerable<NotificationOptions> MessageOptions { get; set; }
    }
}
