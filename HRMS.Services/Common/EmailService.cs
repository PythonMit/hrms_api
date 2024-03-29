using HRMS.Core.Settings;
using Microsoft.Extensions.Options;

namespace HRMS.Services.Common
{
    public abstract class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
    }
}
