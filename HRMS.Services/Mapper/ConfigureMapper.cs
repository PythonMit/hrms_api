using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Mapper
{
    public static class MapperServicesExtensions
    {
        public static void ConfigureMapper(this IServiceCollection services)
        {
            services.AddAutoMapper((serviceProvider, config) =>
            {
                config.ConstructServicesUsing(type => serviceProvider.GetRequiredService(type));
            }, typeof(ServicesMapperProfile));
        }
    }
}
