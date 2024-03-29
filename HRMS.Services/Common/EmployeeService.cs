using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.Services.Interfaces;
using HRMS.DBL.Stores;
using AutoMapper;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.General;
using HRMS.DBL.Extensions;
using System.Linq;
using HRMS.Core.Models.Branch;
using System.Data.Entity;
using HRMS.Core.Consts;
using HRMS.Core.Models.Employee.ExitProccess;
using HRMS.Core.Utilities.Image;
using HRMS.Core.Models.Document;
using HRMS.Core.Models.ImageKit;
using HRMS.Core.Settings;
using Imagekit.Models;
using System;
using Microsoft.Extensions.Options;

namespace HRMS.Services.Common
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeStore _employeeStore;
        private readonly EmployeeContractStore _employeeContractStore;
        private readonly EmployeeDocumentStore _employeeDocumentStore;

        private readonly IMapper _mapper;
        private readonly ISystemFlagService _systemFlagService;
        private readonly IImageKitUtility _imageKitUtility;
        private readonly ImageKitSetting _imageKitSetting;


        public EmployeeService(EmployeeStore employeeStore, IMapper mapper, EmployeeContractStore employeeContractStore, ISystemFlagService systemFlagService, IImageKitUtility imageKitUtility, IOptions<ImageKitSetting> imageKitSetting, EmployeeDocumentStore employeeDocumentStore)
        {
            _employeeStore = employeeStore;
            _mapper = mapper;
            _employeeContractStore = employeeContractStore;
            _systemFlagService = systemFlagService;
            _imageKitUtility = imageKitUtility;
            _imageKitSetting = imageKitSetting.Value;
            _employeeDocumentStore = employeeDocumentStore;
        }
        public async Task<EmployeeListModel> GetAllEmployees(EmployeeFilterModel filter, RoleTypes? roleTypes)
        {
            var data = await _employeeStore.GetAllEmployees(filter, roleTypes);

            int totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = data?.Count() ?? 0;
                data = data?.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize);
            }

            var result = data?.ToEmployeeDetailsModel(filter.Status).ToList();
            //result.ForEach(x => x.ContractStatus = _employeeContractStore.GetContractStatus(x.EmployeeCode));
            return new EmployeeListModel
            {
                TotalRecords = totalRecords,
                EmployeeRecords = result,
            };
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            var result = await _employeeStore.DeleteEmployee(id);
            if (result)
            {
                await _employeeContractStore.DeleteEmployeeContractDetails(id);
            }
            return result;
        }
        public async Task<EmployeeDetailModel> GetEmployeeDetails(int id)
        {
            var data = await _employeeStore.GetEmployeeDetails(id);
            return _mapper.Map<EmployeeDetailModel>(data);
        }
        public async Task<EmployeeInformationModel> GetEmployeeInformation(int id)
        {
            var data = await _employeeStore.GetEmployeeInformation(id);
            var result = _mapper.Map<EmployeeInformationModel>(data);
            if (result != null)
            {
                result.EmployeeDetail = new EmployeeModel
                {
                    Id = data?.Employee.Id ?? 0,
                    EmployeeCode = data?.Employee?.EmployeeCode,
                    FirstName = data?.Employee?.FirstName,
                    LastName = data?.Employee?.LastName,
                    Branch = new BranchModel
                    {
                        Id = data?.Employee?.Branch?.Id ?? 0,
                        Name = data?.Employee?.Branch?.Name,
                        Code = data?.Employee?.Branch?.Code,
                    },
                    Designation = data?.Employee?.DesignationType?.Name,
                    DateOfBirth = data?.Employee?.DateOfBirth,
                    Gender = data?.Employee?.Gender,
                    ProfilePhoto = data?.Employee?.ImagekitDetail?.Url
                };
                result.PresentAddress = _mapper.Map<EmployeeAddressModel>(data?.PresentAddress);
                result.PermanentAddress = _mapper.Map<EmployeeAddressModel>(data?.PermanentAddress);
            }

            return result;
        }
        public async Task<IEnumerable<EmployeeOutlineModel>> GetEmployeeInformationByRoleType(int roleTypeId)
        {
            var data = await _employeeStore.GetEmployeesByRoleType(roleTypeId);
            return data?.ToEmployeeOutLineDetails();
        }
        public async Task<bool> CheckEmployeeCodeExists(string employeeCode)
        {
            return await _employeeStore.CheckEmployeeCodeExists(employeeCode);
        }
        public async Task<IEnumerable<EmployeeOutlineModel>> GetEmployeeInformationByDesignation(IEnumerable<int?> designationIds, bool hasContract, RoleTypes? userRole, int? employeeId)
        {
            var data = await _employeeStore.GetEmployeeInformationByDesignation(designationIds, hasContract, userRole, employeeId);
            return data?.ToEmployeeOutLineDetails().GroupBy(x => x.Code).Select(x => x.FirstOrDefault()).ToList();
        }
        public async Task<EmployeeOutlineModel> GetEmployeeInformationByCode(string employeeCode)
        {
            var data = await _employeeStore.GetEmployeeInformationByCode(employeeCode);
            return data?.ToEmployeeOutLineDetails().FirstOrDefault();
        }
        public async Task<bool?> RemoveEmployeeInformation(string employeeCode)
        {
            var flag = await _systemFlagService.GetSystemFlagsByTag("permanentremove");
            if (flag == null || (flag != null && flag.Value == "disable"))
            {
                return null;
            }
            var contract = await _employeeContractStore.GetTotalContract(employeeCode);
            if (contract > 0)
            {
                return false;
            }
            var data = await _employeeStore.RemoveEmployeeInformation(employeeCode);
            if (!string.IsNullOrEmpty(data))
            {
                await _imageKitUtility.DeleteFolder(data);
            }
            return true;
        }
        public async Task<string> GetEmployeeSlackId(string employeeCode)
        {
            return await _employeeStore.GetEmployeeSlackId(employeeCode);
        }
        public async Task<string> UpdateEmployeeSlackId(string workEmail, string slackUserId)
        {
            return await _employeeStore.UpdateEmployeeSlackId(workEmail, slackUserId);
        }
        public async Task<string> GetEmployeeEmail(string employeeCode)
        {
            return await _employeeStore.GetEmployeeEmail(employeeCode);
        }
        public async Task<string> GetEmployeeName(string employeeCode)
        {
            return await _employeeStore.GetEmployeeName(employeeCode);
        }
        public async Task<Guid?> AddorUpdateEmployeeImage(EmployeeImageModel model, byte[] fileStream, string folderpath, string fileName)
        {
            var documentData = await _employeeStore.GetEmployeeImagekitData(model.Id);
            Result imageData = null;
            ImagekitDetailModel imagekitData = null;
            bool isFileNotAvailable = (documentData == null || (documentData != null && !documentData.ImagekitDetailId.HasValue));
            if (fileStream != null)
            {
                if (isFileNotAvailable)
                {
                    imageData = await _imageKitUtility.TempUpload(folderpath, fileStream, fileName, model.PublicRead);
                }
                else
                {
                    imageData = await _imageKitUtility?.UpdateFile(folderpath, fileStream, fileName, model.PublicRead, documentData.EmployeeId, documentData?.ImagekitDetailFileId);
                    await _employeeDocumentStore?.DeleteImagekitDocument(documentData?.ImagekitDetailId, documentData.EmployeeId);
                }
            }

            if (imageData == null)
            {
                return null;
            }

            imagekitData = new ImagekitDetailModel()
            {
                FileId = imageData.fileId,
                Url = imageData.url,
                Thumbnail = imageData.thumbnailUrl,
                FileType = imageData.fileType,
                FilePath = imageData.filePath,
                IsPrivateFile = imageData.isPrivateFile,
                FileName = imageData.name
            };

            var profilePhotoId = await _employeeDocumentStore.AddorUpdateDocumentData(imagekitData);

            var employeeId = await _employeeStore.GetEmployeeIdByCode(model.EmployeeCode);
            var folderData = await _employeeStore?.GetEmployeeImagekitData(employeeId);
            if (folderData != null && imagekitData != null && employeeId > 0 && isFileNotAvailable)
            {
                await ImagekitUpload(folderData, imageData, employeeId, imagekitData);
            }

            return profilePhotoId;
        }
        private async Task ImagekitUpload(EmployeeImagekitModel folderData, Result imageData, int? newEmployeeId, ImagekitDetailModel imagekitData, string sourceFolder = "profile")
        {
            if (folderData != null && imageData != null)
            {
                if (folderData?.FilePath == imageData?.filePath)
                {
                    var folderPath = folderData.FilePath;
                    var newFilePath = $"{_imageKitSetting.CDNFolderName}/{newEmployeeId}/{sourceFolder}";
                    MoveFileRequest fileRequest = new MoveFileRequest()
                    {
                        sourceFilePath = folderPath,
                        destinationPath = newFilePath
                    };

                    await _imageKitUtility.MoveFile(fileRequest);
                    GetFileListRequest listRequest = new GetFileListRequest()
                    {
                        Path = $"{newFilePath}"
                    };

                    var resultList = await _imageKitUtility.GetFileList(listRequest);
                    var newFiledata = resultList?.FileList?.OrderByDescending(x => x.createdAt).FirstOrDefault();

                    DeleteFolderRequest deleteFolderRequest = new DeleteFolderRequest()
                    {
                        folderPath = folderPath.Substring(0, folderPath.LastIndexOf(("/"))),
                    };
                    await _imageKitUtility.DeleteFolder(deleteFolderRequest);

                    if (newFiledata != null)
                    {
                        imagekitData.FileId = newFiledata.fileId;
                        imagekitData.FilePath = newFiledata?.filePath;
                        imagekitData.Url = newFiledata?.url;

                        var imageKitId = await _employeeDocumentStore.AddorUpdateDocumentData(imagekitData);
                        await _employeeStore.UpdateEmployeeImagekitFileId(newEmployeeId, imageKitId.Value);
                        await _employeeDocumentStore.DeleteImagekitDocument(folderData.ImagekitDetailId);
                    }
                }
            }
        }
        #region Exit Process
        public async Task<EmployeeExitProcessListModel> GetExitProcessEmployees(EmployeeFilterModel filter, RoleTypes? userRole)
        {
            var data = await _employeeStore.GetExitProcessEmployees(filter, userRole);

            int totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = data?.Count() ?? 0;
                data = data?.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize);
            }

            var result = data?.ToEmployeeExitProcessList().ToList();
            result.ForEach(x => x.ContractStatus = _employeeContractStore.GetRunningContractStatus(x.Employee.Code));
            return new EmployeeExitProcessListModel
            {
                TotalRecords = totalRecords,
                Records = result,
            };
        }
        public async Task<int?> AddorUpdateFNFProcessEmployee(EmployeeFNFDetailsRequestModel model, RoleTypes? roleTypes)
        {
            return await _employeeStore.AddorUpdateFNFProcessEmployee(model, roleTypes);
        }
        public async Task<int?> AddorUpdateExitProcessEmployee(EmployeeExitRequestModel model, RoleTypes? roleTypes)
        {
            return await _employeeStore.AddorUpdateExitProcessEmployee(model, roleTypes);
        }
        public async Task<EmployeeFNFDetailsModel> GetExitProcessNotes(string employeeCode)
        {
            var data = await _employeeStore.GetExitProcessNotes(employeeCode);
            var result = data?.ToEmployeeExitProcessList().FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            result.ContractStatus = _employeeContractStore.GetRunningContractStatus(employeeCode);
            return result;
        }
        #endregion Exit Process
    }
}
