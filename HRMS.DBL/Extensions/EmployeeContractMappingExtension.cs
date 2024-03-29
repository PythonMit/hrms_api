using HRMS.Core.Consts;
using HRMS.Core.Models.Branch;
using HRMS.Core.Models.Contract;
using HRMS.Core.Models.DesignationType;
using HRMS.Core.Models.General;
using HRMS.DBL.Entities;
using System;
using System.Linq;

namespace HRMS.DBL.Extensions
{
    public static class EmployeeContractMappingExtension
    {
        public static IQueryable<ContractDetailList> ToEmployeeContractsList(this IQueryable<EmployeeContract> data)
        {
            return data?.Select(x => new ContractDetailList
            {
                Id = x.Id,
                Employee = x.Employee == null ? null : new ContractEmployeeDetailModel
                {
                    Id = x.EmployeeId,
                    Name = $"{x.Employee.FirstName} {x.Employee.LastName}",
                    Code = x.Employee.EmployeeCode,
                    ProfileUrl = x.Employee.ImagekitDetail == null ? "" : x.Employee.ImagekitDetail.Url
                },
                Branch = x.Employee.Branch == null ? null : new BranchModel
                {
                    Id = x.Employee.Branch.Id,
                    Code = x.Employee.Branch.Code
                },
                Designation = x.Employee.DesignationType == null ? null : x.Employee.DesignationType.Name,
                StartDate = x.ContractStartDate,
                EndDate = x.ContractEndDate,
                ProbationPeriod = x.ProbationPeriod,
                TrainingPeriod = x.TrainingPeriod,
                HasFixIncentive = x.EmployeeFixGross.IsFixIncentive,
                Status = new EmployeeContractStatusModel
                {
                    Id = x.EmployeeContractStatus.Id,
                    StatusType = x.EmployeeContractStatus.StatusType
                },
            });
        }
        public static IQueryable<ContractResponseModel> ToEmployeeContractDetail(this IQueryable<EmployeeContract> data)
        {
            return data?.Select(x => new ContractResponseModel
            {
                Id = x.Id,
                Employee = x.Employee == null ? null : new ContractEmployeeDetailModel
                {
                    Id = x.EmployeeId,
                    Code = x.Employee.EmployeeCode,
                    Name = $"{x.Employee.FirstName} {x.Employee.LastName}",
                    ProfileUrl = x.Employee.ImagekitDetail == null ? "" : x.Employee.ImagekitDetail.Url
                },
                Branch = x.Employee.Branch == null ? null : new BranchModel
                {
                    Id = x.Employee.Branch.Id,
                    Code = x.Employee.Branch.Code,
                    Name = x.Employee.Branch.Name
                },
                StartDate = x.ContractStartDate,
                EndDate = x.ContractEndDate,
                ProbationPeriod = x.ProbationPeriod,
                TrainingPeriod = x.TrainingPeriod,
                IsProjectTrainee = x.IsProjectTrainee,
                Remarks = x.Remarks,
                ContractDocument = x.ImagekitDetail == null ? "" : x.ImagekitDetail.Url,
                Status = x.EmployeeContractStatus == null ? null : new EmployeeContractStatusModel
                {
                    Id = x.EmployeeContractStatus.Id,
                    StatusType = x.EmployeeContractStatus.StatusType
                },
                Designation = x.DesignationType == null ? null : new DesignationTypeModel
                {
                    Id = x.DesignationType.Id,
                    Name = x.DesignationType.Name,
                },
                DropInformation = (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.Drop) ? new DropInformationModel
                {
                    DropDate = x.DropDate,
                    Remarks = x.DropRemarks
                } : null,
                NoticePeriod = (x.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod) ? new NoticePeriodModel
                {
                    EndDate = x.NoticePeriodEndDate,
                    StartDate = x.NoticePeriodStartDate,
                    Remarks = x.NoticeRemarks,
                } : null,
                FixGross = x.EmployeeFixGross == null ? null : new EmployeeFixGrossModel
                {
                    Id = x.EmployeeFixGross.Id,
                    EmployeeContractId = x.Id,
                    StipendAmount = x.EmployeeFixGross.StipendAmount,
                    CostToCompany = x.EmployeeFixGross.CostToCompany,
                    Basic = x.EmployeeFixGross.Basic,
                    DA = x.EmployeeFixGross.DA,
                    LTA = x.EmployeeFixGross.LTA,
                    HRA = x.EmployeeFixGross.HRA,
                    ConveyanceAllowance = x.EmployeeFixGross.ConveyanceAllowance,
                    OtherAllowance = x.EmployeeFixGross.OtherAllowance,
                    MedicalAllowance = x.EmployeeFixGross.MedicalAllowance,
                    ChildEducation = x.EmployeeFixGross.ChildEducation,
                    FixIncentiveDetail = new FixIncentiveModel
                    {
                        IsFixIncentive = x.EmployeeFixGross.IsFixIncentive,
                        FixIncentiveDuration = x.EmployeeFixGross.FixIncentiveDuration,
                        FixIncentiveRemarks = x.EmployeeFixGross.FixIncentiveRemarks,
                    }
                },
                TerminateInformation = new TerminateModel()
                {
                    TerminateDate = x.TerminateDate,
                    Remarks = x.TerminateRemarks
                },
                Incentives = x.EmployeeIncentiveDetails == null ? null : x.EmployeeIncentiveDetails.Select(y => new EmployeeIncentiveDetailModel
                {
                    Id = y.Id,
                    EmployeeContractId = y.EmployeeContractId,
                    EmployeeIncentiveStatusId = y.EmployeeIncentiveStatusId,
                    IncentiveAmount = y.IncentiveAmount,
                    IncentiveDate = y.IncentiveDate,
                    Remarks = y.Remarks,
                    Status = new EmployeeIncentiveStatusModel
                    {
                        Id = y.EmployeeIncentiveStatus.Id,
                        StatusType = y.EmployeeIncentiveStatus.StatusType,
                    }
                }),
            });
        }
        public static IQueryable<EmployeeContractHistoryModel> ToEmployeeContractHistoryDetail(this IQueryable<EmployeeContract> data)
        {
            return data.Select(x => new EmployeeContractHistoryModel
            {
                EmployeeContractId = x.Id,
                Employee = new ContractEmployeeDetailModel
                {
                    Id = x.Employee.Id,
                    Code = x.Employee.EmployeeCode,
                    Name = $"{x.Employee.FirstName} {x.Employee.LastName}",
                    ProfileUrl = x.Employee.ImagekitDetail == null ? "" : x.Employee.ImagekitDetail.Url
                },
                ContractStartDate = x.ContractStartDate,
                ContractendDate = x.ContractEndDate,
                CostToCompany = x.EmployeeFixGross.CostToCompany,
                StipendAmount = x.EmployeeFixGross.StipendAmount,
                FixIncentiveRemarks = x.EmployeeFixGross.FixIncentiveRemarks,
                Branch = x.Employee.Branch == null ? null : new BranchModel
                {
                    Id = x.Employee.Branch.Id,
                    Code = x.Employee.Branch.Code,
                    Name = x.Employee.Branch.Name
                },
                Designation = new DesignationTypeModel
                {
                    Id = x.DesignationType.Id,
                    Name = x.DesignationType.Name,
                    Description = x.DesignationType.Description
                },
                IsProjectTrainee = x.IsProjectTrainee,
                TrainingPeriod = x.TrainingPeriod,
                Status = x.EmployeeContractStatus == null ? "" : x.EmployeeContractStatus.StatusType
            });
        }
        public static IQueryable<EmployeeOutlineModel> ToEmployeeOutLineDetails(this IQueryable<EmployeeContract> data)
        {
            return data.Select(x => new EmployeeOutlineModel
            {
                Id = x.Employee.Id,
                Code = x.Employee.EmployeeCode,
                Name = $"{x.Employee.FirstName} {x.Employee.LastName}",
                Designation = x.Employee.DesignationType.Name,
                Branch = x.Employee.Branch == null ? "" : x.Employee.Branch.Name,
                BranchCode = x.Employee.Branch == null ? "" : x.Employee.Branch.Code,
                ProfileUrl = x.Employee.ImagekitDetail == null ? "" : x.Employee.ImagekitDetail.Url,
            });
        }
        public static EmployeeContractViewModel ToRemainingEmployeeDetails(this EmployeeDetail data)
        {
            return new EmployeeContractViewModel
            {
                Id = data.Employee?.Id ?? 0,
                Code = data.Employee?.EmployeeCode,
                Name = $"{data.Employee.FirstName} {data.Employee.LastName}",
                Gender = data.Employee?.Gender,
                JoinDate = data.JoinDate,
                Designation = data.Employee?.DesignationType?.Name,
                Branch = data.Employee?.Branch?.Name,
                BranchCode = data.Employee?.Branch?.Code,
                TotalContract = data.Employee?.EmployeeContracts?.Count() ?? 0,
            };
        }
    }
}
