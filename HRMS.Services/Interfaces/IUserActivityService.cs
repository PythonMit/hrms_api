using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IUserActivityService
    {
        Task<bool> ReadUserActivity(int activityId);
        Task<bool> ReadAllUserActivities(string type);
    }
}
