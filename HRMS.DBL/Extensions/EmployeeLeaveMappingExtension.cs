using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.General;
using HRMS.Core.Models.Leave;
using HRMS.DBL.Entities;
using System.Diagnostics.Contracts;
using System;
using System.Linq;

namespace HRMS.DBL.Extensions
{
    public static class EmployeeLeaveMappingExtension
    {
        public static IQueryable<EmployeeLeaveApplicationModel> ToEmployeeLeaveApplications(this IQueryable<EmployeeLeaveApplication> data, bool? hasRequested = null)
        {
            return data.Select(x => new EmployeeLeaveApplicationModel
            {
                Id = x.Id,
                LeaveFromDate = x.LeaveFromDate.Value.ToUniversalTime(),
                LeaveToDate = x.LeaveToDate.Value.ToUniversalTime(),
                ApplyDate = x.ApplyDate.Value.ToUniversalTime(),
                Employee = new EmployeeOutlineModel
                {
                    Id = x.EmployeeContract.Employee.Id,
                    Code = x.EmployeeContract.Employee.EmployeeCode,
                    Name = $"{x.EmployeeContract.Employee.FirstName} {x.EmployeeContract.Employee.LastName}",
                    Designation = x.EmployeeContract.Employee.DesignationType.Name,
                    Branch = x.EmployeeContract.Employee.Branch.Name,
                    ProfileUrl = x.EmployeeContract.Employee.ImagekitDetail == null ? "" : x.EmployeeContract.Employee.ImagekitDetail.Url
                },
                EmployeeContractId = x.EmployeeContractId,
                NoOfDays = x.NoOfDays,
                ApprovedInformation = new EmployeeLeaveApplicationApproveModel
                {
                    ApprovedBy = x.Employee == null ? null : new LeaveEmployeeDetailModel
                    {
                        Id = x.Employee.Id,
                        Code = x.Employee.EmployeeCode,
                        Name = $"{x.Employee.FirstName} {x.Employee.LastName}",
                        ApprovedRemark = x.ApprovedRemark
                    },
                    ApprovedDate = x.ApprovedDate.Value.ToUniversalTime(),
                    ApprovedFromDate = x.ApprovedFromDate.Value.ToLocalTime().ToUniversalTime(),
                    ApprovedToDate = x.ApprovedToDate.Value.ToLocalTime().ToUniversalTime(),
                    ApprovedDays = x.ApprovedDays,
                    LWPDays = (x.EmployeeLeaveStatusId == (int)EmployeeLeaveStatusType.LWP ? x.NoOfDays : x.LWPDays),
                    DeclineDays = (x.EmployeeLeaveStatusId == (int)EmployeeLeaveStatusType.Declined ? x.NoOfDays : x.DeclineDays),
                },
                PurposeOfLeave = x.PurposeOfLeave,
                Status = x.EmployeeLeaveStatus == null ? null : new EmployeeLeaveStatusModel()
                {
                    Id = x.EmployeeLeaveStatus.Id,
                    Description = x.EmployeeLeaveStatus.Description,
                    StatusType = x.EmployeeLeaveStatus.StatusType,
                },
                Type = x.LeaveType == null ? null : new LeaveTypeModel()
                {
                    Id = x.LeaveType.Id,
                    Name = x.LeaveType.Name,
                    Description = x.LeaveType.Description,
                    TotalLeaves = x.LeaveType.TotalLeaves,
                },
                ProjectManagers = x.EmployeeLeaveApplicationManagers == null ? null : x.EmployeeLeaveApplicationManagers.Select(m => new LeaveEmployeeDetailModel
                {
                    Id = m.Employee.Id,
                    Code = m.Employee.EmployeeCode,
                    Name = $"{m.Employee.FirstName} {m.Employee.LastName}",
                }),
                Category = x.LeaveCategory == null ? null : new LeaveCategoryModel
                {
                    Id = x.LeaveCategoryId ?? 0,
                    Category = x.LeaveCategory.Category
                },
                HasRequested = hasRequested,
                HasProbationPeriod = HasProbationPeriod(x.EmployeeContract),
                HasTrainingPeriod = HasTrainingPeriod(x.EmployeeContract),
                HasNoticePeriod = x.EmployeeContract.EmployeeContractStatusId == (int)EmployeeContractStatusType.NoticePeriod,
                RecordStatus = x.RecordStatus
            });
        }
        public static IQueryable<EmployeeLeaveApplicationCommentModel> ToEmployeeLeaveApplicationComments(this IQueryable<EmployeeLeaveApplicationComment> data)
        {
            return data.Select(x => new EmployeeLeaveApplicationCommentModel
            {
                Id = x.Id,
                Comments = x.Comments,
                CommentDate = x.CommentDate.ToUniversalTime(),
                EmployeeLeaveApplicationId = x.EmployeeLeaveApplicationId,
                CommentBy = new EmployeeOutlineModel
                {
                    Id = x.Employee.Id,
                    Name = $"{x.Employee.FirstName} {x.Employee.LastName}",
                    Code = x.Employee.EmployeeCode,
                    Designation = x.Employee.DesignationType.Name
                }
            });
        }
        public static IQueryable<EmployeeLeaveModel> ToEmployeeLeaves(this IQueryable<EmployeeLeave> data)
        {
            return data.Select(x => new EmployeeLeaveModel
            {
                Id = x.Id,
                EmployeeContractId = x.EmployeeContractId,
                LeaveEndDate = x.LeaveEndDate.ToUniversalTime(),
                LeaveStartDate = x.LeaveStartDate.ToUniversalTime(),
                TotalLeaves = x.TotalLeaves,
                TotalLeavesTaken = x.TotalLeavesTaken,
                LeaveType = new LeaveTypeModel
                {
                    Id = x.LeaveType.Id,
                    Name = x.LeaveType.Name
                },
                Employee = new EmployeeOutlineModel
                {
                    Id = x.EmployeeContract.Employee.Id,
                    Code = x.EmployeeContract.Employee.EmployeeCode,
                    Name = $"{x.EmployeeContract.Employee.FirstName} {x.EmployeeContract.Employee.LastName}",
                    Designation = x.EmployeeContract.Employee.DesignationType.Name
                }
            });
        }
        public static IQueryable<EmployeeLeaveBalanceModel> ToEmployeeLeaveBalances(this IQueryable<EmployeeLeave> data)
        {
            return data.Select(x => new EmployeeLeaveBalanceModel
            {
                ContractId = x.EmployeeContractId,
                TotalLeaves = x.TotalLeaves,
                TotalLeavesTaken = x.TotalLeavesTaken,
                LeaveBalance = (x.TotalLeaves - x.TotalLeavesTaken),
                LeaveType = GeneralExtensions.ToEnum<LeaveTypes>(x.LeaveType.Name.Replace(" ", string.Empty)),
            });
        }
        public static IQueryable<EmployeeLeaveBalanceModel> ToEmployeeLastMonthsLeaveData(this IQueryable<EmployeeLeaveApplication> data)
        {
            return data.Select(x => new EmployeeLeaveBalanceModel
            {
                Month = GeneralExtensions.GetMonthName(x.LeaveFromDate.Value.Month),
                Year = x.LeaveFromDate.Value.Year,
                LeaveType = (x.LeaveType == null ? null : GeneralExtensions.ToEnum<LeaveTypes>(x.LeaveType.Name.Replace(" ", string.Empty))),
                ApprovedDays = x.ApprovedDays,
                LWPDays = x.LWPDays,
                DeclineDays = (x.EmployeeLeaveStatusId == (int)EmployeeLeaveStatusType.Declined ? x.NoOfDays : x.DeclineDays),
                LeaveCategoryType = (x.LeaveCategory == null ? null : GeneralExtensions.ToEnum<LeaveCategoryType>(x.LeaveCategory.Category.Replace(" ", string.Empty))),
            });
        }
        public static IQueryable<EmployeeOutlineModel> ToLeaveEmployeeDetails(this IQueryable<EmployeeLeaveApplication> data)
        {
            return data.Select(x => new EmployeeOutlineModel
            {
                Id = x.EmployeeContract.Employee.Id,
                Code = x.EmployeeContract.Employee.EmployeeCode,
                Name = $"{x.EmployeeContract.Employee.FirstName} {x.EmployeeContract.Employee.LastName}",
                Designation = x.EmployeeContract.Employee.DesignationType.Name,
                Branch = x.EmployeeContract.Employee.Branch.Name,
                ProfileUrl = x.EmployeeContract.Employee.ImagekitDetail == null ? "" : x.EmployeeContract.Employee.ImagekitDetail.Url
            });
        }
        private static bool HasProbationPeriod(EmployeeContract contract)
        {
            var date = contract.ContractStartDate.AddMonths(contract.ProbationPeriod);
            return (contract.ProbationPeriod > 0 ? (date.Date >= DateTime.UtcNow.Date) : false);
        }
        private static bool HasTrainingPeriod(EmployeeContract contract)
        {
            var date = contract.ContractStartDate.AddMonths(contract.TrainingPeriod);
            return (contract.TrainingPeriod > 0 ? (date.Date >= DateTime.UtcNow.Date) : false);
        }
    }
}