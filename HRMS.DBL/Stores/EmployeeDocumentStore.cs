using HRMS.Core.Consts;
using HRMS.Core.Models.Document;
using HRMS.Core.Models.ImageKit;
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
    public class EmployeeDocumentStore : BaseStore
    {
        public EmployeeDocumentStore(HRMSDbContext dbContext) : base(dbContext) { }
        #region Employee Document
        public async Task<Guid?> AddorUpdateDocumentData(ImagekitDetailModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.ImagekitDetails.FirstOrDefaultAsync(x => x.Id == model.Id && x.FileId == model.FileId && x.RecordStatus == RecordStatus.Active);
                var newData = false;
                if (data == null)
                {
                    data = new ImagekitDetail();
                    data.Id = Guid.NewGuid();
                    newData = true;
                }
                data.FileId = model?.FileId;
                data.FilePath = model?.FilePath;
                data.FileName = model?.FileName;
                data.Url = model?.Url;
                data.Thumbnail = model?.Thumbnail;
                data.IsPrivateFile = model.IsPrivateFile;
                data.FileType = model?.FileType;
                if (newData)
                {
                    await _dbContext.ImagekitDetails.AddAsync(data);
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
        public async Task<bool> AddorUpdateDocument(EmployeeDocumentModel model)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeDocuments.FirstOrDefaultAsync(x => x.EmployeeId == model.EmployeeId
                                                                                    && (x.ImagekitDetailId.HasValue ? x.ImagekitDetailId == model.ImagekitDetailId : true) && x.DocumentTypeId == model.DocumentTypeId
                                                                                    && x.RecordStatus == RecordStatus.Active);
                var isNew = false;
                if (data == null)
                {
                    data = new EmployeeDocument();
                    isNew = true;
                }
                data.EmployeeId = model.EmployeeId;
                data.DocumentTypeId = model.DocumentTypeId;
                // TODO: This is not finizilzed and will be implimented later
                // data.DocumentFront = model.DocumentFront;
                // data.DocumentBack = model.DocumentBack;
                data.ImagekitDetailId = model.ImagekitDetailId.Value;
                if (isNew)
                {
                    await _dbContext.EmployeeDocuments.AddAsync(data);
                }
                else
                {
                    _dbContext.Entry(data).State = EntityState.Modified;
                }

                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return true;
            }
        }
        public async Task<bool> DeleteDocument(int id, int documentTypeId)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var data = await _dbContext.EmployeeDocuments.Where(x => x.EmployeeId == id && x.DocumentTypeId == documentTypeId && x.RecordStatus == RecordStatus.Active).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                _dbContext.Remove(data);
                await _dbContext.SaveChangesAsync();
                transaction.Complete();
                return true;
            }
        }
        public async Task<bool> DeleteImagekitDocument(Guid? imageKitId, int? employeeId = null)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (employeeId != null)
                {
                    var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId && x.RecordStatus == RecordStatus.Active);
                    if (employee != null)
                    {
                        employee.ImagekitDetailId = null;
                        await _dbContext.SaveChangesAsync();
                    }
                }

                var data = await _dbContext.ImagekitDetails.Where(x => x.Id == imageKitId && x.RecordStatus == RecordStatus.Active).FirstOrDefaultAsync();
                if (data != null)
                {
                    _dbContext.Remove(data);
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                    return true;
                }

                return false;
            }
        }
        public async Task<IEnumerable<EmployeeDocument>> GetEmployeeDocumentById(int employeeId)
        {
            return await _dbContext.EmployeeDocuments.Include(x => x.ImagekitDetail).Where(x => x.EmployeeId == employeeId && x.RecordStatus == RecordStatus.Active).ToListAsync();
        }
        public async Task<EmployeeDocument> GetEmployeeDocumentById(int id, int documentTypeId)
        {
            return await _dbContext.EmployeeDocuments.Include(x => x.Employee).Include(x => x.ImagekitDetail).Where(x => x.EmployeeId == id && x.DocumentTypeId == documentTypeId && x.RecordStatus == RecordStatus.Active).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<EmployeeContract>> GetEmployeeContracts(int employeeId)
        {
            return await _dbContext.EmployeeContracts.Include(x => x.ImagekitDetail).Where(x => x.EmployeeId == employeeId).ToListAsync();
        }
        #endregion Employee Document
    }
}