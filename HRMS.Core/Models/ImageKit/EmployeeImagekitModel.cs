using System;

namespace HRMS.Core.Models.ImageKit
{
    public class EmployeeImagekitModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Guid? ImagekitDetailId { get; set; }
        public string ImagekitDetailFileId { get; set; }
        public string FilePath { get; set; }
    }
}
