using HRMS.Services.Common;
using HRMS.Services.Common.Builder;
using HRMS.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Configuration
{
    public static class ServicesExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMasterDataService, MasterDataService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ISystemFlagService, SystemFlagService>();
            services.AddScoped<IEmployeeAddressService, EmployeeAddressService>();
            services.AddScoped<IEmployeeDocumentService, EmployeeDocumentService>();
            services.AddScoped<IEmployeeBasicInformationService, EmployeeBasicInformationService>();
            services.AddScoped<IEmployeeContactInformationService, EmployeeContactInformationService>();
            services.AddScoped<IEmployeeJobInformationService, EmployeeJobInformationService>();
            services.AddScoped<IEmployeeSecurityInformationService, EmployeeSecurityInformationService>();
            services.AddScoped<IEmployeeContractService, EmployeeContractService>();
            services.AddScoped<IEmployeeOvertimeService, EmployeeOvertimeService>();
            services.AddScoped<IEmployeeLeaveService, EmployeeLeaveService>();
            services.AddScoped<IEmployeeSalaryService, EmployeeSalaryService>();
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IActivityLogService, ActivityLogService>();
            services.AddScoped<IEmployeeBankInformationService, EmployeeBankInformationService>();
            services.AddScoped<IEmployeeLeaveApplicationService, EmployeeLeaveApplicationService>();
            services.AddScoped<INotificationService<NotificationTemplate>, NotificationBuilder>();
            services.AddScoped<IResourceService, ResourceService>();
        }
    }
}
