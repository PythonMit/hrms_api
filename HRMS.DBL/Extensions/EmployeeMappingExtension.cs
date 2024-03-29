using HRMS.Core.Consts;
using HRMS.Core.Models.Branch;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.Employee.ExitProccess;
using HRMS.Core.Models.General;
using HRMS.DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HRMS.DBL.Extensions
{
    public static class EmployeeMappingExtension
    {
        public static IQueryable<EmployeeModel> ToEmployeeDetailsModel(this IQueryable<Entities.EmployeeDetail> employeesDetails, IEnumerable<int?> Status = null)
        {
            return employeesDetails?.Select(x => new EmployeeModel
            {
                MobileNumber = x.MobileNumber ?? null,
                JoiningDate = (x.JoinDate ?? (DateTime?)null),
                UserId = x.Employee.UserId ?? 0,
                EmployeeCode = x.Employee.EmployeeCode ?? null,
                Id = x.Employee.Id,
                FirstName = x.Employee.FirstName ?? null,
                LastName = x.Employee.LastName ?? null,
                MiddleName = x.Employee.MiddleName ?? null,
                Branch = x.Employee.Branch == null ? null : new BranchModel
                {
                    Id = x.Employee.Branch.Id,
                    Name = x.Employee.Branch.Name,
                    Code = x.Employee.Branch.Code,
                },
                Designation = x.Employee.DesignationType.Name ?? null,
                Gender = x.Employee.Gender ?? null,
                ContractStatus = GetContractStatus(x.Employee.EmployeeContracts), // (x.Employee.EmployeeContracts == null ? "" : x.Employee.EmployeeContracts.Where(y => (Status == null ? true : Status.Contains((int)y.RecordStatus))).Select(y => y.EmployeeContractStatus.StatusType).FirstOrDefault()),
                RecordStatus = x.Employee.RecordStatus
            });
        }
        public static IQueryable<EmployeeOutlineModel> ToEmployeeOutLineDetails(this IQueryable<Employee> data)
        {
            return data.Select(x => new EmployeeOutlineModel
            {
                Id = x.Id,
                Code = x.EmployeeCode,
                Name = $"{(string.IsNullOrEmpty(x.FirstName) ? "" : x.FirstName + " ")}{(string.IsNullOrEmpty(x.MiddleName) ? "" : x.MiddleName + " ")}{(string.IsNullOrEmpty(x.LastName) ? "" : x.LastName)}",
                Designation = x.DesignationType.Name,
                Branch = x.Branch.Name,
                ProfileUrl = x.ImagekitDetail == null ? "" : x.ImagekitDetail.Url
            });
        }
        public static IQueryable<BasicInformationModel> ToEmployeeBasicInformation(this IQueryable<EmployeeDetail> data)
        {
            return data.Select(x => new BasicInformationModel
            {
                Id = x.Employee.Id,
                UserId = x.Employee.UserId,
                FirstName = x.Employee.FirstName,
                MiddleName = x.Employee.MiddleName,
                LastName = x.Employee.LastName,
                DateOfBirth = x.Employee.DateOfBirth,
                Gender = x.Employee.Gender,
                InstituteName = x.InstituteName,
                ProfilePhotoId = x.Employee.ImagekitDetailId,
                ProfilePhotoFileId = x.Employee.ImagekitDetail == null ? "" : x.Employee.ImagekitDetail.FileId,
                ProfilePhotoUrl = x.Employee.ImagekitDetail == null ? "" : x.Employee.ImagekitDetail.Url,
                AllowEditPersonalDetails = x.AllowEditPersonalDetails
            });
        }
        public static EmployeeOutlineModel ToEmployeeOutLineDetails(this EmployeeDetail data)
        {
            return new EmployeeOutlineModel
            {
                Id = data.Id,
                Code = data.Employee.EmployeeCode,
                Name = $"{data.Employee.FirstName} {data.Employee.LastName}",
                Designation = data.Employee.DesignationType.Name,
                Branch = data.Employee.Branch.Name,
                ProfileUrl = data.Employee.ImagekitDetail == null ? "" : data.Employee.ImagekitDetail.Url,
                Gender = data.Employee.Gender,
                JoinDate = data.JoinDate
            };
        }
        public static IQueryable<EmployeeFNFDetailsModel> ToEmployeeExitProcessList(this IQueryable<EmployeeFNFDetails> data)
        {
            return data.Select(x => new EmployeeFNFDetailsModel
            {
                Id = x.Id,
                EmployeeId = x.Employee.Id,
                Employee = new EmployeeOutlineModel
                {
                    Branch = x.Employee.Branch.Name,
                    BranchCode = x.Employee.Branch.Code,
                    Code = x.Employee.EmployeeCode,
                    Name = $"{x.Employee.FirstName} {x.Employee.LastName}",
                    Designation = x.Employee.DesignationType.Name,
                },
                FNFDueDate = x.FNFDueDate,
                ExitNote = x.ExitNote,
                HasCertificateIssued = x.HasCertificateIssued,
                HasSalaryProceed = x.HasSalaryProceed,
                Remarks = x.Remarks,
                SettlementBy = x.SettlementBy,
                SettlementDate = x.SettlementDate
            });
        }
        public static string GetContractStatus(ICollection<EmployeeContract> contracts)
        {
            return contracts.OrderByDescending(x => x.ContractEndDate).Select(x => x.EmployeeContractStatus.StatusType).FirstOrDefault();
        }
    }
}
