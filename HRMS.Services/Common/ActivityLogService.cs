using HRMS.Core.Models.ActivityLog;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly ActivityLogStore _activityLogStore;

        public ActivityLogService(ActivityLogStore activityLogStore) 
        { 
            _activityLogStore = activityLogStore;
        }

        public async Task<Guid?> AddActivityLogs(ActivityLogModel model)
        {
            return await _activityLogStore.AddActivityLogs(model);
        }
    }
}
