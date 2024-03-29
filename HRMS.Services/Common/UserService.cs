using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.DBL.Entities;
using HRMS.DBL.Stores;
using HRMS.Core.Exceptions;
using HRMS.Services.Interfaces;
using HRMS.Core.Models.User;
using HRMS.Core.Utilities.Cipher;
using HRMS.Resources;

namespace HRMS.Services.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserStore _userStore;
        private readonly IAppResourceAccessor _appResourceAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userStore"></param>
        public UserService(IMapper mapper, UserStore userStore, IAppResourceAccessor appResourceAccessor)
        {
            _mapper = mapper;
            _userStore = userStore;
            _appResourceAccessor = appResourceAccessor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> AddUser(UserDetailModel user)
        {
            var userEntity = _mapper.Map<User>(user);
            var userId = await _userStore.AddUser(userEntity);
            return userId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task UpdatePassword(string email, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ApiException(_appResourceAccessor.GetResource("User:NewPasswordRequired"));
            }

            var user = await _userStore.GetUserByEmail(email);

            if (user == null)
            {
                throw new ApiException(_appResourceAccessor.GetResource("User:InvalidPasswordResetRequest"));
            }
            var (newPasswordHash, newPasswordSalt) = CipherUtils.GenerateHash(newPassword);
            await _userStore.UpdatePasswordAsync(user.Id, newPasswordHash, newPasswordSalt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<List<User>> GetUsersByIds(IEnumerable<int> ids)
        {
            return _userStore.GetByIds(ids);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<int> GetUserIdByUserEmail(string email)
        {
            int userId = await _userStore.GetUserByEmailAddress(email);
            if (userId <= 0)
            {
                throw new ApiException(_appResourceAccessor.GetResource("User:InvalidUser"));
            }
            return userId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserByUserEmail(string email)
        {
            int userId = await _userStore.GetUserByEmailAddress(email);
            if (userId > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> HasUser(string email)
        {
            return await _userStore.HasUser(email);
        }
    }
}
