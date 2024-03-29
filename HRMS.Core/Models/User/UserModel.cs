namespace HRMS.Core.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Emailaddress { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
    }
}
