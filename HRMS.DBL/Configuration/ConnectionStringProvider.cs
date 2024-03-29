using Microsoft.Extensions.Configuration;

namespace HRMS.DBL.Configuration
{
    public static class ConnectionStringProvider
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnection");
        }
    }
}
