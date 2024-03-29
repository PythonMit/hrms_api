using System;
using HRMS.Core.Consts;
using HRMS.Core.Models.General;

namespace HRMS.Core.Models.Resource
{
    public class ResourceUserHistoryModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogBy { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
