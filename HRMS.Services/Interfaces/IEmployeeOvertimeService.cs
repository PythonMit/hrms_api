using HRMS.Core.Consts;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.Core.Models.Overtime;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeOvertimeService
    {
        Task<int> AddorUpdateEmployeeOvertime(EmployeeOvertimeRequest request);
        Task<EmployeeOvertimeListModel> GetEmployeeOvertime(string employeeCode, RoleTypes? roleType = null, EmployeeOvertimeFilterModel filter = null);
        Task<EmployeeOvertimeModel> GetEmployeeOvertimeById(int id, string employeeCode, RoleTypes? userRole);
        Task<bool?> SetOvertimeStatus(EmployeeOverTimeStatusModel model);
        Task<EmployeeOvertimeListModel> GetAllEmployeeOvertimes(EmployeeOvertimeFilterModel filter, RoleTypes? userRole, int? employeeId);
        Task<bool> DeleteEmployeeOvertime(int id, string employeeCode);
        Task<bool> RemoveEmployeeOvertime(int id);
    }
}
