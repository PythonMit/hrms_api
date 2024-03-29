﻿namespace HRMS.DBL.Entities
{
    public class ProjectManager
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public virtual Project Project { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
