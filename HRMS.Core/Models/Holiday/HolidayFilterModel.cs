using HRMS.Core.Models.General;
using System.Collections.Generic;
using System.ComponentModel;

namespace HRMS.Core.Models.Holiday
{
    public class HolidayFilterModel
    {
        [DefaultValue("")]
        public string SearchString { get; set; }
        [DefaultValue(null)]
        public IEnumerable<int?> Status { get; set; } = null;   
        [DefaultValue(null)]
        public int? StartYear { get; set; }
        [DefaultValue(null)]
        public int? EndYear { get; set; }
        public PaginationModel  Pagination { get; set; }
    }
}
