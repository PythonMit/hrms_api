using HRMS.Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Models.Employee
{
    public class SecurityInformationModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        [Required]
        public int? RoleId { get; set; }
        public string Password { get; set; }
        public bool HasPassword { get; set; }
        [Required]
        public string EmployeeCode { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public bool UserStatus { get; set; }
        public string SlackUserId { get; set; }
    }
}
