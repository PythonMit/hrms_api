using System.Collections.Generic;
using System.ComponentModel;

namespace HRMS.Core.Models.Report
{
    public class ReportFilterModel
    {
        [DefaultValue("")]
        public string SearchString { get; set; } = "";
        [DefaultValue(null)]
        public IEnumerable<int?> Employee { get; set; } = null;
        [DefaultValue(null)]
        public IEnumerable<int?> Branch { get; set; } = null;
        [DefaultValue(null)]
        public IEnumerable<int?> Month { get; set; } = null;
        [DefaultValue(null)]
        public IEnumerable<int?> Year { get; set; } = null;
    }
}
