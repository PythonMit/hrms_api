using HRMS.Core.Utilities.Auth;
using HRMS.DBL.DbContextConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using HRMS.Core.Models.General;
using System;

namespace HRMS.DBL.Stores
{
    public class MasterDataStore : BaseStore
    {
        public MasterDataStore(HRMSDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<EmployeeStatus>> GetEmployeeStatus()
        {
            return await _dbContext.EmployeeStatus.ToListAsync();
        }
        public async Task<IEnumerable<EmployeeContractStatus>> GetEmployeeContractStatus()
        {
            return await _dbContext.EmployeeContractStatus.ToListAsync();
        }
        public async Task<IEnumerable<DocumentType>> GetDocumentTypes()
        {
            return await _dbContext.DocumentTypes.ToListAsync();
        }
        public async Task<IEnumerable<EmployeeOverTimeStatus>> GetEmployeeOverTimeStatus()
        {
            return await _dbContext.EmployeeOverTimeStatus.ToListAsync();
        }
        public async Task<IEnumerable<EmployeeLeaveStatus>> GetEmployeeLeaveStatus()
        {
            return await _dbContext.EmployeeLeaveStatus.ToListAsync();
        }
        public async Task<IEnumerable<DesignationType>> GetDesignationTypes()
        {
            return await _dbContext.DesignationTypes.ToListAsync();
        }
        public async Task<IEnumerable<LeaveType>> GetLeaveTypes()
        {
            return await _dbContext.LeaveTypes.ToListAsync();
        }
        public async Task<IEnumerable<Branch>> GetBranches()
        {
            return await _dbContext.Branches.ToListAsync();
        }
        public async Task<IEnumerable<Role>> RoleTypes(int priority, bool onPriority)
        {
            return await _dbContext.Roles.Where(x => (onPriority ? x.Priority > priority: true)).ToListAsync();
        }
        public async Task<IEnumerable<string>> GetCountries()
        {
            return await _dbContext.Locations.Select(x => x.CountryName).Distinct().ToListAsync();
        }
        public async Task<IEnumerable<StateModel>> GetStates(string countryName)
        {
            return await _dbContext.Locations.Where(x => string.IsNullOrEmpty(countryName) ? true : x.CountryName.ToLower().Contains(countryName.ToLower()))
                                            .Select(x => new StateModel { Name = x.StateName, Code = x.StateCode })
                                            .Distinct().ToListAsync();
        }
        public async Task<IEnumerable<CityModel>> GetCities(string stateCode, string stateName)
        {
            return await _dbContext.Locations.Where(x => (string.IsNullOrEmpty(stateCode) ? true : x.StateCode.ToLower().Contains(stateCode.ToLower())) && (string.IsNullOrEmpty(stateName) ? true : x.StateName.ToLower().Contains(stateName.ToLower())))
                                            .Select(x => new CityModel { Name = x.CityName, MetroCity = x.MetroCity })
                                            .Distinct().ToListAsync();
        }
        public async Task<IEnumerable<string>> GetMetroCities(string stateCode, string stateName)
        {
            return await _dbContext.Locations.Where(x => (string.IsNullOrEmpty(stateCode) ? true : x.StateCode.ToLower().Contains(stateCode.ToLower())) && (string.IsNullOrEmpty(stateName) ? true : x.StateName.ToLower().Contains(stateName.ToLower())))
                                            .Select(x => x.MetroCity)
                                            .Distinct().ToListAsync();
        }
        public async Task<IEnumerable<string>> GetPostalCode(string cityName, string metroCity)
        {
            return await _dbContext.Locations.Where(x => (string.IsNullOrEmpty(cityName) ? true : x.CityName.ToLower().Contains(cityName.ToLower())) && (string.IsNullOrEmpty(metroCity) ? true : x.MetroCity.ToLower().Contains(metroCity.ToLower())))
                                            .Select(x => x.PostalCode)
                                            .Distinct().ToListAsync();
        }
        public async Task<IEnumerable<EmployeeSalaryStatus>> GetEmployeeSalaryStatus()
        {
            return await _dbContext.EmployeeSalaryStatus.ToListAsync();
        }
        public async Task<IEnumerable<LeaveCategory>> GetLeaveCategoryTypes()
        {
            return await _dbContext.LeaveCategories.ToListAsync();
        }

        public async Task<IEnumerable<ResourceType>> GetResourceTypes()
        {
            return await _dbContext.ResourceTypes.ToListAsync();
        }
    }
}
