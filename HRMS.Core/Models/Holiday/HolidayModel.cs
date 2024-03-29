using HRMS.Core.Consts;
using System;

namespace HRMS.Core.Models.Holiday
{
    public class HolidayModel 
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Event { get; set; }
        public string Description { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
