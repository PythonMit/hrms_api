﻿namespace HRMS.Core.Models.WebPush
{
    public class WebPushSubscription
    {
        public string Endpoint { get; set; }
        public string Auth { get; set; }
        public string P256dh { get; set; }
    }
}
