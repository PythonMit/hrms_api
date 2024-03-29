using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using HRMS.Core.Settings;
using HRMS.Core.Models.Slack;
using System.Net.Http;
using HRMS.Core.Consts;
using Newtonsoft.Json;
using System.Linq;
using System;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Email;

namespace HRMS.Core.Utilities.Notification.Slack
{
    public class SlackUtility : ISlackUtility
    {
        private readonly SlackSettings _settings;
        private readonly ApiSettings _apiSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public SlackUtility(IOptions<SlackSettings> settings, IOptions<ApiSettings> apiSettings)
        {
            _settings = settings.Value;
            _apiSettings = apiSettings.Value;
        }
        public string GenerateMessage(SlackPushOptions options)
        {
            if (!string.IsNullOrEmpty(options.MessageTemplate))
            {
                options.MessageTemplate = options.MessageTemplate.Replace("%TODAYDATE%", DateTime.Now.ToString("MMMM dd, yyyy"));

                foreach (var item in options.MessageOptions.SelectMany(x => x.Parameters).ToList())
                {
                    if (item.Key == "status")
                    {
                        if (item.Value == EmployeeOverTimeStatusType.Approved.GetEnumDescriptionAttribute() || item.Value == EmployeeLeaveStatusType.Approved.GetEnumDescriptionAttribute())
                        {
                            options.MessageTemplate = options.MessageTemplate.Replace("%STATUS%", item.Value);
                        }
                        else
                        {
                            options.MessageTemplate = options.MessageTemplate.Replace(", totaling ", string.Empty).Replace("%TOTALDAYSHOURS%", string.Empty).Replace("%STATUS%", item.Value);
                        }
                    }
                    else if (item.Key == "dates")
                    {
                        options.MessageTemplate = options.MessageTemplate.Replace("%DATES%", item.Value);
                    }
                    else if (item.Key == "totaldayshours")
                    {
                        options.MessageTemplate = options.MessageTemplate.Replace("%TOTALDAYSHOURS%", item.Value);
                    }
                }
            }
            return options.MessageTemplate;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<(SlackChatMessageResponse response, string message)?> SendAsync(SlackPushOptions options)
        {
            if (string.IsNullOrEmpty(_settings.ClientID) && string.IsNullOrEmpty(_settings.ClientSecret))
            {
                return null;
            }

            if (options.MessageOptions == null && string.IsNullOrEmpty(options?.Token))
            {
                return null;
            }

            var messageBody = GenerateMessage(options);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{SlackEndpoints.PostMessage}");
            request.Headers.Add("Authorization", $"Bearer {options.Token}");
            var content = new StringContent(JsonConvert.SerializeObject(new SlackChatMessageRequest { channel = options.ChannelId, text = messageBody }), null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            var t = response.EnsureSuccessStatusCode();
            if (t.IsSuccessStatusCode)
            {
                var resultJsonString = await response.Content.ReadAsStringAsync();
                return (response: JsonConvert.DeserializeObject<SlackChatMessageResponse>(resultJsonString), message: messageBody);
            }
            return (response: null, message: string.Empty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<SlackUserListResponseModel> GetUserList(SlackPushOptions options)
        {
            if (string.IsNullOrEmpty(_settings.ClientID) && string.IsNullOrEmpty(_settings.ClientSecret))
            {
                return null;
            }

            if (options.MessageOptions == null && string.IsNullOrEmpty(options.Token))
            {
                return null;
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{SlackEndpoints.UserList}");
            request.Headers.Add("Authorization", $"Bearer {options.Token}");
            var response = await client.SendAsync(request);
            var t = response.EnsureSuccessStatusCode();
            if (t.IsSuccessStatusCode)
            {
                var resultJsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SlackUserListResponseModel>(resultJsonString);
            }
            return null;
        }
    }
}
