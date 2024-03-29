using HRMS.Core.Consts;
using HRMS.Core.Models.Branch;
using System;

namespace HRMS.Core.Models.Resource
{
    public class ResourceModel
    {
        public int Id { get; set; }
        public ResourceTypes ResourceType { get; set; }
        public BranchModel Branch { get; set; }
        public string Specification { get; set; }
        public string SystemName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PhysicalLocation { get; set; }
        public ResourceStatus Status { get; set; }
        public bool IsFree { get; set; }
        public string Remarks { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
