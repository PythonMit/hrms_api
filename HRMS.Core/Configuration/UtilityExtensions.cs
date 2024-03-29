using Microsoft.Extensions.DependencyInjection;
using HRMS.Core.Utilities.Auth;
using HRMS.Core.Utilities.Image;
using HRMS.Core.Utilities.General;
using HRMS.Core.Utilities.Notification.Email;
using HRMS.Core.Utilities.Notification.Slack;
using HRMS.Core.Utilities.Notification.InApp;

namespace HRMS.Core.Configuration
{
    public static class UtilityExtensions
    {
        public static void ConfigureUtilities(this IServiceCollection services)
        {
            services.AddScoped<IUserContextAccessor, UserContextAccessor>();
            services.AddScoped<IImageKitUtility, ImageKitUtility>();
            services.AddScoped<IGeneralUtilities, GeneralUtilities>();
            services.AddScoped<IEmailUtility, EmailUtility>();
            services.AddScoped<IWebPushUtility, WebPushUtility>();
            services.AddScoped<ISlackUtility, SlackUtility>();
            services.AddScoped<IInAppUtility, InAppUtility>();
        }
    }
}
