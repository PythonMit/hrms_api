using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.Core.Consts;
using HRMS.Core.Models.Branch;
using HRMS.Core.Models.Contract;
using HRMS.Core.Models.DesignationType;
using HRMS.Core.Models.Document;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.General;
using HRMS.Core.Models.Leave;
using HRMS.Core.Models.Overtime;
using HRMS.Core.Models.Resource;
using HRMS.Core.Models.Salary;
using HRMS.Core.Models.User;

namespace HRMS.Services.Interfaces
{
    public interface IMasterDataService
    {
        Task<IEnumerable<EmployeeStatusModel>> GetEmployeeStatus();
        Task<IEnumerable<EmployeeContractStatusModel>> GetEmployeeContractStatus();
        Task<IEnumerable<DocumentTypeModel>> GetDocumentTypes();
        Task<IEnumerable<EmployeeOverTimeStatusModel>> GetEmployeeOverTimeStatus();
        Task<IEnumerable<EmployeeLeaveStatusModel>> GetEmployeeLeaveStatus();
        Task<IEnumerable<DesignationTypeModel>> GetDesignationTypes();
        Task<IEnumerable<LeaveTypeModel>> GetLeaveTypes();
        Task<IEnumerable<BranchModel>> GetBranches();
        Task<IEnumerable<RoleModel>> RoleTypes(RoleTypes? type, bool onPriority = false);
        Task<IEnumerable<string>> GetCountries();
        Task<IEnumerable<StateModel>> GetStates(string countryName);
        Task<IEnumerable<CityModel>> GetCities(string stateCode, string stateName);
        Task<IEnumerable<string>> GetMetroCities(string stateCode, string stateName);
        Task<IEnumerable<string>> GetPostalCode(string cityName, string metroCity);
        Task<IEnumerable<EmployeeSalaryStatusModel>> GetEmployeeSalaryStatus();
        Task<IEnumerable<LeaveCategoryModel>> GetLeaveCategoryTypes();
        Task<IEnumerable<ResourceTypeModel>> GetResourceTypes();
    }
}
