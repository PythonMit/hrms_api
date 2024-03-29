using HRMS.Core.Consts;
using System;

namespace HRMS.Core.Models.Notification
{
    public class NotificationModel
    {
        public Guid? Id { get; set; }
        public int? EmployeeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool HasRead { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
