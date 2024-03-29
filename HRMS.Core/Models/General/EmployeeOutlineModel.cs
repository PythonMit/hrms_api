using System;
using System.ComponentModel;

namespace HRMS.Core.Models.General
{
    public class EmployeeOutlineModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        [DefaultValue(null)]
        public string Designation { get; set; }
        [DefaultValue(null)]
        public string Branch { get; set; }
        [DefaultValue(null)]
        public string BranchCode { get; set; }
        [DefaultValue(null)]
        public string Gender { get; set; }
        [DefaultValue(null)]
        public DateTime? JoinDate { get; set; }
        [DefaultValue(null)]
        public string ProfileUrl { get; set; }
    }
}
