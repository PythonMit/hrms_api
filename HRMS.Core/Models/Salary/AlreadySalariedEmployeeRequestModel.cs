using System.Collections.Generic;

namespace HRMS.Core.Models.Salary
{
    public class AlreadySalariedEmployeeRequestModel
    {
        public string Month { get; set; }
        public int? Year { get; set; }
        public IEnumerable<int?> DesignationIds { get; set; }
        public bool HasContract { get; set; }
    }
}
