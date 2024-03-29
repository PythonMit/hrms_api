using HRMS.Core.Models.Employee;
using HRMS.Core.Models.User;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public class EmployeeSecurityInformationService : IEmployeeSecurityInformationService
    {
        private readonly EmployeeSecurityInformationStore _employeeSecurityInformationStore;
        public EmployeeSecurityInformationService(EmployeeSecurityInformationStore employeeSecurityInformationStore)
        {
            _employeeSecurityInformationStore = employeeSecurityInformationStore;
        }
        public async Task<SecurityInformationModel> GetEmployeeSecurityInfomation(int id)
        {
            return await _employeeSecurityInformationStore.GetEmployeeSecurityInfomation(id);
        }
        public async Task<bool> ManageSecurityInformation(SecurityInformationModel model)
        {
            var userId = await _employeeSecurityInformationStore.GetUserIdByEmployeeCode(model.EmployeeCode);
            if (!userId.HasValue)
            {
                return false;
            }

            var user = new UserModel()
            {
                Id = userId.Value,
                Password = model.Password,
                RoleId = model.RoleId,
            };

            return await _employeeSecurityInformationStore.ManageSecurityInformation(user);
        }
        public async Task<bool> ActiveOrInactiveEmployee(int id, bool status)
        {
            return await _employeeSecurityInformationStore.ActiveOrInactiveEmployee(id, status);
        }
        public async Task<string> DecryptPassword(int id)
        {
            return await _employeeSecurityInformationStore.DecryptPassword(id);
        }
        public async Task<bool> EnableOrDisableEmployeeUser(int id, bool status)
        {
            return await _employeeSecurityInformationStore.EnableOrDisableEmployeeUser(id, status);
        }
    }
}
