using System.Collections.Generic;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveStatusModel
    {
        public int Id { get; set; }
        public string StatusType { get; set; }
        public string Description { get; set; }
    }
}
