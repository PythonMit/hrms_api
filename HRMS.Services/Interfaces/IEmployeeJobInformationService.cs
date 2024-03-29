using HRMS.Core.Models.Employee;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeJobInformationService
    {
        Task<EmployeeJobInformationModel> GetEmployeeJobInformation(int id);
        Task<int?> AddOrUpdateEmployeeJobInformation(EmployeeJobInformationModel model);
    }
}
