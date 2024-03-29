using AutoMapper;
using HRMS.Core.Consts;
using HRMS.Core.Models.Contract;
using HRMS.Core.Models.ImageKit;
using HRMS.Core.Models.Salary;
using HRMS.Core.Settings;
using HRMS.Core.Utilities.General;
using HRMS.Core.Utilities.Image;
using HRMS.DBL.Extensions;
using HRMS.DBL.Stores;
using HRMS.Services.Interfaces;
using Imagekit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public class EmployeeContractService : IEmployeeContractService
    {
        private readonly IGeneralUtilities _generalUtilities;
        private readonly IMapper _mapper;
        private readonly IImageKitUtility _imageKitUtility;
        private readonly ISystemFlagService _systemFlagService;

        private readonly EmployeeContractStore _employeeContractStore;
        private readonly EmployeeDocumentStore _employeeDocumentStore;
        private readonly EmployeeSalaryStore _employeeSalaryStore;
        private readonly ImageKitSetting _imageKitSetting;
        public EmployeeContractService(EmployeeContractStore employeeContractStore, IGeneralUtilities generalUtilities, IMapper mapper, IImageKitUtility imageKitUtility, ISystemFlagService systemFlagService,
            EmployeeDocumentStore employeeDocumentStore, IOptions<ImageKitSetting> imageKitSetting, EmployeeSalaryStore employeeSalaryStore)
        {
            _employeeContractStore = employeeContractStore;
            _generalUtilities = generalUtilities;
            _mapper = mapper;
            _imageKitUtility = imageKitUtility;
            _employeeDocumentStore = employeeDocumentStore;
            _imageKitSetting = imageKitSetting.Value;
            _systemFlagService = systemFlagService;
            _employeeSalaryStore = employeeSalaryStore;
        }
        public async Task<EmployeeContractListModel> GetAllEmployeeContract(EmployeeContractFilterModel filter, RoleTypes? userRole)
        {
            var data = await _employeeContractStore.GetAllEmployeeContract(filter, userRole);

            int totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = data?.Count() ?? 0;
                data = data?.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize);
            }

            var details = await data?.ToEmployeeContractsList().ToListAsync();
            return new EmployeeContractListModel
            {
                TotalRecords = totalRecords,
                ContractList = details,
            };
        }
        public async Task<IEnumerable<EmployeeContractHistoryModel>> GetEmployeeContractHistoryByEmployeeCode(string employeeCode, RecordStatus recordStatus = RecordStatus.Active)
        {
            var data = await _employeeContractStore.GetEmployeeContractHistoryByEmployeeCode(employeeCode, recordStatus);
            return await data?.ToEmployeeContractHistoryDetail().ToListAsync();
        }
        public async Task<EmployeeContractViewModel> GetRemainingEmployeeDetails(string employeeCode)
        {
            var data = await _employeeContractStore.GetRemainingEmployeeDetails(employeeCode);
            var result = data?.ToRemainingEmployeeDetails();
            if (result != null)
            {
                var lastContract = _employeeContractStore.GetLastContractEndDate(employeeCode);
                result.LastContractEndDate = (lastContract?.ContractEndDate == DateTime.MinValue ? null : lastContract?.ContractEndDate);
                result.ContractStatus = lastContract?.EmployeeContractStatus?.StatusType;
            }

            return result;
        }
        public async Task<IEnumerable<ContractEmployeeDetailModel>> GetRemainingEmployees(string employeeName)
        {
            var data = await _employeeContractStore.GetRemainingEmployees(employeeName);
            return data?.Select(x => new ContractEmployeeDetailModel
            {
                Id = x.Employee?.Id ?? 0,
                Code = x.Employee?.EmployeeCode,
                Name = $"{x.Employee.FirstName} {x.Employee.LastName}",
            }).ToList();
        }
        public async Task<EmployeeCurrentContractViewModel> GetEmployeeCurrentContractDetails(int contractId)
        {
            var data = await _employeeContractStore.GetEmployeeCurrentContractDetails(contractId);
            var details = await data?.ToEmployeeContractDetail().FirstOrDefaultAsync();

            if (details == null)
            {
                return null;
            }

            var contractPeriod = new ContractPeriod();
            contractPeriod.IncentiveDate = details.StartDate;
            var ContractLeftDays = details.EndDate - DateTime.Now.Date;
            contractPeriod.ContractEndDaysLeft = ContractLeftDays.Days;

            var probationPeriodLeftDays = details.StartDate.AddMonths(details.ProbationPeriod) - DateTime.Now.Date;
            contractPeriod.ProbationPeriodLastDate = probationPeriodLeftDays.Days > 0 ? details.StartDate.AddMonths(details.ProbationPeriod) : null;
            contractPeriod.ProbationPeriodDaysLeft = probationPeriodLeftDays.Days;

            var trainingPeriodLeftDays = details.StartDate.AddMonths(details.TrainingPeriod) - DateTime.Now.Date;
            contractPeriod.TrainingPeriodLastDate = trainingPeriodLeftDays.Days > 0 ? details.StartDate.AddMonths(details.TrainingPeriod) : null;
            contractPeriod.TrainingPeriodDaysLeft = trainingPeriodLeftDays.Days;

            return new EmployeeCurrentContractViewModel
            {
                EmployeeContractDetail = details,
                ContractPeriod = contractPeriod,
            };
        }
        public async Task<int?> AddOrUpdateEmployeeContractDetail(EmployeeContractRequestModel model, byte[] fileStream, string folderPath, bool publicRead, string fileName)
        {
            var documentData = await _employeeContractStore.GetEmployeeImagekitData(model.EmployeeId, model.ContractStartDate.Year, model.Id);
            Result imageData = null;
            ImagekitDetailModel imagekitData = null;
            bool isFileNotAvailable = (documentData == null || (documentData != null && documentData?.ImagekitDetailId == null));
            if (fileStream != null)
            {
                if (isFileNotAvailable)
                {
                    imageData = await _imageKitUtility.TempUpload(folderPath, fileStream, fileName, publicRead);
                }
                else
                {
                    imageData = await _imageKitUtility?.UpdateFile(folderPath, fileStream, fileName, publicRead, (documentData?.EmployeeId ?? 0), documentData?.ImagekitDetailFileId, model.ContractStartDate.Year);
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

                model.ImagekitDetailId = await _employeeDocumentStore.AddorUpdateDocumentData(imagekitData);
            }

            var result = await _employeeContractStore?.AddOrUpdateEmployeeContractDetail(model);

            if (result > 0)
            {
                if (model.FixGrossDetail?.CostToCompany > 0 || model.FixGrossDetail?.StipendAmount > 0)
                {
                    var systemFlags = await _systemFlagService.GetSystemFlagsByTags(GenericConst.FixGrossTags);

                    var ctc = (model.FixGrossDetail?.CostToCompany > 0 ? model.FixGrossDetail?.CostToCompany : Convert.ToDecimal(model.FixGrossDetail?.StipendAmount));
                    var grossCalculation = _generalUtilities.GetFixGrossCalculation(ctc ?? 0, systemFlags);
                    var data = _mapper.Map<EmployeeFixGrossModel>(grossCalculation);
                    if (model.DesignationTypeId == (int)DesignationTypes.ProjectTrainee)
                    {
                        data.StipendAmount = model.FixGrossDetail?.StipendAmount ?? 0;
                    }
                    else
                    {
                        data.CostToCompany = model.FixGrossDetail?.CostToCompany ?? 0;
                    }

                    data.EmployeeContractId = result.Value;
                    data.DesignationTypeId = model.DesignationTypeId;

                    data.FixIncentiveDetail = new FixIncentiveModel();
                    data.FixIncentiveDetail.IsFixIncentive = model.FixGrossDetail?.IsFixIncentive ?? false;
                    data.FixIncentiveDetail.FixIncentiveDuration = model.FixGrossDetail?.FixIncentiveDuration ?? 0;
                    data.FixIncentiveDetail.FixIncentiveRemarks = model.FixGrossDetail?.FixIncentiveRemarks;
                    data.FixIncentiveDetail.FixIncentiveDate = model.FixGrossDetail?.FixIncentiveDate;

                    await _employeeContractStore.AddOrUpdateEmployeeGrossDetail(data);


                    if (model.HasIncentiveUpdate)
                    {
                        await _employeeContractStore.AddOrUpdateEmployeeIncentives(result, data.FixIncentiveDetail.FixIncentiveDate);
                    }
                }

                var folderData = await _employeeContractStore?.GetEmployeeImagekitData(model.EmployeeId, model.ContractStartDate.Year, result);
                if (folderData != null && imagekitData != null && isFileNotAvailable)
                {
                    await ImagekitUpload(folderData, imageData, result, (documentData?.EmployeeId ?? model?.EmployeeId), imagekitData, model.ContractStartDate.Year);
                }
            }

            return result;
        }
        public async Task<bool> DeleteEmployeeContractDetails(int contractId)
        {
            return await _employeeContractStore.DeleteEmployeeContractDetails(contractId);
        }
        private async Task ImagekitUpload(EmployeeImagekitModel folderData, Result imageData, int? contractId, int? employeeId, ImagekitDetailModel imagekitData, int year, string sourceFolder = "contract")
        {
            if (folderData != null && imageData != null)
            {
                if (folderData?.FilePath == imageData?.filePath)
                {
                    var folderPath = folderData.FilePath;
                    var newFilePath = $"{_imageKitSetting.CDNFolderName}/{employeeId}/{sourceFolder}/{year}";
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
                        await _employeeContractStore.UpdateEmployeeImagekitFileId(contractId, employeeId, imageKitId, year);
                        await _employeeDocumentStore.DeleteImagekitDocument(folderData.ImagekitDetailId);
                    }
                }
            }
        }
        public async Task<EmployeeFixGrossModel> GetEmployeeFixGrossDetails(int contractId)
        {
            var data = await _employeeContractStore.GetEmployeeFixGrossByContractId(contractId);
            if (data == null)
            {
                return null;
            }

            return new EmployeeFixGrossModel
            {
                Id = data.Id,
                EmployeeContractId = data.EmployeeContractId,
                CostToCompany = Convert.ToDecimal(data.CostToCompany),
                Basic = data.Basic,
                DA = data.DA,
                HRA = data.HRA,
                ConveyanceAllowance = Convert.ToDouble(data.ConveyanceAllowance),
                OtherAllowance = Convert.ToDouble(data.OtherAllowance),
                FixIncentiveDetail = new FixIncentiveModel
                {
                    IsFixIncentive = data.IsFixIncentive,
                    FixIncentiveDuration = data.FixIncentiveDuration,
                    FixIncentiveRemarks = data.FixIncentiveRemarks,
                }
            };
        }
        public async Task<int?> GetEmployeeCurrentContractIdByEmployeeId(int employeeId)
        {
            return await _employeeContractStore.GetEmployeeCurrentContractIdByEmployeeId(employeeId);
        }
        public async Task<bool> GetRunningContract(RunningContractRequestModel model)
        {
            return await _employeeContractStore.GetRunningContract(model);
        }
        public async Task<bool> SetEmployeeContractStatus(int contractId, int statusType)
        {
            return await _employeeContractStore.SetEmployeeContractStatus(contractId, statusType);
        }
        public async Task<EmployeeIncentiveDataModel> GetEmployeeIncentivesDetails(string employeeCode)
        {
            var data = await _employeeSalaryStore.GetEmployeeIncentivesDetails(employeeCode);
            var reamrks = await _employeeSalaryStore.GetEmployeeIncentivesRemarks(employeeCode);
            return new EmployeeIncentiveDataModel
            {
                Remarks = reamrks,
                Incentives = data?.ToSalaryEmployeeIncentiveDetails().ToList()
            };
        }
        public async Task<bool?> RemoveContractInformations(int id)
        {
            var flag = await _systemFlagService.GetSystemFlagsByTag("permanentremove");
            if (flag == null || (flag != null && flag.Value == "disable"))
            {
                return null;
            }
            var data = await _employeeContractStore.RemoveContractInformations(id);
            if (!string.IsNullOrEmpty(data))
            {
                await _imageKitUtility.DeleteFolder(data);
            }
            return true;
        }
    }
}
