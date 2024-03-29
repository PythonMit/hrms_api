using System.IO;

namespace HRMS.Services.Models
{
    public class SaveDocumentModel
    {
        public string OriginalFileName { get; set; }
        public Stream FileStream { get; set; }
        public string ContentType { get; set; }
        public string FileFolder { get; set; }
        public string UserId { get; set; }
        public bool publicRead { get; set; }
    }
}
