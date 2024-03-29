using HRMS.Core.Consts;
using HRMS.Core.Models.Leave;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeLeaveService
    {
        #region Leave Balance
        Task<IEnumerable<int>> GenerateEmployeeLeave(IEnumerable<EmployeeLeaveRequestModel> model, string employeeCode);
        Task<EmployeeLeaveListModel> GetEmployeeLeaveBalance(EmployeeLeaveFilterModel filter, int? employeeId);
        Task<EmployeeLeaveListModel> GetEmployeeLeaveBalanceV2(EmployeeLeaveFilterModel filter, int? employeeId, RoleTypes? userRole);
        Task<double?> GetCurrentEmployeeLeaveBalance(string employeeCode, string leaveFor);
        Task<IEnumerable<LeaveEmployeeDetailModel>> GetRaminingEmployeeForLeaves();
        Task<IEnumerable<LeaveEmployeeDetailModel>> GetLeaveAcquiredEmployee();
        Task<IEnumerable<string>> GetLeaveBalanceCalibrate();
        Task<bool> DeleteLeaveBalanceInformations(string emploeeCode);
        #endregion Leave Balance        
        #region Leave Detail
        #region Type System - Old
        [Obsolete]
        Task<EmployeeLeaveDetailModel> GetEmployeeLeaveDetails(Guid id);
        [Obsolete]
        Task<EmployeeLeaveBalanceDetailModel> GetEmployeeLeaveBalanceDetails(string employeeCode);
        #endregion Type System - Old
        #region Flat System - New
        Task<EmployeeLeaveDetailModel> GetEmployeeLeaveDetailsV2(Guid id);
        Task<EmployeeLeaveBalanceDetailModel> GetEmployeeLeaveBalanceDetailsV2(string employeeCode);
        #endregion Flat System - New
        #endregion Leave Detail
        #region Leave Transactions
        Task<EmployeeLeaveTransactionDetailModel> GetEmployeeLeaveTransactions(string employeeCode);
        Task<EmployeeLeaveTransactionModel> UpdateEmployeeLeaveTransactions(LeaveTransactionUpdateRequestModel model);
        Task<EmployeeLeaveTransactionModel> AddEmployeeLeaveTransactions(LeaveTransactionUpdateRequestModel model);
        #endregion Leave Transactions

    }
}
