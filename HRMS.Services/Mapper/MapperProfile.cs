using AutoMapper;
using HRMS.DBL.Entities;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.Branch;
using HRMS.Core.Models.DesignationType;
using HRMS.Core.Models.Document;
using HRMS.Core.Models.Contract;
using HRMS.Core.Models.Overtime;
using HRMS.Core.Models.User;
using HRMS.Core.Models.Leave;
using HRMS.Core.Models.SystemFlag;
using System.Linq;
using HRMS.Core.Models.Salary;
using HRMS.Core.Models.Holiday;
using HRMS.Core.Models.ImageKit;
using HRMS.Core.Models.Project;
using HRMS.Core.Models.Notification;
using HRMS.Core.Models.Resource;
using System;
using HRMS.Core.Consts;
using HRMS.Core.Exstensions;

namespace HRMS.Services.Mapper
{
    public class ServicesMapperProfile : Profile
    {
        public ServicesMapperProfile()
        {
            CreateCommonMapping();
            CreateEmployeeInformationMapping();
            CreateEmployeeContractMapping();
            CreateEmployeeOverTimeMapping();
            CreateEmployeeLeaveMapping();
            CreateEmployeeSalaryMapping();
            CreateResourceMapping();
        }

        private void CreateCommonMapping()
        {
            CreateMap<UserModel, User>();
            CreateMap<EmployeeStatus, EmployeeStatusModel>();
            CreateMap<EmployeeContractStatus, EmployeeContractStatusModel>();
            CreateMap<DocumentType, DocumentTypeModel>();
            CreateMap<EmployeeOverTimeStatus, EmployeeOverTimeStatusModel>();
            CreateMap<EmployeeLeaveStatus, EmployeeLeaveStatusModel>();
            CreateMap<DesignationType, DesignationTypeModel>();
            CreateMap<LeaveType, LeaveTypeModel>();
            CreateMap<Branch, BranchModel>().ReverseMap();
            CreateMap<EmployeeModel, Employee>();
            CreateMap<Employee, EmployeeModel>();
            CreateMap<SystemFlagModel, SystemFlag>().ReverseMap();
            CreateMap<Role, RoleModel>().ReverseMap();
            CreateMap<EmployeeFixGross, EmployeeCurrentContractViewModel>().ReverseMap();
            CreateMap<EmployeeDetailModel, EmployeeCurrentContractViewModel>().ReverseMap();
            CreateMap<EmployeeFixGross, EmployeeContractHistoryModel>().ReverseMap();
            CreateMap<EmployeeSalaryStatus, EmployeeSalaryStatusModel>().ReverseMap();
            CreateMap<ImagekitDetail, ImagekitDetailModel>();
            CreateMap<Holiday, HolidayModel>();
            CreateMap<Project, ProjectModel>()
                 .AfterMap((s, d) =>
                 {
                     d.ProjectManagers = s.ProjectManagers?.Select(x => new ProjectManagerModel
                     {
                         Id = x?.EmployeeId ?? 0,
                         Name = (x.Employee == null ? "" : $"{(string.IsNullOrEmpty(x.Employee?.FirstName) ? "" : x.Employee?.FirstName + " ")}{(string.IsNullOrEmpty(x.Employee?.LastName) ? "" : x.Employee?.LastName)}"),
                         Code = x.Employee.EmployeeCode,
                     }).ToList();
                 });
            CreateMap<LeaveCategory, LeaveCategoryModel>();
            CreateMap<EmployeeHoldSalary, EmployeeHoldSalaryModel>();
            CreateMap<Notification, NotificationModel>();
            CreateMap<ResourceType, ResourceTypeModel>();
        }
        private void CreateEmployeeInformationMapping()
        {
            CreateMap<EmployeeDetail, EmployeeContactInformationModel>().ReverseMap();
            CreateMap<EmployeeDetail, EmployeePreviousEmployeerInformation>();
            CreateMap<EmployeeDetail, EmployeeDetailModel>();
            CreateMap<EmployeeDetailModel, EmployeeDetail>();
            CreateMap<EmployeeAddressModel, EmployeeAddress>().ReverseMap();
            CreateMap<EmployeeDocumentModel, EmployeeDocument>().ReverseMap();
            CreateMap<EmployeeJobInformationModel, Employee>().ReverseMap();
            CreateMap<BasicInformationModel, Employee>();
            CreateMap<EmployeeDetail, EmployeeInformationModel>();
            CreateMap<EmployeeBankDetail, EmployeeBankInformationModel>();
        }
        private void CreateEmployeeContractMapping()
        {
            CreateMap<ContractResponseModel, EmployeeContract>();
            CreateMap<EmployeeFixGrossModel, EmployeeFixGross>().ReverseMap();
            CreateMap<EmployeeContractViewModel, EmployeeDetail>().ReverseMap();
            CreateMap<EmployeeContract, EmployeeFixGrossModel>().ReverseMap();
            CreateMap<EmployeeContract, EmployeeFixGross>().ReverseMap();
            CreateMap<EmployeeFixGrossModel, FixGrossCalculationModel>().ReverseMap();
        }
        private void CreateEmployeeOverTimeMapping()
        {
            CreateMap<Employee, OvertimeEmployeeDetailModel>().ReverseMap();
            CreateMap<EmployeeOverTime, EmployeeOvertimeModel>()
                .ForMember(d => d.Status, m => m.MapFrom(s => s.EmployeeOverTimeStatus))
                .ForMember(d => d.EmployeeId, m => m.MapFrom(s => s.EmployeeContract.Employee.Id))
                    .AfterMap((s, d) =>
                    {
                        d.OverTimeMinutes = s.OverTimeMinutes;
                        d.Employee = new OvertimeEmployeeDetailModel
                        {
                            Id = s.EmployeeContract?.Employee?.Id ?? 0,
                            Name = $"{s.EmployeeContract?.Employee?.FirstName} {s.EmployeeContract?.Employee?.LastName}",
                            Designation = s.EmployeeContract?.Employee?.DesignationType?.Name,
                            Code = s.EmployeeContract?.Employee?.EmployeeCode
                        };
                        d.Branch = new BranchModel
                        {
                            Id = s.EmployeeContract?.Employee?.Branch?.Id ?? 0,
                            Code = s.EmployeeContract?.Employee?.Branch?.Code,
                            Name = s.EmployeeContract?.Employee?.Branch?.Name
                        };
                        d.ProjectManagers = s.EmployeeOverTimeManagers.Select(x => new OvertimeEmployeeDetailModel
                        {
                            Id = x.Employee?.Id ?? 0,
                            Name = (x.Employee == null ? "" : $"{(string.IsNullOrEmpty(x.Employee?.FirstName) ? "" : x.Employee?.FirstName + " ")}{(string.IsNullOrEmpty(x.Employee?.LastName) ? "" : x.Employee?.LastName)}"),
                        }).ToList();
                        d.ApprovedOvertime = new EmployeeOvertimeApprovedModel
                        {
                            ApprovedBy = s.ApprovedBy,
                            ApprovedByName = (s.Employee == null ? null : $"{(string.IsNullOrEmpty(s.Employee?.FirstName) ? "" : s.Employee?.FirstName + " ")}{(string.IsNullOrEmpty(s.Employee?.LastName) ? "" : s.Employee?.LastName)}"),
                            ApprovedDate = s.ApprovedDate.Value,
                            ApprovedMinutes = s.ApprovedMinutes,
                            OverTimeAmount = s.OverTimeAmount,
                            Remarks = s.Remarks
                        };
                    });
        }
        private void CreateEmployeeLeaveMapping()
        {
            CreateMap<Employee, LeaveEmployeeDetailModel>()
                 .AfterMap((s, d) =>
                 {
                     d.Id = s?.Id ?? 0;
                     d.Code = s?.EmployeeCode;
                     d.Name = $"{s?.FirstName} {s?.LastName}";
                     d.Designation = s?.DesignationType?.Name;
                 });
            CreateMap<EmployeeLeaveApplication, EmployeeLeaveApplicationModel>()
                .ForMember(d => d.Status, m => m.MapFrom(s => s.EmployeeLeaveStatus))
                .ForMember(d => d.Type, m => m.MapFrom(s => s.LeaveType))
                    .AfterMap((s, d) =>
                    {
                        d.Employee = new LeaveEmployeeDetailModel
                        {
                            Id = s.EmployeeContract?.Employee?.Id ?? 0,
                            Code = s.EmployeeContract?.Employee?.EmployeeCode,
                            Name = $"{s.EmployeeContract?.Employee?.FirstName} {s.EmployeeContract?.Employee?.LastName}",
                            Designation = s?.EmployeeContract?.Employee?.DesignationType.Name
                        };
                        d.ApprovedInformation = new EmployeeLeaveApplicationApproveModel
                        {
                            ApprovedBy = new LeaveEmployeeDetailModel
                            {
                                Id = s.Employee?.Id ?? 0,
                                Code = s.Employee?.EmployeeCode,
                                Name = $"{s.Employee?.FirstName} {s.Employee?.LastName}",
                            },
                            ApprovedDate = s.ApprovedDate
                        };
                        d.LeaveFromDate = s.LeaveFromDate;
                        d.LeaveToDate = s.LeaveToDate;
                    });
            CreateMap<EmployeeLeave, EmployeeLeaveModel>()
                .ForMember(d => d.LeaveType, m => m.MapFrom(s => s.LeaveType))
                    .AfterMap((s, d) =>
                    {
                        var e = s.EmployeeContract?.Employee;
                        d.Employee = e == null ? null : new LeaveEmployeeDetailModel
                        {
                            Id = e.Id,
                            Code = e.EmployeeCode,
                            Name = $"{e.FirstName} {e?.LastName}",
                            Designation = e.DesignationType.Name,
                            Branch = e.Branch.Name,
                            BranchCode = e.Branch.Code
                        };
                    });
            CreateMap<EmployeeLeaveApplicationComment, EmployeeLeaveApplicationCommentModel>()
                .AfterMap((s, d) =>
                {
                    d.CommentBy = new LeaveEmployeeDetailModel
                    {
                        Id = s.Employee.Id,
                        Name = $"{s.Employee.FirstName} {s.Employee.LastName}",
                        Code = s.Employee.EmployeeCode,
                        Designation = s.Employee.DesignationType.Name
                    };
                });
            CreateMap<EmployeeLeave, EmployeeLeaveBalanceModel>();
            CreateMap<EmployeeLeaveTransaction, EmployeeLeaveTransactionModel>();
        }
        private void CreateEmployeeSalaryMapping()
        {
            CreateMap<EmployeeEarningGross, EmployeeSalaryModel>()
                .ForMember(d => d.Status, m => m.MapFrom(s => s.EmployeeEarningGrossStatus))
                .ForMember(d => d.EarningGross, m => m.MapFrom(s => s))
                .ForMember(d => d.OtherAllowance, m => m.MapFrom(s => s))
                .ForMember(d => d.CreatedBy, m => m.MapFrom(s => s.Employee))
                .AfterMap((s, d) =>
                {
                    d.CreatedBy.Designation = s.CreatedByUser.Employee.DesignationType.Name;
                    d.CreatedBy.Branch = s.CreatedByUser.Employee.Branch.Name;
                    d.CreatedBy.Name = $"{s.Employee?.FirstName} {s.Employee?.LastName}";
                });
        }
        private void CreateResourceMapping()
        {
            CreateMap<Resource, ResourceRequestModel>();
            CreateMap<Resource, ResourceModel>()
                 .AfterMap((s, d) =>
                 {
                     //d.ResourceType = GeneralExtensions.ToEnum<ResourceTypes>(s.ResourceType.Name);
                     d.Branch = new BranchModel
                     {
                         Id = s.Branch.Id,
                         Name = s.Branch.Name,
                         Code = s.Branch.Code,
                     };
                 });
            CreateMap<ResourceUserHistory, ResourceUserHistoryModel>()
                .AfterMap((s, d) =>
                {
                    d.LogBy = $"{s.Employee?.FirstName} {s.Employee?.LastName}";
                });
        }
    }
}
