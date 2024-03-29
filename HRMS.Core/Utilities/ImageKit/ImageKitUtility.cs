using HRMS.Core.Settings;
using System;
using System.Linq;
using System.Threading.Tasks;
using Imagekit.Sdk;
using Microsoft.Extensions.Options;
using Imagekit.Models;
using System.IO;
using HRMS.Core.Models.ImageKit;
using static System.Net.WebRequestMethods;

namespace HRMS.Core.Utilities.Image
{
    public class ImageKitUtility : IImageKitUtility
    {

        private readonly ImageKitSetting _imageKitSetting;

        public ImageKitUtility(IOptions<ImageKitSetting> options)
        {
            _imageKitSetting = options.Value;
        }
        public async Task<ResultDelete> DeleteFile(string fileId)
        {
            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            if (string.IsNullOrEmpty(fileId))
            {
                return null;
            }
            return await imagekit.DeleteFileAsync(fileId);
        }
        public async Task<ImakgeKitResponse> GetFile(string fileId)
        {
            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            ResponseMetaData resp = await imagekit.GetFileDetailAsync(fileId);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ImakgeKitResponse>(resp.Raw);
        }
        public async Task<Result> UpdateFile(string folderPath, byte[] filestream, string fileName, bool publicRead, int emploeyeeId, string fileId, int? year = null)
        {
            if (filestream == null)
            {
                return null;
            }

            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            FileUpdateRequest updateob = new FileUpdateRequest
            {
                fileId = fileId,
            };
            await imagekit.UpdateFileDetailAsync(updateob);
            await imagekit.DeleteFileAsync(fileId);

            byte[] bytes = filestream;
            FileCreateRequest ob = new FileCreateRequest
            {
                file = bytes,
                fileName = fileName,
                folder = $"{_imageKitSetting.CDNFolderName}/{emploeyeeId}/{folderPath}{(year.HasValue ? $"/{year}" : "")}",
                isPrivateFile = publicRead
            };
            return await imagekit.UploadAsync(ob);
        }
        public async Task<Result> Upload(string folderPath, byte[] filestream, string fileName, bool publicRead, int emploeyeeId, string fileId = "", int? year = null)
        {
            if (filestream == null)
            {
                return null;
            }

            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            CreateFolderRequest createFolderRequest = new CreateFolderRequest
            {
                folderName = folderPath,
                parentFolderPath = $"{_imageKitSetting.CDNFolderName}/{emploeyeeId}"
            };
            await imagekit.CreateFolderAsync(createFolderRequest);
            byte[] bytes = filestream;

            var path = $"{createFolderRequest.parentFolderPath}/{createFolderRequest.folderName}{(year.HasValue ? $"/{year}" : "")}";
            if (!string.IsNullOrEmpty(fileName))
            {
                var fileExists = await CheckFileExistsByName(path, fileName);

                if (fileExists == true)
                {
                    fileName = $"{Path.GetFileNameWithoutExtension(fileName)}_1{Path.GetExtension(fileName)}";
                }
            }

            FileCreateRequest ob = new FileCreateRequest
            {
                file = bytes,
                fileName = fileName,
                folder = path,
                isPrivateFile = publicRead,
            };
            return await imagekit.UploadAsync(ob);
        }
        public async Task<ResponseMetaData> MoveFolder(MoveFolderRequest request)
        {
            if (request == null)
            {
                return null;
            }

            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            return await imagekit.MoveFolderAsync(request);
        }
        public async Task<ResultNoContent> MoveFile(MoveFileRequest request)
        {
            if (request == null)
            {
                return null;
            }

            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            return await imagekit.MoveFileAsync(request);
        }
        public async Task<ResultNoContent> MoveFile(string sourceFilePath, string destinationPath)
        {
            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            return await imagekit.MoveFileAsync(new MoveFileRequest { sourceFilePath = sourceFilePath, destinationPath = destinationPath });
        }
        public async Task<ResultNoContent> CopyFile(CopyFileRequest request)
        {
            if (request == null)
            {
                return null;
            }

            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            return await imagekit.CopyFileAsync(request);
        }
        public async Task<ResultNoContent> CopyFile(string sourceFilePath, string destinationPath)
        {
            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            return await imagekit.CopyFileAsync(new CopyFileRequest { sourceFilePath = sourceFilePath, destinationPath = destinationPath });
        }
        public async Task<ResultList> GetFileList(GetFileListRequest request)
        {
            if (request == null)
            {
                return null;
            }

            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            return await imagekit.GetFileListRequestAsync(request);
        }
        public async Task<bool?> CheckFileExistsById(string path, string fileId)
        {
            if (string.IsNullOrEmpty(path.Trim()) && string.IsNullOrEmpty(fileId.Trim()))
            {
                return null;
            }

            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            var resultList = await imagekit.GetFileListRequestAsync(new GetFileListRequest { Path = path });
            return resultList?.FileList?.Any(x => x.fileId == fileId);
        }
        public async Task<bool?> CheckFileExistsByName(string path, string fileName)
        {
            if (string.IsNullOrEmpty(path.Trim()) && string.IsNullOrEmpty(fileName.Trim()))
            {
                return null;
            }

            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            var resultList = await imagekit.GetFileListRequestAsync(new GetFileListRequest { Path = path });
            return resultList?.FileList?.Any(x => x.name == fileName);
        }
        public async Task<Result> TempUpload(string folderPath, Byte[] fileStream, string fileName, bool publicRead)
        {
            if (fileStream == null)
            {
                return null;
            }

            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            Random rnd = new Random();
            byte[] bytes = fileStream;
            FileCreateRequest ob = new FileCreateRequest
            {
                file = bytes,
                fileName = fileName,
                folder = $"{rnd.Next()}_{folderPath}",
                isPrivateFile = publicRead,
            };
            return await imagekit.UploadAsync(ob);
        }
        public async Task<ResponseMetaData> DeleteFolder(DeleteFolderRequest deleteFolderRequest)
        {
            if (deleteFolderRequest == null)
            {
                return null;
            }

            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            return await imagekit.DeleteFolderAsync(deleteFolderRequest);
        }
        public async Task<ResponseMetaData> DeleteFolder(string path)
        {
            if (string.IsNullOrEmpty(path.Trim()))
            {
                return null;
            }

            var imagekit = new ImagekitClient(_imageKitSetting.PublicKey, _imageKitSetting.PrivateKey, _imageKitSetting.UrlEndPoint);
            return await imagekit.DeleteFolderAsync(new DeleteFolderRequest { folderPath = path });
        }
    }

}
