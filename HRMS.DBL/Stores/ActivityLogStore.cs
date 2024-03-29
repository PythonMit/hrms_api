using HRMS.Core.Models.ActivityLog;
using HRMS.DBL.DbContextConfiguration;
using HRMS.DBL.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace HRMS.DBL.Stores
{
    public class ActivityLogStore : BaseStore
    {
        public ActivityLogStore(HRMSDbContext dbContext) : base(dbContext) { }

        public async Task<Guid?> AddActivityLogs(ActivityLogModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                ActivityLog data = new ActivityLog();
                data.Id = model.Id.Value;
                data.ActivityJson = model.ActivityJson;
                data.EventLocation = model.EventLocation;
                data.EventType = model.EventType;
                data.IPAddress = model.IPAddress;

                _dbContext.ActivityLogs.Add(data);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Id;
            }
        }
    }
}
