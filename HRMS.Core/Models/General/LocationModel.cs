namespace HRMS.Core.Models.General
{
    public class LocationModel
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string PostalCode { get; set; }
    }
}
