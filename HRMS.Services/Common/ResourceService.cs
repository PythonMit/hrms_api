using AutoMapper;
using HRMS.Core.Consts;
using HRMS.Core.Models.Resource;
using HRMS.DBL.Entities;
using HRMS.DBL.Extensions;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public class ResourceService : IResourceService
    {
        private readonly ResourceStore _resourceStore;

        private readonly IMapper _mapper;

        public ResourceService(ResourceStore resourceStore, IMapper mapper)
        {
            _resourceStore = resourceStore;
            _mapper = mapper;
        }

        public Task<int> AddOrUpdateRecourceDetails(ResourceRequestModel model)
        {
            return _resourceStore.AddOrUpdateRecourceDetails(model);
        }
        public Task<int> AddRecourceAllocation(ResourceAllocationModel model)
        {
            return _resourceStore.AddRecourceAllocation(model);
        }
        public async Task<ResourceModel> GetRecourceDetail(int id, RecordStatus status = RecordStatus.Active)
        {
            var data = await _resourceStore.GetRecourceDetail(id, status);
            return data.ToResourceDetailsModel().FirstOrDefault();
        }
        public async Task<ResourceListModel> GetRecourceDetails(ResourceFilterModel filter)
        {
            var data = await _resourceStore.GetRecourceDetails(filter);
            var result = data.ToResourceDetailsModel();

            int totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = result?.Count() ?? 0;
                result = result?.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize);
            }

            return new ResourceListModel
            {
                Records = result.ToList(),
                TotalRecords = totalRecords
            };
        }
        public async Task<IEnumerable<ResourceUserHistoryModel>> GetRecourceDetails(int resourceId)
        {
            var data = await _resourceStore.GetResourceUserHistory(resourceId);
            return data.Select(x => _mapper.Map<ResourceUserHistoryModel>(x));
        }
        public async Task<int?> DeleteResources(int Id)
        {
            return await _resourceStore.DeleteResources(Id);
        }
    }
}
