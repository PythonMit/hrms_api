using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using HRMS.Core.Consts;
using HRMS.Core.Models.Notification;

namespace HRMS.Services.Interfaces
{
    public interface INotificationService<TData>
    {
        string GetTemplateText(string templateFile, NotificationTypes notificationType);
        Task<NotificationListModel> GetNotifications(NotificationFilterModel filter);
        Task<string> SendNotification(TData data, NotificationTypes notificationTypes);
        Task<bool> SetNotificationStatus(IEnumerable<Guid> Ids, bool readAll = false);
        Task<IEnumerable<string>> SyncSlackUserInformation(string employeeEmail);
    }
}