﻿namespace HRMS.Core.Models.Slack
{
    public class SlackChatMessageResponse
    {
        public bool ok { get; set; }
        public string channel { get; set; }
        public string ts { get; set; }
        public Message message { get; set; }
        public string warning { get; set; }
        public Response_Metadata response_metadata { get; set; }
    }

    public class Message
    {
        public string bot_id { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public string user { get; set; }
        public string ts { get; set; }
        public string app_id { get; set; }
        public Block[] blocks { get; set; }
        public string team { get; set; }
        public Bot_Profile bot_profile { get; set; }
    }

    public class Bot_Profile
    {
        public string id { get; set; }
        public string app_id { get; set; }
        public string name { get; set; }
        public Icons icons { get; set; }
        public bool deleted { get; set; }
        public int updated { get; set; }
        public string team_id { get; set; }
    }

    public class Icons
    {
        public string image_36 { get; set; }
        public string image_48 { get; set; }
        public string image_72 { get; set; }
    }

    public class Block
    {
        public string type { get; set; }
        public string block_id { get; set; }
        public Element[] elements { get; set; }
    }

    public class Element
    {
        public string type { get; set; }
        public Element1[] elements { get; set; }
    }

    public class Element1
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class Response_Metadata
    {
        public string[] warnings { get; set; }
    }

}
