using HRMS.Core.Models.Slack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Core.Utilities.Notification.Slack
{
    public interface ISlackUtility
    {
        Task<(SlackChatMessageResponse response, string message)?> SendAsync(SlackPushOptions options);
        Task<SlackUserListResponseModel> GetUserList(SlackPushOptions options);
        public string GenerateMessage(SlackPushOptions options);
    }
}
