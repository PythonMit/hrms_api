using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.DBL.Entities;
public class ResourceUserHistory : TrackableEntity
{
    public Guid Id { get; set; }
    public int ResourceId { get; set; }
    public string Description { get; set; }
    public DateTime LogDateTime { get; set; }
    [ForeignKey("Employee")]
    public int LogBy { get; set; }
    public virtual Resource Resource { get; set; }
    public virtual Employee Employee { get; set; }
}