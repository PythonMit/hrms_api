namespace HRMS.Core.Exstensions
{
    public static class ApiExtentions
    {
        public static string WithApiKey(this string url, string key)
        {
            return $"{url}&apiKey={key}";
        }
    }
}
