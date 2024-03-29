using System;

namespace HRMS.DBL.Entities
{
    public class ImagekitDetail : TrackableEntity
    {
        public Guid Id { get; set; }
        public string FileId { get; set; }
        public bool IsPrivateFile { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}