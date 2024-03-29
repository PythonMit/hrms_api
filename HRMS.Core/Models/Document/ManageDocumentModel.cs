using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Document
{
    public class ManageDocumentModel
    {
        [Required]
        public IFormFile FormFile { get; set; }
        [Required]
        public string FolderPath { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int DocumenTypeId { get; set; }
        public bool PrivateFile { get; set; } = false;
    }
}
