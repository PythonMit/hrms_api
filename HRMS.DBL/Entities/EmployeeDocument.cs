using System;

namespace HRMS.DBL.Entities
{
    public class EmployeeDocument : TrackableEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentFront { get; set; }
        public string DocumentBack { get; set; }
        public Guid? ImagekitDetailId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual ImagekitDetail ImagekitDetail { get; set; }
    }
}
