using HRMS.DBL.DbContextConfiguration;
using System.Linq;
using System.Threading.Tasks;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using HRMS.Core.Consts;
using System.Transactions;
using HRMS.Core.Models.Employee;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace HRMS.DBL.Stores
{
    public class EmployeeBankInformationStore : BaseStore
    {
        private readonly EmployeeContractStore _employeeContractStore;

        public EmployeeBankInformationStore(HRMSDbContext dbContext, EmployeeContractStore employeeContractStore) : base(dbContext)
        {
            _employeeContractStore = employeeContractStore;
        }

        public async Task<EmployeeBankDetail> GetEmployeeBankInfomation(string employeeCode)
        {
            var recordId = await _dbContext.EmployeeDetails.Where(x => x.Employee.EmployeeCode == employeeCode && x.RecordStatus == RecordStatus.Active).Select(x => x.EmployeeBankDetailId).FirstOrDefaultAsync();
            return await _dbContext.EmployeeBankDetails.FirstOrDefaultAsync(x => x.Id == recordId);
        }
        public async Task<int> AddorUpdateBankInformation(EmployeeBankInformationModel employeedetails)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var record = await _dbContext.EmployeeDetails.FirstOrDefaultAsync(x => x.Employee.EmployeeCode == employeedetails.EmployeeCode && x.RecordStatus == RecordStatus.Active);

                var data = (record != null ? await _dbContext.EmployeeBankDetails.FirstOrDefaultAsync(x => x.Id == record.EmployeeBankDetailId) : new EmployeeBankDetail());
                if (data == null)
                {
                    data = new EmployeeBankDetail();
                }
                data.BankName = employeedetails.BankName;
                data.IFSCCode = employeedetails.IFSCCode;
                data.BeneficiaryACNumber = employeedetails.BeneficiaryACNumber;
                data.BeneficiaryName = employeedetails.BeneficiaryName;
                data.BeneficiaryEmail = employeedetails.BeneficiaryEmail;
                data.TransactionType = employeedetails?.TransactionType;

                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);

                await _dbContext.SaveChangesAsync();

                await UpdateEmployeeDetails(record, data.Id);
                transaction.Complete();
                return data.Id;
            }
        }
        private async Task UpdateEmployeeDetails(EmployeeDetail record, int bankdetailId)
        {
            if (record != null)
            {
                record.EmployeeBankDetailId = bankdetailId;
                _dbContext.Entry(record).State = EntityState.Modified;
            }

            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<EmployeeBankDetail>> GetEmployeeBankInfomationList(IEnumerable<string> employeeCodes)
        {
            var contracts = await _employeeContractStore.GetRunningEmployeeContracts(employeeCodes);
            var recordIds = await _dbContext.EmployeeDetails.Where(x => contracts.Any(y => y.EmployeeId == x.EmployeeId) && x.Employee.RecordStatus == RecordStatus.Active)
                                                            .Select(x => x.EmployeeBankDetailId)
                                                            .ToListAsync();
            return await _dbContext.EmployeeBankDetails.Where(x => recordIds.Contains(x.Id)).ToListAsync();
        }
    }
}
