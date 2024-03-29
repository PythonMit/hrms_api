using HRMS.Core.Consts;
using HRMS.Core.Models.User;
using System;

namespace HRMS.Core.Models.Auth
{
    public class SignInResponseModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeCode { get; set; }
        public TokenModel Token { get; set; }
        public RoleModel Role { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public string ProfileImage { get; set; }
        public DateTime? JoinDate { get; set; }
    }

    public class TokenModel
    {
        public string Access_Token { get; set; }
        public DateTime? ExpirationTime { get; set; }

    }
}
