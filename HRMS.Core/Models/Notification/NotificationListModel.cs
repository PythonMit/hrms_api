using System.Collections.Generic;

namespace HRMS.Core.Models.Notification
{
    public class NotificationListModel
    {
        public IEnumerable<NotificationModel> Notifications { get; set; }
        public int TotalRecords { get; set; }
    }
}
