using AutoMapper;
using HRMS.Core.Consts;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.User;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public class EmployeeContactInformationService : IEmployeeContactInformationService
    {
        private readonly EmployeeContactInformationStore _employeeContactInformationStore;
        private readonly IMapper _mapper;
        private readonly UserStore _userStore;
        public EmployeeContactInformationService(EmployeeContactInformationStore employeeContactInformationStore, IMapper mapper, UserStore userStore)
        {
            _employeeContactInformationStore = employeeContactInformationStore;
            _mapper = mapper;
            _userStore = userStore;
        }
        public async Task<EmployeeContactInformationModel> GetEmployeeContactInformation(int id)
        {
            var data = await _employeeContactInformationStore.GetEmployeeContactInfomation(id);
            var result = _mapper.Map<EmployeeContactInformationModel>(data);
            return result;
        }
        public async Task<int> ManageContactInformation(EmployeeContactInformationModel model)
        {
            var result = await _employeeContactInformationStore.AddorUpdateContactInformation(model);
            var user = new UserModel
            {
                RoleId = (int)RoleTypes.Employee,
                Username = (model.WorkEmail.Contains('@') ? model.WorkEmail.Split('@')[0] : ""),
                Emailaddress = model.WorkEmail,
                Id = result,
            };
            await _userStore.UpdateBasicInformation(user);
            return result;
        }
    }
}
