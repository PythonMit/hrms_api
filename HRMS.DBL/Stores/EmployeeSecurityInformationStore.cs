using HRMS.Core.Utilities.Auth;
using HRMS.DBL.DbContextConfiguration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HRMS.Core.Consts;
using System.Transactions;
using HRMS.Core.Models.User;
using HRMS.Core.Models.Employee;
using HRMS.Core.Utilities.Cipher;

namespace HRMS.DBL.Stores
{
    public class EmployeeSecurityInformationStore : BaseStore
    {
        private readonly UserStore _userStore;

        public EmployeeSecurityInformationStore(HRMSDbContext dbContext, UserStore userStore) : base(dbContext)
        {
            _userStore = userStore;
        }
        public async Task<bool> ManageSecurityInformation(UserModel model)
        {

            var data = await _userStore.UpdateSercurityInformation(model);
            return data;
        }
        public async Task<SecurityInformationModel> GetEmployeeSecurityInfomation(int id)
        {
            var result = await _dbContext.Employees.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);

            if (result?.User == null)
            {
                return null;
            }

            return new SecurityInformationModel
            {
                Id = result.Id,
                Username = result.User.Username ?? "",
                RoleId = result.User.RoleId ?? null,
                EmployeeCode = result.EmployeeCode,
                RecordStatus = result.RecordStatus,
                HasPassword = !string.IsNullOrEmpty(result.User.Password),
                UserStatus = (result.User == null ? false : result.User.Disabled),
                SlackUserId = result.SlackUserId
            };
        }
        public async Task<bool> ActiveOrInactiveEmployee(int id, bool status)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task<string> DecryptPassword(int id)
        {
            var result = await _dbContext.Employees.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id && x.RecordStatus == RecordStatus.Active);
            if (result.User == null)
            {
                return null;
            }
            return CipherUtils.DecryptString(AuthOptions.HashKey, result.User.CipherText);
        }
        public async Task<int?> GetUserIdByEmployeeCode(string employeeCode)
        {
            return await _dbContext.Employees.Include(x => x.User).Where(x => x.EmployeeCode == employeeCode && x.RecordStatus == RecordStatus.Active).Select(x => x.UserId).FirstOrDefaultAsync();
        }
        public async Task<bool> EnableOrDisableEmployeeUser(int id, bool status)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
                if (data == null)
                {
                    return false;
                }
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == data.UserId);
                if (user == null)
                {
                    return false;
                }

                user.Disabled = status;
                _dbContext.Entry(user).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return true;
            }
        }
    }
}

