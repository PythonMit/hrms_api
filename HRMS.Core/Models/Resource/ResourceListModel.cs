using System.Collections.Generic;

namespace HRMS.Core.Models.Resource
{
    public class ResourceListModel
    {
        public IEnumerable<ResourceModel> Records { get; set; }
        public int TotalRecords { get; set; }
    }
}
