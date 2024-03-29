using HRMS.DBL.DbContextConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using HRMS.Core.Consts;
using System.Transactions;
using HRMS.Core.Models.Contract;
using HRMS.Core.Models.ImageKit;
using HRMS.Core.Utilities.General;
using LinqKit;
using System;
using System.Drawing;
using System.Diagnostics.Contracts;

namespace HRMS.DBL.Stores
{
    public class EmployeeContractStore : BaseStore
    {
        private readonly EmployeeStore _employeeStore;
        private readonly IGeneralUtilities _generalUtilities;
        public EmployeeContractStore(HRMSDbContext dbContext, EmployeeStore employeeStore, IGeneralUtilities generalUtilities) : base(dbContext)
        {
            _employeeStore = employeeStore;
            _generalUtilities = generalUtilities;
        }

        public async Task<IQueryable<EmployeeContract>> GetAllEmployeeContract(EmployeeContractFilterModel filter, RoleTypes? userRole)
        {
            return _dbContext.EmployeeContracts.Include(x => x.Employee)
                                    .Include(x => x.Employee).ThenInclude(x => x.Branch)
                                    .Include(x => x.EmployeeContractStatus)
                                    .Include(x => x.EmployeeFixGross)
                                    .Where(x => (userRole == RoleTypes.SuperAdmin ? true : !GenericConst.AnchoredEmployeeCode.Contains(x.Employee.EmployeeCode))
                                                && x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active
                                                && (filter == null ? true : ((string.IsNullOrEmpty(filter.SearchString) ? true :
                                                                                                ((x.Employee.EmployeeCode.Contains(filter.SearchString))
                                                                                                    || (x.Employee.FirstName.Contains(filter.SearchString))
                                                                                                    || (x.Employee.LastName.Contains(filter.SearchString))
                                                                                                    || (string.Concat(x.Employee.FirstName, " ", x.Employee.LastName).Contains(filter.SearchString))
                                                                                                    || (x.Employee.Branch.Name.Contains(filter.SearchString))))
                                                                    && (filter.StartYear.HasValue && filter.EndYear.HasValue ? x.ContractStartDate.Year >= filter.StartYear && x.ContractStartDate.Year <= filter.EndYear : true)
                                                                    && (filter.Status != null && filter.Status.Any() ? filter.Status.Contains(x.EmployeeContractStatusId) : true))))
                                    .OrderByDescending(x => x.ContractStartDate).ThenByDescending(x => x.Id);
        }
        public async Task<IQueryable<EmployeeContract>> GetEmployeeContractHistoryByEmployeeCode(string employeeCode, RecordStatus recordStatus = RecordStatus.Active)
        {
            var id = await _employeeStore.GetEmployeeIdByCode(employeeCode, recordStatus);
            return _dbContext.EmployeeContracts.Include(x => x.Employee).ThenInclude(x => x.Branch)
                                        .Include(x => x.Employee).ThenInclude(x => x.ImagekitDetail)
                                        .Include(x => x.DesignationType)
                                        .Include(x => x.EmployeeFixGross)
                                        .Include(x => x.EmployeeContractStatus)
                                    .Where(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == recordStatus && x.EmployeeId == id)
                                    .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id);
        }
        public async Task<EmployeeDetail> GetRemainingEmployeeDetails(string employeeCode)
        {
            var id = await _employeeStore.GetEmployeeIdByCode(employeeCode);
            return await _dbContext.EmployeeDetails
                                    .Include(x => x.Employee).ThenInclude(x => x.Branch)
                                    .Include(x => x.Employee.DesignationType)
                                    .Include(x => x.Employee).ThenInclude(x => x.EmployeeContracts)
                                    .FirstOrDefaultAsync(x => x.Employee.RecordStatus == RecordStatus.Active && x.EmployeeId == id);
        }
        public async Task<IEnumerable<EmployeeDetail>> GetRemainingEmployees(string employeeName)
        {
            var contractIds = await _dbContext.EmployeeContracts.Where(x => x.RecordStatus == RecordStatus.Active).Select(x => x.EmployeeId).Distinct().ToListAsync();
            return await _dbContext.EmployeeDetails.Include(x => x.Employee).ThenInclude(x => x.Branch)
                                                   .Include(x => x.Employee.DesignationType)
                                                   .Where(x => x.Employee.RecordStatus == RecordStatus.Active && !contractIds.Contains(x.EmployeeId)
                                                            && (string.IsNullOrEmpty(employeeName) ? true : (x.Employee.FirstName.Contains(employeeName)
                                                                    || x.Employee.MiddleName.Contains(employeeName)
                                                                    || x.Employee.LastName.Contains(employeeName)))).ToListAsync();
        }
        public async Task<EmployeeContract> GetEmployeeContractById(int contractId)
        {
            return await _dbContext.EmployeeContracts.FirstOrDefaultAsync(x => x.Id == contractId);
        }
        public async Task<int?> GetEmployeeCurrentContractIdByEmployeeId(int employeeId)
        {
            return await _dbContext.EmployeeContracts.Where(x => x.RecordStatus == RecordStatus.Active && x.EmployeeId == employeeId
                                                        && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod))
                                                    .OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();
        }
        public async Task<EmployeeFixGross> GetEmployeeFixGrossByContractId(int contractId)
        {
            return await _dbContext.EmployeeFixGross.FirstOrDefaultAsync(x => x.EmployeeContractId == contractId);
        }
        public async Task<IQueryable<EmployeeContract>> GetEmployeeCurrentContractDetails(int contractId)
        {
            return _dbContext.EmployeeContracts
                                    .Include(x => x.Employee.Branch)
                                    .Include(x => x.EmployeeContractStatus)
                                    .Include(x => x.DesignationType)
                                    .Include(x => x.EmployeeFixGross)
                                .Where(x => x.RecordStatus == RecordStatus.Active && x.Id == contractId);

        }
        public async Task<int?> AddOrUpdateEmployeeContractDetail(EmployeeContractRequestModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeContracts.FirstOrDefaultAsync(x => x.RecordStatus == RecordStatus.Active && x.Id == model.Id && x.EmployeeId == model.EmployeeId);

                if (data == null || model.IsRenew)
                {
                    data = new EmployeeContract();
                    data.EmployeeId = model.EmployeeId;
                    data.IsProjectTrainee = (!model.IsRenew ? model.DesignationTypeId == (int)DesignationTypes.ProjectTrainee : data.IsProjectTrainee);
                }

                data.ContractStartDate = model.ContractStartDate;
                data.ContractEndDate = model.ContractEndDate;
                data.DesignationTypeId = model.DesignationTypeId;
                data.EmployeeContractStatusId = model.EmployeeContractStatusId;
                data.ProbationPeriod = model.ProbationPeriod;
                data.TrainingPeriod = model.TrainingPeriod;
                data.Remarks = model.Remarks;
                data.ImagekitDetailId = model.ImagekitDetailId;

                if (data.EmployeeContractStatusId == (int)EmployeeContractStatusType.Drop)
                {
                    if (model.DropInformation.DropDate != null)
                    {
                        data.DropDate = model.DropInformation.DropDate.Value;
                    }
                    data.DropRemarks = model.DropInformation.Remarks;
                }

                if (data.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod)
                {
                    if (model.NoticePeriod.StartDate != null && model.NoticePeriod.EndDate != null)
                    {
                        data.NoticePeriodStartDate = model.NoticePeriod.StartDate.Value;
                        data.NoticePeriodEndDate = model.NoticePeriod.EndDate.Value;
                        data.NoticeRemarks = model.NoticePeriod.Remarks;
                    }
                }

                if (data.EmployeeContractStatusId == (int)EmployeeContractStatusType.Terminate)
                {
                    data.TerminateDate = model.Terminate.TerminateDate.Value;
                    data.TerminateRemarks = model.Terminate.Remarks;
                }

                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);

                await UpdateEmployeeCurrentDesignation(model.EmployeeId, model.DesignationTypeId);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Id;
            }
        }
        public async Task<int?> AddOrUpdateEmployeeGrossDetail(EmployeeFixGrossModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeFixGross.Include(x => x.EmployeeContract).FirstOrDefaultAsync(x => x.EmployeeContract.RecordStatus == RecordStatus.Active && x.EmployeeContractId == model.EmployeeContractId);
                if (data == null)
                {
                    data = new EmployeeFixGross();
                }

                data.EmployeeContractId = model.EmployeeContractId;
                data.CostToCompany = model.CostToCompany;
                if (model.DesignationTypeId == (int)DesignationTypes.ProjectTrainee)
                {
                    data.StipendAmount = model.StipendAmount;
                }
                data.Basic = model.Basic;
                data.DA = model.DA;
                data.HRA = model.HRA;
                data.LTA = model.LTA;
                data.ConveyanceAllowance = model.ConveyanceAllowance;
                data.OtherAllowance = model.OtherAllowance;
                data.MedicalAllowance = model.MedicalAllowance;
                data.ChildEducation = model.ChildEducation;
                data.IsFixIncentive = model.FixIncentiveDetail?.IsFixIncentive ?? false;
                data.FixIncentiveDuration = model.FixIncentiveDetail?.FixIncentiveDuration ?? 0;
                data.FixIncentiveRemarks = model.FixIncentiveDetail?.FixIncentiveRemarks;

                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();

                return data.EmployeeContractId;
            }
        }
        public async Task AddOrUpdateEmployeeIncentives(int? employeeContractId, DateTime? fixIncentiveDate)
        {
            var contract = await _dbContext.EmployeeContracts.Include(x => x.EmployeeFixGross).Include(x => x.EmployeeIncentiveDetails).FirstOrDefaultAsync(x => x.Id == employeeContractId);
            if (contract.DesignationTypeId == (int)DesignationTypes.ProjectTrainee)
            {
                return;
            }

            var fixGross = contract?.EmployeeFixGross;
            if (contract != null && fixGross.IsFixIncentive && fixGross.FixIncentiveDuration > 0)
            {
                var dates = new List<DateTime>();
                if (fixGross.FixIncentiveDuration == 12)
                {
                    dates.Add(fixIncentiveDate.Value);
                }
                else
                {
                    var newStartDate = (contract.TrainingPeriod > 0 ? contract.ContractStartDate.AddMonths(contract.TrainingPeriod) : contract.ContractStartDate);

                    if (newStartDate.Day <= 15)
                    {
                        newStartDate = newStartDate.AddMonths(-1);
                    }

                    //var newEndDate = contract.ContractEndDate;
                    //if (newStartDate.Day < 15)
                    //{
                    //    if (newStartDate.Month != newEndDate.Month && DateTime.DaysInMonth(newEndDate.Year, newEndDate.Month) == newEndDate.Day)
                    //    {
                    //        var p = contract.ContractEndDate.AddDays(1).AddMonths(-1);
                    //        var t = contract.ContractEndDate.AddDays(1);
                    //        newEndDate = t.AddMonths(-1);
                    //    }
                    //    else
                    //    {
                    //        newEndDate = contract.ContractEndDate.AddMonths(-1);
                    //    }
                    //}

                    newStartDate = new DateTime(newStartDate.Year, newStartDate.Month, 7);
                    var newEndDate = new DateTime(contract.ContractEndDate.Year, contract.ContractEndDate.Month, 7);

                    dates = _generalUtilities.GetMonthlyDates(newStartDate, newEndDate, fixGross.FixIncentiveDuration).ToList();
                }
                if (dates != null && dates.Any())
                {
                    var data = new List<EmployeeIncentiveDetails>();
                    using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var anyUpdated = contract?.EmployeeIncentiveDetails?.Count(x => x.EmployeeIncentiveStatusId == (int)EmployeeIncentiveStatusType.Paid);
                        if (contract.EmployeeIncentiveDetails != null && contract.EmployeeIncentiveDetails.Count > 0 && (anyUpdated == null || anyUpdated <= 0))
                        {
                            _dbContext.EmployeeIncentiveDetails.RemoveRange(contract.EmployeeIncentiveDetails);
                        }

                        if (anyUpdated == null || anyUpdated <= 0)
                        {
                            dates.ForEach(x => data.Add(new EmployeeIncentiveDetails
                            {
                                EmployeeContractId = contract.Id,
                                IncentiveDate = x,
                                EmployeeIncentiveStatusId = (int)EmployeeIncentiveStatusType.Pending,
                            }));

                            if (data != null && data.Count > 0)
                            {
                                await _dbContext.EmployeeIncentiveDetails.AddRangeAsync(data);
                                await _dbContext.SaveChangesAsync();
                                transaction.Complete();
                            }
                        }
                    }
                }
            }
            else if (contract != null && !fixGross.IsFixIncentive && fixGross.FixIncentiveDuration <= 0)
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var anyUpdated = contract?.EmployeeIncentiveDetails?.Count(x => x.EmployeeIncentiveStatusId == (int)EmployeeIncentiveStatusType.Paid);
                    if (contract.EmployeeIncentiveDetails != null && contract.EmployeeIncentiveDetails.Count > 0 && (anyUpdated == null || anyUpdated <= 0))
                    {
                        _dbContext.EmployeeIncentiveDetails.RemoveRange(contract.EmployeeIncentiveDetails);
                        await _dbContext.SaveChangesAsync();
                        transaction.Complete();
                    }
                }
            }
        }
        public async Task<bool> DeleteEmployeeContractDetails(int contractId)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeContracts.FirstOrDefaultAsync(x => x.RecordStatus == RecordStatus.Active && x.Id == contractId);
                if (data != null)
                {
                    await DeleteEmployeeContractLeaves(contractId);
                    await DeleteEmployeeContractFixedGross(contractId);
                    await DeleteEmployeeContractEarningGross(contractId);
                    await DeleteEmployeeContractOvertime(contractId);

                    data.RecordStatus = RecordStatus.InActive;
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return true;
                }
                return false;
            }
        }
        private async Task DeleteEmployeeContractLeaves(int contractId)
        {
            var data = await _dbContext.EmployeeLeaves.Where(x => x.RecordStatus == RecordStatus.Active && x.EmployeeContractId == contractId).ToListAsync();
            if (data != null)
            {
                data.ForEach(x => x.RecordStatus = RecordStatus.InActive);
                await _dbContext.SaveChangesAsync();
            }
        }
        private async Task DeleteEmployeeContractFixedGross(int contractId)
        {
            var data = await _dbContext.EmployeeFixGross.Where(x => x.RecordStatus == RecordStatus.Active && x.EmployeeContractId == contractId).ToListAsync();
            if (data != null)
            {
                data.ForEach(x => x.RecordStatus = RecordStatus.InActive);
                await _dbContext.SaveChangesAsync();
            }
        }
        private async Task DeleteEmployeeContractEarningGross(int contractId)
        {
            var data = await _dbContext.EmployeeEarningGross.Where(x => x.RecordStatus == RecordStatus.Active && x.EmployeeContractId == contractId).ToListAsync();
            if (data != null)
            {
                data.ForEach(x => x.RecordStatus = RecordStatus.InActive);
                await _dbContext.SaveChangesAsync();
            }
        }
        private async Task DeleteEmployeeContractOvertime(int contractId)
        {
            var data = await _dbContext.EmployeeOverTimes.Where(x => x.RecordStatus == RecordStatus.Active && x.EmployeeContractId == contractId).ToListAsync();
            if (data != null)
            {
                data.ForEach(x => x.RecordStatus = RecordStatus.InActive);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<string> RemoveContractInformations(int id)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await GetEmployeeContract(id);
                if (data != null)
                {
                    var overtime = await _dbContext.EmployeeOverTimes.Include(x => x.EmployeeOverTimeManagers).Where(x => x.EmployeeContractId == data.Id).ToListAsync();
                    var overtimeManagers = overtime.SelectMany(x => x.EmployeeOverTimeManagers);
                    var leaves = await _dbContext.EmployeeLeaves.Where(x => x.EmployeeContractId == data.Id).ToListAsync();
                    var leaveTransactions = await _dbContext.EmployeeLeaveTransactions.Where(x => x.EmployeeContractId == data.Id).ToListAsync();
                    var leavesApplications = await _dbContext.EmployeeLeaveApplications.Include(x => x.EmployeeLeaveApplicationManagers).Include(x => x.EmployeeLeaveApplicationComments).Where(x => x.EmployeeContractId == data.Id).ToListAsync();
                    var leavesApplicationsManagers = leavesApplications.SelectMany(x => x.EmployeeLeaveApplicationManagers);
                    var leavesApplicationsComments = leavesApplications.SelectMany(x => x.EmployeeLeaveApplicationComments);
                    var fixGross = await _dbContext.EmployeeFixGross.Where(x => x.EmployeeContractId == data.Id).ToListAsync();
                    var earningGross = await _dbContext.EmployeeEarningGross.Where(x => x.EmployeeContractId == data.Id).ToListAsync();
                    var incentives = await _dbContext.EmployeeIncentiveDetails.Where(x => x.EmployeeContractId == data.Id).ToListAsync();
                    var document = await _dbContext.ImagekitDetails.FirstOrDefaultAsync(x => x.Id == data.ImagekitDetailId);

                    _dbContext.EmployeeFixGross.RemoveRange(fixGross);
                    if (earningGross != null)
                    {
                        _dbContext.EmployeeEarningGross.RemoveRange(earningGross);
                    }
                    if (overtimeManagers != null)
                    {
                        _dbContext.EmployeeOverTimeManagers.RemoveRange(overtimeManagers);
                    }
                    if (overtime != null)
                    {
                        _dbContext.EmployeeOverTimes.RemoveRange(overtime);
                    }
                    if (incentives != null)
                    {
                        _dbContext.EmployeeIncentiveDetails.RemoveRange(incentives);
                    }
                    if (leavesApplicationsComments != null)
                    {
                        _dbContext.EmployeeLeaveApplicationComments.RemoveRange(leavesApplicationsComments);
                    }
                    if (leavesApplicationsManagers != null)
                    {
                        _dbContext.EmployeeLeaveApplicationManagers.RemoveRange(leavesApplicationsManagers);
                    }
                    if (leavesApplications != null)
                    {
                        _dbContext.EmployeeLeaveApplications.RemoveRange(leavesApplications);
                    }
                    if (leaves != null)
                    {
                        _dbContext.EmployeeLeaves.RemoveRange(leaves);
                    }
                    if (leaveTransactions != null)
                    {
                        _dbContext.EmployeeLeaveTransactions.RemoveRange(leaveTransactions);
                    }
                    if (document != null)
                    {
                        _dbContext.ImagekitDetails.Remove(document);
                    }

                    _dbContext.EmployeeContracts.Remove(data);

                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return document?.FilePath;
                }
            }
            return string.Empty;
        }
        #region General Methods
        public async Task<EmployeeImagekitModel> GetEmployeeImagekitData(int? employeeid, int year, int? contractId = null)
        {
            return await _dbContext.EmployeeContracts.Include(x => x.ImagekitDetail)
                                            .Where(x => x.RecordStatus == RecordStatus.Active && (contractId.HasValue || contractId > 0 ? x.Id == contractId : true) && x.EmployeeId == employeeid && x.ContractStartDate.Year == year)
                                            .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id)
                                            .Select(x => new EmployeeImagekitModel
                                            {
                                                Id = x.Id,
                                                EmployeeId = x.EmployeeId,
                                                ImagekitDetailId = x.ImagekitDetailId,
                                                ImagekitDetailFileId = x.ImagekitDetail.FileId,
                                                FilePath = x.ImagekitDetail.FilePath
                                            }).FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateEmployeeImagekitFileId(int? employeeContractId, int? employeeId, Guid? imagekitId, int year)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeContracts.FirstOrDefaultAsync(x => x.RecordStatus == RecordStatus.Active && x.Id == employeeContractId && x.EmployeeId == employeeId && x.ContractStartDate.Year == year);
                if (data != null)
                {
                    data.ImagekitDetailId = imagekitId;
                    _dbContext.Entry(data).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> GetRunningContract(RunningContractRequestModel model)
        {
            return await _dbContext.EmployeeContracts.AnyAsync(x => x.Employee.EmployeeCode == model.employeeCode
                                                                && x.RecordStatus == RecordStatus.Active
                                                                && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod)
                                                                && (model.StartDate.HasValue ? x.ContractStartDate.Date == model.StartDate.Value.Date : true)
                                                                && (model.EndDate.HasValue ? x.ContractEndDate.Date == model.EndDate.Value.Date : true));
        }
        public async Task<bool> SetEmployeeContractStatus(int contractId, int statusType)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeContracts.FirstOrDefaultAsync(x => x.RecordStatus == RecordStatus.Active && x.Id == contractId);
                if (data != null)
                {
                    data.EmployeeContractStatusId = statusType;
                    _dbContext.Entry(data).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return true;
                }
            }
            return false;
        }
        public async Task UpdateEmployeeCurrentDesignation(int employeeId, int designationId)
        {
            var data = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            if (data != null)
            {
                data.DesignationTypeId = designationId;
                _dbContext.Entry(data).State = EntityState.Modified;
            }
        }
        public EmployeeContract GetLastContractEndDate(string employeeCode)
        {
            return _dbContext.EmployeeContracts.Include(x => x.EmployeeContractStatus).Where(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active && x.Employee.EmployeeCode == employeeCode
                                                      && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Completed || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Drop))
                                                .OrderByDescending(x => x.CreatedDateTimeUtc).ThenBy(x => x.ContractEndDate).FirstOrDefault();
        }
        public async Task<EmployeeContract> GetRunningEmployeeContract(string employeeCode)
        {
            return await _dbContext.EmployeeContracts.Include(x => x.EmployeeFixGross)
                                                     .Include(x => x.Employee)
                                                     .FirstOrDefaultAsync(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active && x.Employee.EmployeeCode == employeeCode
                                                                        && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod));
        }
        public async Task<EmployeeContract> GetRunningEmployeeContract(int employeeContractId)
        {
            return await _dbContext.EmployeeContracts.Include(x => x.EmployeeFixGross)
                                                     .Include(x => x.Employee)
                                                     .FirstOrDefaultAsync(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active && x.Id == employeeContractId
                                                                        && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod));
        }
        public async Task<IEnumerable<EmployeeContract>> GetRunningEmployeeContracts(IEnumerable<string> employeeCodes)
        {
            return await _dbContext.EmployeeContracts.Include(x => x.EmployeeFixGross)
                                                     .Include(x => x.Employee)
                                                     .Where(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active
                                                                && (employeeCodes != null && employeeCodes.Any() ? employeeCodes.Contains(x.Employee.EmployeeCode) : true)
                                                                && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod))
                                                     .ToListAsync();
        }
        public int GetRunningEmployeeProbationPeriod(string employeeCode)
        {
            return _dbContext.EmployeeContracts.Where(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active && x.Employee.EmployeeCode == employeeCode
                                                                        && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod))
                                                    .Select(x => x.ProbationPeriod).FirstOrDefault();
        }
        public int GetRunningEmployeeProbationPeriod(int? contractId)
        {
            return _dbContext.EmployeeContracts.Where(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active && x.Id == contractId
                                                                        && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod))
                                                    .Select(x => x.ProbationPeriod).FirstOrDefault();
        }
        public async Task<DateTime> GetRunningContractStarDate(int? employeeContractId)
        {
            return await _dbContext.EmployeeContracts.Where(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active && x.Id == employeeContractId).Select(x => x.ContractStartDate).FirstOrDefaultAsync();
        }
        public async Task<DateTime> GetRunningContractStarDateByEmployeeId(int? employeeId)
        {
            return await _dbContext.EmployeeContracts.Where(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active && x.EmployeeId == employeeId).Select(x => x.ContractStartDate).FirstOrDefaultAsync();
        }
        public (DateTime StartDate, DateTime EndDate)? GetRunningContractDates(int? employeeContractId)
        {
            var data = _dbContext.EmployeeContracts.Where(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active && x.Id == employeeContractId).FirstOrDefault();
            return (data == null ? null : (data.ContractStartDate, data.ContractEndDate));
        }
        public async Task<(DateTime StartDate, DateTime EndDate)?> GetRunningContractDates(string employeeCode)
        {
            var contract = await GetRunningEmployeeContract(employeeCode);
            var data = await _dbContext.EmployeeContracts.Where(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active && x.Id == contract.Id).FirstOrDefaultAsync();
            return (data == null ? null : (data.ContractStartDate, data.ContractEndDate));
        }
        public async Task<int?> GetTotalRunningContract(string employeeCode)
        {
            return await _dbContext.EmployeeContracts.Include(x => x.Employee).CountAsync(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active
                                                                                            && x.Employee.EmployeeCode == employeeCode && x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running);
        }
        public async Task<int?> GetTotalContract(string employeeCode)
        {
            return await _dbContext.EmployeeContracts.Include(x => x.Employee).CountAsync(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active && x.Employee.EmployeeCode == employeeCode);
        }
        public async Task<IEnumerable<int>> GetAllCurrentRunningContractIds()
        {
            return await _dbContext.EmployeeContracts.Where(x => x.RecordStatus == RecordStatus.Active
                                                        && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod))
                                            .Select(x => x.Id).ToListAsync();
        }
        public async Task<IEnumerable<EmployeeContract>> GetAllCurrentRunningContract()
        {
            return await _dbContext.EmployeeContracts.Where(x => x.RecordStatus == RecordStatus.Active
                                                        && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod))
                                                     .ToListAsync();
        }
        public async Task<bool> CheckCurrentContract(int? employeeContractId)
        {
            return await _dbContext.EmployeeContracts.AnyAsync(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active && x.Id == employeeContractId
                                                                    && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod));
        }
        public async Task<EmployeeContract> GetPreviousEmployeeContracts(string employeeCode, int year)
        {
            return await _dbContext.EmployeeContracts.Include(x => x.EmployeeFixGross).Where(x => x.RecordStatus == RecordStatus.Active && x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Completed && x.Employee.EmployeeCode == employeeCode && x.ContractEndDate.Year == year).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<int>> GetContractYears(string employeeCode, bool fetchCurrent = false)
        {
            var data = await _dbContext.EmployeeContracts.Where(x => x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active
                                                            && (fetchCurrent ? (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod) : true)
                                                            && x.Employee.EmployeeCode == employeeCode).Select(x => new { StartYear = x.ContractStartDate.Year, EndYear = x.ContractEndDate.Year }).ToListAsync();
            var result = new List<int>();
            foreach (var item in data)
            {
                var years = _generalUtilities.GetYearRange(item.StartYear, item.EndYear);
                foreach (var subitem in years)
                {
                    result.Add(subitem);
                }
            }

            return result.Distinct().ToList();
        }
        public async Task<DateTime?> GetActualContractStartDate(string employeeCode)
        {
            var contract = await GetRunningEmployeeContract(employeeCode);
            if (contract != null && contract.ProbationPeriod > 0 && contract.TrainingPeriod > 0)
            {
                return contract.ContractStartDate.AddMonths(contract.ProbationPeriod + contract.TrainingPeriod);
            }
            return null;
        }
        public string GetRunningContractStatus(string employeeCode)
        {
            return _dbContext.EmployeeContracts.Include(x => x.EmployeeFixGross)
                                                     .Include(x => x.Employee)
                                                     .Where(x => x.Employee.EmployeeCode == employeeCode && (x.EmployeeContractStatusId != (int)EmployeeContractStatusType.Running))
                                                     .Select(x => x.EmployeeContractStatus.StatusType).FirstOrDefault();
        }
        public string GetContractStatus(string employeeCode)
        {
            return _dbContext.EmployeeContracts.Where(x => x.Employee.EmployeeCode == employeeCode).OrderByDescending(x => x.ContractEndDate).Select(x => x.EmployeeContractStatus.StatusType).FirstOrDefault();
        }
        public async Task<EmployeeContract> GetEmployeeContract(int id)
        {
            return await _dbContext.EmployeeContracts.Include(x => x.EmployeeFixGross).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<int?> GetEmployeeEarningGrossId(int? employeeContractId, bool partlyPaid = false)
        {
            return await _dbContext.EmployeeEarningGross.Where(x => x.EmployeeContractId == employeeContractId && x.PartlyPaid == partlyPaid).Select(x => x.Id).FirstOrDefaultAsync();
        }
        #endregion General Methods
    }
}