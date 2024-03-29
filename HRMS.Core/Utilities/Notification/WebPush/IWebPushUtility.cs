using HRMS.Core.Models.WebPush;
using System.Threading.Tasks;

namespace HRMS.Core.Utilities.Notification.Slack
{
    public interface IWebPushUtility
    {
        Task SendAsync(WebPushOptions webPushMessageOptions);
    }
}
