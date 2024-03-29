using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Services.Interfaces;
using HRMS.DBL.Stores;
using AutoMapper;
using HRMS.Core.Models.Branch;
using HRMS.Core.Models.DesignationType;
using HRMS.Core.Models.Document;
using HRMS.Core.Models.Contract;
using HRMS.Core.Models.Overtime;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.User;
using HRMS.Core.Models.Leave;
using HRMS.Core.Models.General;
using HRMS.Core.Models.Salary;
using HRMS.Core.Consts;
using HRMS.Core.Utilities.General;
using HRMS.DBL.Entities;
using HRMS.Core.Models.Resource;

namespace HRMS.Services.Common
{
    public class MasterDataService : IMasterDataService
    {
        private readonly MasterDataStore _masterDataStore;
        private readonly IMapper _mapper;
        private readonly IGeneralUtilities _generalUtilities;

        public MasterDataService(MasterDataStore masterDataStore, IMapper mapper, IGeneralUtilities generalUtilities)
        {
            _masterDataStore = masterDataStore;
            _mapper = mapper;
            _generalUtilities = generalUtilities;
        }

        public async Task<IEnumerable<EmployeeStatusModel>> GetEmployeeStatus()
        {
            var data = await _masterDataStore.GetEmployeeStatus();
            return data.Select(x => _mapper.Map<EmployeeStatusModel>(x));
        }
        public async Task<IEnumerable<EmployeeContractStatusModel>> GetEmployeeContractStatus()
        {
            var data = await _masterDataStore.GetEmployeeContractStatus();
            return data.Select(x => _mapper.Map<EmployeeContractStatusModel>(x));
        }
        public async Task<IEnumerable<DocumentTypeModel>> GetDocumentTypes()
        {
            var data = await _masterDataStore.GetDocumentTypes();
            return data.Select(x => _mapper.Map<DocumentTypeModel>(x));
        }
        public async Task<IEnumerable<EmployeeOverTimeStatusModel>> GetEmployeeOverTimeStatus()
        {
            var data = await _masterDataStore.GetEmployeeOverTimeStatus();
            return data.Select(x => _mapper.Map<EmployeeOverTimeStatusModel>(x));
        }
        public async Task<IEnumerable<EmployeeLeaveStatusModel>> GetEmployeeLeaveStatus()
        {
            var data = await _masterDataStore.GetEmployeeLeaveStatus();
            return data.Select(x => _mapper.Map<EmployeeLeaveStatusModel>(x));
        }
        public async Task<IEnumerable<DesignationTypeModel>> GetDesignationTypes()
        {
            var data = await _masterDataStore.GetDesignationTypes();
            return data.Select(x => _mapper.Map<DesignationTypeModel>(x));
        }
        public async Task<IEnumerable<LeaveTypeModel>> GetLeaveTypes()
        {
            var data = await _masterDataStore.GetLeaveTypes();
            return data.Select(x => _mapper.Map<LeaveTypeModel>(x));
        }
        public async Task<IEnumerable<BranchModel>> GetBranches()
        {
            var data = await _masterDataStore.GetBranches();
            return data.Select(x => _mapper.Map<BranchModel>(x));
        }
        public async Task<IEnumerable<RoleModel>> RoleTypes(RoleTypes? type, bool onPriority = false)
        {
            var priority = _generalUtilities.GetRolePriority(type);
            var data = await _masterDataStore.RoleTypes(priority, onPriority);
            return data.Select(x => _mapper.Map<RoleModel>(x));
        }
        public async Task<IEnumerable<string>> GetCountries()
        {
            return await _masterDataStore.GetCountries();
        }
        public async Task<IEnumerable<StateModel>> GetStates(string countryName)
        {
            return await _masterDataStore.GetStates(countryName);
        }
        public async Task<IEnumerable<CityModel>> GetCities(string stateCode, string stateName)
        {
            return await _masterDataStore.GetCities(stateCode, stateName);
        }
        public async Task<IEnumerable<string>> GetMetroCities(string stateCode, string stateName)
        {
            return await _masterDataStore.GetMetroCities(stateCode, stateName);
        }
        public async Task<IEnumerable<string>> GetPostalCode(string cityName, string metroCity)
        {
            return await _masterDataStore.GetPostalCode(cityName, metroCity);
        }
        public async Task<IEnumerable<EmployeeSalaryStatusModel>> GetEmployeeSalaryStatus()
        {
            var data = await _masterDataStore.GetEmployeeSalaryStatus();
            return data.Select(x => _mapper.Map<EmployeeSalaryStatusModel>(x));
        }
        public async Task<IEnumerable<LeaveCategoryModel>> GetLeaveCategoryTypes()
        {
            var data = await _masterDataStore.GetLeaveCategoryTypes();
            return data.Select(x => _mapper.Map<LeaveCategoryModel>(x));
        }
        public async Task<IEnumerable<ResourceTypeModel>> GetResourceTypes()
        {
            var data = await _masterDataStore.GetResourceTypes();
            return data.Select(x => _mapper.Map<ResourceTypeModel>(x));
        }
    }
}
