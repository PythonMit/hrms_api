using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum EmployeeSalaryStatusType
    {
        [Description("In Process")]
        InProcess = 1,
        [Description("Hold")]
        Hold = 2,
        [Description("Paid")]
        Paid = 3,
        [Description("Partial Paid")]
        PartialPaid = 4
    }
}

