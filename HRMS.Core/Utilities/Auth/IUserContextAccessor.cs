using HRMS.Core.Consts;

namespace HRMS.Core.Utilities.Auth
{
    public interface IUserContextAccessor
    {
        public int? UserId { get; }
        public bool? IsAuthorizedUser { get; }
        public RoleTypes? UserRole { get; }
        public int? EmployeeId { get; }
        public string EmployeeCode { get; }
        public int? BranchId { get; }
        public int? Priority { get; }
    }
}
