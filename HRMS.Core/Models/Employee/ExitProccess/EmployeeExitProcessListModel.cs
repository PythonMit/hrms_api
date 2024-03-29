using System.Collections.Generic;

namespace HRMS.Core.Models.Employee.ExitProccess
{
    public class EmployeeExitProcessListModel
    {
        public IEnumerable<EmployeeFNFDetailsModel> Records { get; set; }
        public int TotalRecords { get; set; }
    }
}
