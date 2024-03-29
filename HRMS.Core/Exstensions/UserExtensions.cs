using System.Security.Claims;

namespace HRMS.Core.Exstensions
{
    public static class UserExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userIdStr, out int userId);
            return userId > 0 ? userId : null;
        }
    }
}
