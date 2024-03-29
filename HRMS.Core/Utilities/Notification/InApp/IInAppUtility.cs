using HRMS.Core.Models.Email;
using HRMS.Core.Models.InApp;

namespace HRMS.Core.Utilities.Notification.InApp
{
    public interface IInAppUtility
    {
        string GenerateMessage(InAppOptions options);
    }
}
