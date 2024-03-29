using AutoMapper;
using HRMS.Core.Models.Holiday;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public class HolidayService : IHolidayService
    {
        private readonly HolidayStore _holidayStore;
        private readonly IMapper _mapper;
        public HolidayService(HolidayStore HolidayStore, IMapper mapper)
        {
            _holidayStore = HolidayStore;
            _mapper = mapper;
        }

        public async Task<int?> AddOrUpdateEmployeeHoliday(HolidayRequestModel model)
        {
            return await _holidayStore.AddOrUpdateEmployeeHoliday(model);
        }
        public async Task<HolidatListModel> GetEmployeeHolidays(HolidayFilterModel filter)
        {
            var data = await _holidayStore.GetEmployeeHolidays(filter);

            if (data == null)
            {
                return null;
            }

            var totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = data.Count();
                data = data?.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize);
            }

            return new HolidatListModel
            {
                HolidayRecords = _mapper.Map<IEnumerable<HolidayModel>>(data),
                TotalRecords = totalRecords
            };
        }
        public async Task<HolidayModel> GetEmployeeHoliday(int Id)
        {
            var result = await _holidayStore.GetEmployeeHoliday(Id);
            return _mapper.Map<HolidayModel>(result);
        }
        public async Task<bool> DeleteEmployeeHoliday(IEnumerable<int> ids)
        {
            return await _holidayStore.DeleteEmployeeHoliday(ids);
        }
        public async Task<bool> SetEmployeeHolidayStatus(HolidayStatusRequestModel model)
        {
            return await _holidayStore.SetEmployeeHolidayStatus(model);
        }
    }
}
