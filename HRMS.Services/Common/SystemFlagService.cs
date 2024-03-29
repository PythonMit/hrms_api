using AutoMapper;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Core.Models.SystemFlag;

namespace HRMS.Services.Common
{
    /// <summary>
    /// SystemFlagService class is used for mapping data from model into Entity.
    /// </summary>
    public class SystemFlagService : ISystemFlagService
    {
        private readonly SystemFlagStore _systemFlagStore;
        private readonly IMapper _mapper;
        public SystemFlagService(SystemFlagStore systemFlagStore, IMapper mapper)
        {
            _systemFlagStore = systemFlagStore;
            _mapper = mapper;
        }
        public async Task<int> AddorUpdateSystemFlag(SystemFlagModel model)
        {
            return await _systemFlagStore.AddorUpdateSystemFlag(model);
        }
        public async Task<bool> DeleteSystemFlag(int id)
        {
            return await _systemFlagStore.DeleteSystemFlag(id);
        }
        public async Task<SystemFlagListModel> GetAllSystemFlags(SystemFlagFilterModel filter)
        {
            var data = await _systemFlagStore.GetAllSystemFlags(filter);
            var totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = data?.Count() ?? 0;
                data = data?.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize);
            }

            return new SystemFlagListModel
            {
                SystemFlagRecords = data.Select(x => _mapper.Map<SystemFlagModel>(x)),
                TotalRecords = totalRecords
            };
        }
        public async Task<SystemFlagModel> GetFlagDetailsByName(string flagName)
        {
            var data = await _systemFlagStore.GetFlagDetailsByName(flagName);
            return _mapper.Map<SystemFlagModel>(data);
        }
        public async Task<bool> CheckFlagExists(string flagName) 
        {
            return await _systemFlagStore.CheckFlagExists(flagName);
        }
        public async Task<bool> StatusChange(int id, bool status)
        {
            return await _systemFlagStore.StatusChange(id, status);
        }
        public async Task<IEnumerable<SystemFlagModel>> GetSystemFlagsByTags(IEnumerable<string> Tag)
        {
            var data = await _systemFlagStore.GetSystemFlagsByTags(Tag);
            return data.Select(x => _mapper.Map<SystemFlagModel>(x));
        }
        public async Task<SystemFlagModel> GetSystemFlagsByTag(IEnumerable<string> Tag)
        {
            var data = await _systemFlagStore.GetSystemFlagsByTags(Tag);
            return _mapper.Map<SystemFlagModel>(data);
        }
        public async Task<SystemFlagModel> GetSystemFlagsByTag(string Tag)
        {
            var data = await _systemFlagStore.GetSystemFlagsByTag(Tag);
            return _mapper.Map<SystemFlagModel>(data);
        }
    }
}

