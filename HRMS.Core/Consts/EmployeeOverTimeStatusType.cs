using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum EmployeeOverTimeStatusType
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Approved")]
        Approved = 2,
        [Description("Declined")]
        Declined = 3,
    }
}
