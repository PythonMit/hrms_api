using System;
using HRMS.Core.Models.Contract;

namespace HRMS.Core.Models.Salary
{
    public class EarningGrossCalculationModel : FixGrossCalculationModel
    {
        public int Id { get; set; }
        public string SalaryMonth { get; set; }
        public double TotalDays { get; set; }
        public double LWP { get; set; }
        public double PaidDays { get; set; }
        public double NetSalary { get; set; }
        public DateTime? PaidDate { get; set; }
        public double TotalEarning { get; set; }
        public double TotalDeduction { get; set; }
        public double CostToCompany { get; set; }
        public double StipendAmount { get; set; }
        public int Year { get; set; }
        public double FixIncentive { get; set; }
        public double Incentive { get; set; }
        public double HoldAmount { get; set; }
        public double PaidAmount { get; set; }
    }
}
