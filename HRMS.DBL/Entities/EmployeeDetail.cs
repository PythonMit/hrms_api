using System;

namespace HRMS.DBL.Entities
{
    public class EmployeeDetail : TrackableEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string WorkEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateMobileNumber { get; set; }
        public int? PresentAddressId { get; set; }
        public int? PermanentAddressId { get; set; }
        public string PreviousEmployeer { get; set; }
        public decimal? Experience { get; set; }
        public string InstituteName { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool AllowEditPersonalDetails { get; set; }
        public bool HasExited { get; set; }
        public bool HasFNFSettled { get; set; }
        public string WorkingFormat { get; set; }
        public int? EmployeeBankDetailId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual EmployeeAddress PresentAddress { get; set; }
        public virtual EmployeeAddress PermanentAddress { get; set; }
        public virtual EmployeeBankDetail EmployeeBankDetail { get; set; }
    }
}
