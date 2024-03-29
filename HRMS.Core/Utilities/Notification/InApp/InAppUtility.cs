using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Email;
using HRMS.Core.Models.InApp;
using HRMS.Core.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace HRMS.Core.Utilities.Notification.InApp
{
    public class InAppUtility : IInAppUtility
    {
        private readonly ApiSettings _apiSettings;

        public InAppUtility(IOptions<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
        }

        public string GenerateMessage(InAppOptions options)
        {
            if (!string.IsNullOrEmpty(options.Template))
            {
                foreach (var item in options.Options.SelectMany(x => x.Parameters).ToList())
                {
                    if (item.Key == "name")
                    {
                        options.Template = options.Template.Replace("%Employee%", item.Value);
                    }
                    if (item.Key == "status")
                    {
                        if (item.Value == EmployeeOverTimeStatusType.Approved.GetEnumDescriptionAttribute() || item.Value == EmployeeLeaveStatusType.Approved.GetEnumDescriptionAttribute())
                        {
                            options.Template = options.Template.Replace("%STATUS%", item.Value);
                        }
                        else
                        {
                            options.Template = options.Template.Replace(", totaling ", string.Empty).Replace("%TOTALDAYSHOURS%", string.Empty).Replace("%STATUS%", item.Value);
                        }
                    }
                    else if (item.Key == "dates")
                    {
                        options.Template = options.Template.Replace("%DATES%", item.Value);
                    }
                    else if (item.Key == "totaldayshours")
                    {
                        options.Template = options.Template.Replace("%TOTALDAYSHOURS%", item.Value);
                    }
                }
            }
            return options.Template;
        }
    }
}
