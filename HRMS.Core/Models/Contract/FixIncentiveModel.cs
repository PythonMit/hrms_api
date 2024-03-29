using System;

namespace HRMS.Core.Models.Contract
{
    public class FixIncentiveModel
    {
        public bool IsFixIncentive { get; set; }
        public int FixIncentiveDuration { get; set; }
        public string FixIncentiveRemarks { get; set; }
        public DateTime? FixIncentiveDate { get; set; }
    }
}
