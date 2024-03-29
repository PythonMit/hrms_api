using System;
using System.ComponentModel;

namespace HRMS.Core.Models.Contract
{
    public class FixGrossRequestModel
    {
        public int Id { get; set; }
        [DefaultValue(0)]
        public decimal CostToCompany { get; set; } = 0;
        [DefaultValue(0)]
        public double StipendAmount { get; set; } = 0;
        [DefaultValue(false)]
        public bool IsFixIncentive { get; set; }
        [DefaultValue(0)]
        public int FixIncentiveDuration { get; set; }
        public string FixIncentiveRemarks { get; set; }
        [DefaultValue(null)]
        public DateTime? FixIncentiveDate { get; set; } = null;
    }
}
