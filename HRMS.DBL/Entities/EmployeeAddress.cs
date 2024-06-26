﻿namespace HRMS.DBL.Entities
{
    public class EmployeeAddress : TrackableEntity
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public int Pincode { get; set; }
    }
}
