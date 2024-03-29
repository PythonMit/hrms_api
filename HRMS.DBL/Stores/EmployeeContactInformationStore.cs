using HRMS.DBL.DbContextConfiguration;
using System.Linq;
using System.Threading.Tasks;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using HRMS.Core.Consts;
using System.Transactions;
using AutoMapper;
using HRMS.Core.Models.Employee;

namespace HRMS.DBL.Stores
{
    public class EmployeeContactInformationStore : BaseStore
    {
        private readonly IMapper _mapper;

        public EmployeeContactInformationStore(HRMSDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<EmployeeDetail> GetEmployeeContactInfomation(int id)
        {
            var data = await _dbContext.EmployeeDetails.Include(x => x.PresentAddress).Include(x => x.PermanentAddress).Where(x => x.EmployeeId == id && x.RecordStatus == RecordStatus.Active).FirstOrDefaultAsync();
            return data;
        }
        public async Task<int> AddorUpdateContactInformation(EmployeeContactInformationModel employeedetails)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeDetails.Include(x => x.Employee).FirstOrDefaultAsync(x => x.EmployeeId == employeedetails.EmployeeId && x.RecordStatus == RecordStatus.Active);
                if (data == null)
                {
                    data = new EmployeeDetail();
                    data.EmployeeId = employeedetails.EmployeeId.Value;
                }
                data.MobileNumber = employeedetails.MobileNumber;
                data.AlternateMobileNumber = employeedetails.AlternateMobileNumber;
                data.PersonalEmail = employeedetails.PersonalEmail;
                data.WorkEmail = employeedetails.WorkEmail;
                data.PresentAddressId = await AddorUpdatePresentAddress(employeedetails);
                data.PermanentAddressId = await AddorUpdatePermanentAddress(employeedetails);
                _dbContext.Entry(data).State = (data.Id == 0 ? EntityState.Added : EntityState.Modified);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return data.Employee.UserId.Value;
            }
        }
        public async Task<int> AddorUpdatePermanentAddress(EmployeeContactInformationModel employeeaddress)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var employeeAddress = _dbContext.EmployeeDetails.FirstOrDefault(x => x.PermanentAddressId == employeeaddress.PermanentAddress.Id && x.RecordStatus == RecordStatus.Active);
                if (employeeAddress == null)
                {
                    var data = _mapper.Map<EmployeeAddress>(employeeaddress.PermanentAddress);
                    var result = _dbContext.EmployeeAddresses.Add(data);
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return result.Entity.Id;
                }
                else
                {
                    var data = await _dbContext.EmployeeAddresses.FirstOrDefaultAsync(x => x.Id == employeeAddress.PermanentAddressId && x.RecordStatus == RecordStatus.Active);
                    if (data == null)
                    {
                        return 0;
                    }

                    data.AddressLine1 = employeeaddress.PermanentAddress.AddressLine1;
                    data.AddressLine2 = employeeaddress.PermanentAddress.AddressLine2;
                    data.Pincode = employeeaddress.PermanentAddress.Pincode;
                    data.City = employeeaddress.PermanentAddress.City;
                    data.State = employeeaddress.PermanentAddress.State;
                    data.Country = employeeaddress.PermanentAddress.Country;
                    _dbContext.Entry(data).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return data.Id;
                }
            }
        }
        public async Task<int> AddorUpdatePresentAddress(EmployeeContactInformationModel employeeaddress)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var employeeAddress = _dbContext.EmployeeDetails.FirstOrDefault(x => x.PresentAddressId == employeeaddress.PresentAddress.Id && x.RecordStatus == RecordStatus.Active);
                if (employeeAddress == null)
                {
                    var data = _mapper.Map<EmployeeAddress>(employeeaddress.PresentAddress);
                    var result = _dbContext.EmployeeAddresses.Add(data);
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return result.Entity.Id;
                }
                else
                {
                    var data = await _dbContext.EmployeeAddresses.FirstOrDefaultAsync(x => x.Id == employeeAddress.PresentAddressId && x.RecordStatus == RecordStatus.Active);
                    if (data == null)
                    {
                        return 0;
                    }
                    data.AddressLine1 = employeeaddress.PresentAddress.AddressLine1;
                    data.AddressLine2 = employeeaddress.PresentAddress.AddressLine2;
                    data.City = employeeaddress.PresentAddress.City;
                    data.Country = employeeaddress.PresentAddress.Country;
                    data.State = employeeaddress.PresentAddress.State;
                    data.Pincode = employeeaddress.PresentAddress.Pincode;
                    _dbContext.Entry(data).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return data.Id;
                }
            }
        }
    }
}
