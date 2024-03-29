using AutoMapper;
using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Contract;
using HRMS.Core.Models.Salary;
using HRMS.Core.Utilities.General;
using HRMS.DBL.DbContextConfiguration;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace HRMS.DBL.Stores
{
    public class EmployeeSalaryStore : BaseStore
    {
        private readonly IGeneralUtilities _generalUtilities;
        private readonly IMapper _mapper;
        private readonly EmployeeContractStore _employeeContractStore;
        public EmployeeSalaryStore(HRMSDbContext dbContext, IGeneralUtilities generalUtilities, IMapper mapper, EmployeeContractStore employeeContractStore) : base(dbContext)
        {
            _generalUtilities = generalUtilities;
            _mapper = mapper;
            _employeeContractStore = employeeContractStore;
        }
        public async Task<IEnumerable<int>> AddorUpdateEmployeeSalary(IEnumerable<EmployeeSalaryRequestModel> requests)
        {
            var result = new List<int>();
            var earningGrossId = 0;
            double actualEarnings = 0;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (var item in requests)
                {
                    var contract = await _employeeContractStore.GetRunningEmployeeContract(item.EmployeeCode);
                    if (contract == null)
                    {
                        return null;
                    }

                    if (item.IsPartlyPaid)
                    {
                        var previousContract = await _employeeContractStore.GetPreviousEmployeeContracts(item.EmployeeCode, contract.ContractStartDate.Year);
                        if (previousContract == null)
                        {
                            return null;
                        }
                        contract = previousContract;
                    }

                    var data = await _dbContext.EmployeeEarningGross.FirstOrDefaultAsync(x => x.Id == item.Id && x.EmployeeContractId == contract.Id && x.RecordStatus == RecordStatus.Active);
                    if (data == null)
                    {
                        data = new EmployeeEarningGross();
                    }

                    var fixGross = _mapper.Map<EmployeeFixGrossModel>(contract.EmployeeFixGross);
                    if (fixGross != null)
                    {
                        fixGross.DesignationTypeId = contract.DesignationTypeId;
                    }
                    var earningGross = _generalUtilities.GetEarningGrossCalculation(fixGross, item.TotalDays, item.PaidDays);
                    if (earningGross == null)
                    {
                        return null;
                    }

                    int professionalTax = 0;
                    double totalDeductions = 0;

                    if (!item.IsPartlyPaid)
                    {
                        professionalTax = _generalUtilities.GetProfessionalTaxAmount(earningGross.TotalEarning);
                        totalDeductions = (professionalTax + item.TDS);
                    }

                    data.Basic = earningGross.Basic;
                    data.DA = earningGross.DA;
                    data.LTA = earningGross.LTA;
                    data.HRA = earningGross.HRA;
                    data.ConveyanceAllowance = earningGross.ConveyanceAllowance;
                    data.OtherAllowance = earningGross.OtherAllowance;
                    data.MedicalAllowance = earningGross.MedicalAllowance;
                    data.ChildEducation = earningGross.ChildEducation;
                    data.EmployeePF = 0;
                    data.EmployerPF = 0;
                    data.PT = professionalTax;
                    data.EmployeeContractId = contract.Id;
                    data.EmployeeEarningGrossStatusId = (int)EmployeeEarningGrossStatusType.InProcess;
                    data.OverTimeAmount = item.OverTimeAmount;
                    data.Remarks = item.Remarks;
                    data.SalaryMonth = item.Month;
                    data.TDS = item.TDS;
                    data.ESI = item.ESI;
                    data.FixIncentive = item.FixIncentive;
                    data.Incentive = item.Incentive;
                    data.LWP = item.LWP;
                    data.TotalDays = item.TotalDays;
                    data.PaidDays = item.PaidDays;
                    data.NetSalary = (earningGross.TotalEarning - totalDeductions);
                    data.CreatedBy = item.CreatedBy;
                    data.CalculationType = item.CalculationType.GetEnumDescriptionAttribute();
                    data.AdjustmentAmount = item.AdjustmentAmount;
                    data.PartlyPaid = item.IsPartlyPaid;
                    data.SalaryYear = item.Year.Value;

                    var incentiveAmount = (item.Incentive + item.FixIncentive);
                    await UpdateMonthlyIncentiveAmount(contract.Employee.EmployeeCode, item.Month, item.Year, incentiveAmount);

                    _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                    await _dbContext.SaveChangesAsync();
                    if (!item.IsPartlyPaid)
                    {
                        earningGrossId = data.Id;
                    }

                    //actualEarnings += earningGross.TotalEarning;
                    result.Add(data.Id);
                    var calculationType = requests.Count(x => x.CalculationType == CalculationType.Manual);
                    if (calculationType <= 1)
                    {
                        continue;
                    }
                }
                //if (result.Count == 2)
                //{
                //    await UpdateCertainEarningGross(earningGrossId, actualEarnings);
                //}
                transaction.Complete();
            }
            return result;
        }
        public async Task<IQueryable<EmployeeEarningGross>> GetEmployeeSalary(EmployeeSalaryFilterModel filter, int? employeeId = null)
        {
            return _dbContext.EmployeeEarningGross.Include(x => x.EmployeeContract).ThenInclude(x => x.EmployeeFixGross)
                                                  .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee)
                                                  .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.Branch)
                                                  .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.DesignationType)
                                                  .Include(x => x.EmployeeEarningGrossStatus)
                                                  .Include(x => x.Employee)
                                            .Join(_dbContext.EmployeeDetails,
                                                    eg => eg.EmployeeContract.Employee.Id,
                                                    ed => ed.EmployeeId,
                                                    (eg, ed) => new { Earning = eg, Details = ed })
                                            .Where(x => //x.Earning.RecordStatus == RecordStatus.Active
                                                        //&& x.Earning.EmployeeContract.Employee.RecordStatus == RecordStatus.Active
                                                        (employeeId.HasValue ? x.Earning.EmployeeContract.EmployeeId == employeeId : true)
                                                        && (filter == null ? true : (filter.Id.HasValue ? x.Earning.Id == filter.Id : true)
                                                            && ((string.IsNullOrEmpty(filter.SearchString) ? true : (x.Earning.EmployeeContract.Employee.FirstName.Contains(filter.SearchString)
                                                                                                                || x.Earning.EmployeeContract.Employee.LastName.Contains(filter.SearchString)
                                                                                                                || x.Earning.EmployeeContract.Employee.Branch.Name.Contains(filter.SearchString)
                                                                                                                || x.Earning.EmployeeContract.Employee.DesignationType.Name.Contains(filter.SearchString))))
                                                            && (filter.Branch != null && filter.Branch.Any() ? filter.Branch.Contains(x.Earning.EmployeeContract.Employee.BranchId.Value) : true)
                                                            && (filter.SalaryMonth != null && filter.SalaryMonth.Any() ? filter.SalaryMonth.Contains(x.Earning.SalaryMonth) : true)
                                                            && (filter.Status != null && filter.Status.Any() ? filter.Status.Contains(x.Earning.EmployeeEarningGrossStatusId) : true)
                                                            && (filter.Year != null && filter.Year.Any() ? filter.Year.Contains(x.Earning.SalaryYear) : true)))
                                            .OrderByDescending(x => x.Details.JoinDate).Select(x => x.Earning).AsQueryable();
        }
        public async Task<bool> DeleteEmployeeSalary(IEnumerable<int> ids)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeEarningGross.Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).Where(x => ids.Contains(x.Id)).ToListAsync();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        if (item != null && item.CalculationType == CalculationType.Manual.GetEnumDescriptionAttribute())
                        {
                            var manual = await _dbContext.EmployeeEarningGross.FirstOrDefaultAsync(x => x.Id != item.Id && x.RecordStatus == RecordStatus.Active
                                                                                            && x.EmployeeContract.Employee.EmployeeCode == item.EmployeeContract.Employee.EmployeeCode && x.SalaryMonth == item.SalaryMonth && x.PartlyPaid == true
                                                                                            && x.CalculationType == CalculationType.Manual.GetEnumDescriptionAttribute() && x.SalaryYear == item.SalaryYear);
                            if (manual != null)
                            {
                                _dbContext.EmployeeEarningGross.Remove(manual);
                            }
                        }

                        _dbContext.EmployeeEarningGross.Remove(item);
                        await _dbContext.SaveChangesAsync();

                        await UpdateMonthlyIncentiveAmount(item.EmployeeContract.Employee.EmployeeCode, item.SalaryMonth, item.SalaryYear, null, EmployeeIncentiveStatusType.Pending);
                    }
                    transaction.Complete();
                    return true;
                }
                return false;
            }
        }
        public async Task<bool> SetEmployeeSalaryStatus(IEnumerable<EmployeeSalaryStatusRequestModel> model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (var item in model)
                {
                    var data = await _dbContext.EmployeeEarningGross.Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).FirstOrDefaultAsync(x => x.Id == item.Id);
                    if (data != null)
                    {
                        if (data != null && data.CalculationType == CalculationType.Manual.GetEnumDescriptionAttribute())
                        {
                            var manual = await _dbContext.EmployeeEarningGross.FirstOrDefaultAsync(x => x.Id != data.Id && x.RecordStatus == RecordStatus.Active
                                                                                                && x.EmployeeContract.Employee.EmployeeCode == data.EmployeeContract.Employee.EmployeeCode && x.SalaryYear == data.SalaryYear
                                                                                                && x.CalculationType == CalculationType.Manual.GetEnumDescriptionAttribute() && x.SalaryMonth == data.SalaryMonth && x.PartlyPaid == true);
                            if (manual != null)
                            {
                                manual.EmployeeEarningGrossStatusId = (int)item.Status;
                                manual.PaidDate = DateTime.UtcNow;
                                _dbContext.Entry(manual).State = EntityState.Modified;
                                await _dbContext.SaveChangesAsync();
                            }
                        }
                        data.EmployeeEarningGrossStatusId = (int)item.Status;
                        data.Remarks = item.Remarks;
                        if (item.Status == EmployeeEarningGrossStatusType.Paid)
                        {
                            data.PaidDate = DateTime.UtcNow;
                        }

                        await UpdateIncentiveStatus(data.EmployeeContract.Employee.EmployeeCode, data.SalaryMonth, (data.SalaryYear > 0 ? data.SalaryYear : data.CreatedDateTimeUtc.Year), (int)item.Status);

                        _dbContext.Entry(data).State = EntityState.Modified;
                        await _dbContext.SaveChangesAsync();
                    }
                }
                transaction.Complete();
                return true;
            }
        }
        public DateTime? GetEmployeeJoindate(int? employeeId)
        {
            return _dbContext.EmployeeDetails.Where(x => x.EmployeeId == employeeId && x.Employee.RecordStatus == RecordStatus.Active).Select(x => x.JoinDate).FirstOrDefault();
        }
        public async Task<IQueryable<EmployeeEarningGross>> GetEmployeeSalaryDetailsAsync(EmployeeSalaryQueryModel query)
        {
            return _dbContext.EmployeeEarningGross.Include(x => x.EmployeeContract).ThenInclude(x => x.EmployeeFixGross)
                                                 .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee)
                                                 .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.Branch)
                                                 .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.DesignationType)
                                                 .Include(x => x.EmployeeEarningGrossStatus)
                                                 .Include(x => x.Employee)
                                           .Where(x => x.RecordStatus == RecordStatus.Active && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active
                                                        && (query.Id.HasValue && query.Id > 0 ? x.Id == query.Id : true)
                                                        && (string.IsNullOrEmpty(query.EmployeeCode) ? true : x.EmployeeContract.Employee.EmployeeCode == query.EmployeeCode)
                                                        && ((string.IsNullOrEmpty(query.Month) ? true : x.SalaryMonth == query.Month) && (query.Year.HasValue ? x.SalaryYear == query.Year : true)))
                                           .OrderBy(x => x.CreatedDateTimeUtc).ThenBy(x => x.Id);
        }
        public IQueryable<EmployeeEarningGross> GetEmployeeSalaryDetails(EmployeeSalaryQueryModel query)
        {
            return _dbContext.EmployeeEarningGross.Include(x => x.EmployeeContract).ThenInclude(x => x.EmployeeFixGross)
                                                 .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee)
                                                 .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.Branch)
                                                 .Include(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.DesignationType)
                                                 .Include(x => x.EmployeeEarningGrossStatus)
                                                 .Include(x => x.Employee)
                                           .Where(x => x.RecordStatus == RecordStatus.Active && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active
                                                        && (query.Id.HasValue && query.Id > 0 ? x.Id == query.Id : true)
                                                        && (string.IsNullOrEmpty(query.EmployeeCode) ? true : x.EmployeeContract.Employee.EmployeeCode == query.EmployeeCode)
                                                        && ((string.IsNullOrEmpty(query.Month) ? true : x.SalaryMonth == query.Month) && (query.Year.HasValue ? x.SalaryYear == query.Year : true)))
                                           .OrderBy(x => x.CreatedDateTimeUtc).ThenBy(x => x.Id);
        }
        public async Task<EmployeeFixGross> GetEmployeeSalaryDetails(string employeeCode)
        {
            var contract = await _employeeContractStore.GetRunningEmployeeContract(employeeCode);
            return await _dbContext.EmployeeFixGross.Where(x => x.RecordStatus == RecordStatus.Active && x.EmployeeContractId == contract.Id).FirstOrDefaultAsync();
        }
        public async Task<double> GetActualLeaveWitoutPay(EmployeeSalaryQueryModel query)
        {
            var monthNumber = GeneralExtensions.GetMonthNumber(query.Month);
            var contract = await _employeeContractStore.GetRunningEmployeeContract(query.EmployeeCode);
            return await _dbContext.EmployeeLeaveApplications.Where(x => x.EmployeeContract.Id == contract.Id && x.EmployeeLeaveStatusId == (int)EmployeeLeaveStatusType.Approved
                                                                    && (x.LeaveFromDate.Value.Month == monthNumber || x.LeaveToDate.Value.Month == monthNumber)
                                                                    && (x.LeaveFromDate.Value.Year == query.Year || x.LeaveToDate.Value.Year == query.Year))
                                                             .SumAsync(x => x.LWPDays);
        }
        public async Task<decimal?> GetActualApprovedOvertime(EmployeeSalaryQueryModel query)
        {
            var monthNumber = GeneralExtensions.GetMonthNumber(query.Month);
            var contract = await _employeeContractStore.GetRunningEmployeeContract(query.EmployeeCode);
            return await _dbContext.EmployeeOverTimes.Where(x => x.EmployeeContractId == contract.Id && x.EmployeeOverTimeStatusId == (int)EmployeeOverTimeStatusType.Approved
                                                            && x.OverTimeDate.Value.Month == monthNumber && x.OverTimeDate.Value.Year == query.Year)
                                                     .SumAsync(x => x.OverTimeAmount);
        }
        public async Task<EmployeeIncentiveDetails> GetCurrentMonthIncentive(string employeeCode, string month, int? year, EmployeeIncentiveStatusType? status = null)
        {
            var monthNumber = GeneralExtensions.GetMonthNumber(month);
            var contract = await _employeeContractStore.GetRunningEmployeeContract(employeeCode);
            if (contract == null)
            {
                return null;
            }

            return await _dbContext.EmployeeIncentiveDetails.FirstOrDefaultAsync(x => x.EmployeeContractId == contract.Id && x.RecordStatus == RecordStatus.Active && (status == null ? true : x.EmployeeIncentiveStatusId == (int)status)
                                                                                    && x.IncentiveDate.Value.Month == monthNumber && x.IncentiveDate.Value.Year == year);
        }
        public async Task UpdateIncentiveStatus(string employeeCode, string month, int? year, int status)
        {
            var data = await GetCurrentMonthIncentive(employeeCode, month, year);
            if (data != null)
            {
                data.EmployeeIncentiveStatusId = (status == (int)EmployeeEarningGrossStatusType.PartiallyPaid ? (int)EmployeeEarningGrossStatusType.Paid : status);
            }
        }
        public async Task<bool> CheckMonthlySalaryExist(EmployeeSalaryQueryModel query)
        {
            return await _dbContext.EmployeeEarningGross.AnyAsync(x => x.RecordStatus == RecordStatus.Active && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active
                                                                    && x.EmployeeContract.Employee.EmployeeCode == query.EmployeeCode && x.SalaryMonth == query.Month && x.SalaryYear == query.Year);
        }
        public async Task<IQueryable<EmployeeIncentiveDetails>> GetEmployeeIncentivesDetails(string employeeCode)
        {
            var contract = await _employeeContractStore.GetRunningEmployeeContract(employeeCode);
            return _dbContext.EmployeeIncentiveDetails.Include(x => x.EmployeeContract).ThenInclude(x => x.EmployeeFixGross)
                                                        .Where(x => x.RecordStatus == RecordStatus.Active && x.EmployeeContractId == contract.Id
                                                                    && x.EmployeeContract.EmployeeFixGross.IsFixIncentive && x.EmployeeContract.EmployeeFixGross.FixIncentiveDuration > 0);
        }
        public async Task<string> GetEmployeeIncentivesRemarks(string employeeCode)
        {
            var contract = await _employeeContractStore.GetRunningEmployeeContract(employeeCode);
            return await _dbContext.EmployeeFixGross.Where(x => x.EmployeeContractId == contract.Id).Select(x => x.FixIncentiveRemarks).FirstOrDefaultAsync();
        }
        private async Task UpdateMonthlyIncentiveAmount(string employeeCode, string month, int? year, double? amount, EmployeeIncentiveStatusType? status = null)
        {
            var data = await GetCurrentMonthIncentive(employeeCode, month, year);
            if (data != null)
            {
                data.IncentiveAmount = amount;
                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<int>> GetAlreadySalariedEmployees(string month, int? year)
        {
            return _dbContext.EmployeeEarningGross.Where(x => x.SalaryMonth == month && x.SalaryYear == year).Select(x => x.EmployeeContract.EmployeeId).ToList();
        }
        #region Partial Hold Salary
        public async Task<Guid?> AddorUpdateEmployeeHoldSalary(EmployeeHoldSalaryRequestModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var isNew = false;
                var employeeEarningGrossId = await _employeeContractStore.GetEmployeeEarningGrossId(model.EmployeeContractId);
                if (!employeeEarningGrossId.HasValue)
                {
                    return null;
                }

                var data = await _dbContext.EmployeeHoldSalaries.FirstOrDefaultAsync(x => x.Id == model.Id && x.EmployeeEarningGrossId == employeeEarningGrossId);
                if (data == null)
                {
                    data = new EmployeeHoldSalary();
                    data.Id = Guid.NewGuid();
                    isNew = true;
                }

                data.EmployeeEarningGrossId = employeeEarningGrossId.Value;
                data.EmployeeSalaryStatusId = (int)model.Status;
                data.Remarks = model.Remarks;
                data.HoldAmount = model.HoldAmount;
                if (model.Status == EmployeeSalaryStatusType.Paid)
                {
                    data.PaidDate = model.PaidDate;
                    data.EmployeeSalaryStatusId = (int)EmployeeSalaryStatusType.Paid;
                }

                _dbContext.Entry(data).State = (isNew ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();

                if (model.Status == EmployeeSalaryStatusType.Paid)
                {
                    await UpdateEmployeeHoldEarningGross(employeeEarningGrossId);
                }
                transaction.Complete();

                return data.Id;
            }
        }
        public async Task<IQueryable<EmployeeHoldSalary>> GetEmployeeHoldSalary(EmployeeHoldSalaryFilterModel filter)
        {
            return _dbContext.EmployeeHoldSalaries.Include(x => x.EmployeeEarningGross)
                                                  .Include(x => x.EmployeeEarningGross).ThenInclude(x => x.EmployeeContract)
                                                  .Include(x => x.EmployeeEarningGross).ThenInclude(x => x.EmployeeContract).ThenInclude(x => x.Employee)
                                                  .Include(x => x.EmployeeEarningGross).ThenInclude(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.Branch)
                                                  .Include(x => x.EmployeeEarningGross).ThenInclude(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.DesignationType)
                                                  .Include(x => x.EmployeeSalaryStatus)
                                                  .Where(x => x.EmployeeEarningGross.RecordStatus == RecordStatus.Active
                                                            && (filter == null ? true : (filter.Id.HasValue ? x.Id == filter.Id : true)
                                                            && ((string.IsNullOrEmpty(filter.SearchString) ? true : (x.EmployeeEarningGross.EmployeeContract.Employee.FirstName.Contains(filter.SearchString)
                                                                                                                || x.EmployeeEarningGross.EmployeeContract.Employee.LastName.Contains(filter.SearchString)
                                                                                                                || x.EmployeeEarningGross.EmployeeContract.Employee.Branch.Name.Contains(filter.SearchString)
                                                                                                                || x.EmployeeEarningGross.EmployeeContract.Employee.DesignationType.Name.Contains(filter.SearchString))))
                                                            && (filter.Branch != null && filter.Branch.Any() ? filter.Branch.Contains(x.EmployeeEarningGross.EmployeeContract.Employee.BranchId.Value) : true)
                                                            && (string.IsNullOrEmpty(filter.SalaryMonth) ? true : filter.SalaryMonth == x.EmployeeEarningGross.SalaryMonth)
                                                            && (!filter.SalaryYear.HasValue ? true : filter.SalaryYear == x.EmployeeEarningGross.SalaryYear)
                                                            && (filter.Status != null && filter.Status.Any() ? filter.Status.Contains(x.EmployeeSalaryStatusId) : true)))
                                                  .AsNoTracking().AsSplitQuery();
        }
        public async Task<IQueryable<EmployeeHoldSalary>> GetHistoryEmployeeHoldSalary(int employeeContractId)
        {
            return _dbContext.EmployeeHoldSalaries.Include(x => x.EmployeeEarningGross)
                                                  .Include(x => x.EmployeeEarningGross).ThenInclude(x => x.EmployeeContract)
                                                  .Include(x => x.EmployeeEarningGross).ThenInclude(x => x.EmployeeContract).ThenInclude(x => x.Employee)
                                                  .Include(x => x.EmployeeEarningGross).ThenInclude(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.Branch)
                                                  .Include(x => x.EmployeeEarningGross).ThenInclude(x => x.EmployeeContract).ThenInclude(x => x.Employee).ThenInclude(x => x.DesignationType)
                                                  .Include(x => x.EmployeeSalaryStatus)
                                                  .Where(x => x.EmployeeEarningGross.RecordStatus == RecordStatus.Active && x.EmployeeEarningGross.EmployeeContractId == employeeContractId)
                                                  .AsNoTracking().AsSplitQuery();
        }
        public double? GetHoldSalaryAmount(int EmployeeEarningGrossId)
        {
            return _dbContext.EmployeeHoldSalaries.Where(x => x.EmployeeEarningGrossId == EmployeeEarningGrossId && x.EmployeeSalaryStatusId == (int)EmployeeSalaryStatusType.PartialPaid).Select(x => x.HoldAmount).FirstOrDefault();
        }
        public async Task UpdateEmployeeHoldEarningGross(int? employeeEarningGrossId)
        {
            var data = await _dbContext.EmployeeEarningGross.FirstOrDefaultAsync(x => x.Id == employeeEarningGrossId && x.EmployeeEarningGrossStatusId == (int)EmployeeEarningGrossStatusType.PartiallyPaid);
            if (data != null)
            {
                data.EmployeeEarningGrossStatusId = (int)EmployeeEarningGrossStatusType.Paid;
                data.PaidDate = DateTime.UtcNow;

                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<bool> RemoveEmployeeHoldSalary(IEnumerable<Guid> ids)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeHoldSalaries.Where(x => ids.Contains(x.Id)).ToListAsync();
                if (data == null || (data != null && data.Any()))
                {
                    return false;
                }

                _dbContext.RemoveRange(data);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();

                return true;
            }
        }
        #endregion Partial Hold Salary
        #region Others
        public async Task<IQueryable<EmployeeDetail>> GetEmployeeBankInfomationList(IEnumerable<string> employeeCodes, string month, int year)
        {
            var contracts = await _employeeContractStore.GetRunningEmployeeContracts(employeeCodes);
            var cIds = contracts.Select(x => x.Id).ToList();
            var salaryies = await _dbContext.EmployeeEarningGross.Where(x => cIds.Contains(x.EmployeeContractId)
                                                                        && x.SalaryMonth == month && x.SalaryYear == year
                                                                        && x.EmployeeEarningGrossStatusId == (int)EmployeeEarningGrossStatusType.InProcess).Select(x => x.EmployeeContractId).ToListAsync();
            var employees = contracts.Where(x => salaryies.Contains(x.Id)).Select(x => x.EmployeeId).ToList();

            return _dbContext.EmployeeDetails.Include(x => x.Employee).Include(x => x.EmployeeBankDetail).Where(x => employees.Contains(x.EmployeeId) && x.Employee.RecordStatus == RecordStatus.Active).OrderBy(x => x.JoinDate);
        }
        #endregion Others
    }
}
