using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.Core.Consts;
using HRMS.Core.Models.Document;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.Employee.ExitProccess;
using HRMS.Core.Models.General;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeListModel> GetAllEmployees(EmployeeFilterModel filter, RoleTypes? roleTypes);
        Task<bool> DeleteEmployee(int id);
        Task<EmployeeDetailModel> GetEmployeeDetails(int id);
        Task<EmployeeInformationModel> GetEmployeeInformation(int id);
        Task<IEnumerable<EmployeeOutlineModel>> GetEmployeeInformationByRoleType(int roleTypeId);
        Task<bool> CheckEmployeeCodeExists(string employeeCode);
        Task<IEnumerable<EmployeeOutlineModel>> GetEmployeeInformationByDesignation(IEnumerable<int?> designationIds, bool hasContract, RoleTypes? userRole, int? employeeId);
        Task<EmployeeOutlineModel> GetEmployeeInformationByCode(string employeeCode);
        Task<bool?> RemoveEmployeeInformation(string employeeCode);
        Task<string> GetEmployeeSlackId(string employeeCode);
        Task<string> UpdateEmployeeSlackId(string workEmail, string slackUserId);
        Task<string> GetEmployeeEmail(string employeeCode);
        Task<string> GetEmployeeName(string employeeCode);
        Task<Guid?> AddorUpdateEmployeeImage(EmployeeImageModel model, byte[] fileStream, string folderpath, string fileName);
        #region Exit Process
        Task<EmployeeExitProcessListModel> GetExitProcessEmployees(EmployeeFilterModel filter, RoleTypes? userRole);
        Task<int?> AddorUpdateFNFProcessEmployee(EmployeeFNFDetailsRequestModel model, RoleTypes? roleTypes);
        Task<int?> AddorUpdateExitProcessEmployee(EmployeeExitRequestModel model, RoleTypes? roleTypes);
        Task<EmployeeFNFDetailsModel> GetExitProcessNotes(string employeeCode);
        #endregion Exit Process
    }
}