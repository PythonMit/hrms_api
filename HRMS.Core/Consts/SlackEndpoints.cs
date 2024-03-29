namespace HRMS.Core.Consts
{
    public static class SlackEndpoints
    {
        public static string Authorize = "https://slack.com/oauth/authorize?scope={{scope}}&client_id=";
        public static string OAuthAccess = "https://slack.com/api/oauth.access";
        public static string UserList = "https://slack.com/api/users.list";        
        public static string PostMessage = "https://slack.com/api/chat.postMessage";
    }
}
