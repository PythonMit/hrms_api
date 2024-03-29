using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Services.Interfaces;

namespace HRMS.Services.Common
{
    public class RoleService : IRoleService
    {
        public bool IsAdmin(string role)
        {
            return role == RoleTypes.Admin.GetEnumDescriptionAttribute();
        }

        public bool IsHRManager(string role)
        {
            return role == RoleTypes.HRManager.GetEnumDescriptionAttribute();
        }

        public bool IsEmployee(string role)
        {
            return role == RoleTypes.Employee.GetEnumDescriptionAttribute();
        }

        public bool IsGuest(string role)
        {
            return role == RoleTypes.Guest.GetEnumDescriptionAttribute();
        }

        public bool IsProjectManager(string role)
        {
            return role == RoleTypes.Manager.GetEnumDescriptionAttribute();
        }
    }
}
