using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.DBL.Entities
{
    public class EmployeeLeaveApplicationComment : RecordStatusEntity
    {
        public Guid Id { get; set; }
        public Guid EmployeeLeaveApplicationId { get; set; }
        public string Comments { get; set; }
        [ForeignKey("Employee")]
        public int? CommentBy { get; set; }
        public DateTime CommentDate { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual EmployeeLeaveApplication EmployeeLeaveApplication { get; set; }
    }
}
