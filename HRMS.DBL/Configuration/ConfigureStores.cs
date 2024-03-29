using Microsoft.Extensions.DependencyInjection;
using HRMS.DBL.Stores;

namespace HRMS.DBL.Configuration
{
    public static class StoresExtensions
    {
        public static void ConfigureStores(this IServiceCollection services)
        {
            services.AddScoped<UserStore>();
            services.AddScoped<MasterDataStore>();
            services.AddScoped<EmployeeStore>();
            services.AddScoped<SystemFlagStore>();
            services.AddScoped<EmployeeAddressStore>();
            services.AddScoped<EmployeeDocumentStore>();
            services.AddScoped<EmployeeBasicInformationStore>();
            services.AddScoped<EmployeeContactInformationStore>();
            services.AddScoped<EmployeeJobInformationStore>();
            services.AddScoped<EmployeeSecurityInformationStore>();
            services.AddScoped<EmployeeContractStore>();
            services.AddScoped<EmployeeOvertimeStore>();
            services.AddScoped<EmployeeLeaveStore>();
            services.AddScoped<EmployeeSalaryStore>();
            services.AddScoped<HolidayStore>();
            services.AddScoped<ReportStore>();
            services.AddScoped<ProjectStore>();
            services.AddScoped<ActivityLogStore>();
            services.AddScoped<NotificationStore>();
            services.AddScoped<EmployeeBankInformationStore>();
            services.AddScoped<EmployeeLeaveApplicationStore>();
            services.AddScoped<ResourceStore>();
        }
    }
}
