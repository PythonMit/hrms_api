using HRMS.Core.Consts;
using HRMS.Core.Models.Employee;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeSecurityInformationService
    {
        Task<SecurityInformationModel> GetEmployeeSecurityInfomation(int id);
        Task<bool> ManageSecurityInformation(SecurityInformationModel model);
        Task<bool> EnableOrDisableEmployeeUser(int id, bool status);
        Task<bool> ActiveOrInactiveEmployee(int id, bool status);
        Task<string> DecryptPassword(int id);
    }
}
