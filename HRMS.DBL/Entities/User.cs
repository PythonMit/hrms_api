namespace HRMS.DBL.Entities
{
    public class User : TrackableEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Emailaddress { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public bool? IsVerified { get; set; }
        public bool IsAdmin { get; set; }
        public int? RoleId { get; set; }
        public bool Disabled { get; set; }
        public string CipherText { get; set; }
        public Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}
