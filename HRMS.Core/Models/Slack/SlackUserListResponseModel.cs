﻿namespace HRMS.Core.Models.Slack
{
    public class SlackUserListResponseModel
    {
        public bool ok { get; set; }
        public Member[] members { get; set; }
        public int cache_ts { get; set; }
        public Response_Metadata response_metadata { get; set; }
    }

    public class Member
    {
        public string id { get; set; }
        public string team_id { get; set; }
        public string name { get; set; }
        public bool deleted { get; set; }
        public string color { get; set; }
        public string real_name { get; set; }
        public string tz { get; set; }
        public string tz_label { get; set; }
        public int tz_offset { get; set; }
        public Profile profile { get; set; }
        public bool is_admin { get; set; }
        public bool is_owner { get; set; }
        public bool is_primary_owner { get; set; }
        public bool is_restricted { get; set; }
        public bool is_ultra_restricted { get; set; }
        public bool is_bot { get; set; }
        public bool is_app_user { get; set; }
        public int updated { get; set; }
        public bool is_email_confirmed { get; set; }
        public string who_can_share_contact_card { get; set; }
        public bool is_invited_user { get; set; }
        public bool is_forgotten { get; set; }
    }

    public class Profile
    {
        public string title { get; set; }
        public string phone { get; set; }
        public string skype { get; set; }
        public string real_name { get; set; }
        public string real_name_normalized { get; set; }
        public string display_name { get; set; }
        public string display_name_normalized { get; set; }
        public Fields fields { get; set; }
        public string status_text { get; set; }
        public string status_emoji { get; set; }
        public Status_Emoji_Display_Info[] status_emoji_display_info { get; set; }
        public int status_expiration { get; set; }
        public string avatar_hash { get; set; }
        public bool always_active { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string image_24 { get; set; }
        public string image_32 { get; set; }
        public string image_48 { get; set; }
        public string image_72 { get; set; }
        public string image_192 { get; set; }
        public string image_512 { get; set; }
        public string status_text_canonical { get; set; }
        public string team { get; set; }
        public string image_original { get; set; }
        public bool is_custom_image { get; set; }
        public string email { get; set; }
        public string huddle_state { get; set; }
        public int huddle_state_expiration_ts { get; set; }
        public string image_1024 { get; set; }
        public string api_app_id { get; set; }
        public string bot_id { get; set; }
        public string who_can_share_contact_card { get; set; }
    }

    public class Fields
    {
    }

    public class Status_Emoji_Display_Info
    {
        public string emoji_name { get; set; }
        public string display_url { get; set; }
        public string unicode { get; set; }
    }

}
