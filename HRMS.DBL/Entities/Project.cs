using System.Collections.Generic;

namespace HRMS.DBL.Entities
{
    public class Project : TrackableEntity
    {
        public Project()
        {
            ProjectManagers = new List<ProjectManager>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public virtual ICollection<ProjectManager> ProjectManagers { get; set; }
    }
}
