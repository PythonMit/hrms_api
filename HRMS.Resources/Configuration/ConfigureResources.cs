using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Resources.Configuration
{
    public static class ResourceExtensions
    {
        public static void ConfigureResources(this IServiceCollection services)
        {
            services.AddScoped<IAppResourceAccessor, ShareadStringResources>();
        }
    }
}
