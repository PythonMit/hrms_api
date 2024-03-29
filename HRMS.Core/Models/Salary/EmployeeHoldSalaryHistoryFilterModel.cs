using HRMS.Core.Models.General;

namespace HRMS.Core.Models.Salary
{
    public class EmployeeHoldSalaryHistoryFilterModel
    {
        public int EmployeeContractId { get; set; }
        public string SalaryMonth { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
