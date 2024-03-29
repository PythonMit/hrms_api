using HRMS.Core.Models.SystemFlag;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface ISystemFlagService
    {
        Task<int> AddorUpdateSystemFlag(SystemFlagModel model);
        Task<bool> DeleteSystemFlag(int id);
        Task<SystemFlagListModel> GetAllSystemFlags(SystemFlagFilterModel model);
        Task<bool> StatusChange(int id, bool status);
        Task<SystemFlagModel> GetFlagDetailsByName(string flagName);
        Task<bool> CheckFlagExists(string flagName);
        Task<IEnumerable<SystemFlagModel>> GetSystemFlagsByTags(IEnumerable<string> Tag);
        Task<SystemFlagModel> GetSystemFlagsByTag(IEnumerable<string> Tag);
        Task<SystemFlagModel> GetSystemFlagsByTag(string Tag);
    }
}

