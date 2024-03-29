using HRMS.Core.Models.Employee;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeContactInformationService
    {
        Task<EmployeeContactInformationModel>GetEmployeeContactInformation(int id);
        Task<int> ManageContactInformation(EmployeeContactInformationModel model);

    }
}
