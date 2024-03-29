using HRMS.Core.Models.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeBankInformationService
    {
        Task<EmployeeBankInformationModel> GetEmployeeBankInformation(string employeeCode);
        Task<int> ManageBankInformation(EmployeeBankInformationModel model);
    }
}
