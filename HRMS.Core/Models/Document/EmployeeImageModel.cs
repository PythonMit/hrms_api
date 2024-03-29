using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace HRMS.Core.Models.Document
{
    public class EmployeeImageModel
    {
        [DefaultValue(null)]
        public int? Id { get; set; } = null;
        [DefaultValue("")]
        public string EmployeeCode { get; set; } = string.Empty;
        [DefaultValue(null)]
        public IFormFile FormFile { get; set; } = null;
        [DefaultValue("")]
        public string FolderPath { get; set; } = string.Empty;
        [DefaultValue(false)]
        public bool PublicRead { get; set; } = true;
    }
}
