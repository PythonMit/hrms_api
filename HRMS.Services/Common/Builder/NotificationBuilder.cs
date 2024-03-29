using HRMS.Core.Consts;
using HRMS.Core.Models.Notification;
using HRMS.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Common.Builder
{
    public class NotificationTemplate : INotificationParameters<NotificationParameters>
    {
        public IEnumerable<NotificationParameters> TemplateParameters { get; set; }
        public string TemplateName { get; set; }
        public string TemplateText { get; set; }
        public NotificationTypes TemplateType { get; set; }
    }

    public class NotificationParameters : NotificationParametersBase<NotificationTemplate>
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("dates")]
        public string Dates { get; set; }
        [JsonProperty("totaldayshours")]
        public string TotalDaysHours { get; set; }
        [JsonProperty("approvedby")]
        public string ApprovedBy { get; set; }
        [JsonProperty("approvedposition")]
        public string ApprovedPosition { get; set; }
    }

    public class NotificationBuilder : NotificationService<NotificationTemplate, NotificationParameters>
    {
        public NotificationBuilder(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        protected override Task<(string Name, NotificationTypes Type)> GetTemplateFileName(NotificationTemplate data)
        {
            var templateName = data.TemplateName;
            var templateType = data.TemplateType;
            return Task.FromResult((Name: templateName, Type: templateType));
        }
    }
}
