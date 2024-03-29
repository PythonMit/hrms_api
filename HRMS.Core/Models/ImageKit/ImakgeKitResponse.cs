using System;

namespace HRMS.Core.Models.ImageKit
{

    public class ImakgeKitResponse
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FileId { get; set; }
        public object Tags { get; set; }
        public object AITags { get; set; }
        public Versioninfo VersionInfo { get; set; }
        public Embeddedmetadata EmbeddedMetadata { get; set; }
        public object CustomCoordinates { get; set; }
        public Custommetadata CustomMetadata { get; set; }
        public bool IsPrivateFile { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Size { get; set; }
        public bool HasAlpha { get; set; }
        public string Mime { get; set; }
    }

    public class Versioninfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Embeddedmetadata
    {
        public int XResolution { get; set; }
        public int YResolution { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }

    public class Custommetadata
    {
    }
}