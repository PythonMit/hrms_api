using HRMS.Core.Models.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeAddressService
    {
        Task<bool> AddAddress(EmployeeAddressModel model);
        Task<bool> UpdateAddress(EmployeeAddressModel model);
        Task<IEnumerable<EmployeeAddressModel>> GetAddressById(int id);
        Task<bool> DeleteAddress(int id);
    }
}

