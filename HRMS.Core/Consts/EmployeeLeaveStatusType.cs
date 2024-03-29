using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum EmployeeLeaveStatusType
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Approved")]
        Approved = 2,
        [Description("Declined")]
        Declined = 3,
        [Description("LWP")]
        LWP = 4,
    }
}
