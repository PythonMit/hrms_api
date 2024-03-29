using HRMS.Core.Models.Notification;
using System.Collections.Generic;

namespace HRMS.Core.Models.InApp
{
    public class InAppOptions
    {
        public InAppOptions()
        {
            Options = new List<NotificationOptions>();
        }
        public string TemplateId { get; set; }
        public string Template { get; set; }
        public IEnumerable<NotificationOptions> Options { get; set; }
    }
}
