using HRMS.Core.Models.Resource;
using HRMS.DBL.DbContextConfiguration;
using HRMS.DBL.Entities;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using HRMS.Core.Consts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HRMS.DBL.Stores
{
    public class ResourceStore : BaseStore
    {
        public ResourceStore(HRMSDbContext dbContext) : base(dbContext) { }

        public async Task<int> AddOrUpdateRecourceDetails(ResourceRequestModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Resources.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (data == null)
                {
                    data = new Resource();
                }

                data.Status = model.Status;
                data.PurchaseDate = model.PurchaseDate;
                data.BranchId = model.BranchId;
                data.ResourceTypeId = model.ResourceTypeId;
                data.Specification = model.Specification;
                data.SystemName = model.SystemName;
                data.Remarks = model.Remarks;
                data.IsFree = true;

                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Id;
            }
        }
        public async Task<int> AddRecourceAllocation(ResourceAllocationModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Resources.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (data == null)
                {
                    return -1;
                }

                data.PhysicalLocation = model.PhysicalLocation;

                await AddOrUpdateResourceUserHistory(data.Id, model.EmployeeId, data.Remarks);
                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Id;
            }
        }
        public async Task<IQueryable<Resource>> GetRecourceDetails(ResourceFilterModel filter)
        {
            return _dbContext.Resources.Include(x => x.Branch)
                                            .Include(x => x.ResourceType)
                                            .Where(x => (filter == null ? true : ((string.IsNullOrEmpty(filter.SearchString) ? true :
                                                            (x.SystemName.Contains(filter.SearchString)
                                                            || x.Specification.Contains(filter.SearchString)
                                                            || x.PhysicalLocation.Contains(filter.SearchString)))
                                                    && (filter.Branch != null && filter.Branch.Any() ? filter.Branch.Contains(x.BranchId) : true)
                                                    && (filter.Status != null && filter.Status.Any() ? filter.Status.Contains((int)x.Status) : true)
                                                    && (filter.recordStatus != null && filter.recordStatus.Any() ? filter.recordStatus.Contains((int)x.RecordStatus) : true))))
                                             .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id)
                                             .AsNoTracking().AsSplitQuery();
        }
        public async Task<IQueryable<Resource>> GetRecourceDetail(int id, RecordStatus status = RecordStatus.Active)
        {
            return _dbContext.Resources.Include(x => x.Branch)
                                            .Include(x => x.ResourceType)
                                            .Where(x => x.Id == id && x.RecordStatus == status)
                                            .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id)
                                            .AsNoTracking().AsSplitQuery();
        }
        public async Task<IEnumerable<ResourceUserHistory>> GetResourceUserHistory(int resourceId)
        {
            return await _dbContext.ResourceUserHistories.Include(x => x.Employee)
                                                .Where(x => x.ResourceId == resourceId)
                                                .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id)
                                                .AsNoTracking().AsSplitQuery()
                                                .ToListAsync();
        }
        public async Task<int?> DeleteResources(int Id)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Resources.FirstOrDefaultAsync(x => x.Id == Id);
                if (data == null)
                {
                    return null;
                }

                data.RecordStatus = RecordStatus.InActive;
                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Id;
            }
        }
        private async Task AddOrUpdateResourceUserHistory(int Id, int employeeId, string description)
        {
            var data = new ResourceUserHistory()
            {
                ResourceId = Id,
                LogBy = employeeId,
                Description = description,
                LogDateTime = DateTime.UtcNow,
            };

            await _dbContext.ResourceUserHistories.AddAsync(data);
            await _dbContext.SaveChangesAsync();
        }

    }
}
