using System.Collections.Generic;
using HRMS.Core.Models.WebPush;
using Newtonsoft.Json;

namespace HRMS.Core.Models.Notification
{
    public class NotificationParametersBase<T>
    {
        public NotificationParametersBase()
        {
            CustomData = new Dictionary<string, string>();
        }
        [JsonIgnore]
        public string Subject { get; set; }
        [JsonIgnore]
        public string ToMail { get; set; }
        [JsonIgnore]
        public string ToPhoneNumber { get; set; }
        [JsonIgnore]
        public string Url { get; set; }
        [JsonIgnore]
        public IEnumerable<WebPushSubscription> WebPushSubscriptions { get; set; }
        [JsonIgnore]
        public Dictionary<string, string> CustomData { get; set; }
        [JsonIgnore]
        public string MessageType { get { return typeof(T).Name; } }
        [JsonIgnore]
        public bool WebSocketEnabled { get; set; }
        [JsonIgnore]        
        public string ChannelId { get; set; }
        [JsonIgnore]
        public int? EmployeeId { get; set; }
    }
}
