using System.Collections.Generic;

namespace HRMS.Core.Models.Overtime
{
    public class EmployeeOvertimeListModel
    {
        public IEnumerable<EmployeeOvertimeModel> EmployeeOvertimeRecords { get; set; }
        public int TotalRecords { get; set; }
    }
}
