using HRMS.Core.Consts;
using HRMS.Core.Models.Resource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IResourceService
    {
        Task<int> AddOrUpdateRecourceDetails(ResourceRequestModel model);
        Task<ResourceListModel> GetRecourceDetails(ResourceFilterModel filters);
        Task<ResourceModel> GetRecourceDetail(int id, RecordStatus status = RecordStatus.Active);
        Task<IEnumerable<ResourceUserHistoryModel>> GetRecourceDetails(int resourceId);
        Task<int?> DeleteResources(int id);
        Task<int> AddRecourceAllocation(ResourceAllocationModel model);
    }
}
