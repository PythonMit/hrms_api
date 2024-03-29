using HRMS.Core.Models.Notification;
using System.Collections.Generic;

namespace HRMS.Core.Models.WebPush
{
    public class WebPushOptions
    {
        public WebPushOptions()
        {
            MessageOptions = new List<NotificationOptions>();
        }
        public string MessageTemplate { get; set; }
        public IEnumerable<NotificationOptions> MessageOptions { get; set; }
    }
}
