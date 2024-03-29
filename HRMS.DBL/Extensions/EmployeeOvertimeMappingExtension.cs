using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Branch;
using HRMS.Core.Models.Overtime;
using HRMS.DBL.Entities;
using System.Linq;

namespace HRMS.DBL.Extensions
{
    public static class EmployeeOvertimeMappingExtension
    {
        public static IQueryable<EmployeeOvertimeModel> ToEmployeeOvertimes(this IQueryable<EmployeeOverTime> data, bool? hasRequested = null)
        {
            return data.Select(s => new EmployeeOvertimeModel
            {
                Id = s.Id,
                OverTimeMinutes = s.OverTimeMinutes.Value,
                OverTimeDate = s.OverTimeDate.Value.ToUniversalTime(),
                ProjectName = s.ProjectName,
                TaskDescription = s.TaskDescription,
                EmployeeId = s.EmployeeContract.Employee == null ? 0 : s.EmployeeContract.EmployeeId,
                Status = s.EmployeeOverTimeStatus == null ? null : new EmployeeOverTimeStatusModel
                {
                    Id = s.EmployeeOverTimeStatus.Id,
                    StatusType = GeneralExtensions.ToEnum<EmployeeOverTimeStatusType>(s.EmployeeOverTimeStatus.StatusType),
                },
                Employee = new OvertimeEmployeeDetailModel
                {
                    Id = s.EmployeeContract.EmployeeId,
                    Name = $"{s.EmployeeContract.Employee.FirstName} {s.EmployeeContract.Employee.LastName}",
                    Designation = s.EmployeeContract.Employee.DesignationType.Name,
                    Code = s.EmployeeContract.Employee.EmployeeCode,
                    ProfileUrl = s.EmployeeContract.Employee.ImagekitDetail == null ? "" : s.EmployeeContract.Employee.ImagekitDetail.Url,
                },
                Branch = s.EmployeeContract.Employee.Branch == null ? null : new BranchModel
                {
                    Id = s.EmployeeContract.Employee.Branch.Id,
                    Code = s.EmployeeContract.Employee.Branch.Code,
                    Name = s.EmployeeContract.Employee.Branch.Name
                },
                ProjectManagers = s.EmployeeOverTimeManagers == null ? null : s.EmployeeOverTimeManagers.Select(x => new OvertimeEmployeeDetailModel
                {
                    Id = x.Employee.Id,
                    Name = (x.Employee == null ? "" : $"{x.Employee.FirstName} {x.Employee.LastName}"),
                }).ToList(),
                ApprovedOvertime = new EmployeeOvertimeApprovedModel
                {
                    ApprovedBy = s.ApprovedBy,
                    ApprovedByName = (s.Employee == null ? "" : $"{s.Employee.FirstName} {s.Employee.LastName}"),
                    ApprovedDate = s.ApprovedDate.Value,
                    ApprovedMinutes = s.ApprovedMinutes.Value,
                    OverTimeAmount = s.OverTimeAmount,
                    Remarks = s.Remarks
                },
                hasRequested = hasRequested
            }) ;
        }
    }
}
