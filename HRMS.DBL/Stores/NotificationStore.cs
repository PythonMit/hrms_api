using HRMS.DBL.DbContextConfiguration;
using HRMS.DBL.Entities;
using System.Threading.Tasks;
using System.Transactions;
using System;
using HRMS.Core.Models.Notification;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRMS.DBL.Stores
{
    public class NotificationStore : BaseStore
    {
        public NotificationStore(HRMSDbContext dbContext) : base(dbContext) { }

        public async Task<Guid?> AddNotifications(NotificationModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Notification data = new Notification();
                data.Id = model.Id.Value;
                data.Title = model.Title;
                data.Description = model.Description;
                data.Type = model.Type;
                data.HasRead = model.HasRead;
                data.EmployeeId = model.EmployeeId;

                _dbContext.Notifications.Add(data);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Id;
            }
        }
        public async Task<IEnumerable<Guid?>> AddNotifications(IEnumerable<NotificationModel> model)
        {
            var result = new List<Guid?>();
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (var item in model)
                {
                    Notification data = new Notification();
                    data.Id = item.Id.Value;
                    data.Title = item.Title;
                    data.Description = item.Description;
                    data.Type = item.Type;
                    data.HasRead = item.HasRead;
                    data.EmployeeId = item.EmployeeId;

                    _dbContext.Notifications.Add(data);
                    await _dbContext.SaveChangesAsync();
                    result.Add(item.Id);
                }
                transaction.Complete();
                return result;
            }
        }
        public async Task<bool> SetNotificationStatus(IEnumerable<Guid> Ids, bool readAll = false)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Notifications.Where(x => (readAll ? !x.HasRead : Ids.Contains(x.Id))).ToListAsync();
                if (data != null && data.Any())
                {
                    foreach (var item in data)
                    {
                        item.HasRead = true;

                        _dbContext.Entry(item).State = EntityState.Modified;
                        await _dbContext.SaveChangesAsync();
                    }
                    transaction.Complete();
                }
            }
            return false;
        }
        public async Task<IQueryable<Notification>> GetNotifications(bool? hasRead, int? employeeId)
        {
            return _dbContext.Notifications.Where(x => (hasRead.HasValue ? x.HasRead == hasRead : true) && (!employeeId.HasValue ? true : x.EmployeeId == employeeId));
        }
    }
}