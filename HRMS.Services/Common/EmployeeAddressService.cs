using AutoMapper;
using HRMS.Core.Models.Employee;
using HRMS.DBL.Entities;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public class EmployeeAddressService: IEmployeeAddressService
    {
        private readonly EmployeeAddressStore _employeeAddressStore;
        private readonly IMapper _mapper;
        public EmployeeAddressService(EmployeeAddressStore employeeAddressStore, IMapper mapper)
        {
            _employeeAddressStore = employeeAddressStore;
            _mapper = mapper;
        }
        public async Task<bool> AddAddress(EmployeeAddressModel model)
        {
            var data = _mapper.Map<EmployeeAddress>(model);
            return await _employeeAddressStore.AddEmployeeAddress(data);
        }

        public async Task<bool> UpdateAddress(EmployeeAddressModel model)
        {
            return await _employeeAddressStore.UpdateEmployeeAddress(model);
        }

        public async Task<IEnumerable<EmployeeAddressModel>> GetAddressById(int id)
        {
            var data = await _employeeAddressStore.GetEmployeeAddressById(id);
            return _mapper.Map<IEnumerable<EmployeeAddressModel>>(data);
        }
        public async Task<bool> DeleteAddress(int id)
        {
            return await _employeeAddressStore.DeleteEmployeeAddress(id);
        }
    }
}

