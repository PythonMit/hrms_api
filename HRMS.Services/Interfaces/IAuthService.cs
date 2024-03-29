using HRMS.Core.Models.Auth;
using HRMS.Core.Models.User;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserDetailModel> SignUp(SignUpRequestModel model);
        Task<SignInResponseModel> SignIn(SignInModel model);
        Task<bool> ValidUser(SignInModel model);
        Task<bool> GenerateSlackToken(string code, string state = "", string error = "");
    }
}
