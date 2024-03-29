using HRMS.Core.Consts;
using HRMS.Core.Models.Contract;
using HRMS.Core.Models.Salary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeContractService
    {
        Task<EmployeeContractListModel> GetAllEmployeeContract(EmployeeContractFilterModel filter, Core.Consts.RoleTypes? userRole);
        Task<IEnumerable<EmployeeContractHistoryModel>> GetEmployeeContractHistoryByEmployeeCode(string employeeCode, RecordStatus recordStatus = RecordStatus.Active);
        Task<EmployeeContractViewModel> GetRemainingEmployeeDetails(string employeeCode);
        Task<IEnumerable<ContractEmployeeDetailModel>> GetRemainingEmployees(string employeeName);
        Task<EmployeeCurrentContractViewModel> GetEmployeeCurrentContractDetails(int contractId);
        Task<EmployeeFixGrossModel> GetEmployeeFixGrossDetails(int contractId);
        Task<int?> GetEmployeeCurrentContractIdByEmployeeId(int employeeId);
        Task<int?> AddOrUpdateEmployeeContractDetail(EmployeeContractRequestModel model, Byte[] fileStream, string folderPath, bool publicRead, string fileName);
        Task<bool> DeleteEmployeeContractDetails(int contractId);
        Task<bool> GetRunningContract(RunningContractRequestModel model);
        Task<bool> SetEmployeeContractStatus(int contractId, int statusType);
        Task<EmployeeIncentiveDataModel> GetEmployeeIncentivesDetails(string employeeCode);
        Task<bool?> RemoveContractInformations(int id);
    }
}
