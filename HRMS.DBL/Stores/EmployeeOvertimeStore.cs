using HRMS.Core.Consts;
using HRMS.Core.Models.Overtime;
using HRMS.DBL.DbContextConfiguration;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace HRMS.DBL.Stores
{
    public class EmployeeOvertimeStore : BaseStore
    {
        private readonly EmployeeStore _employeeStore;

        public EmployeeOvertimeStore(HRMSDbContext dbContext, EmployeeStore employeeStore) : base(dbContext)
        {
            _employeeStore = employeeStore;
        }

        public async Task<int> AddorUpdateEmployeeOvertime(EmployeeOvertimeRequest request)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeOverTimes.Include(x => x.EmployeeOverTimeManagers).FirstOrDefaultAsync(x => x.Id == request.Id && x.RecordStatus == RecordStatus.Active && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active);

                var employeeId = await _employeeStore.GetEmployeeIdByCode(request.EmployeeCode);
                var contractId = await GetCurrentContractIdByEmployeeId(employeeId);
                if (employeeId == 0 && contractId.HasValue)
                {
                    return 0;
                }

                if (data == null)
                {
                    data = new EmployeeOverTime();
                }

                data.ProjectName = request.ProjectName;
                data.TaskDescription = request.TaskDescription;
                data.OverTimeDate = request.OverTimeDate;
                data.OverTimeMinutes = request.OverTimeMinutes;
                data.EmployeeOverTimeStatusId = (int)EmployeeOverTimeStatusType.Pending;
                if (contractId.HasValue)
                {
                    data.EmployeeContractId = contractId.Value;
                }

                AddManyToManyData(data, request);
                if (data.Id == 0)
                {
                    await _dbContext.EmployeeOverTimes.AddAsync(data);
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
        public async Task<IQueryable<EmployeeOverTime>> GetEmployeeOvertime(string employeeCode, RoleTypes? roleTypes = null, EmployeeOvertimeFilterModel filter = null)
        {
            var employeeId = await _employeeStore.GetEmployeeIdByCode(employeeCode);
            var contractId = await GetCurrentContractIdByEmployeeId(employeeId);

            return _dbContext.EmployeeOverTimes.Include(x => x.EmployeeContract).ThenInclude(y => y.Employee)
                                                   .Include(x => x.EmployeeContract).ThenInclude(y => y.Employee).ThenInclude(z => z.DesignationType)
                                                   .Include(x => x.EmployeeContract).ThenInclude(y => y.Employee).ThenInclude(x => x.Branch)
                                                   .Include(x => x.EmployeeOverTimeStatus)
                                                   .Include(x => x.EmployeeOverTimeManagers).ThenInclude(x => x.Employee)
                                                   .Include(x => x.Employee)
                                                .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id)
                                                .Where(x => x.EmployeeContractId == contractId && x.RecordStatus == RecordStatus.Active && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active
                                                        && (filter == null ? true :
                                                            ((string.IsNullOrEmpty(filter.SearchString) ? true :
                                                                (x.EmployeeContract.Employee.FirstName.Contains(filter.SearchString))
                                                                    || (x.EmployeeContract.Employee.LastName.Contains(filter.SearchString))
                                                                    || (x.EmployeeContract.Employee.Branch.Name.Contains(filter.SearchString))
                                                                    || (x.EmployeeContract.Employee.DesignationType.Name.Contains(filter.SearchString))
                                                                    || (x.EmployeeContract.Employee.EmployeeCode.Contains(filter.SearchString))
                                                                    || (x.ProjectName.Contains(filter.SearchString)))
                                                                && (filter.Status != null && filter.Status.Any() ? filter.Status.Contains(x.EmployeeOverTimeStatusId) : true)
                                                                && (filter.DateFrom.HasValue && filter.DateTo.HasValue ? x.OverTimeDate >= filter.DateFrom && x.OverTimeDate <= filter.DateTo : true))));
        }
        public async Task<IQueryable<EmployeeOverTime>> GetEmployeeOvertimeById(int id, string employeeCode, RoleTypes? userRole)
        {
            return _dbContext.EmployeeOverTimes.Include(x => x.EmployeeContract).ThenInclude(y => y.Employee)
                                                   .Include(x => x.EmployeeContract).ThenInclude(y => y.Employee).ThenInclude(z => z.DesignationType)
                                                   .Include(x => x.EmployeeContract).ThenInclude(y => y.Employee).ThenInclude(x => x.Branch)
                                                   .Include(x => x.EmployeeContract).ThenInclude(y => y.Employee).ThenInclude(x => x.ImagekitDetail)
                                                   .Include(x => x.EmployeeOverTimeStatus)
                                                   .Include(x => x.EmployeeOverTimeManagers).ThenInclude(x => x.Employee)
                                                   .Include(x => x.Employee)
                                                .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id)
                                                .Where(x => x.Id == id && (string.IsNullOrEmpty(employeeCode) ? true : x.EmployeeContract.Employee.EmployeeCode == employeeCode) && x.RecordStatus == RecordStatus.Active && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active);
        }
        public async Task<bool> SetOvertimeStatus(EmployeeOverTimeStatusModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeOverTimes.FirstOrDefaultAsync(x => x.Id == model.Id && x.RecordStatus == RecordStatus.Active);
                if (data != null)
                {
                    if (model.StatusType == EmployeeOverTimeStatusType.Approved)
                    {
                        data.ApprovedMinutes = model.ApprovedMinutes;
                        data.OverTimeAmount = model.OverTimeAmount;
                    }
                    data.ApprovedBy = model.ApprovedBy;
                    data.ApprovedDate = DateTime.UtcNow;
                    data.Remarks = model.Description;
                    data.EmployeeOverTimeStatusId = (int)model.StatusType;
                    _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return true;
                }
            }
            return false;
        }
        public async Task<IQueryable<EmployeeOverTime>> GetAllEmployeeOvertimes(EmployeeOvertimeFilterModel filter, RoleTypes? userRole, int? employeeId)
        {
            return _dbContext.EmployeeOverTimes.Include(x => x.EmployeeContract).ThenInclude(y => y.Employee).ThenInclude(z => z.DesignationType)
                                                   .Include(x => x.EmployeeContract).ThenInclude(y => y.Employee).ThenInclude(x => x.Branch)
                                                   .Include(x => x.EmployeeOverTimeStatus)
                                                   .Include(x => x.EmployeeOverTimeManagers).ThenInclude(x => x.Employee)
                                                   .Include(x => x.Employee)
                                                .Where(x => x.RecordStatus == RecordStatus.Active && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active
                                                            && (x.EmployeeContract.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContract.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod)
                                                            && (filter == null ? true :
                                                                ((string.IsNullOrEmpty(filter.SearchString) ? true :
                                                                    (x.EmployeeContract.Employee.FirstName.Contains(filter.SearchString))
                                                                        || (x.EmployeeContract.Employee.LastName.Contains(filter.SearchString))
                                                                        || (string.Concat(x.EmployeeContract.Employee.FirstName, " ", x.EmployeeContract.Employee.LastName).Contains(filter.SearchString))
                                                                        || (x.EmployeeContract.Employee.Branch.Name.Contains(filter.SearchString))
                                                                        || (x.EmployeeContract.Employee.DesignationType.Name.Contains(filter.SearchString))
                                                                        || (x.EmployeeContract.Employee.EmployeeCode.Contains(filter.SearchString))
                                                                        || (x.ProjectName.Contains(filter.SearchString)))
                                                                    && (filter.Status != null && filter.Status.Any() ? filter.Status.Contains(x.EmployeeOverTimeStatusId) : true)
                                                                    && (filter.DateFrom.HasValue && filter.DateTo.HasValue ? x.OverTimeDate >= filter.DateFrom && x.OverTimeDate <= filter.DateTo : true))))
                                                .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id).AsNoTracking().AsSplitQuery();
        }
        public async Task<bool> DeleteEmployeeOvertime(int id, string employeeCode)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeOverTimes.Where(x => x.Id == id && (string.IsNullOrEmpty(employeeCode) ? true : x.EmployeeContract.Employee.EmployeeCode == employeeCode) && x.RecordStatus == RecordStatus.Active).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }

                data.RecordStatus = RecordStatus.InActive;
                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return true;
            }
        }
        public async Task<IQueryable<EmployeeOverTime>> GetEmployeeAssignedOvertime(string employeeCode)
        {
            var employeeId = await _employeeStore.GetEmployeeIdByCode(employeeCode);
            var requestedId = await _dbContext.EmployeeOverTimeManagers.Where(x => x.EmployeeId == employeeId).Select(x => x.EmployeeOvertimeId).ToListAsync();
            return _dbContext.EmployeeOverTimes.Include(x => x.EmployeeContract).ThenInclude(y => y.Employee)
                                                .Include(x => x.EmployeeContract).ThenInclude(y => y.Employee).ThenInclude(z => z.DesignationType)
                                                .Include(x => x.EmployeeContract).ThenInclude(y => y.Employee).ThenInclude(x => x.Branch)
                                                .Include(x => x.EmployeeOverTimeStatus)
                                                .Include(x => x.EmployeeOverTimeManagers).ThenInclude(x => x.Employee)
                                                .Include(x => x.Employee)
                                            .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id)
                                            .Where(x => requestedId.Contains(x.Id) && x.RecordStatus == RecordStatus.Active && x.EmployeeContract.Employee.RecordStatus == RecordStatus.Active);
        }
        private async Task<int?> GetCurrentContractIdByEmployeeId(int employeeId)
        {
            return await _dbContext.EmployeeContracts.Where(x => x.EmployeeId == employeeId && x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active
                                                            && (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod))
                                                    .Select(x => x.Id).FirstOrDefaultAsync();
        }
        private void AddManyToManyData(EmployeeOverTime enitity, EmployeeOvertimeRequest model)
        {
            if (model.ProjectManagerIds != null && model.ProjectManagerIds.Any())
            {
                if (enitity?.EmployeeOverTimeManagers != null)
                {
                    _dbContext.EmployeeOverTimeManagers?.RemoveRange(enitity?.EmployeeOverTimeManagers);
                }
                foreach (var item in model.ProjectManagerIds)
                {
                    enitity.EmployeeOverTimeManagers.Add(new EmployeeOverTimeManager { EmployeeId = item, EmployeeOvertimeId = enitity.Id });
                }
            }
        }
        public async Task<bool> RemoveEmployeeOvertime(int id)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeOverTimes.Include(x => x.EmployeeOverTimeManagers).Where(x => x.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                if (data?.EmployeeOverTimeManagers != null)
                {
                    _dbContext.EmployeeOverTimeManagers.RemoveRange(data?.EmployeeOverTimeManagers);
                }                
                _dbContext.EmployeeOverTimes.RemoveRange(data); 

                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return true;
            }
        }
    }
}
