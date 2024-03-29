using AutoMapper;
using HRMS.Core.Models.Document;
using HRMS.Core.Models.ImageKit;
using HRMS.Core.Utilities.Image;
using HRMS.DBL.Entities;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public class EmployeeDocumentService : IEmployeeDocumentService
    {
        private readonly EmployeeDocumentStore _employeeDocumentStore;
        private readonly IMapper _mapper;
        private readonly IImageKitUtility _imageKitUtility;
        private readonly EmployeeStore _employeeStore;
        public EmployeeDocumentService(EmployeeDocumentStore employeeDocumentStore, IMapper mapper, IImageKitUtility imageKitUtility, EmployeeStore employeeStore)
        {
            _employeeDocumentStore = employeeDocumentStore;
            _mapper = mapper;
            _imageKitUtility = imageKitUtility;
            _employeeStore = employeeStore;

        }

        #region Employee Document
        public async Task<bool> ManageDocument(EmployeeDocumentModel model)
        {
            var documentData = await _employeeDocumentStore.GetEmployeeDocumentById(model.EmployeeId, model.DocumentTypeId);
            Result imageData = null;

            var employee = await _employeeStore.GetEmployeeIdById(model.EmployeeId);
            if (employee == null)
            {
                return false;
            }

            if (documentData == null)
            {
                imageData = await _imageKitUtility.Upload(model.FolderPath, model.FileStream, model.FileName, model.IsPrivateFile, employee.Id);
            }
            else
            {
                var fileId = documentData?.ImagekitDetail?.FileId ?? employee?.ImagekitDetail?.FileId;
                var imageKitId = documentData?.ImagekitDetailId ?? employee?.ImagekitDetailId;
                imageData = await _imageKitUtility.UpdateFile(model.FolderPath, model.FileStream, model.FileName, model.IsPrivateFile, documentData.Employee.Id, fileId);
                await _employeeDocumentStore.DeleteImagekitDocument(imageKitId);
            }

            var imagekitData = new ImagekitDetailModel()
            {
                FileId = imageData.fileId,
                Url = imageData.url,
                Thumbnail = imageData.thumbnailUrl,
                FileType = imageData.fileType,
                FilePath = imageData.filePath,
                IsPrivateFile = imageData.isPrivateFile,
                FileName = imageData.name
            };

            var newFileId = await _employeeDocumentStore.AddorUpdateDocumentData(imagekitData);
            model.ImagekitDetailId = newFileId;

            var result = await _employeeDocumentStore.AddorUpdateDocument(model);
            return result;
        }
        public async Task<bool> DeleteDocument(DeleteDocumentModel model)
        {
            var documentData = await _employeeDocumentStore.GetEmployeeDocumentById(model.EmployeeId, model.DocumentTypeId);
            await _imageKitUtility.DeleteFile(documentData?.ImagekitDetail?.FileId);
            await _employeeDocumentStore.DeleteImagekitDocument(documentData?.ImagekitDetail?.Id);
            var result = await _employeeDocumentStore.DeleteDocument(model.EmployeeId, model.DocumentTypeId);
            return result;
        }
        public async Task<IEnumerable<EmployeeDocumentModel>> GetEmployeeDocumentById(int employeeId)
        {
            var data = await _employeeDocumentStore.GetEmployeeDocumentById(employeeId);
            return data?.Select(e => new EmployeeDocumentModel
            {
                Id = e.Id,
                EmployeeId = e.EmployeeId,
                DocumentTypeId = e.DocumentTypeId,
                ImagekitDetailId = e.ImagekitDetailId,
                FileId = e.ImagekitDetail?.FileId,
                FolderPath = e.ImagekitDetail?.FilePath,
                IsPrivateFile = e.ImagekitDetail?.IsPrivateFile ?? true,
                Url = e.ImagekitDetail?.Url,
                Thumbnail = e.ImagekitDetail?.Thumbnail,
                FileType = e.ImagekitDetail?.FileType,
            }).GroupBy(x => x.DocumentTypeId).Select(x => x.LastOrDefault()).ToList();
        }
        public async Task<IEnumerable<EmployeeContractDocumentModel>> GetEmployeeContracts(int employeeId)
        {
            var data = await _employeeDocumentStore.GetEmployeeContracts(employeeId);
            return data?.Select(x => new EmployeeContractDocumentModel
            {
                Id = x.ImagekitDetailId,
                Name = x.ImagekitDetail?.FileName,
                Url = x.ImagekitDetail?.Url,
                Year = x.ContractStartDate.Year,
            });
        }
        #endregion Employee Document
    }
}

