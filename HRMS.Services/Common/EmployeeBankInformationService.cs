using AutoMapper;
using HRMS.Core.Models.Employee;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public class EmployeeBankInformationService : IEmployeeBankInformationService
    {
        private readonly EmployeeBankInformationStore _employeeBankInformationStore;
        private readonly IMapper _mapper;

        public EmployeeBankInformationService(EmployeeBankInformationStore employeeContactInformationStore, IMapper mapper)
        {
            _employeeBankInformationStore = employeeContactInformationStore;
            _mapper = mapper;
        }
        public async Task<EmployeeBankInformationModel> GetEmployeeBankInformation(string employeeCode)
        {
            var data = await _employeeBankInformationStore.GetEmployeeBankInfomation(employeeCode);
            return _mapper.Map<EmployeeBankInformationModel>(data);
        }
        public async Task<int> ManageBankInformation(EmployeeBankInformationModel model)
        {
            return await _employeeBankInformationStore.AddorUpdateBankInformation(model);
        }
    }
}
