using HRMS.Core.Utilities.Auth;
using HRMS.DBL.DbContextConfiguration;
using HRMS.DBL.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using HRMS.Core.Models.Employee;
using HRMS.Core.Consts;

namespace HRMS.DBL.Stores
{
    public class EmployeeAddressStore : BaseStore
    {
        public EmployeeAddressStore(HRMSDbContext dbContext) : base(dbContext) { }
        public async Task<bool> AddEmployeeAddress(EmployeeAddress entity)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbContext.EmployeeAddresses.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return true;
            }
        }
        public async Task<bool> UpdateEmployeeAddress(EmployeeAddressModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeAddresses.FirstOrDefaultAsync(x => x.Id == model.Id && x.RecordStatus == RecordStatus.Active);
                if (data == null)
                {
                    return false;
                }

                data.AddressLine1 = model.AddressLine1;
                data.AddressLine2 = model.AddressLine2;
                data.City = model.City;
                data.Country = model.Country;
                data.State = model.State;
                data.Pincode = model.Pincode;
                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return true;
            }
        }
        public async Task<IEnumerable<EmployeeAddress>> GetEmployeeAddressById(int id)
        {
            var employeeDetail = await _dbContext.EmployeeDetails.Include(x => x.Employee).Include(x => x.PermanentAddress).Include(x => x.PresentAddress).FirstOrDefaultAsync(x => x.EmployeeId == id && x.RecordStatus == RecordStatus.Active);
            return await _dbContext.EmployeeAddresses.Where(x => x.Id == employeeDetail.PermanentAddressId || x.Id == employeeDetail.PresentAddressId && x.RecordStatus == RecordStatus.Active).ToListAsync();
        }
        public async Task<bool> DeleteEmployeeAddress(int id)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var employeeDetail = await _dbContext.EmployeeDetails.Include(x => x.Employee).Include(x => x.PermanentAddress).Include(x => x.PresentAddress).FirstOrDefaultAsync(x => x.EmployeeId == id && x.RecordStatus == RecordStatus.Active);
                var data = await _dbContext.EmployeeAddresses.Where(x => x.Id == employeeDetail.PresentAddressId || x.Id == employeeDetail.PermanentAddressId && x.RecordStatus == RecordStatus.Active).ToListAsync();
                if (data == null)
                {
                    return false;
                }

                _dbContext.RemoveRange(data);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return true;
            }
        }
    }
}

