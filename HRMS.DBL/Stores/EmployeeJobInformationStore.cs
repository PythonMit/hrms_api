using HRMS.Core.Utilities.Auth;
using HRMS.DBL.DbContextConfiguration;
using System.Threading.Tasks;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using HRMS.Core.Consts;
using System.Transactions;
using HRMS.Core.Models.Employee;

namespace HRMS.DBL.Stores
{
    public class EmployeeJobInformationStore : BaseStore
    {
        public EmployeeJobInformationStore(HRMSDbContext dbContext) : base(dbContext) { }
        public async Task<EmployeeDetail> GetEmployeeJobInfomation(int id)
        {
            return await _dbContext.EmployeeDetails.Include(x => x.Employee).FirstOrDefaultAsync(x => x.EmployeeId == id && x.RecordStatus == RecordStatus.Active);
        }
        public async Task<int?> AddorUpdateEmployeeJobInformation(EmployeeJobInformationModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == model.Id && x.RecordStatus == RecordStatus.Active);
                if (data == null)
                {
                    data = new Employee();
                }
                data.BranchId = model.BranchId;
                data.EmployeeCode = model.EmployeeCode;
                data.DesignationTypeId = model.DesignationTypeId;
                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Id;
            }
        }
        public async Task<int?> AddorUpdateEmployeeDetailJobInformation(EmployeeDetailModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeDetails.FirstOrDefaultAsync(x => x.EmployeeId == model.EmployeeId && x.RecordStatus == RecordStatus.Active);
                if (data == null)
                {
                    data = new EmployeeDetail();
                    data.EmployeeId = model.EmployeeId;
                }
                data.JoinDate = model.JoinDate;
                data.PreviousEmployeer = model.PreviousEmployeer;
                data.Experience = model.Experience;
                data.WorkingFormat = model.WorkingFormat;
                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.EmployeeId;
            }
        }
    }
}
