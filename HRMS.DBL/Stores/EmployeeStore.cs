using HRMS.DBL.DbContextConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using HRMS.Core.Consts;
using System.Transactions;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.ImageKit;
using HRMS.Core.Models.Employee.ExitProccess;
using Org.BouncyCastle.Crypto.Engines;

namespace HRMS.DBL.Stores
{
    public class EmployeeStore : BaseStore
    {
        private readonly SystemFlagStore _systemFlagStore;
        public EmployeeStore(HRMSDbContext dbContext, SystemFlagStore systemFlagStore) : base(dbContext)
        {
            _systemFlagStore = systemFlagStore;
        }

        public async Task<IQueryable<EmployeeDetail>> GetAllEmployees(EmployeeFilterModel filter, RoleTypes? roleTypes)
        {
            return _dbContext.EmployeeDetails.Include(x => x.Employee).ThenInclude(x => x.EmployeeContracts).ThenInclude(x => x.EmployeeContractStatus)
                                             .Where(x => (roleTypes == RoleTypes.SuperAdmin ? true : !GenericConst.AnchoredEmployeeCode.Contains(x.Employee.EmployeeCode))
                                                         && (filter == null ? true : ((string.IsNullOrEmpty(filter.SearchString) ? true :
                                                                                        (x.Employee.FirstName.Contains(filter.SearchString)
                                                                                        || x.Employee.LastName.Contains(filter.SearchString)
                                                                                        || (string.Concat(x.Employee.FirstName, " ", x.Employee.LastName).Contains(filter.SearchString))
                                                                                        || x.Employee.Branch.Name.Contains(filter.SearchString)
                                                                                        || x.Employee.EmployeeCode.Contains(filter.SearchString)
                                                                                        || x.MobileNumber.Contains(filter.SearchString)))
                                                                                && (filter.Branch != null && filter.Branch.Any() ? filter.Branch.Contains(x.Employee.BranchId) : true)
                                                                                && (filter.Status != null && filter.Status.Any() ? filter.Status.Contains((int)x.Employee.RecordStatus) : true))))
                                             .OrderByDescending(x => x.JoinDate).ThenByDescending(x => x.Id).AsNoTracking().AsSplitQuery();
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Employees.Where(x => x.Id == id && x.RecordStatus == RecordStatus.Active).FirstOrDefaultAsync();
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
        public async Task<int> GetEmployeeIdByCode(string employeeCode, RecordStatus recordStatus = RecordStatus.Active)
        {
            return await _dbContext.Employees.Where(x => x.EmployeeCode == employeeCode && x.RecordStatus == recordStatus).Select(x => x.Id).FirstOrDefaultAsync();
        }
        public async Task<Employee> GetEmployeeIdById(int id)
        {
            return await _dbContext.Employees.Include(x => x.ImagekitDetail).Where(x => x.Id == id && x.RecordStatus == RecordStatus.Active).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Employee>> GetEmployeeIdByIds(IEnumerable<int> id)
        {
            return await _dbContext.Employees.Include(x => x.ImagekitDetail).Where(x => id.Contains(x.Id) && x.RecordStatus == RecordStatus.Active).ToListAsync();
        }
        public async Task<EmployeeDetail> GetEmployeeDetails(int id)
        {
            return await _dbContext.EmployeeDetails.Include(x => x.Employee).FirstOrDefaultAsync(x => x.EmployeeId == id && x.Employee.RecordStatus == RecordStatus.Active);
        }
        public async Task<EmployeeDetail> GetEmployeeDetails(string employeeCode)
        {
            return await _dbContext.EmployeeDetails.Include(x => x.Employee)
                                                     .Include(x => x.Employee).ThenInclude(x => x.Branch)
                                                     .Include(x => x.Employee).ThenInclude(x => x.DesignationType)
                                                 .FirstOrDefaultAsync(x => x.Employee.EmployeeCode == employeeCode && x.Employee.RecordStatus == RecordStatus.Active);
        }
        public async Task<EmployeeDetail> GetEmployeeInformation(int id)
        {
            return await _dbContext.EmployeeDetails.Include(x => x.Employee).ThenInclude(x => x.Branch)
                                                .Include(x => x.Employee).ThenInclude(x => x.DesignationType)
                                                .Include(x => x.Employee).ThenInclude(x => x.ImagekitDetail)
                                                .Include(x => x.PresentAddress)
                                                .Include(x => x.PermanentAddress)
                                                .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id)
                                                .FirstOrDefaultAsync(x => x.EmployeeId == id);
        }
        public async Task<IQueryable<Employee>> GetEmployeesByRoleType(int roleTypeId)
        {
            return _dbContext.Employees.Include(x => x.User)
                                       .Include(x => x.Branch)
                                       .Include(x => x.DesignationType)
                                    .Where(x => x.User.RoleId == roleTypeId && x.RecordStatus == RecordStatus.Active);
        }
        public async Task<EmployeeImagekitModel> GetEmployeeImagekitData(int? id)
        {
            return await _dbContext.Employees.Include(x => x.ImagekitDetail)
                                            .Where(x => x.Id == id && x.RecordStatus == RecordStatus.Active)
                                            .Select(x => new EmployeeImagekitModel
                                            {
                                                EmployeeId = x.Id,
                                                ImagekitDetailFileId = x.ImagekitDetail.FileId,
                                                ImagekitDetailId = x.ImagekitDetailId,
                                                FilePath = x.ImagekitDetail.FilePath
                                            }).FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateEmployeeImagekitFileId(int? employeeId, Guid imagekDetailiId)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId && x.RecordStatus == RecordStatus.Active);
                if (data != null)
                {
                    data.ImagekitDetailId = imagekDetailiId;
                    _dbContext.Entry(data).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return true;
                }
            }
            return false;
        }
        public async Task<int> GetEmployeeIdByUserId(int userId)
        {
            return await _dbContext.Employees.Where(x => x.UserId == userId && x.User.Role.Id == (int)RoleTypes.SuperAdmin ? true : x.RecordStatus == RecordStatus.Active).Select(x => x.Id).FirstOrDefaultAsync();
        }
        public async Task<bool> CheckEmployeeCodeExists(string employeeCode)
        {
            return await _dbContext.Employees.AnyAsync(x => x.EmployeeCode == employeeCode);
        }
        public async Task<IQueryable<EmployeeContract>> GetEmployeeInformationByDesignation(IEnumerable<int?> designationIds, bool hasContract, RoleTypes? userRole, int? employeeId)
        {
            var employeeIds = await GetEmployeeExitProcessList(designationIds);
            return _dbContext.EmployeeContracts.Include(x => x.Employee)
                                        .Include(x => x.Employee).ThenInclude(x => x.Branch)
                                        .Include(x => x.Employee).ThenInclude(x => x.DesignationType)
                                    .Where(x => designationIds.Contains(x.Employee.DesignationTypeId) && (userRole == RoleTypes.Manager ? x.EmployeeId != employeeId : true)
                                            && (hasContract ? x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Running
                                                                || x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod
                                                                || (employeeIds != null && employeeIds.Any() ? x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Completed && employeeIds.Contains(x.EmployeeId) : true)
                                                            : true)
                                            && x.RecordStatus == RecordStatus.Active && x.Employee.RecordStatus == RecordStatus.Active)
                                    .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id)
                                    .Distinct().AsNoTracking().AsSplitQuery();
        }
        public async Task<IQueryable<Employee>> GetEmployeeInformationByCode(string employeeCode)
        {
            return _dbContext.Employees.Include(x => x.Branch).Include(x => x.DesignationType)
                                    .Where(x => x.EmployeeCode == employeeCode && x.RecordStatus == RecordStatus.Active)
                                    .Distinct().AsNoTracking().AsSplitQuery();
        }
        public async Task<DateTime> GetEmployeeDateOfBirth(string employeeCode)
        {
            return await _dbContext.Employees.Where(x => x.EmployeeCode == employeeCode).Select(x => x.DateOfBirth).FirstOrDefaultAsync();
        }
        public async Task<string> GetEmployeeSlackId(string employeeCode)
        {
            return await _dbContext.Employees.Where(x => x.EmployeeCode == employeeCode).Select(x => x.SlackUserId).FirstOrDefaultAsync();
        }
        public async Task<string> UpdateEmployeeSlackId(string workEmail, string slackUserId)
        {
            var employeeCode = await _dbContext.EmployeeDetails.Where(x => x.WorkEmail == workEmail).Select(x => x.Employee.EmployeeCode).FirstOrDefaultAsync();
            var data = await _dbContext.Employees.FirstOrDefaultAsync(x => !string.IsNullOrEmpty(x.EmployeeCode) && x.EmployeeCode == employeeCode);
            if (data == null)
            {
                return string.Empty;
            }

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                data.SlackUserId = slackUserId;
                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                transaction.Complete();
                return data?.SlackUserId;
            }
        }
        public async Task<string> GetEmployeeEmail(string employeeCode)
        {
            return await _dbContext.EmployeeDetails.Where(x => x.Employee.EmployeeCode == employeeCode).Select(x => x.WorkEmail).FirstOrDefaultAsync();
        }
        public async Task<string> GetEmployeeName(string employeeCode)
        {
            return await _dbContext.EmployeeDetails.Where(x => x.Employee.EmployeeCode == employeeCode).Select(x => $"{x.Employee.FirstName} {x.Employee.LastName}").FirstOrDefaultAsync();
        }
        public async Task<string> GetEmployeeName(int? employeeId)
        {
            return await _dbContext.EmployeeDetails.Where(x => x.EmployeeId == employeeId).Select(x => $"{x.Employee.FirstName} {x.Employee.LastName}").FirstOrDefaultAsync();
        }
        #region Exit Process
        public async Task<int?> AddorUpdateExitProcessEmployee(EmployeeExitRequestModel model, RoleTypes? roleTypes)
        {
            if (model == null)
            {
                return null;
            }

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeFNFDetails.FirstOrDefaultAsync(x => x.EmployeeId == model.EmployeeId);
                if (data == null)
                {
                    data = new EmployeeFNFDetails();
                }

                data.ExitNote = model.ExitNote;
                data.EmployeeId = model.EmployeeId;

                var fnfDate = await _systemFlagStore.GetSystemFlagsByTags(new string[] { "fnfenddatepermit" });
                data.FNFDueDate = fnfDate?.FirstOrDefault()?.Value == "enable" ? model.FNFDueDate : model.Exitdate.Value.AddMonths(1);

                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();

                await UpdateEmployeeExitData(model.EmployeeId, model.Exitdate);
                transaction.Complete();
                return data.Id;
            }
        }
        public async Task<IQueryable<EmployeeFNFDetails>> GetExitProcessEmployees(EmployeeFilterModel filter, RoleTypes? roleTypes)
        {
            var emplyeeIds = _dbContext.EmployeeDetails.Include(x => x.Employee).ThenInclude(x => x.Branch)
                                                       .Include(x => x.Employee).ThenInclude(x => x.DesignationType)
                                                       .Include(x => x.Employee).ThenInclude(x => x.EmployeeContracts)
                                                .Where(x => x.Employee.RecordStatus == RecordStatus.Active && (roleTypes == RoleTypes.SuperAdmin ? true : !GenericConst.AnchoredEmployeeCode.Contains(x.Employee.EmployeeCode))
                                                         && (filter == null ? true : ((string.IsNullOrEmpty(filter.SearchString) ? true :
                                                                                    (x.Employee.FirstName.Contains(filter.SearchString)
                                                                                        || x.Employee.LastName.Contains(filter.SearchString)
                                                                                        || (string.Concat(x.Employee.FirstName, " ", x.Employee.LastName).Contains(filter.SearchString))
                                                                                        || x.Employee.Branch.Name.Contains(filter.SearchString)
                                                                                        || x.Employee.EmployeeCode.Contains(filter.SearchString)
                                                                                        || x.MobileNumber.Contains(filter.SearchString)))
                                                                                && (filter.Branch != null && filter.Branch.Any() ? filter.Branch.Contains(x.Employee.BranchId) : true)
                                                                                && (filter.HasExited.HasValue ? x.HasExited == filter.HasExited : true)
                                                                                && (filter.HasFNFSettled.HasValue ? x.HasFNFSettled == filter.HasFNFSettled : true)
                                                                                && (filter.Status != null && filter.Status.Any() ? filter.Status.Contains((int)x.Employee.RecordStatus) : true))))
                                                .OrderByDescending(x => x.CreatedDateTimeUtc).ThenByDescending(x => x.Id).Select(x => x.EmployeeId);

            return _dbContext.EmployeeFNFDetails.Where(x => emplyeeIds.Contains(x.EmployeeId) && (filter == null ? true :
                                                                                (filter.SettlementDate.HasValue ? (x.SettlementDate.Value.Month == filter.SettlementDate.Value.Month && x.SettlementDate.Value.Year == filter.SettlementDate.Value.Year) : true)))
                                                .AsNoTracking().AsSplitQuery();
        }
        public async Task<IQueryable<EmployeeFNFDetails>> GetExitProcessNotes(string employeeCode)
        {
            return _dbContext.EmployeeFNFDetails.Include(x => x.Employee)
                                                .Include(x => x.Employee).ThenInclude(x => x.Branch)
                                                .Include(x => x.Employee).ThenInclude(x => x.DesignationType)
                                            .Where(x => x.Employee.EmployeeCode == employeeCode).AsNoTracking().AsSplitQuery();
        }
        public async Task<int?> AddorUpdateFNFProcessEmployee(EmployeeFNFDetailsRequestModel model, RoleTypes? roleTypes)
        {
            if (model == null)
            {
                return null;
            }

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeFNFDetails.FirstOrDefaultAsync(x => x.EmployeeId == model.EmployeeId);
                if (data == null)
                {
                    data = new EmployeeFNFDetails();
                }

                data.EmployeeId = model.EmployeeId;
                data.Remarks = model.Remarks;
                data.HasCertificateIssued = model.HasCertificateIssued;
                data.HasSalaryProceed = model.HasSalaryProceed;
                data.SettlementBy = model.SettlementBy;
                data.SettlementDate = model.SettlementDate;

                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();

                await UpdateEmployeeExitData(model.EmployeeId, null, true);
                transaction.Complete();
                return data.Id;
            }
        }
        private async Task UpdateEmployeeExitData(int employeeId, DateTime? endDate, bool hasFNFUpdate = false)
        {
            var data = await _dbContext.EmployeeDetails.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            if (data != null)
            {
                if (hasFNFUpdate)
                {
                    data.HasFNFSettled = true;
                    await UpdateEmployeeRecordStatusOnFNFSettled(employeeId);
                }
                else
                {
                    data.EndDate = endDate;
                    data.HasExited = true;
                    await UpdateUserStatusDataOnExit(employeeId);
                    await UpdateEmployeeContractStatus(employeeId);
                }

                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
        private async Task UpdateUserStatusDataOnExit(int employeeId)
        {
            var data = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            if (data != null)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == data.UserId);
                if (user != null)
                {
                    user.Disabled = true;
                    _dbContext.Entry(user).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
        private async Task UpdateEmployeeContractStatus(int employeeId)
        {
            var data = await _dbContext.EmployeeContracts.FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod);
            if (data != null)
            {
                data.EmployeeContractStatusId = (int)EmployeeContractStatusType.Completed;

                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
        private async Task UpdateEmployeeRecordStatusOnFNFSettled(int employeeId)
        {
            var data = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            if (data != null)
            {
                data.RecordStatus = RecordStatus.InActive;
                _dbContext.Entry(data).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<string> RemoveEmployeeInformation(string employeeCode)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeCode == employeeCode);
                if (data != null)
                {
                    string path = string.Empty;
                    var details = await _dbContext.EmployeeDetails.FirstOrDefaultAsync(x => x.EmployeeId == data.Id);
                    var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == data.UserId);
                    var document = await _dbContext.EmployeeDocuments.FirstOrDefaultAsync(x => x.EmployeeId == data.Id);
                    var fnf = await _dbContext.EmployeeFNFDetails.FirstOrDefaultAsync(x => x.EmployeeId == data.Id);
                    var permanentAddress = await _dbContext.EmployeeAddresses.FirstOrDefaultAsync(x => x.Id == details.PermanentAddressId);
                    var presentAddress = await _dbContext.EmployeeAddresses.FirstOrDefaultAsync(x => x.Id == details.PresentAddressId);
                    if (document != null)
                    {
                        var imagekit = await _dbContext.ImagekitDetails.FirstOrDefaultAsync(x => x.Id == document.ImagekitDetailId);
                        path = imagekit.FilePath;

                        _dbContext.EmployeeDocuments.Remove(document);
                    }
                    if (fnf != null)
                    {
                        _dbContext.EmployeeFNFDetails.Remove(fnf);
                    }
                    if (permanentAddress != null)
                    {
                        _dbContext.EmployeeAddresses.Remove(permanentAddress);
                    }
                    if (presentAddress != null)
                    {
                        _dbContext.EmployeeAddresses.Remove(presentAddress);
                    }

                    _dbContext.EmployeeDetails.Remove(details);
                    _dbContext.Employees.Remove(data);
                    _dbContext.Users.Remove(user);

                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return path;
                }
                return string.Empty;
            }
        }
        public async Task<IEnumerable<int>> GetEmployeeExitProcessList(IEnumerable<int?> designationIds)
        {
            return await _dbContext.EmployeeDetails.Where(x => designationIds.Contains(x.Employee.DesignationTypeId) && x.HasExited).Select(x => x.EmployeeId).ToListAsync();
        }
        #endregion Exit Process
    }
}
