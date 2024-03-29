using HRMS.Core.Consts;
using HRMS.Core.Models.Leave;
using HRMS.Core.Utilities.General;
using HRMS.DBL.DbContextConfiguration;
using HRMS.DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Transactions;
using HRMS.Core.Settings;
using Microsoft.Extensions.Options;

namespace HRMS.DBL.Stores
{
    public class EmployeeLeaveApplicationStore : BaseStore
    {
        private readonly IGeneralUtilities _generalUtilities;
        private readonly EmployeeContractStore _employeeContractStore;
        private readonly GeneralSettings _generalSettings;

        public EmployeeLeaveApplicationStore(HRMSDbContext dbContext, IGeneralUtilities generalUtilities, EmployeeContractStore employeeContractStore, IOptions<GeneralSettings> generalSettings) : base(dbContext)
        {
            _generalUtilities = generalUtilities;
            _employeeContractStore = employeeContractStore;
            _generalSettings = generalSettings.Value;
        }

        #region Leave Application
        public async Task<Guid> AddorUpdateEmployeeLeaveApplication(EmployeeLeaveApplicationRequestModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var isNew = false;
                var contract = await _employeeContractStore.GetRunningEmployeeContract(model.EmployeeCode);
                if (contract == null)
                {
                    return Guid.Empty;
                }

                var checkLeave = await GetCheckEmployeeLeaveBalance(contract.Id);
                if (!checkLeave)
                {
                    return Guid.Empty;
                }

                var data = await _dbContext.EmployeeLeaveApplications.Include(x => x.EmployeeLeaveApplicationManagers).FirstOrDefaultAsync(x => x.Id == model.Id && (model.RecordStatus == null ? x.RecordStatus == RecordStatus.Active : x.RecordStatus == model.RecordStatus));
                if (data == null)
                {
                    data = new EmployeeLeaveApplication();
                    data.Id = Guid.NewGuid();
                    isNew = true;
                }

                data.LeaveTypeId = (int)LeaveTypes.FlatLeave;
                data.LeaveCategoryId = _generalUtilities.GetLeaveCategory(model.LeaveType);
                data.NoOfDays = model.NoOfDays;
                data.ApplyDate = DateTime.UtcNow;
                data.EmployeeLeaveStatusId = (int)EmployeeLeaveStatusType.Pending;
                data.PurposeOfLeave = model.PurposeOfLeave;
                data.LeaveFromDate = model.LeaveFromDate;
                data.LeaveToDate = model.LeaveToDate;
                data.EmployeeContractId = contract.Id;

                AddManyToManyData(data, model);
                if (isNew)
                {
                    await _dbContext.EmployeeLeaveApplications.AddAsync(data);
                }
                else
                {
                    _dbContext.Entry(data).State = EntityState.Modified;
                }

                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Id;
            }
        }
        public async Task<IQueryable<EmployeeLeaveApplication>> GetEmployeeLeaveApplications(EmployeeLeaveApplicationFilterModel filter, int? employeeId = null)
        {
            return _dbContext.EmployeeLeaveApplications.Include(x => x.EmployeeLeaveApplicationManagers).ThenInclude(x => x.Employee)
                                                           .Include(x => x.EmployeeLeaveApplicationComments)
                                                           .Include(x => x.EmployeeLeaveStatus)
                                                           .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee)
                                                           .Include(x => x.LeaveType)
                                                           .Include(x => x.LeaveCategory)
                                                    .Where(x => (employeeId.HasValue ? x.EmployeeContract.Employee.Id == employeeId : true)
                                                        && x.RecordStatus == RecordStatus.Active && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active
                                                        && (filter == null ? true : ((string.IsNullOrEmpty(filter.SearchString) ? true :
                                                                                     (x.EmployeeContract.Employee.EmployeeCode.Contains(filter.SearchString)
                                                                                        || (x.EmployeeContract.Employee.FirstName.Contains(filter.SearchString))
                                                                                        || (x.EmployeeContract.Employee.MiddleName.Contains(filter.SearchString))
                                                                                        || (x.EmployeeContract.Employee.LastName.Contains(filter.SearchString))
                                                                                        || (string.Concat(x.EmployeeContract.Employee.FirstName, " ", x.EmployeeContract.Employee.LastName).Contains(filter.SearchString))
                                                                                        || (x.EmployeeContract.Employee.Branch.Name.Contains(filter.SearchString))
                                                                                        || (x.EmployeeContract.Employee.DesignationType.Name.Contains(filter.SearchString)))))
                                                                                    && (filter.Branch != null && filter.Branch.Any() ? filter.Branch.Contains(x.EmployeeContract.Employee.BranchId) : true)
                                                                                    && (filter.Status != null && filter.Status.Any() ? filter.Status.Contains(x.EmployeeLeaveStatusId) : true)
                                                                                    && (filter.LeaveFrom.HasValue && filter.LeaveTo.HasValue ? x.LeaveFromDate.Value.Date >= filter.LeaveFrom.Value.Date && x.LeaveFromDate.Value.Date <= filter.LeaveTo.Value.Date : true)))
                                                    .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id).AsNoTracking().AsSplitQuery();
        }
        public async Task<IQueryable<EmployeeLeaveApplication>> GetRequestedEmployeeLeaveApplications(EmployeeLeaveApplicationFilterModel filter, int? employeeId = null, RoleTypes? userRole = null)
        {
            return _dbContext.EmployeeLeaveApplications.Include(x => x.EmployeeLeaveApplicationManagers).ThenInclude(x => x.Employee)
                                                           .Include(x => x.EmployeeLeaveApplicationComments)
                                                           .Include(x => x.EmployeeLeaveStatus)
                                                           .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee)
                                                           .Include(x => x.LeaveType)
                                                           .Include(x => x.LeaveCategory)
                                                    .Where(x => (userRole.HasValue ? x.EmployeeLeaveApplicationManagers.Any(z => z.EmployeeId == employeeId) : true)
                                                            && x.RecordStatus == RecordStatus.Active && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active
                                                            && (filter == null ? true : ((string.IsNullOrEmpty(filter.SearchString) ? true :
                                                                                     (x.EmployeeContract.Employee.EmployeeCode.Contains(filter.SearchString)
                                                                                        || (x.EmployeeContract.Employee.FirstName.Contains(filter.SearchString))
                                                                                        || (x.EmployeeContract.Employee.MiddleName.Contains(filter.SearchString))
                                                                                        || (x.EmployeeContract.Employee.LastName.Contains(filter.SearchString))
                                                                                        || (string.Concat(x.EmployeeContract.Employee.FirstName, " ", x.EmployeeContract.Employee.LastName).Contains(filter.SearchString))
                                                                                        || (x.EmployeeContract.Employee.Branch.Name.Contains(filter.SearchString))
                                                                                        || (x.EmployeeContract.Employee.DesignationType.Name.Contains(filter.SearchString)))))
                                                                                    && (filter.Branch != null && filter.Branch.Any() ? filter.Branch.Contains(x.EmployeeContract.Employee.BranchId) : true)
                                                                                    && (filter.Status != null && filter.Status.Any() ? filter.Status.Contains(x.EmployeeLeaveStatusId) : true)
                                                                                    && (filter.LeaveFrom.HasValue && filter.LeaveTo.HasValue ? x.LeaveFromDate >= filter.LeaveFrom && x.LeaveToDate <= filter.LeaveTo : true)))
                                                    .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id).AsNoTracking().AsSplitQuery();
        }
        public async Task<bool> DeleteEmployeeLeaveApplications(IEnumerable<Guid> ids)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeLeaveApplications.Where(x => ids.Contains(x.Id)).ToListAsync();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        item.RecordStatus = RecordStatus.InActive;
                        _dbContext.Entry(item).State = EntityState.Modified;
                        await _dbContext.SaveChangesAsync();
                    }
                    transaction.Complete();
                    return true;
                }
                return false;
            }
        }
        public async Task<(LeaveStatusResponseModel reponse, IEnumerable<LeaveTransactionRequestModel> transactions)?> SetEmployeeLeaveApplicationsStatus(EmployeeLeaveApplicationStatusRequestModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeLeaveApplications.Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).FirstOrDefaultAsync(x => x.Id == model.Id); // && x.EmployeeLeaveStatusId != (int)EmployeeLeaveStatusType.Approved
                if (data == null)
                {
                    return (null, null);
                }

                var contract = await _employeeContractStore.GetRunningEmployeeContract(data.EmployeeContract.Employee.EmployeeCode);
                if (contract == null)
                {
                    return (null, null);
                }

                data.EmployeeLeaveStatusId = (int)model.Status;
                data.ApprovedBy = model.ApprovedBy;
                data.ApprovedDate = DateTime.UtcNow;
                data.ApprovedRemark = model.Remark;

                var noOfDays = data.NoOfDays;
                var transactions = new List<LeaveTransactionRequestModel>();

                if (model.Status == EmployeeLeaveStatusType.Declined)
                {
                    data.DeclineDays = noOfDays;
                }
                else if (model.Status == EmployeeLeaveStatusType.LWP)
                {
                    data.ApprovedDays = 0;
                    data.LWPDays = (contract.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod ? (noOfDays * 2) : noOfDays);

                    if (data.LWPDays > 0)
                    {
                        await UpdateBalanceLeavesTaken(data, data.ApprovedDays, data.LWPDays);

                        transactions.Add(new LeaveTransactionRequestModel
                        {
                            TransactionDate = model.ApprovedFromDate.Value,
                            LWP = data.LWPDays,
                            EmployeeCode = data.EmployeeContract.Employee.EmployeeCode,
                            LeaveApplicationId = data.Id,
                            Used = 0
                        });
                    }
                }
                else if (model.Status == EmployeeLeaveStatusType.Approved)
                {
                    if (model.DeclineDays > 0)
                    {
                        data.DeclineDays = model.DeclineDays;
                        noOfDays -= model.DeclineDays;
                    }

                    var probationDates = contract.ContractStartDate.AddMonths(contract.ProbationPeriod);
                    var trainingDates = contract.ContractStartDate.AddMonths(contract.TrainingPeriod);
                    if (contract.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod)
                    {
                        data.ApprovedDays = 0;
                        data.LWPDays = noOfDays * 2;
                        data.ApprovedRemark = $"In NOTICE PERIOD, {data.ApprovedRemark}";
                    }
                    else if (contract.TrainingPeriod > 0 && (data.LeaveFromDate.Value.Date >= contract.ContractStartDate.Date && data.LeaveFromDate.Value.Date <= trainingDates.Date))
                    {
                        data.ApprovedDays = 0;
                        data.LWPDays = noOfDays;
                        data.ApprovedRemark = $"In TRAINING PERIOD, {data.ApprovedRemark}";
                    }
                    else if (contract.ProbationPeriod > 0 && (data.LeaveFromDate.Value.Date >= contract.ContractStartDate.Date && data.LeaveFromDate.Value.Date <= probationDates.Date))
                    {
                        data.ApprovedDays = 0;
                        data.LWPDays = noOfDays;
                        data.ApprovedRemark = $"In PROBATION PERIOD, {data.ApprovedRemark}";
                    }
                    else
                    {
                        data.ApprovedDays = model.ApprovedDays;
                        data.LWPDays = model.LWPDays;
                    }

                    data.ApprovedFromDate = model.ApprovedFromDate.Value;
                    data.ApprovedToDate = model.ApprovedToDate.Value;
                    await UpdateBalanceLeavesTaken(data, data.ApprovedDays, data.LWPDays);

                    if (data.ApprovedDays > 0)
                    {
                        var dates = _generalUtilities.GetDateRange(data.ApprovedFromDate.Value, data.ApprovedToDate.Value, _generalSettings.HalfDay);
                        foreach (var date in dates)
                        {
                            var halfDay = _generalUtilities.IsHalfDay(date, _generalSettings.HalfDay);
                            transactions.Add(new LeaveTransactionRequestModel
                            {
                                TransactionDate = date,
                                Used = (model.ApprovedDays == 0.5 || halfDay ? 0.5 : 1),
                                EmployeeCode = data.EmployeeContract.Employee.EmployeeCode,
                                LeaveApplicationId = data.Id,
                            });
                        }
                    }

                    if (data.LWPDays > 0)
                    {
                        transactions.Add(new LeaveTransactionRequestModel
                        {
                            TransactionDate = data.ApprovedFromDate,
                            LWP = data.LWPDays,
                            EmployeeCode = data.EmployeeContract.Employee.EmployeeCode,
                            LeaveApplicationId = data.Id,
                            Used = 0
                        });
                    }
                }

                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return (new LeaveStatusResponseModel { EmployeeCode = data.EmployeeContract.Employee.EmployeeCode, Status = true }, transactions);
            }
        }
        public async Task<IQueryable<EmployeeLeaveApplication>> GetEmployeeLeaveApplications(Guid id)
        {
            return _dbContext.EmployeeLeaveApplications.Include(x => x.EmployeeLeaveApplicationManagers).ThenInclude(x => x.Employee)
                                                        .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.Branch)
                                                        .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.DesignationType)
                                                        .Include(x => x.EmployeeLeaveApplicationComments)
                                                        .Include(x => x.EmployeeLeaveStatus)
                                                        .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee)
                                                        .Include(x => x.LeaveType)
                                                        .Include(x => x.LeaveCategory)
                                                  .Where(x => x.Id == id && x.RecordStatus == RecordStatus.Active && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active)
                                                  .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id).AsNoTracking().AsSplitQuery();
        }
        #endregion Leave Application
        #region Leave Comment
        public async Task<Guid> AddorUpdateEmployeeLeaveApplicationComment(EmployeeLeaveApplicationCommentRequestModel model, int? employeeId)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var isNew = false;
                var data = await _dbContext.EmployeeLeaveApplicationComments.FirstOrDefaultAsync(x => x.Id == model.Id && x.EmployeeLeaveApplicationId == model.EmployeeLeaveApplicationId && x.RecordStatus == RecordStatus.Active);
                if (data == null)
                {
                    data = new EmployeeLeaveApplicationComment();
                    data.Id = Guid.NewGuid();
                    isNew = true;
                }

                data.Comments = model.Comments;
                data.CommentDate = DateTime.UtcNow;
                data.EmployeeLeaveApplicationId = model.EmployeeLeaveApplicationId;
                data.CommentBy = employeeId;
                if (isNew)
                {
                    data.RecordStatus = RecordStatus.Active;
                    data.CreatedDateTimeUtc = DateTime.UtcNow;
                }
                else
                {
                    data.UpdatedDateTimeUtc = DateTime.UtcNow;
                }

                _dbContext.Entry(data).State = (isNew ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Id;
            }
        }
        public async Task<IQueryable<EmployeeLeaveApplicationComment>> GetEmployeeLeaveApplicationComments(EmployeeLeaveApplicationCommentFilterModel filter)
        {
            return _dbContext.EmployeeLeaveApplicationComments.Where(x => x.EmployeeLeaveApplicationId == filter.EmployeeLeaveApplicationId && (filter.RecordStatus == null ? true : x.RecordStatus == filter.RecordStatus))
                                                        .Include(x => x.Employee).ThenInclude(x => x.DesignationType)
                                                .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id).AsNoTracking().AsSplitQuery();
        }
        public async Task<bool> DeleteEmployeeLeaveApplicationComments(IEnumerable<Guid> commentIds, Guid employeeLeaveApplicationId)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeLeaveApplicationComments.Where(x => commentIds.Contains(x.Id) && x.EmployeeLeaveApplicationId == employeeLeaveApplicationId).ToListAsync();
                if (data != null)
                {
                    data.ForEach(x => x.RecordStatus = RecordStatus.InActive);
                    foreach (var item in data)
                    {
                        item.RecordStatus = RecordStatus.InActive;
                        _dbContext.Entry(item).State = EntityState.Modified;
                        await _dbContext.SaveChangesAsync();
                    }
                    transaction.Complete();
                    return true;
                }
                return false;
            }
        }
        #endregion Leave Comment
        #region Others
        private async Task<bool> GetCheckEmployeeLeaveBalance(int contractId)
        {
            return await _dbContext.EmployeeLeaves.AnyAsync(x => x.EmployeeContractId == contractId);
        }
        private async Task UpdateBalanceLeavesTaken(EmployeeLeaveApplication model, double approvedDays, double lWPDays)
        {
            var data = await _dbContext.EmployeeLeaves.FirstOrDefaultAsync(x => x.EmployeeContractId == model.EmployeeContractId && x.LeaveTypeId == model.LeaveTypeId);
            if (data == null)
            {
                return;
            }

            data.TotalLeavesTaken += approvedDays;
            _dbContext.Entry(data).State = EntityState.Modified;

            if (lWPDays > 0)
            {
                await UpdateLWPLeaveTaken(lWPDays, data.EmployeeContractId);
            }
        }
        private async Task UpdateLWPLeaveTaken(double lWPDays, int? EmployeeContractId)
        {
            var lwpData = await _dbContext.EmployeeLeaves.FirstOrDefaultAsync(x => x.EmployeeContractId == EmployeeContractId && x.LeaveTypeId == (int)LeaveTypes.LeaveWithoutPay);
            if (lwpData != null)
            {
                lwpData.TotalLeavesTaken += lWPDays;
                _dbContext.Entry(lwpData).State = EntityState.Modified;
            }
        }
        private async Task<(double approvedDays, double lwpDays)> LeaveApplicationLWPDays(int? employeeContractId, int leaveTypeId, double appliedDays)
        {
            var data = await _dbContext.EmployeeLeaves.FirstOrDefaultAsync(x => x.EmployeeContractId == employeeContractId && x.LeaveTypeId == leaveTypeId);
            if (data != null)
            {
                var balance = (data.TotalLeaves - data.TotalLeavesTaken);
                var approved = (balance > 0 ? (appliedDays >= balance ? ((appliedDays - balance) <= balance ? balance : appliedDays) : (balance == data.TotalLeaves ? appliedDays : balance)) : 0);
                var lwp = (balance > 0 ? (appliedDays >= balance ? appliedDays - approved : 0) : appliedDays);

                return (approvedDays: approved, lwpDays: lwp);
            }
            return (approvedDays: appliedDays, lwpDays: 0);
        }
        private void AddManyToManyData(EmployeeLeaveApplication enitity, EmployeeLeaveApplicationRequestModel model)
        {
            if (model.ProjectManagerIds != null && model.ProjectManagerIds.Any())
            {
                if (enitity?.EmployeeLeaveApplicationManagers != null)
                {
                    _dbContext.EmployeeLeaveApplicationManagers?.RemoveRange(enitity?.EmployeeLeaveApplicationManagers);
                }
                foreach (var item in model.ProjectManagerIds)
                {
                    enitity.EmployeeLeaveApplicationManagers.Add(new EmployeeLeaveApplicationManager { EmployeeId = item, EmployeeLeaveApplicationId = enitity.Id });
                }
            }
        }
        #endregion Others
    }
}
