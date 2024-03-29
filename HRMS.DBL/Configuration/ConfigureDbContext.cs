using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HRMS.Core.Settings;
using HRMS.DBL.DbContextConfiguration;

namespace HRMS.DBL.Configuration
{
    public static class DbContextExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment, AppSettings appSettings)
        {
            services.AddDbContext<HRMSDbContext>(options =>
            {
                var connectionString = ConnectionStringProvider.GetConnectionString(configuration);
                options.UseSqlServer(connectionString, opts =>
                {
                    opts.MigrationsAssembly(typeof(HRMSDbContext).Assembly.GetName().Name);
                    if (HRMSParams.IsMigrating)
                    {
                        opts.CommandTimeout(30 * 60);
                    }
                    else
                    {
                        opts.CommandTimeout(1 * 60);
                    }
                });

                if (hostEnvironment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                }
            });
        }
    }
}
