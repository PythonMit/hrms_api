using HRMS.Core.Models.Notification;
using System.Collections.Generic;

namespace HRMS.Core.Models.Email
{
    public class EmailOptions
    {
        public EmailOptions()
        {
            Options = new List<NotificationOptions>();
        }
        public string TemplateId { get; set; }
        public string Template { get; set; }
        public string FromEmail { get; set; }
        public IEnumerable<NotificationOptions> Options { get; set; }
    }
}
