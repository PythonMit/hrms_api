namespace HRMS.Core.Models.Auth
{
    public class SignUpRequestModel
    {
        public string Username { get; set; }
        public string Emailaddress { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Physicaladdress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int? RoleId { get; set; }
        public string Mobilenumber { get; set; }
    }
}
