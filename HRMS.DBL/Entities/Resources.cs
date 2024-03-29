using HRMS.Core.Consts;
using System;

namespace HRMS.DBL.Entities;
public class Resource : TrackableEntity
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
    public virtual ResourceType ResourceType { get; set; }
    public virtual Branch Branch { get; set; }
}

