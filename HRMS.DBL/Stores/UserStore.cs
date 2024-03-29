using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.DBL.DbContextConfiguration;
using HRMS.DBL.Entities;
using System.Transactions;
using HRMS.Core.Consts;
using HRMS.Core.Utilities.Cipher;
using HRMS.Core.Models.User;

namespace HRMS.DBL.Stores
{
    public class UserStore : BaseStore
    {
        public UserStore(HRMSDbContext dbContext) : base(dbContext) { }

        public async Task<int> AddUser(User user)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var inserted = await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return inserted.Entity.Id;
            }
        }
        public async Task AddEmployeeDetails(Employee employee)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _dbContext.Employees.Add(employee);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
            }
        }
        public async Task UpdatePasswordAsync(int userId, string passwordHash, byte[] passwordSalt)
        {
            var currentUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (currentUser != null)
            {
                currentUser.Password = passwordHash;
                currentUser.Salt = passwordSalt;
                _dbContext.Users.Update(currentUser);
                await _dbContext.SaveChangesAsync();
            }
        }
        public Task<User> GetUserByEmail(string email)
        {
            var data = _dbContext.Users
                .Include(x => x.Role)
                .Include(x => x.Employee).ThenInclude(x => x.ImagekitDetail)
                .FirstOrDefaultAsync(u => u.Emailaddress == email);
            return data;
        }
        public Task<User> GetUserByUsername(string username)
        {
            var data = _dbContext.Users
                .Include(x => x.Role)
                .Include(x => x.Employee).ThenInclude(x => x.ImagekitDetail)
                .FirstOrDefaultAsync(u => u.Username == username);
            return data;
        }
        public Task<User> GetUserById(int userId)
        {
            return _dbContext.Users
                 .Include(x => x.Role)
                .Include(x => x.Employee).ThenInclude(x => x.ImagekitDetail)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public Task<User> GetUserByEmployeeId(int employeeId)
        {
            return _dbContext.Users
                 .Include(x => x.Role)
                .Include(x => x.Employee).ThenInclude(x => x.ImagekitDetail)
                .FirstOrDefaultAsync(u => u.Employee.Id == employeeId);
        }
        public async Task<List<User>> GetByIds(IEnumerable<int> ids)
        {
            return await _dbContext.Users
                .Include(x => x.Employee).ThenInclude(x => x.ImagekitDetail)
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }
        public async Task<int> GetUserByEmailAddress(string email)
        {
            return await _dbContext.Users.Where(x => x.Emailaddress == email).Select(x => x.Id).FirstOrDefaultAsync();
        }
        public async Task<bool> HasUser(string email)
        {
            return await _dbContext.Users.AnyAsync(x => x.Emailaddress == email);
        }
        public async Task<bool> UpdateBasicInformation(UserModel model)
        {
            var user = await GetUserById(model.Id);
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (user != null)
                {
                    user.Emailaddress = model.Emailaddress;
                    user.Username = model.Username;
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return true;
                }
                return false;
            }
        }
        public async Task<bool> UpdateSercurityInformation(UserModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = await GetUserById(model.Id);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(model.Password?.Trim()))
                    {
                        var (hash, salt) = CipherUtils.GenerateHash(model.Password);
                        user.Password = hash;
                        user.Salt = salt;
                        user.CipherText = CipherUtils.EncryptString(AuthOptions.HashKey, model.Password);
                    }

                    if (model.RoleId.HasValue)
                    {
                        user.RoleId = model.RoleId;
                    }

                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return true;
                }
                return false;
            }
        }
        public async Task<DateTime?> GetJoinDateByEmployeeId(int? employeeId)
        {
            return await _dbContext.EmployeeDetails.Where(u => u.Employee.Id == employeeId).Select(x => x.JoinDate).FirstOrDefaultAsync();
        }
    }
}
