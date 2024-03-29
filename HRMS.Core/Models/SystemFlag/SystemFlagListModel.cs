using System.Collections.Generic;

namespace HRMS.Core.Models.SystemFlag
{
    public class SystemFlagListModel
    {
        public IEnumerable<SystemFlagModel> SystemFlagRecords { get; set; }
        public int TotalRecords { get; set; }
    }
}
