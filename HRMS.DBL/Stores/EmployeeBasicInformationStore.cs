using HRMS.Core.Utilities.Auth;
using HRMS.DBL.DbContextConfiguration;
using System.Threading.Tasks;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using HRMS.Core.Models.Employee;
using HRMS.Core.Consts;
using System.Linq;

namespace HRMS.DBL.Stores
{
    public class EmployeeBasicInformationStore : BaseStore
    {
        private readonly UserStore _userStore;

        public EmployeeBasicInformationStore(HRMSDbContext dbContext, UserStore userStore) : base(dbContext)
        {
            _userStore = userStore;
        }

        public async Task<IQueryable<EmployeeDetail>> GetEmployeeBasicInfomation(int id)
        {
            return _dbContext.EmployeeDetails.Include(x => x.Employee).ThenInclude(x => x.ImagekitDetail).Where(x => x.EmployeeId == id && x.RecordStatus == RecordStatus.Active);
        }
        public async Task<int> AddorUpdateEmployeeBasicInformation(BasicInformationModel model)
        {
            if (string.IsNullOrEmpty(model.FirstName) && string.IsNullOrEmpty(model.LastName) && string.IsNullOrEmpty(model.Gender))
            {
                return -1;
            }

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == model.Id && x.RecordStatus == RecordStatus.Active);
                if (data == null)
                {
                    var userId = await _userStore.AddUser(new User());
                    data = new Employee();
                    data.UserId = userId;
                }

                data.FirstName = model.FirstName;
                data.LastName = model.LastName;
                data.MiddleName = model.MiddleName;
                data.DateOfBirth = model.DateOfBirth;
                data.Gender = model?.Gender;

                if (model.ProfilePhotoId.HasValue)
                {
                    data.ImagekitDetailId = model.ProfilePhotoId;
                }

                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);

                await _dbContext.SaveChangesAsync();
                transaction.Complete();

                return data.Id;
            }
        }
        public async Task<int?> AddorUpdateEmployeeDetailBasicInformation(EmployeeDetailModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeDetails.FirstOrDefaultAsync(x => x.EmployeeId == model.EmployeeId && x.RecordStatus == RecordStatus.Active);
                if (data == null)
                {
                    data = new EmployeeDetail();
                    data.EmployeeId = model.EmployeeId;
                }

                data.InstituteName = model.InstituteName;
                data.AllowEditPersonalDetails = model.AllowEditPersonalDetails;

                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.EmployeeId;
            }
        }

    }
}
