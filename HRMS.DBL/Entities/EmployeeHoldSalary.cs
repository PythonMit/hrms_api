using HRMS.Core.Consts;
using System;

namespace HRMS.DBL.Entities
{
    public class EmployeeHoldSalary : TrackableEntity
    {
        public Guid Id { get; set; }
        public int EmployeeEarningGrossId { get; set; }
        public double? HoldAmount { get; set; }
        public int EmployeeSalaryStatusId { get; set; }
        public string Remarks { get; set; }
        public DateTime? PaidDate { get; set; }
        public virtual EmployeeEarningGross EmployeeEarningGross { get; set; }
        public virtual EmployeeSalaryStatus EmployeeSalaryStatus { get; set; }
    }
}
