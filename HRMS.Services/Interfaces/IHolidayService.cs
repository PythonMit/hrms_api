using HRMS.Core.Models.Holiday;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IHolidayService
    {
        Task<int?> AddOrUpdateEmployeeHoliday(HolidayRequestModel model);
        Task<HolidatListModel> GetEmployeeHolidays(HolidayFilterModel filter);
        Task<HolidayModel> GetEmployeeHoliday(int Id);
        Task<bool> DeleteEmployeeHoliday(IEnumerable<int> ids);
        Task<bool> SetEmployeeHolidayStatus(HolidayStatusRequestModel model);
    }
}
