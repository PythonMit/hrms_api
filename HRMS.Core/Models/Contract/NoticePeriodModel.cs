using System;

namespace HRMS.Core.Models.Contract
{
    public class NoticePeriodModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Remarks { get; set; }
    }
}
