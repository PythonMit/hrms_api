using System.Collections.Generic;

namespace HRMS.Core.Models.Employee
{
    public class EmployeeListModel
    {
        public IEnumerable<EmployeeModel> EmployeeRecords { get; set; }
        public int TotalRecords { get; set; }
    }
}
