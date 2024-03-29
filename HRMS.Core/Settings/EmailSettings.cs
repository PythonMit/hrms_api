namespace HRMS.Core.Settings
{
    public class EmailSettings
    {
        public string Name { get; set; }
        public string From { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public SmtpSettings Smtp { get; set; }
        public ThirdPartySettings ThirdParty { get; set; }
        public string CC { get; set; }
    }

    public class SmtpSettings
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
    }

    public class ThirdPartySettings
    {
        public string ClientType { get; set; }
        public string ApiKey { get; set; }
        public string ApiBaseUri { get; set; }
        public string RequestUri { get; set; }
    }
}
