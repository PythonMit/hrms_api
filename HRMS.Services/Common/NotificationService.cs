using AutoMapper;
using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Email;
using HRMS.Core.Models.InApp;
using HRMS.Core.Models.Notification;
using HRMS.Core.Models.Slack;
using HRMS.Core.Settings;
using HRMS.Core.Utilities.Notification.Email;
using HRMS.Core.Utilities.Notification.InApp;
using HRMS.Core.Utilities.Notification.Slack;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public abstract class NotificationService<TData, TParameters> : INotificationService<TData>
    {
        private readonly AppSettings _appSettings;
        private readonly EmailSettings _emailSettings;
        private readonly NotificationSettings _notificationSettings;
        private readonly NotificationStore _notificationStore;

        private readonly ISystemFlagService _systemFlagService;
        private readonly IEmailUtility _emailUtility;
        private readonly ISlackUtility _slackPushUtility;
        private readonly IInAppUtility _inAppUtility;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        protected TData Data { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        protected NotificationService(IServiceProvider serviceProvider)
        {
            _appSettings = serviceProvider.GetService<IOptions<AppSettings>>().Value;
            _emailSettings = serviceProvider.GetService<IOptions<EmailSettings>>()?.Value;
            _notificationSettings = serviceProvider.GetService<IOptions<NotificationSettings>>()?.Value;
            _systemFlagService = serviceProvider.GetService<ISystemFlagService>();
            _emailUtility = serviceProvider.GetService<IEmailUtility>();
            _notificationStore = serviceProvider.GetService<NotificationStore>();
            _slackPushUtility = serviceProvider.GetService<ISlackUtility>();
            _inAppUtility = serviceProvider.GetService<IInAppUtility>();
            _employeeService = serviceProvider.GetService<IEmployeeService>();
            _mapper = serviceProvider.GetService<IMapper>();
        }
        /// <summary>
        /// Send Notification
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<string> SendNotification(TData data, NotificationTypes notificationTypes)
        {
            var flags = await _systemFlagService.GetSystemFlagsByTag("disableallcommunications");
            if (string.IsNullOrEmpty(flags?.Value) || flags?.Value == FlagStatus.Disable.GetEnumDescriptionAttribute())
            {
                return string.Empty;
            }

            Data = data;
            var template = await GetTemplateFileName(data);
            var notificationOptions = GetNotificationOptions(data);

            switch (notificationTypes)
            {
                case NotificationTypes.Email:
                    var emailMessage = GetTemplateText(template.Name, notificationTypes);
                    if (!string.IsNullOrEmpty(emailMessage))
                    {
                        await EmailNotification(emailMessage, notificationOptions);
                    }
                    break;
                case NotificationTypes.Sms:
                    break;
                case NotificationTypes.WebPush:
                    break;
                case NotificationTypes.InApp:
                    var inAppMessage = GetTemplateText(template.Name, notificationTypes);
                    if (!string.IsNullOrEmpty(inAppMessage))
                    {
                        await InAppNotification(inAppMessage, notificationOptions);
                    }
                    break;
                case NotificationTypes.Slack:
                    var slackMessage = GetTemplateText(template.Name, notificationTypes);
                    if (!string.IsNullOrEmpty(slackMessage))
                    {
                        await SlackNotification(slackMessage, notificationOptions);
                    }
                    break;
                default:
                    break;
            }
            return string.Empty;
        }
        /// <summary>
        /// Get Notification Options
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerable<NotificationOptions> GetNotificationOptions(TData data)
        {
            var parametersList = (data as INotificationParameters<TParameters>).TemplateParameters;

            foreach (var item in parametersList)
            {
                NotificationOptions options = new NotificationOptions();
                var parametersSerialized = JsonConvert.SerializeObject(item);
                var destinationData = item as NotificationParametersBase<TData>;

                options.CustomData = destinationData.CustomData;
                options.Parameters = JsonConvert.DeserializeObject<IDictionary<string, string>>(parametersSerialized);
                options.Subject = destinationData.Subject;
                options.ToMail = destinationData.ToMail;
                options.ToPhoneNumber = destinationData.ToPhoneNumber;
                options.ToPushSubscriptions = destinationData.WebPushSubscriptions;
                options.MessageType = destinationData.MessageType;
                options.Url = destinationData.Url;
                options.EnableWebSocket = destinationData.WebSocketEnabled;
                options.ChannelId = destinationData.ChannelId;
                options.EmployeeId = destinationData.EmployeeId;
                yield return options;
            }

        }
        /// <summary>
        /// Get Template Text
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="notificationType"></param>
        /// <returns></returns>
        public string GetTemplateText(string templateFile, NotificationTypes notificationType)
        {
            string notificationTemplate = string.Empty;

            if (notificationType == NotificationTypes.Email)
            {
                notificationTemplate = $"{_notificationSettings.EmailNamespace}.{templateFile}.html";
            }
            else if (notificationType == NotificationTypes.InApp)
            {
                notificationTemplate = $"{_notificationSettings.InAppNamespace}.{templateFile}.txt";
            }
            else if (notificationType == NotificationTypes.Slack)
            {
                notificationTemplate = $"{_notificationSettings.SlackNamespace}.{templateFile}.txt";
            }

            using (Stream resource = _notificationSettings.Assembly.GetManifestResourceStream(notificationTemplate))
            {
                if (resource != null)
                {
                    using (var reader = new StreamReader(resource))
                    {
                        return reader.ReadToEnd();
                    }
                }

            }

            return null;
        }
        /// <summary>
        /// Get Template File Name
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected abstract Task<(string Name, NotificationTypes Type)> GetTemplateFileName(TData data);
        /// <summary>
        /// Email Notification
        /// </summary>
        /// <param name="message"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private async Task EmailNotification(string message, IEnumerable<NotificationOptions> options = null)
        {
            var emailOption = new EmailOptions
            {
                FromEmail = _emailSettings.From,
                Template = message,
                Options = options
            };

            await _emailUtility.SendAsync(emailOption, MailerType.Smtp);
        }
        /// <summary>
        /// Slack Notification
        /// </summary>
        /// <param name="message"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private async Task SlackNotification(string message, IEnumerable<NotificationOptions> options = null)
        {
            var slackOption = new SlackPushOptions
            {
                MessageTemplate = message,
                MessageOptions = options,
                ChannelId = options?.Select(x => x.ChannelId).FirstOrDefault(),
            };

            var token = await _systemFlagService.GetSystemFlagsByTag("slackaccesstoken");
            if (string.IsNullOrEmpty(token?.Value))
            {
                return;
            }

            slackOption.Token = token?.Value;
            var result = await _slackPushUtility.SendAsync(slackOption);
        }
        /// <summary>
        /// In App Notification
        /// </summary>
        /// <param name="message"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private async Task InAppNotification(string message, IEnumerable<NotificationOptions> options = null)
        {
            var inAppOption = new InAppOptions
            {
                Template = message,
                Options = options,
            };
            message = _inAppUtility.GenerateMessage(inAppOption);
            await _notificationStore.AddNotifications(new NotificationModel
            {
                Id = Guid.NewGuid(),
                Title = options?.Select(x => x.Subject).FirstOrDefault(),
                Description = message,
                Type = NotificationTypes.InApp.GetEnumDescriptionAttribute(),
                HasRead = false,
                EmployeeId = options?.Select(x => x.EmployeeId).FirstOrDefault()
            });
        }
        /// <summary>
        /// Sync slack user information
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> SyncSlackUserInformation(string employeeEmail)
        {
            var result = new List<string>();
            var flag = await _systemFlagService.GetSystemFlagsByTag("slackaccesstoken");
            var userList = await _slackPushUtility.GetUserList(new SlackPushOptions { Token = flag?.Value });
            var members = userList?.members.Select(x => new { Id = x.id, Email = x.profile.email }).ToList();
            if (!string.IsNullOrEmpty(employeeEmail))
            {
                members = members.Where(x => x.Email == employeeEmail).ToList();
            }
            foreach (var item in members)
            {
                if (string.IsNullOrEmpty(item.Email))
                {
                    continue;
                }

                var code = await _employeeService.UpdateEmployeeSlackId(item.Email, item.Id);
                if (!string.IsNullOrEmpty(code))
                {
                    result.Add(code);
                }
            }
            return result;
        }
        /// <summary>
        /// Set Notification Status
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public async Task<bool> SetNotificationStatus(IEnumerable<Guid> Ids, bool readAll = false)
        {
            return await _notificationStore.SetNotificationStatus(Ids, readAll);
        }
        /// <summary>
        /// Get All Notifications
        /// </summary>
        /// <param name="hasRead"></param>
        /// <returns></returns>
        public async Task<NotificationListModel> GetNotifications(NotificationFilterModel filter)
        {
            var data = await _notificationStore.GetNotifications(filter.HasRead, filter.EmployeeId);
            var result = data?.Select(x => _mapper.Map<NotificationModel>(x));

            var totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = result?.Count() ?? 0;
                result = result?.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize);
            }

            return new NotificationListModel
            {
                Notifications = result,
                TotalRecords = totalRecords,
            };
        }
    }
}
