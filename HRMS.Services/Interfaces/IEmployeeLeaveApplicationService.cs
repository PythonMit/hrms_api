using HRMS.Core.Consts;
using HRMS.Core.Models.Leave;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IEmployeeLeaveApplicationService
    {
        #region Leave Application
        Task<Guid> AddorUpdateEmployeeLeaveApplication(EmployeeLeaveApplicationRequestModel model);
        Task<EmployeeLeaveApplicationListModel> GetEmployeeLeaveApplications(EmployeeLeaveApplicationFilterModel filter, int? employeeId, RoleTypes? userRole);
        Task<bool> DeleteEmployeeLeaveApplications(IEnumerable<Guid> ids);
        Task<bool> SetEmployeeLeaveApplicationsStatus(EmployeeLeaveApplicationStatusRequestModel model);
        Task<EmployeeLeaveApplicationModel> GetEmployeeLeaveApplications(Guid id);
        Task<int?> GetLeaveApplicationSandwichDays(SandwichDateRequestModel model);
        #endregion Leave Application
        #region Leave Comment
        Task<Guid> AddorUpdateEmployeeLeaveApplicationComment(EmployeeLeaveApplicationCommentRequestModel model);
        Task<EmployeeLeaveApplicationCommentListModel> GetEmployeeLeaveApplicationComments(EmployeeLeaveApplicationCommentFilterModel filter);
        Task<bool> DeleteEmployeeLeaveApplicationComments(IEnumerable<Guid> commentIds, Guid employeeLeaveApplicationId);
        Task<int?> GetTotalRunningContract(string employeeCode);
        #endregion Leave Comment
    }
}
