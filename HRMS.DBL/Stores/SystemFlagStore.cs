using HRMS.Core.Consts;
using HRMS.Core.Models.SystemFlag;
using HRMS.DBL.DbContextConfiguration;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace HRMS.DBL.Stores
{
    /// <summary>
    /// SystemFlagStore class is used for to communicate with database.
    /// </summary>
    public class SystemFlagStore : BaseStore
    {
        public SystemFlagStore(HRMSDbContext dbContext) : base(dbContext) { }

        public async Task<int> AddorUpdateSystemFlag(SystemFlagModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.SystemFlags.FirstOrDefaultAsync(x => x.Id == (model.Id ));
                if (data == null)
                {
                    data = new SystemFlag();
                }
                data.Name = model.Name;
                data.Value = model.Value;
                data.Description = model.Description;
                data.RecordStatus = model.RecordStatus;
                data.Tags = model.Tags;
                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Id;
            }
        }
        public async Task<bool> DeleteSystemFlag(int id)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.SystemFlags.FirstOrDefaultAsync(x => x.Id == id);
                _dbContext.Remove(data);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return true;
            }
        }
        public async Task<bool> StatusChange(int id, bool status)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.SystemFlags.FirstOrDefaultAsync(x => x.Id == id);
                if (data == null)
                {
                    return false;
                }
                data.RecordStatus = (status ? RecordStatus.Active : RecordStatus.InActive);
                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return true;
            }
        }
        public async Task<IEnumerable<SystemFlag>> GetAllSystemFlags(SystemFlagFilterModel filter)
        {
            return await _dbContext.SystemFlags.Where(x => (filter == null ? true :
                                                            ((string.IsNullOrEmpty(filter.SearchString) ? true : (x.Name.Contains(filter.SearchString) || x.Value.Contains(filter.SearchString)
                                                                                                        || x.Tags.Contains(filter.SearchString) || x.Description.Contains(filter.SearchString))))))
                                                .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id).ToListAsync();
        }
        public async Task<SystemFlag> GetFlagDetailsByName(string flagName)
        {
            return await _dbContext.SystemFlags.OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id).FirstOrDefaultAsync(x => x.Name == flagName);
        }
        public async Task<bool> CheckFlagExists(string flagName)
        {
            return await _dbContext.SystemFlags.AnyAsync(x => x.Name == flagName);
        }
        public async Task<IEnumerable<SystemFlag>> GetSystemFlagsByTags(IEnumerable<string> tags)
        {
            return await _dbContext.SystemFlags.OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id).Where(x => x.RecordStatus == RecordStatus.Active && ((tags != null && tags.Count() > 0) ? tags.Contains(x.Tags.ToLower()) : true)).ToListAsync();
        }
        public async Task<SystemFlag> GetSystemFlagsByTag(string tag)
        {
            return await _dbContext.SystemFlags.OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id).FirstOrDefaultAsync(x => x.RecordStatus == RecordStatus.Active && x.Tags.ToLower() == tag.ToLower());
        }
    }
}

