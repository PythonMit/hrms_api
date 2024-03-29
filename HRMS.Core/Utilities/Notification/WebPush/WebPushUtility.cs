using HRMS.Core.Models.WebPush;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPush;
using System.Linq;
using HRMS.Core.Settings;

namespace HRMS.Core.Utilities.Notification.Slack
{
    public class WebPushUtility : IWebPushUtility
    {
        private readonly WebPushSettings _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public WebPushUtility(IOptions<WebPushSettings> options)
        {
            _options = options.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="webPushMessageOptions"></param>
        /// <returns></returns>
        public async Task SendAsync(WebPushOptions webPushMessageOptions)
        {
            if (webPushMessageOptions.MessageOptions == null)
            {
                return;
            }
            var innerExceptions = new List<WebPushException>();

            foreach (var sub in webPushMessageOptions.MessageOptions)
            {
                var toPushSubscriptions = sub.ToPushSubscriptions.Distinct();
                foreach (var toPushSubscription in sub.ToPushSubscriptions)
                {
                    var subscription = new PushSubscription(toPushSubscription.Endpoint, toPushSubscription.P256dh, toPushSubscription.Auth);
                    var vapidDetails = new VapidDetails(_options.Subject, _options.PublicKey, _options.PrivateKey);

                    var messageToBeSent = webPushMessageOptions.MessageTemplate;

                    foreach (var item in sub.Parameters)
                    {
                        messageToBeSent = messageToBeSent.Replace($"%recipient.{item.Key}%", item.Value);
                    }

                    var customData = new Dictionary<string, string>();
                    customData.Concat(sub.CustomData);

                    if (!customData.ContainsKey("url"))
                    {
                        customData.Add("url", sub.Url);

                    }
                    if (!customData.ContainsKey("msgType"))
                    {
                        customData.Add("msgType", sub.MessageType);
                    }

                    var message = JsonConvert.SerializeObject(new { title = sub.Subject, description = messageToBeSent, icon = "", customData = sub.CustomData });
                    var webPushClient = new WebPushClient();

                    try
                    {
                        await webPushClient.SendNotificationAsync(subscription, message, vapidDetails);

                    }
                    catch (WebPushException exception)
                    {
                        innerExceptions.Add(exception);
                    }
                }
            }
        }
    }
}
