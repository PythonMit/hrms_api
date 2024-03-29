using HRMS.Core.Consts;
using System.ComponentModel;

namespace HRMS.Core.Models.Salary
{
    public class EmployeeSalaryRequestModel
    {
        public int Id { get; set; }  
        public string  EmployeeCode { get; set; }
        public string Month { get; set; }
        public int? Year { get; set; }
        public double TotalDays { get; set; }
        public double PaidDays { get; set; }
        public double OverTimeAmount { get; set; }
        public string Remarks { get; set; }
        public double TDS { get; set; }
        public double FixIncentive { get; set; }
        public double Incentive { get; set; }
        public double LWP { get; set; }
        [DefaultValue(null)]
        public int? CreatedBy { get; set; }
        public CalculationType CalculationType { get; set; }
        public bool IsPartlyPaid { get; set; }
        public double AdjustmentAmount { get; set; }
        [DefaultValue(0)]
        public double ESI { get; set; } = 0;
    }
}
