using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.DBL.Entities
{
    public class EmployeeEarningGross : TrackableEntity
    {
        public int Id { get; set; }
        public int EmployeeContractId { get; set; }
        public string SalaryMonth { get; set; }
        public int SalaryYear { get; set; }
        public double TotalDays { get; set; }
        public double LWP { get; set; }
        public double PaidDays { get; set; }
        public double Basic { get; set; }
        public double DA { get; set; }
        public double LTA { get; set; }
        public double HRA { get; set; }
        public double ConveyanceAllowance { get; set; }
        public double OtherAllowance { get; set; }
        public double MedicalAllowance { get; set; }
        public double ChildEducation { get; set; }
        public double FixIncentive { get; set; }
        public double Incentive { get; set; }
        public string CalculationType { get; set; }
        public string DaysJSON { get; set; }
        public int PT { get; set; }
        public double TDS { get; set; }
        public double ESI { get; set; }
        public double EmployeePF { get; set; }
        public double EmployerPF { get; set; }
        public double OverTimeAmount { get; set; }
        public double NetSalary { get; set; }
        public string Remarks { get; set; }
        public int EmployeeEarningGrossStatusId { get; set; }
        public DateTime? PaidDate { get; set; }
        public double AdjustmentAmount { get; set; }
        [ForeignKey("Employee")]
        public int? CreatedBy { get; set; }
        public bool PartlyPaid { get; set; }
        public virtual EmployeeEarningGrossStatus EmployeeEarningGrossStatus { get; set; }
        public virtual EmployeeContract EmployeeContract { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<EmployeeHoldSalary> EmployeeHoldSalaries { get; set; }
    }
}
