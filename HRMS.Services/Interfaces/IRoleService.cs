namespace HRMS.Services.Interfaces
{
    public interface IRoleService
    {
        bool IsAdmin(string role);
        bool IsGuest(string role);
        bool IsEmployee(string role);
        bool IsHRManager(string role);
        bool IsProjectManager(string role);
    }
}
