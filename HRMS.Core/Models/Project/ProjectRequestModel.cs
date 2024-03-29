using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Project
{
    public class ProjectRequestModel
    {
        [DefaultValue(null)]
        public int? Id { get; set; } = null;
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [DefaultValue("")]
        public string Type { get; set; } = "";
        [DefaultValue(null)]
        public IEnumerable<int> ProjectManagerIds { get; set; } = null;
    }
}
