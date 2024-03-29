using HRMS.DBL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.Core.Models.User;

namespace HRMS.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> AddUser(UserDetailModel user);
        //Task<UserDetail> AddUserDetails(UserDetailModel userDetailsModel);
        Task<List<User>> GetUsersByIds(IEnumerable<int> ids);
        Task<int> GetUserIdByUserEmail(string userEmail);
        Task<bool> CheckUserByUserEmail(string email);
        Task<bool> HasUser(string userName);
        Task UpdatePassword(string resetPasswordCode, string newPassword);
    }
}
