using HRMS.Core.Consts;
using HRMS.Core.Models.General;
using System.Collections.Generic;
using System.ComponentModel;

namespace HRMS.Core.Models.Resource
{
    public class ResourceFilterModel
    {
        [DefaultValue("")]
        public string SearchString { get; set; } = "";
        [DefaultValue(null)]
        public IEnumerable<int?> Branch { get; set; } = null;
        [DefaultValue(null)]
        public IEnumerable<int?> Status { get; set; } = null;
        [DefaultValue(null)]
        public IEnumerable<int> recordStatus { get; set; } = null;
        public PaginationModel Pagination { get; set; }
    }
}
