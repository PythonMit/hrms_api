using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.Leave;
using HRMS.Core.Utilities.General;
using HRMS.DBL.DbContextConfiguration;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace HRMS.DBL.Stores
{
    public class EmployeeLeaveStore : BaseStore
    {
        private readonly IGeneralUtilities _generalUtilities;
        private readonly HolidayStore _holidayStore;
        private readonly EmployeeContractStore _employeeContractStore;
        private readonly EmployeeStore _employeeStore;

        public EmployeeLeaveStore(HRMSDbContext dbContext, IGeneralUtilities generalUtilities, HolidayStore holidayStore, EmployeeContractStore employeeContractStore, EmployeeStore employeeStore) : base(dbContext)
        {
            _generalUtilities = generalUtilities;
            _holidayStore = holidayStore;
            _employeeContractStore = employeeContractStore;
            _employeeStore = employeeStore;
        }

        #region Leave Balance
        public async Task<(IEnumerable<int> leaveIds, IEnumerable<LeaveTransactionRequestModel> transactions)?> GenerateEmployeeLeaveWithoutTransactions(IEnumerable<EmployeeLeaveRequestModel> model, string employeeCode)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var Ids = new List<int>();
                var contract = await _employeeContractStore.GetRunningEmployeeContract(employeeCode);
                if (contract == null)
                {
                    return null;
                }

                var leaveTransactions = new List<LeaveTransactionRequestModel>();
                foreach (var item in model)
                {
                    var data = await _dbContext.EmployeeLeaves.FirstOrDefaultAsync(x => x.EmployeeContractId == contract.Id && x.LeaveTypeId == item.LeaveTypeId);
                    if (data == null)
                    {
                        data = new EmployeeLeave();
                    }

                    if (item.LeaveTypeId == (int)LeaveTypes.CarryForward)
                    {
                        item.TotalLeaves += (await GetLastContractRemainingLeaves(contract.Id) ?? 0);
                    }

                    data.EmployeeContractId = contract.Id;
                    data.LeaveTypeId = item.LeaveTypeId;
                    data.TotalLeaves = item.TotalLeaves;
                    var newStartDate = contract.ContractStartDate;
                    if ((contract.DesignationTypeId == (int)DesignationTypes.ProjectTrainee && contract.TrainingPeriod > 0) || (contract.DesignationTypeId != (int)DesignationTypes.ProjectTrainee && contract.TrainingPeriod > 0 && contract.IsProjectTrainee))
                    {
                        newStartDate = contract.ContractStartDate.AddMonths(contract.TrainingPeriod);
                    }
                    data.LeaveStartDate = newStartDate;
                    data.LeaveEndDate = contract.ContractEndDate;

                    if (item.LeaveTypeId != (int)LeaveTypes.LeaveWithoutPay)
                    {
                        var probationDate = contract.ContractStartDate.AddMonths(contract.ProbationPeriod);
                        if (item.LeaveTypeId == (int)LeaveTypes.FlatLeave)
                        {
                            var endDate = (contract.EmployeeContractStatusId == (int)EmployeeContractStatusType.Completed ||
                                            ((contract.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || contract.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod)
                                                && DateTime.UtcNow > contract.ContractEndDate) ? contract.ContractEndDate.AddMonths(-1) : DateTime.UtcNow);

                            var dates = _generalUtilities.GetMonthlyDates(newStartDate.AddMonths(-1), endDate);
                            dates = dates.GroupBy(x => new { x.Month, x.Year }).Select(x => x.FirstOrDefault()).Where(x => x != DateTime.MinValue).ToList();
                            foreach (var subitem in dates)
                            {
                                if (newStartDate > subitem)
                                {
                                    continue;
                                }

                                leaveTransactions.Add(new LeaveTransactionRequestModel
                                {
                                    EmployeeCode = employeeCode,
                                    LeaveTypeId = item.LeaveTypeId,
                                    Added = (item.LeaveTypeId == (int)LeaveTypes.CarryForward ? item.TotalLeaves : 0),
                                    HasProbationPeriod = contract.ProbationPeriod > 0 ? (probationDate.Date > subitem.Date) : false,
                                    TransactionDate = subitem,
                                    LeavePeriod = (contract.ProbationPeriod > 0 ? _generalUtilities.GetActualContractPeriod(newStartDate, contract.ContractEndDate, contract.ProbationPeriod) : 0)
                                });
                            }
                        }
                        else
                        {
                            leaveTransactions.Add(new LeaveTransactionRequestModel
                            {
                                EmployeeCode = employeeCode,
                                LeaveTypeId = item.LeaveTypeId,
                                Added = (item.LeaveTypeId == (int)LeaveTypes.CarryForward ? item.TotalLeaves : 0),
                                HasProbationPeriod = contract.ProbationPeriod > 0 ? (probationDate.Date > DateTime.UtcNow.Date) : false,
                                TransactionDate = newStartDate,
                            });
                        }
                    }

                    _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                    await _dbContext.SaveChangesAsync();

                    Ids.Add(data.Id);
                }

                if (Ids != null && Ids.Any())
                {
                    transaction.Complete();
                    return (leaveIds: Ids, transactions: leaveTransactions);
                }
            }
            return null;
        }
        public async Task<IEnumerable<int>> GenerateEmployeeLeave(IEnumerable<EmployeeLeaveRequestModel> model, string employeeCode)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var Ids = new List<int>();
                var contract = await _employeeContractStore.GetRunningEmployeeContract(employeeCode);
                if (contract == null)
                {
                    return null;
                }

                var leaveTransactions = new List<LeaveTransactionRequestModel>();
                foreach (var item in model)
                {
                    var data = await _dbContext.EmployeeLeaves.FirstOrDefaultAsync(x => x.EmployeeContractId == contract.Id && x.LeaveTypeId == item.LeaveTypeId);
                    if (data == null)
                    {
                        data = new EmployeeLeave();
                    }

                    if (item.LeaveTypeId == (int)LeaveTypes.CarryForward)
                    {
                        item.TotalLeaves = (await GetLastContractRemainingLeaves(contract.Id) ?? 0);
                    }

                    var probationPeriod = _employeeContractStore.GetRunningEmployeeProbationPeriod(contract.Id);
                    data.EmployeeContractId = contract.Id;
                    data.LeaveTypeId = item.LeaveTypeId;
                    data.TotalLeaves = item.TotalLeaves;
                    var newStartDate = contract.ContractStartDate;
                    if (contract.DesignationTypeId == (int)DesignationTypes.ProjectTrainee && contract.TrainingPeriod > 0)
                    {
                        newStartDate = contract.ContractStartDate.AddMonths(contract.TrainingPeriod);
                    }
                    data.LeaveEndDate = contract.ContractEndDate;

                    _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                    await _dbContext.SaveChangesAsync();

                    if (item.LeaveTypeId != (int)LeaveTypes.LeaveWithoutPay)
                    {
                        var probationDate = newStartDate.AddMonths(probationPeriod);
                        var endDate = (contract.EmployeeContractStatusId == (int)EmployeeContractStatusType.Completed ? contract.ContractEndDate : DateTime.UtcNow);
                        if (item.LeaveTypeId == (int)LeaveTypes.FlatLeave)
                        {
                            var dates = _generalUtilities.GetMonthlyDates(newStartDate.AddMonths(-1), endDate);
                            foreach (var subitem in dates)
                            {
                                leaveTransactions.Add(new LeaveTransactionRequestModel
                                {
                                    EmployeeCode = employeeCode,
                                    LeaveTypeId = item.LeaveTypeId,
                                    Added = (item.LeaveTypeId == (int)LeaveTypes.CarryForward ? item.TotalLeaves : 0),
                                    HasProbationPeriod = probationPeriod > 0 ? (probationDate.Date > subitem.Date) : false,
                                    TransactionDate = newStartDate,
                                });
                            }
                        }
                        else
                        {
                            leaveTransactions.Add(new LeaveTransactionRequestModel
                            {
                                EmployeeCode = employeeCode,
                                LeaveTypeId = item.LeaveTypeId,
                                Added = (item.LeaveTypeId == (int)LeaveTypes.CarryForward ? item.TotalLeaves : 0),
                                HasProbationPeriod = probationPeriod > 0 ? (probationDate.Date > DateTime.UtcNow.Date) : false,
                                TransactionDate = newStartDate,
                            });
                        }
                    }
                    Ids.Add(data.Id);
                }

                if (Ids != null && Ids.Any())
                {
                    await GenerateLeaveTransactions(leaveTransactions, employeeCode);
                    transaction.Complete();
                    return Ids;
                }
            }
            return null;
        }
        public async Task<IQueryable<EmployeeLeave>> GetEmployeeLeaveBalanceList(EmployeeLeaveFilterModel filter, int? employeeId = null, int? employeeContractId = null, RoleTypes? userRole = null)
        {
            return _dbContext.EmployeeLeaves.Include(x => x.EmployeeContract).ThenInclude(x => x.Employee)
                                            .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.DesignationType)
                                            .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.Branch)
                                            .Include(x => x.LeaveType)
                                        .Where(x => (userRole == RoleTypes.SuperAdmin ? true : !GenericConst.AnchoredEmployeeCode.Contains(x.EmployeeContract.Employee.EmployeeCode))
                                                && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active && (employeeId.HasValue ? x.EmployeeContract.Employee.Id == employeeId : true)
                                                && (x.EmployeeContract.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContract.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod)
                                                && (employeeContractId.HasValue ? x.EmployeeContractId == employeeContractId : true)
                                                && (filter == null ? true :
                                                    ((string.IsNullOrEmpty(filter.SearchString) ? true :
                                                        (x.EmployeeContract.Employee.FirstName.Contains(filter.SearchString))
                                                        || (x.EmployeeContract.Employee.LastName.Contains(filter.SearchString))
                                                        || (x.EmployeeContract.Employee.Branch.Name.Contains(filter.SearchString))
                                                        || (x.EmployeeContract.Employee.DesignationType.Name.Contains(filter.SearchString)))
                                                    && (filter.Branch != null && filter.Branch.Any() ? filter.Branch.Contains(x.EmployeeContract.Employee.BranchId) : true))))
                                        .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id).AsNoTracking().AsSplitQuery();
        }
        public async Task<double?> GetCurrentEmployeeLeaveBalance(int contractId, int? leaveTypeId)
        {
            var data = await _dbContext.EmployeeLeaves.FirstOrDefaultAsync(x => x.EmployeeContractId == contractId && x.LeaveTypeId == leaveTypeId);
            return (data?.TotalLeaves - data?.TotalLeavesTaken) ?? 0;
        }
        public async Task<double?> GetCurrentEmployeeTotalLeaves(int contractId, int? leaveTypeId)
        {
            var data = await _dbContext.EmployeeLeaves.FirstOrDefaultAsync(x => x.EmployeeContractId == contractId && x.LeaveTypeId == leaveTypeId);
            return data?.TotalLeaves ?? 0;
        }
        public async Task<IQueryable<Employee>> GetRaminingEmployeeForLeaves(RoleTypes? userRole)
        {
            var contractIds = await _dbContext.EmployeeContracts.Where(x => x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running && x.Employee.RecordStatus == RecordStatus.Active && x.RecordStatus == RecordStatus.Active).Select(x => x.Id).ToListAsync();
            var exitsingIds = await _dbContext.EmployeeLeaves.Select(x => x.EmployeeContractId).Distinct().ToListAsync();
            var remainingsIds = contractIds.Except(exitsingIds);
            if (remainingsIds != null && remainingsIds.Any())
            {
                var employeeIds = await _dbContext.EmployeeContracts.Where(x => (userRole == RoleTypes.SuperAdmin ? true : !GenericConst.AnchoredEmployeeCode.Contains(x.Employee.EmployeeCode))
                                                && remainingsIds.Contains(x.Id) && x.Employee.RecordStatus == RecordStatus.Active && x.RecordStatus == RecordStatus.Active).Select(x => x.Employee.Id).Distinct().ToListAsync();
                return _dbContext.Employees.Include(x => x.DesignationType).Where(x => employeeIds.Contains(x.Id));
            }

            return null;
        }
        public DateTime? GetEmployeeJoindate(int? employeeId)
        {
            return _dbContext.EmployeeDetails.Where(x => x.EmployeeId == employeeId && x.Employee.RecordStatus == RecordStatus.Active).Select(x => x.JoinDate).FirstOrDefault();
        }
        public async Task<IQueryable<Employee>> GetLeaveAcquiredEmployee()
        {
            var employeeIds = await _dbContext.EmployeeLeaves.Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.DesignationType)
                                            .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee)
                                            .Include(x => x.LeaveType)
                                        .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id).Select(x => x.EmployeeContract.EmployeeId).ToListAsync();
            return _dbContext.Employees.Where(x => x.RecordStatus == RecordStatus.Active && employeeIds.Contains(x.Id)).AsNoTracking().AsSplitQuery();
        }
        public async Task<double?> GetCurrentEmployeeTotalLeavesTaken(int? contractId, int leaveTypeId)
        {
            var data = await _dbContext.EmployeeLeaves.FirstOrDefaultAsync(x => x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active && x.EmployeeContractId == contractId && x.LeaveTypeId == leaveTypeId);
            return data?.TotalLeavesTaken ?? 0;
        }
        public async Task<int> GetLeaveApplicationSandwichDays(SandwichDateRequestModel model)
        {
            var contract = await _employeeContractStore.GetRunningEmployeeContract(model.EmployeeCode);
            if (contract == null)
            {
                return 0;
            }

            var data = await _dbContext.EmployeeLeaveApplications.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.EmployeeContractId == contract.Id
                                                                && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active && x.RecordStatus == RecordStatus.Active
                                                                && x.EmployeeLeaveStatusId == (int)EmployeeLeaveStatusType.Approved);
            if (data == null)
            {
                return 0;
            }

            var toDate = data.LeaveToDate.Value;
            var tR = _generalUtilities.CheckDateInPreviousWeek(toDate);
            if (!tR)
            {
                return 0;
            }

            var dates = _generalUtilities.GetDateRange(data.LeaveToDate.Value, model.LeaveStartDate);
            if (dates != null && dates.Count() > 3)
            {
                return 0;
            }

            var dateOfBith = await _employeeStore.GetEmployeeDateOfBirth(model.EmployeeCode);

            var result = 0;
            foreach (var item in dates)
            {
                var holiday = await _holidayStore.GetEmployeeHolidays(item);
                if (!item.HasValue)
                {
                    continue;
                }

                var date = item.Value;

                if (dateOfBith.Date == date.Date)
                {
                    continue;
                }

                if (date.DayOfWeek == DayOfWeek.Sunday && (holiday != null && holiday?.StartDate.Date == date.Date || holiday?.EndDate.Date == date.Date))
                {
                    result++;
                }
                else if (date.DayOfWeek == DayOfWeek.Saturday && (holiday != null && holiday?.StartDate.Date == date.Date || holiday?.EndDate.Date == date.Date))
                {
                    if (model.ConsiderAllSaturday)
                    {
                        result++;
                    }
                    else
                    {
                        var t = _generalUtilities.FindDay(date.Year, date.Month, DayOfWeek.Saturday, 3);
                        result = (date.Day == t ? (result + 1) : result);
                    }
                }
                else if (holiday != null && holiday?.StartDate.Date == date.Date || holiday?.EndDate.Date == date.Date)
                {
                    result++;
                }
            }

            return result;
        }
        public async Task<double?> GetAvailableLeaveCount(int? contractId)
        {
            return await _dbContext.EmployeeLeaveTransactions.OrderByDescending(x => x.CreatedDateTimeUtc).Where(x => x.EmployeeContractId == contractId).Select(x => x.Balance).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<string>> GetLeaveBalanceCalibrate()
        {
            var contract = await _employeeContractStore.GetAllCurrentRunningContract();
            var Ids = contract?.Select(x => x.Id).ToList();
            var data = await _dbContext.EmployeeLeaves.Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).Where(x => x.LeaveTypeId == (int)LeaveTypes.FlatLeave && Ids.Contains(x.EmployeeContractId)).ToListAsync(); // .GroupBy(x => x.EmployeeContractId).Select(x => x.FirstOrDefault())
            if (data == null)
            {
                return null;
            }

            var result = new List<string>();
            foreach (var item in data)
            {
                var dataCount = await _dbContext.EmployeeLeaveTransactions.CountAsync(x => x.EmployeeContractId == item.EmployeeContractId);
                if (dataCount <= 0)
                {
                    continue;
                }

                var totalAcc = await _dbContext.EmployeeLeaveTransactions.Where(x => x.EmployeeContractId == item.EmployeeContractId && x.TransactionType == Enum.GetName(typeof(TransactionType), TransactionType.ACC)).SumAsync(x => x.Added);
                if (totalAcc == item.TotalLeaves)
                {
                    continue;
                }

                var exists = await _dbContext.EmployeeLeaveTransactions.AnyAsync(x => x.EmployeeContractId == item.EmployeeContractId && x.TransactionDate.Value.Month == DateTime.UtcNow.Month && x.TransactionType == Enum.GetName(typeof(TransactionType), TransactionType.ACC));
                if (exists)
                {
                    continue;
                }

                var probationPeriod = contract.Where(x => x.Id == item.EmployeeContractId).Select(x => x.ProbationPeriod).FirstOrDefault();
                var startDate = contract.Where(x => x.Id == item.EmployeeContractId).Select(x => x.ContractStartDate).FirstOrDefault();
                //var exists = await _dbContext.EmployeeLeaveTransactions.FirstOrDefaultAsync(x => x.EmployeeContractId == item.EmployeeContractId && x.TransactionDate.Value.Month == DateTime.UtcNow.Month && x.TransactionType == Enum.GetName(typeof(TransactionType), TransactionType.ACC));
                //if (exists != null)
                //{
                //var probationDate = startDate.AddMonths(probationPeriod);
                //if (probationDate.Date >= exists.TransactionDate.Value.Date && exists.TransactionDate.Value.Month == probationDate.Month && probationDate.Day <= 20)
                //{
                //    await UpdateEmployeeLeaveTransactions(new LeaveTransactionUpdateRequestModel
                //    {
                //        Added = exists.Added,
                //        Balance = exists.Balance,
                //        Description = exists.Description,B
                //        LWP = exists.LWP,
                //        Id = exists.Id,
                //        Used = exists.Used,
                //    });
                //}
                //else
                //{
                //continue;
                //}
                //}

                var trainingPeriod = contract.Where(x => x.Id == item.EmployeeContractId).Select(x => x.TrainingPeriod).FirstOrDefault();
                var designationTypeId = contract.Where(x => x.Id == item.EmployeeContractId).Select(x => x.DesignationTypeId).FirstOrDefault();
                var endDate = contract.Where(x => x.Id == item.EmployeeContractId).Select(x => x.ContractEndDate).FirstOrDefault();
                if (designationTypeId == (int)DesignationTypes.ProjectTrainee && trainingPeriod > 0 && startDate.AddMonths(trainingPeriod) > DateTime.UtcNow)
                {
                    continue;
                }

                var tranactions = new LeaveTransactionRequestModel();
                tranactions.IsReCalculate = true;
                tranactions.EmployeeCode = item.EmployeeContract.Employee.EmployeeCode;
                tranactions.HasProbationPeriod = (probationPeriod > 0 ? (startDate.AddMonths(probationPeriod) >= DateTime.UtcNow) : false);
                tranactions.LeavePeriod = (probationPeriod > 0 ? _generalUtilities.GetActualContractPeriod(startDate, endDate, probationPeriod) : 0);

                if (await GenerateLeaveTransactions(tranactions, item.EmployeeContractId) == true)
                {
                    result.Add($"{item.EmployeeContract.Employee.EmployeeCode} - {item.EmployeeContract.Employee.FirstName} {item.EmployeeContract.Employee.LastName}");
                }
            }

            return result;
        }
        public async Task<bool> DeleteLeaveBalanceInformations(string employeeCode)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                var contract = await _employeeContractStore.GetRunningEmployeeContract(employeeCode);
                var data = _dbContext.EmployeeLeaves.FirstOrDefaultAsync(x => x.EmployeeContractId == contract.Id);
                if (data != null)
                {
                    _dbContext.Remove(data);
                    transaction.Complete();
                    return true;
                }
                return false;
            }
        }
        #endregion Leave Balance
        #region Leave Details
        public async Task<IQueryable<EmployeeLeaveApplication>> GetEmployeeLeaveDetails(Guid? id, int? employeeContractId = null, EmployeeLeaveStatusType? status = EmployeeLeaveStatusType.Approved, int? previousMonths = null)
        {
            return _dbContext.EmployeeLeaveApplications.Include(x => x.EmployeeContract).ThenInclude(x => x.Employee)
                                                        .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.Branch)
                                                        .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.DesignationType)
                                                        .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.ImagekitDetail)
                                                        .Include(x => x.EmployeeLeaveStatus)
                                                        .Include(x => x.LeaveType)
                                                        .Include(x => x.EmployeeLeaveApplicationManagers)
                                                        .Include(x => x.Employee).ThenInclude(x => x.Branch)
                                                        .Include(x => x.Employee).ThenInclude(x => x.DesignationType)
                                                        .Include(x => x.LeaveCategory)
                                                    .Where(x => (id.HasValue ? x.Id == id : true) && (employeeContractId.HasValue ? x.EmployeeContractId == employeeContractId : true)
                                                                && (previousMonths.HasValue ? ((status == null ? true : x.EmployeeLeaveStatusId == (int)status)
                                                                    && DateTime.Compare(x.ApprovedDate.Value, DateTime.Today.AddMonths(-3)) >= 0) : true))
                                                    .AsNoTracking().AsSplitQuery();
        }
        public async Task<IQueryable<EmployeeContract>> GetEmployeeDetailsFromLeaveApplication(int? employeeContractId)
        {
            return _dbContext.EmployeeContracts.Include(x => x.Employee)
                                               .Include(x => x.Employee).ThenInclude(x => x.Branch)
                                               .Include(x => x.Employee).ThenInclude(x => x.DesignationType)
                                               .Include(x => x.Employee).ThenInclude(x => x.ImagekitDetail)
                                            .Where(x => (employeeContractId.HasValue ? x.Id == employeeContractId : true))
                                            .AsNoTracking().AsSplitQuery();
        }
        public async Task<IEnumerable<LeaveType>> GetLeaveType(IEnumerable<int> exceptId = null)
        {
            return await _dbContext.LeaveTypes.Where(x => exceptId != null && exceptId.Any() ? !exceptId.Contains(x.Id) : true).ToListAsync();
        }
        public async Task<IEnumerable<LeaveCategory>> GetLeaveCategories(IEnumerable<int> exceptId = null)
        {
            return await _dbContext.LeaveCategories.Where(x => exceptId != null && exceptId.Any() ? !exceptId.Contains(x.Id) : true).ToListAsync();
        }
        #endregion Leave Details
        #region Leave Transactions
        public async Task<bool?> GenerateLeaveTransactions(LeaveTransactionRequestModel model, int? contractId = null)
        {
            double? availableLeaves = 0;
            DateTime? transacationDate = null;
            string description = "", transactionType = "";

            if (model.Added == null || model.Used == null || model.LWP == null)
            {
                return null;
            }

            var employeeContractId = (contractId ?? 0);
            if (!contractId.HasValue)
            {
                var contract = await _employeeContractStore.GetRunningEmployeeContract(model.EmployeeCode);
                employeeContractId = (contract?.Id ?? 0);
            }

            var leaveCounts = await GetCurrentEmployeeTotalLeaves(employeeContractId, (int)LeaveTypes.FlatLeave) ?? 0;
            var totalContract = await _employeeContractStore.GetTotalContract(model.EmployeeCode);
            var baseLeaveAmount = Math.Round((leaveCounts / 12), 2);

            if (model.LeavePeriod > 0)
            {
                baseLeaveAmount = Math.Round((leaveCounts / model.LeavePeriod), 2);
            }

            var data = new EmployeeLeaveTransaction();
            if (!model.IsReCalculate)
            {
                var dataCount = await _dbContext.EmployeeLeaveTransactions.CountAsync(x => x.EmployeeContractId == employeeContractId);
                if (totalContract > 1 && model.LeaveTypeId == (int)LeaveTypes.CarryForward)
                {
                    availableLeaves = (model.Added == null || (model.Added != null && model.Added == 0)) ? (await GetLastContractRemainingLeaves(employeeContractId) ?? 0) : model.Added;
                    description = $"Leave balance carry forward from previous contract";
                    transactionType = Enum.GetName(typeof(TransactionType), TransactionType.CFL);
                }

                if (model.LeaveTypeId == (int)LeaveTypes.FlatLeave && dataCount >= 1)
                {
                    model.Added = baseLeaveAmount;
                }
            }
            else
            {
                var exists = await _dbContext.EmployeeLeaveTransactions.AnyAsync(x => x.EmployeeContractId == employeeContractId && x.TransactionDate.Value.Month == DateTime.UtcNow.Month
                                                                                      && x.TransactionType == Enum.GetName(typeof(TransactionType), TransactionType.ACC));
                if (!exists)
                {
                    model.Added = baseLeaveAmount;
                }
                else
                {
                    return false;
                }
            }

            var lastTransaction = await GetLastEmployeeLeaveTransaction(employeeContractId);
            if (model.Added > 0 && transactionType != Enum.GetName(typeof(TransactionType), TransactionType.CFL))
            {
                description = $"New Leave balance - {DateTime.UtcNow.ToString("Y")}";
                availableLeaves = (lastTransaction?.Balance ?? 0) + (model.HasProbationPeriod ? 0 : model.Added);
                transactionType = Enum.GetName(typeof(TransactionType), TransactionType.ACC);
                transacationDate = DateTime.UtcNow;
            }
            else if (model.Used > 0)
            {
                description = $"Leave approved - {model.TransactionDate.Value.ToString("dd MMMM yyyy hh:mm:ss tt")}, Ref leave application #{model.LeaveApplicationId}";
                availableLeaves = (lastTransaction?.Balance ?? 0) - (model.HasProbationPeriod ? 0 : model.Used);
                transactionType = Enum.GetName(typeof(TransactionType), TransactionType.USE);
                transacationDate = model.TransactionDate;
            }
            else if (model.LWP > 0)
            {
                description = $"Leave declined and considered LWP - {model.TransactionDate.Value.ToString("Y")}, Ref leave application #{model.LeaveApplicationId}";
                transactionType = Enum.GetName(typeof(TransactionType), TransactionType.LWP);
                transacationDate = DateTime.UtcNow;
            }

            data.EmployeeContractId = employeeContractId;
            data.TransactionDate = transacationDate;
            data.Balance = availableLeaves;
            data.Added = (model.HasProbationPeriod ? 0 : model.Added);
            data.Used = (model.HasProbationPeriod ? 0 : model.Used);
            data.LWP = model.LWP;
            data.Description = description;
            data.TransactionType = transactionType;

            _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool?> GenerateLeaveTransactions(IEnumerable<LeaveTransactionRequestModel> dataList, string employeeCode)
        {
            if (dataList == null || (dataList != null && dataList.Count() == 0))
            {
                return false;
            }

            double? availableLeaves = 0;
            DateTime? transacationDate = null;
            string description = "", transactionType = "";

            if (string.IsNullOrEmpty(employeeCode))
            {
                employeeCode = dataList?.Select(x => x.EmployeeCode).FirstOrDefault();
            }

            //var totalContract = await _employeeContractStore.GetTotalContract(employeeCode);
            var contract = await _employeeContractStore.GetRunningEmployeeContract(employeeCode);
            var employeeContractId = (contract?.Id ?? 0);
            var leaveCounts = await GetCurrentEmployeeTotalLeaves(employeeContractId, (int)LeaveTypes.FlatLeave) ?? 0;
            var baseLeaveAmount = Math.Round((leaveCounts / 12), 2);

            foreach (var item in dataList)
            {
                if (item.Added == null || item.Used == null || item.LWP == null)
                {
                    continue;
                }

                if (item.LeavePeriod > 0)
                {
                    baseLeaveAmount = Math.Round((leaveCounts / item.LeavePeriod), 2);
                }

                var data = new EmployeeLeaveTransaction();
                if (!item.IsReCalculate)
                {
                    var dataCount = await _dbContext.EmployeeLeaveTransactions.CountAsync(x => x.EmployeeContractId == employeeContractId);
                    if (item.LeaveTypeId == (int)LeaveTypes.CarryForward)
                    {
                        availableLeaves = (item.Added == null || (item.Added != null && item.Added == 0)) ? (await GetLastContractRemainingLeaves(employeeContractId) ?? 0) : item.Added;
                        description = $"Leave balance carry forward from previous contract";
                        transactionType = Enum.GetName(typeof(TransactionType), TransactionType.CFL);
                        transacationDate = item.TransactionDate;
                    }

                    if (item.LeaveTypeId == (int)LeaveTypes.FlatLeave && dataCount >= 1)
                    {
                        item.Added = baseLeaveAmount;
                    }
                }
                else
                {
                    var exists = await _dbContext.EmployeeLeaveTransactions.AnyAsync(x => x.EmployeeContractId == employeeContractId && x.TransactionDate.Value.Month == DateTime.UtcNow.Month
                                                                                                            && x.TransactionType == Enum.GetName(typeof(TransactionType), TransactionType.ACC));
                    if (!exists)
                    {
                        item.Added = baseLeaveAmount;
                    }
                }

                var lastTransaction = await GetLastEmployeeLeaveTransaction(employeeContractId);
                if (item.Added > 0 && string.IsNullOrEmpty(transactionType) && transactionType != Enum.GetName(typeof(TransactionType), TransactionType.CFL))
                {
                    description = $"New Leave balance - {item.TransactionDate.Value.ToString("Y")}";
                    availableLeaves = (lastTransaction?.Balance ?? 0) + (item.HasProbationPeriod ? 0 : item.Added);
                    transactionType = Enum.GetName(typeof(TransactionType), TransactionType.ACC);
                    transacationDate = item.TransactionDate.Value;
                }
                else if (item.Used > 0)
                {
                    description = $"Leave approved - {item.TransactionDate.Value.ToString("dd MMMM yyyy hh:mm:ss tt")}, Ref leave application #{item.LeaveApplicationId}";
                    availableLeaves = (lastTransaction?.Balance ?? 0) - (item.HasProbationPeriod ? 0 : item.Used);
                    transactionType = Enum.GetName(typeof(TransactionType), TransactionType.USE);
                    transacationDate = item.TransactionDate.Value;
                }
                else if (item.LWP > 0)
                {
                    description = $"Leave declined and considered LWP - {item.TransactionDate.Value.ToString("Y")}, Ref leave application #{item.LeaveApplicationId}";
                    transactionType = Enum.GetName(typeof(TransactionType), TransactionType.LWP);
                    transacationDate = item.TransactionDate.Value;
                }

                data.EmployeeContractId = employeeContractId;
                data.TransactionDate = transacationDate;
                data.Balance = availableLeaves;
                data.Added = (item.HasProbationPeriod ? 0 : item.Added);
                data.Used = (item.HasProbationPeriod ? 0 : item.Used);
                data.LWP = item.LWP;
                data.Description = description;
                data.TransactionType = transactionType;

                transactionType = "";
                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
        public async Task<IEnumerable<EmployeeLeaveTransaction>> GetEmployeeLeaveTransactions(string employeeCode)
        {
            var contract = await _employeeContractStore.GetRunningEmployeeContract(employeeCode);
            return await _dbContext.EmployeeLeaveTransactions.Where(x => x.EmployeeContractId == contract.Id).ToListAsync();
        }
        public async Task<double?> GetEmployeeLeaveTransactionsCount(int contractId)
        {
            return await _dbContext.EmployeeLeaveTransactions.Where(x => x.EmployeeContractId == contractId && x.TransactionType == Enum.GetName(typeof(TransactionType), TransactionType.ACC)).SumAsync(x => x.Added);
        }
        private async Task<EmployeeLeaveTransaction> GetLastEmployeeLeaveTransaction(int employeeContractId)
        {
            return await _dbContext.EmployeeLeaveTransactions.OrderByDescending(x => x.CreatedDateTimeUtc).FirstOrDefaultAsync(x => x.EmployeeContractId == employeeContractId);
        }
        public async Task<bool> UpdateEmployeeLeaveTransactions(LeaveTransactionUpdateRequestModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeLeaveTransactions.FirstOrDefaultAsync(x => x.Id == model.Id && x.RecordStatus == RecordStatus.Active);
                if (data == null)
                {
                    return false;
                }

                data.Added = model.Added;
                data.LWP = model.LWP;
                data.Used = model.Used;
                data.Balance = model.Balance;

                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return true;
            }
        }
        public async Task<EmployeeLeaveTransaction> AddEmployeeLeaveTransactions(LeaveTransactionUpdateRequestModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var contract = await _employeeContractStore.GetRunningEmployeeContract(model.EmployeeCode);
                if (contract == null)
                {
                    return null;
                }
                var leaveTransaction = await _dbContext.EmployeeLeaveTransactions.OrderByDescending(x => x.CreatedDateTimeUtc).FirstOrDefaultAsync(x => x.EmployeeContractId == contract.Id && x.RecordStatus == RecordStatus.Active);

                var data = new EmployeeLeaveTransaction();
                if (model.Added > 0)
                {
                    data.Added = model.Added;
                    data.Used = 0;
                    data.Balance = leaveTransaction.Balance + model.Added;
                    data.TransactionType = Enum.GetName(typeof(TransactionType), TransactionType.ADJ);
                }
                else if (model.Used > 0)
                {
                    data.Used = model.Used;
                    data.Added = 0;
                    data.Balance = leaveTransaction.Balance - model.Used;
                    data.TransactionType = Enum.GetName(typeof(TransactionType), TransactionType.USE);
                }
                data.LWP = 0;
                data.Description = model.Description;
                data.TransactionDate = DateTime.UtcNow;
                data.EmployeeContractId = contract.Id;

                await _dbContext.EmployeeLeaveTransactions.AddAsync(data);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data;
            }
        }
        #endregion Leave Transactions
        #region Other
        public async Task<double?> GetCurrentContractLeavesBalance(int? employeeContractId)
        {
            var contract = await _dbContext.EmployeeContracts.OrderByDescending(x => x.CreatedDateTimeUtc).FirstOrDefaultAsync(x => x.Id == employeeContractId
                                                                        && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod));
            if (contract == null)
            {
                return null;
            }
            var data = await _dbContext.EmployeeLeaveTransactions.OrderByDescending(x => x.CreatedDateTimeUtc).FirstOrDefaultAsync(x => x.EmployeeContractId == contract.Id);
            return data?.Balance;
        }
        private async Task<double?> GetLastContractRemainingLeaves(int employeeContractId)
        {
            var currentContract = await _dbContext.EmployeeContracts.OrderByDescending(x => x.CreatedDateTimeUtc).FirstOrDefaultAsync(x => x.Id == employeeContractId
                                                                                    && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod));
            if (currentContract == null)
            {
                return null;
            }

            var lastContractId = await _dbContext.EmployeeContracts.Where(x => x.EmployeeId == currentContract.EmployeeId && x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Completed)
                                                                    .OrderByDescending(x => x.CreatedDateTimeUtc).Select(x => x.Id).FirstOrDefaultAsync();

            var data = await _dbContext.EmployeeLeaveTransactions.OrderByDescending(x => x.CreatedDateTimeUtc).FirstOrDefaultAsync(x => x.EmployeeContractId == lastContractId);
            return data?.Balance;
        }
        #endregion Other
    }
}
