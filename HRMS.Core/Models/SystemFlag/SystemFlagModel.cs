using HRMS.Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.SystemFlag
{
    public class SystemFlagModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}

