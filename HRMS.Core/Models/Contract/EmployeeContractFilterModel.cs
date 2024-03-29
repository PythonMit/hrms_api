using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using HRMS.Core.Consts;
using HRMS.Core.Models.General;

namespace HRMS.Core.Models.Contract
{
    public class EmployeeContractFilterModel
    {
        [DefaultValue("")]
        public string SearchString { get; set; }
        [DefaultValue(null)]
        public int? StartYear {get; set;}
        [DefaultValue(null)]
        public int? EndYear { get; set; }
        [DefaultValue(null)]
        public IEnumerable<int> Status { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
