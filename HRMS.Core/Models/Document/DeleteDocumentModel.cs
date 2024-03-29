using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Document
{
    public class DeleteDocumentModel
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int DocumentId { get; set; }
        [Required]
        public int DocumentTypeId { get; set; }
    }
}
