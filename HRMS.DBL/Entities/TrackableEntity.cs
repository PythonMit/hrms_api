using HRMS.Core.Consts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.DBL.Entities
{
    public class RecordStatusEntity
    {
        public RecordStatus RecordStatus { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public DateTime? UpdatedDateTimeUtc { get; set; }
    }
    public class TrackableEntity : RecordStatusEntity
    {
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }

        [NotMapped]
        public virtual User CreatedByUser { get; set; }

        [NotMapped]
        public virtual User UpdatedByUser { get; set; }
    }
}
