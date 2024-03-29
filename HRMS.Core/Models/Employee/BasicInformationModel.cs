using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;

namespace HRMS.Core.Models.Employee
{
    public class BasicInformationModel
    {
        [DefaultValue(null)]
        public int? Id { get; set; } = null;
        [DefaultValue(null)]
        public int? UserId { get; set; } = null;
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string InstituteName { get; set; }
        [DefaultValue("")]
        public string ProfilePhotoFileId { get; set; } = "";
        [DefaultValue(null)]
        public Guid? ProfilePhotoId { get; set; } = null;
        [DefaultValue("")]
        public string ProfilePhotoUrl { get; set; } = "";
        [DefaultValue(false)]
        public bool AllowEditPersonalDetails { get; set; } = false;
        public IFormFile formFile { get; set; }
        [DefaultValue("")]
        public string folderPath { get; set; } = "";
    }
}