using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum NotificationTypes
    {
        [Description("Email")]
        Email = 1,
        [Description("SMS")]
        Sms = 2,
        [Description("WebPush")]
        WebPush = 3,
        [Description("InApp")]
        InApp = 4,
        [Description("Slack")]
        Slack = 5
    }
}
