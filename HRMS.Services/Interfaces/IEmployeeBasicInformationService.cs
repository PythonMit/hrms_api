using System;
using System.Threading.Tasks;
using HRMS.Core.Models.Employee;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeBasicInformationService
    {

        Task<BasicInformationModel> GetEmployeeBasicInformation(int id);
        Task<int?> AddorUpdateEmployeeBasicInformation(BasicInformationModel model, Byte[] fileStream, string folderPath, bool publicRead, string fileName);
    }
}
