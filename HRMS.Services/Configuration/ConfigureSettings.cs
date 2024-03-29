using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HRMS.Core.Settings;
using HRMS.Core.Models.Notification;
using HRMS.Services.Common.Builder;

namespace HRMS.Services.Configuration
{
    public static class SettingsExtensions
    {
        public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiSettings>(configuration.GetSection(nameof(ApiSettings)));
            services.Configure<GlobalAdminSettings>(configuration.GetSection(nameof(GlobalAdminSettings)));
            services.Configure<ImageKitSetting>(configuration.GetSection(nameof(ImageKitSetting)));
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.Configure<NotificationSettings>(x => {
                x.Assembly = typeof(NotificationBuilder).Assembly;
                x.EmailNamespace = "HRMS.Services.Templates.Email";
                x.SlackNamespace = "HRMS.Services.Templates.Slack";
                x.InAppNamespace = "HRMS.Services.Templates.InApp";
            });
            services.Configure<SlackSettings>(configuration.GetSection(nameof(SlackSettings)));
            services.Configure<GeneralSettings>(configuration.GetSection(nameof(GeneralSettings)));
        }
    }
}
