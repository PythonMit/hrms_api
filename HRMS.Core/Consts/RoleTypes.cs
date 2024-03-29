using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum RoleTypes
    {
        [Description("Super Admin")]
        SuperAdmin = 6,
        [Description("Admin")]
        Admin = 1,
        [Description("HR Manager")]
        HRManager = 2,
        [Description("Employee")]
        Employee = 3,
        [Description("Manager")]
        Manager = 4,
        [Description("Guest")]
        Guest = 5,
    }
}
