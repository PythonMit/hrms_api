using System.Collections.Generic;

namespace HRMS.Core.Models.Holiday
{
    public class HolidatListModel
    {
        public IEnumerable<HolidayModel> HolidayRecords { get; set; }
        public int TotalRecords { get; set; }
    }
}
