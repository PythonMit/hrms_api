using HRMS.Core.Consts;
using HRMS.Core.Models.Holiday;
using HRMS.DBL.DbContextConfiguration;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace HRMS.DBL.Stores
{
    public class HolidayStore : BaseStore
    {
        public HolidayStore(HRMSDbContext dbContext) : base(dbContext) { }

        public async Task<int?> AddOrUpdateEmployeeHoliday(HolidayRequestModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Holidays.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (data == null)
                {
                    data = new Holiday();
                }

                data.StartDate = model.StartDate;
                data.EndDate = model.EndDate;
                data.Event = model.Event;
                data.Description = model.Description;

                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Id;
            }
        }
        public async Task<IEnumerable<Holiday>> GetEmployeeHolidays(HolidayFilterModel filter)
        {
            return await _dbContext.Holidays.Where(x => x.RecordStatus == RecordStatus.Active
                                                        && (filter == null ? true :
                                                            (string.IsNullOrEmpty(filter.SearchString) ? true : 
                                                                (x.Event.Contains(filter.SearchString) || x.Description.Contains(filter.SearchString)))
                                                                && (filter.Status != null && filter.Status.Any() ? filter.Status.Contains((int)x.RecordStatus) : true)
                                                                && (filter.EndYear.HasValue && filter.StartYear.HasValue ? x.StartDate.Year >= filter.StartYear && x.EndDate.Year <= filter.EndYear : true)))
                                                    .AsSplitQuery().AsNoTracking().ToListAsync();
        }
        public async Task<Holiday> GetEmployeeHoliday(int Id)
        {
            return await _dbContext.Holidays.AsSplitQuery().AsNoTracking().FirstOrDefaultAsync(x => x.RecordStatus == RecordStatus.Active && x.Id == Id);
        }
        public async Task<bool> DeleteEmployeeHoliday(IEnumerable<int> ids)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Holidays.Where(x => ids.Contains(x.Id)).ToListAsync();
                if (data != null && data.Any())
                {
                    foreach (var item in data)
                    {
                        item.RecordStatus = RecordStatus.InActive;
                        _dbContext.Entry(item).State = EntityState.Modified;
                    }
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return true;
                }
                return false;
            }
        }
        public async Task<bool> SetEmployeeHolidayStatus(HolidayStatusRequestModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Holidays.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (data != null)
                {
                    data.RecordStatus = (model.Status ? RecordStatus.Active : RecordStatus.InActive);
                    _dbContext.Entry(data).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return true;
                }
                return false;
            }
        }
        public async Task<IEnumerable<Holiday>> GetEmployeeHolidays(int? month = null, int? year = null)
        {
            return await _dbContext.Holidays.AsSplitQuery().AsNoTracking().Where(x => x.RecordStatus == RecordStatus.Active
                                                                            && (month.HasValue && year.HasValue ? x.StartDate.Month == month && x.StartDate.Year == year : x.StartDate.Month == DateTime.UtcNow.Month && x.StartDate.Year == DateTime.UtcNow.Year)).ToListAsync();
        }
        public async Task<Holiday> GetEmployeeHolidays(DateTime? givenDate)
        {
            return await _dbContext.Holidays.AsSplitQuery().AsNoTracking().Where(x => x.RecordStatus == RecordStatus.Active && (x.StartDate >= givenDate && x.EndDate <= givenDate)).FirstOrDefaultAsync();
        }
        public async Task<bool> HasHolidayWithinLeave(DateTime? givenDate)
        {
            return await _dbContext.Holidays.AsSplitQuery().AsNoTracking().AnyAsync(x => x.RecordStatus == RecordStatus.Active && (x.StartDate >= givenDate && x.EndDate <= givenDate));
        }
    }
}
