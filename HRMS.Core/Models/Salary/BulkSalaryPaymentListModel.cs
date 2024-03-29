using System;
using System.Collections.Generic;

namespace HRMS.Core.Models.Salary
{
    public class BulkSalaryPaymentListModel
    {
        public string EmployeeCode { get; set; }
        public DateTime? JoinDate { get; set; }
        public BulkSalaryPaymentModel BulkSalary { get; set; }
    }
}
