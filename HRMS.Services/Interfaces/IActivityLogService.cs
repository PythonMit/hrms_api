using HRMS.Core.Models.ActivityLog;
using System;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IActivityLogService
    {
        Task<Guid?> AddActivityLogs(ActivityLogModel model);
    }
}
