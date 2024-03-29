using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum RoleTypePriority
    {
        [Description("Super Admin")]
        SuperAdmin = 1,
        [Description("Admin")]
        Admin = 4,
        [Description("HR Manager")]
        HRManager = 7,
        [Description("Employee")]
        Employee = 13,
        [Description("Manager")]
        Manager = 10,
        [Description("Guest")]
        Guest = 16,
    }
}
