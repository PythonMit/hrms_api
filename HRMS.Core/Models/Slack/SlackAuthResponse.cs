namespace HRMS.Core.Models.Slack
{
    public class SlackAuthResponse
    {
        public bool ok { get; set; }
        public string access_token { get; set; }
        public string scope { get; set; }
        public string user_id { get; set; }
        public string team_id { get; set; }
        public object enterprise_id { get; set; }
        public string team_name { get; set; }
        public Incoming_Webhook incoming_webhook { get; set; }
    }

    public class Incoming_Webhook
    {
        public string channel { get; set; }
        public string channel_id { get; set; }
        public string configuration_url { get; set; }
        public string url { get; set; }
    }
}


