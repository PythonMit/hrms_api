using System;

namespace HRMS.Core.Models.Document
{
    public class EmployeeDocumentModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DocumentTypeId { get; set; }
        public byte[] FileStream { get; set; }
        public string ContentType { get; set; }
        public Guid? ImagekitDetailId { get; set; }
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string FolderPath { get; set; }
        public bool IsPrivateFile { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public string FileType { get; set; }
    }
}
