using HRMS.DBL.Stores;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Services.Interfaces;
using HRMS.Core.Utilities.Image;
using HRMS.Core.Settings;
using Microsoft.Extensions.Options;
using Imagekit.Models;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.ImageKit;
using HRMS.DBL.Extensions;

namespace HRMS.Services.Common
{
    public class EmployeeBasicInformationService : IEmployeeBasicInformationService
    {
        private readonly EmployeeBasicInformationStore _employeeBasicInformationStore;
        private readonly IMapper _mapper;
        private readonly UserStore _userStore;
        private readonly IImageKitUtility _imageKitUtility;
        private readonly EmployeeDocumentStore _employeeDocumentStore;
        private readonly EmployeeStore _employeeStore;
        private readonly ImageKitSetting _imageKitSetting;

        public EmployeeBasicInformationService(EmployeeBasicInformationStore employeeBasicInformationStore, IMapper mapper, UserStore userStore, IImageKitUtility imageKitUtility, EmployeeDocumentStore employeeDocumentStore, EmployeeStore employeeStore, IOptions<ImageKitSetting> imageKitSetting)
        {
            _employeeBasicInformationStore = employeeBasicInformationStore;
            _mapper = mapper;
            _userStore = userStore;
            _imageKitUtility = imageKitUtility;
            _employeeDocumentStore = employeeDocumentStore;
            _employeeStore = employeeStore;
            _imageKitSetting = imageKitSetting.Value;
        }

        public async Task<BasicInformationModel> GetEmployeeBasicInformation(int id)
        {
            var data = await _employeeBasicInformationStore.GetEmployeeBasicInfomation(id);
            return data.ToEmployeeBasicInformation().FirstOrDefault();
        }

        public async Task<int?> AddorUpdateEmployeeBasicInformation(BasicInformationModel model, byte[] fileStream, string folderpath, bool publicRead, string fileName)
        {
            int? result = null;
            var documentData = await _employeeStore.GetEmployeeImagekitData(model.Id);
            Result imageData = null;
            ImagekitDetailModel imagekitData = null;
            bool isFileNotAvailable = (documentData == null || (documentData != null && !documentData.ImagekitDetailId.HasValue));
            if (fileStream != null)
            {
                if (isFileNotAvailable)
                {
                    imageData = await _imageKitUtility.TempUpload(folderpath, fileStream, fileName, publicRead);
                }
                else
                {
                    imageData = await _imageKitUtility?.UpdateFile(folderpath, fileStream, fileName, publicRead, documentData.EmployeeId, documentData?.ImagekitDetailFileId);
                    await _employeeDocumentStore?.DeleteImagekitDocument(documentData?.ImagekitDetailId, documentData.EmployeeId);
                }
            }

            if (imageData != null)
            {
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

                model.ProfilePhotoId = await _employeeDocumentStore.AddorUpdateDocumentData(imagekitData);
            }

            var newEmployeeId = await _employeeBasicInformationStore?.AddorUpdateEmployeeBasicInformation(model);

            if (newEmployeeId > 0)
            {
                var employeeDetail = new EmployeeDetailModel();
                employeeDetail.EmployeeId = (int)newEmployeeId;
                employeeDetail.InstituteName = model.InstituteName;
                employeeDetail.AllowEditPersonalDetails = model.AllowEditPersonalDetails;
                result = await _employeeBasicInformationStore.AddorUpdateEmployeeDetailBasicInformation(employeeDetail);
            }

            var folderData = await _employeeStore?.GetEmployeeImagekitData(newEmployeeId);
            if (folderData != null && imagekitData != null && newEmployeeId > 0 && isFileNotAvailable)
            {
                await ImagekitUpload(folderData, imageData, newEmployeeId, imagekitData);
            }

            return result;
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
    }
}
