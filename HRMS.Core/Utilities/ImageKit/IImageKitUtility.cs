using HRMS.Core.Models.ImageKit;
using Imagekit.Models;
using System;
using System.Threading.Tasks;

namespace HRMS.Core.Utilities.Image
{
    public interface IImageKitUtility
    {
        Task<Result> Upload(string folderPath, byte[] fileStream, string fileName, bool publicRead, int employeeid, string fileId = "", int? year = null);
        Task<ImakgeKitResponse> GetFile(string fileId);
        Task<ResultDelete> DeleteFile(string fileId);
        Task<Result> UpdateFile(string folderPath, byte[] fileStream, string fileName, bool publicRead, int employeeid, string fileId, int? year = null);
        Task<ResponseMetaData> MoveFolder(MoveFolderRequest request);
        Task<Result> TempUpload(string folderPath, Byte[] fileStream, string fileName, bool publicRead);
        Task<ResultNoContent> MoveFile(MoveFileRequest request);
        Task<ResultNoContent> MoveFile(string sourceFilePath, string destinationPath);
        Task<ResultNoContent> CopyFile(CopyFileRequest request);
        Task<ResultNoContent> CopyFile(string sourceFilePath, string destinationPath);
        Task<ResultList> GetFileList(GetFileListRequest request);
        Task<bool?> CheckFileExistsById(string path, string fileId);
        Task<bool?> CheckFileExistsByName(string path, string name);
        Task<ResponseMetaData> DeleteFolder(DeleteFolderRequest request);
        Task<ResponseMetaData> DeleteFolder(string path);
    }
}