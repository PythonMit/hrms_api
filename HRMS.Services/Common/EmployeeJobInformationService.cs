using AutoMapper;
using HRMS.Core.Models.Employee;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public class EmployeeJobInformationService : IEmployeeJobInformationService
    {
        private readonly EmployeeJobInformationStore _employeeJobInformationStore;
        private readonly IMapper _mapper;
        private readonly UserStore _userStore;
        public EmployeeJobInformationService(EmployeeJobInformationStore employeeJobInformationStore, IMapper mapper, UserStore userStore)
        {
            _employeeJobInformationStore = employeeJobInformationStore;
            _mapper = mapper;
            _userStore = userStore;
        }
        public async Task<EmployeeJobInformationModel> GetEmployeeJobInformation(int id)
        {
            var data = await _employeeJobInformationStore.GetEmployeeJobInfomation(id);
            var result = new EmployeeJobInformationModel
            {
                Id = data.Employee.Id,
                BranchId = data.Employee.BranchId ?? null,
                EmployeeCode = data.Employee.EmployeeCode?? null,
                DesignationTypeId = data.Employee.DesignationTypeId ??null ,
                JoinDate = data.JoinDate ?? (DateTime?)null,
                PreviousEmployeer = data.PreviousEmployeer ?? null,
                Experience = data.Experience ?? 0,
                WorkingFormat = data.WorkingFormat
            };
            return result;
        }
        public async Task<int?> AddOrUpdateEmployeeJobInformation(EmployeeJobInformationModel model)
        {
            var newEmployeeId = await _employeeJobInformationStore.AddorUpdateEmployeeJobInformation(model);
            if (newEmployeeId > 0)
            {
                var employeeDetail = new EmployeeDetailModel();
                employeeDetail.EmployeeId = (int)newEmployeeId;
                employeeDetail.JoinDate = model.JoinDate;
                employeeDetail.PreviousEmployeer = model.PreviousEmployeer;
                employeeDetail.Experience = model.Experience ?? 0;
                employeeDetail.WorkingFormat = model.WorkingFormat;
                var result = await _employeeJobInformationStore.AddorUpdateEmployeeDetailJobInformation(employeeDetail);
                return result;
            }
            return null;
        }
    }
}
