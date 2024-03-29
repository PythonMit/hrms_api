using HRMS.DBL.Entities;
using Microsoft.Extensions.Configuration;
using HRMS.Core.Consts;
using System;
using System.Linq;
using System.Threading.Tasks;
using HRMS.DBL.DbContextConfiguration;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using HRMS.Core.Utilities.Cipher;

namespace HRMS.DBL.Configuration
{
    public static class DataSeeder
    {
        public static async Task SeedGlobalSuperAdminData(HRMSDbContext dbContext, IConfiguration configuration)
        {
            var emailAddress = configuration["GlobalSuperAdmin:EmailAddress"];
            var userName = emailAddress.Split("@").FirstOrDefault();
            var password = configuration["GlobalSuperAdmin:Password"];
            var role =  configuration["GlobalSuperAdmin:Role"];

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var isAdmin = dbContext.Users.Any(x => x.IsAdmin && x.Emailaddress == emailAddress);
                if (!isAdmin)
                {
                    var (hash, salt) = CipherUtils.GenerateHash(password);
                    var user = new User
                    {
                        Username = userName,
                        Emailaddress = emailAddress,
                        Password = hash,
                        Salt = salt,
                        RecordStatus = RecordStatus.Active,
                        IsAdmin = true,
                        RoleId =  (int)(RoleTypes)Enum.Parse(typeof(RoleTypes), role),
                        CreatedDateTimeUtc = DateTime.Now.ToUniversalTime()
                    };
                    var result = await dbContext.AddAsync(user);
                    await dbContext.SaveChangesAsync();

                    var employee = new Employee
                    {
                        UserId = result.Entity.Id,
                        FirstName = "gsa",
                        LastName = "",
                        EmployeeCode = "GSA",
                        BranchId = Convert.ToInt32(BranchCode.ST),
                        DateOfBirth = DateTime.UtcNow,
                        Email = emailAddress,
                        CreatedDateTimeUtc = DateTime.Now.ToUniversalTime()
                    };
                    await dbContext.AddAsync(employee);
                }

                await dbContext.SaveChangesAsync();

                transaction.Complete();
            }
        }

        public static async Task SeedDemoEmployeeData(HRMSDbContext dbContext, IConfiguration configuration)
        {
            var emailAddress = configuration["DemoUser:EmailAddress"];
            var userName = emailAddress.Split("@").FirstOrDefault();
            var password = configuration["DemoUser:Password"];
            var role = configuration["DemoUser:Role"];
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var user = await dbContext.Users.FirstOrDefaultAsync(x=>x.Emailaddress==emailAddress);
                if (user == null)
                {
                    user = new User();
                }
                var (hash, salt) = CipherUtils.GenerateHash(password);
                user.Username = userName;
                user.Emailaddress = emailAddress;
                user.Password = hash;
                user.Salt = salt;
                user.RecordStatus = RecordStatus.Active;
                user.IsAdmin = true;
                user.RoleId = (int)(RoleTypes)Enum.Parse(typeof(RoleTypes), role);
                user.CreatedDateTimeUtc = DateTime.Now.ToUniversalTime();
                dbContext.Entry(user).State = (user.Id == 0 ? EntityState.Added:EntityState.Modified);
                await dbContext.SaveChangesAsync();
                var result=user.Id;
                var employeeResult= dbContext.Employees.FirstOrDefault(x=>x.UserId==result);
                if (employeeResult == null)
                {
                    var employee = new Employee
                    {
                        UserId = result,
                        FirstName = "demo",
                        LastName = "employee",
                        EmployeeCode = "DEMO",
                        BranchId = Convert.ToInt32(BranchCode.ST),
                        DateOfBirth = DateTime.UtcNow,
                        Email = emailAddress,
                        CreatedDateTimeUtc = DateTime.UtcNow.ToUniversalTime()
                    };
                    await dbContext.AddAsync(employee);
                    await dbContext.SaveChangesAsync();
                }
                transaction.Complete();
            }
        }

        public static async Task SeedAdminUserData(HRMSDbContext dbContext, IConfiguration configuration)
        {
            var emailAddress = configuration["AdminUser:EmailAddress"];
            var userName = emailAddress.Split("@").FirstOrDefault();
            var password = configuration["AdminUser:Password"];
            var role = configuration["AdminUser:Role"];

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var isAdmin = dbContext.Users.Any(x => x.IsAdmin && x.Emailaddress == emailAddress);
                if (!isAdmin)
                {
                    var (hash, salt) = CipherUtils.GenerateHash(password);
                    var user = new User
                    {
                        Username = userName,
                        Emailaddress = emailAddress,
                        Password = hash,
                        Salt = salt,
                        RecordStatus = RecordStatus.Active,
                        IsAdmin = true,
                        RoleId = (int)(RoleTypes)Enum.Parse(typeof(RoleTypes), role),
                        CreatedDateTimeUtc = DateTime.Now.ToUniversalTime()
                    };
                    var result = await dbContext.AddAsync(user);
                    await dbContext.SaveChangesAsync();

                    var employee = new Employee
                    {
                        UserId = result.Entity.Id,
                        FirstName = "admin",
                        LastName = "",
                        EmployeeCode = "ADMIN",
                        BranchId = Convert.ToInt32(BranchCode.ST),
                        DateOfBirth = DateTime.UtcNow,
                        Email = emailAddress,
                        CreatedDateTimeUtc = DateTime.Now.ToUniversalTime()
                    };
                    await dbContext.AddAsync(employee);
                }

                await dbContext.SaveChangesAsync();

                transaction.Complete();
            }
        }
    }
}
