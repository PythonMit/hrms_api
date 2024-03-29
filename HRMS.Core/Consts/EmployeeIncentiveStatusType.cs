using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum EmployeeIncentiveStatusType
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Hold")]
        Hold = 2,
        [Description("Paid")]
        Paid = 3
    }
}
