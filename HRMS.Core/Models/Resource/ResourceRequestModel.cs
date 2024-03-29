using HRMS.Core.Consts;
using System;

namespace HRMS.Core.Models.Resource
{
    public class ResourceRequestModel
    {
        public int Id { get; set; }
        public int ResourceTypeId { get; set; }
        public int BranchId { get; set; }
        public string Specification { get; set; }
        public string SystemName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PhysicalLocation { get; set; }
        public ResourceStatus Status { get; set; }
        public bool IsFree { get; set; }
        public string Remarks { get; set; }
        public int EmployeeId { get; set; }
    }
}
