using HRMS.Core.Models.General;
using System.ComponentModel;

namespace HRMS.Core.Models.Notification
{
    public class NotificationFilterModel
    {
        [DefaultValue(null)]
        public bool? HasRead { get; set; } = null;
        [DefaultValue(null)]
        public int? EmployeeId { get; set; } = null;
        public PaginationModel Pagination { get; set; }
    }
}
