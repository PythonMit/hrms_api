using HRMS.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.ImageKit
{
    public class ImagekitDetailModel
    {
        public Guid Id { get; set; }
        public string FileId { get; set; }
        public bool IsPrivateFile { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
