using HRMS.Core.Consts;
using HRMS.Core.Models.Email;
using System.Threading.Tasks;

namespace HRMS.Core.Utilities.Notification.Email
{
    public interface IEmailUtility
    {
        Task<(bool reponse, string message)?> SendAsync(EmailOptions options, MailerType type);
        string GenerateMessage(EmailOptions options, bool hasHtml = true);
    }
}
