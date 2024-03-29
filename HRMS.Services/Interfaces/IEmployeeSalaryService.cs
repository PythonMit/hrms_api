using HRMS.Core.Consts;
using HRMS.Core.Models.General;
using HRMS.Core.Models.Salary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeSalaryService
    {
        Task<IEnumerable<int>> AddorUpdateEmployeeSalary(IEnumerable<EmployeeSalaryRequestModel> model);
        Task<EmployeeSalaryListModel> GetEmployeeSalary(EmployeeSalaryFilterModel filter, int? employeeId = null);
        Task<bool> DeleteEmployeeSalary(IEnumerable<int> ids);
        Task<bool> SetEmployeeSalaryStatus(IEnumerable<EmployeeSalaryStatusRequestModel> model);
        Task<EmployeeSalaryModel> GetEmployeeSalaryDetailsAsync(EmployeeSalaryQueryModel query);
        Task<IEnumerable<EmployeeSalaryModel>> GetEmployeePartlySalaryDetails(EmployeeSalaryQueryModel query);
        Task<SalaryEmployeeInformationModel> GetEmployeeDetails(string employeeCode);
        Task<double> GetActualLeaveWitoutPay(EmployeeSalaryQueryModel query);
        Task<decimal?> GetActualApprovedOvertime(EmployeeSalaryQueryModel query);
        Task<bool> CheckMonthlySalaryExist(EmployeeSalaryQueryModel query);
        Task<IEnumerable<int>> GetContractYears(string employeeCode);
        Task<IEnumerable<EmployeeOutlineModel>> GetAlreadySalariedEmployee(AlreadySalariedEmployeeRequestModel model, RoleTypes? userRole, int? employeeId);
        #region Partial Hold Salary
        Task<Guid?> AddorUpdateEmployeeHoldSalary(EmployeeHoldSalaryRequestModel model);
        Task<EmployeeHoldSalaryListModel> GetEmployeeHoldSalary(EmployeeHoldSalaryFilterModel filters);
        Task<EmployeeHoldSalaryListModel> GetHistoryEmployeeHoldSalary(EmployeeHoldSalaryHistoryFilterModel filters);
        Task<bool> RemoveEmployeeHoldSalary(IEnumerable<Guid> ids);
        #endregion Partial Hold Salary
        #region Others 
        Task<IEnumerable<BulkSalaryPaymentModel>> GetEmployeeSalarySheet(string employeeCodes, string month, int year);
        #endregion Others
    }
}
