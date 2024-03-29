using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Branch;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.Resource;
using HRMS.DBL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HRMS.DBL.Extensions
{
    public static class ResourceMappingExtension
    {
        public static IQueryable<ResourceModel> ToResourceDetailsModel(this IQueryable<Entities.Resource> data)
        {
            return data?.Select(x => new ResourceModel
            {
                Id = x.Id,
                IsFree = x.IsFree,
                PhysicalLocation = x.PhysicalLocation,
                PurchaseDate = x.PurchaseDate,
                RecordStatus = x.RecordStatus,
                Remarks = x.Remarks,
                ResourceType = GeneralExtensions.ToEnum<ResourceTypes>(x.ResourceType.Name),
                Specification = x.Specification,
                Status = x.Status,
                SystemName = x.SystemName,
                Branch = new BranchModel
                {
                    Id = x.BranchId,
                    Name = x.Branch.Name,
                    Code = x.Branch.Code,
                }
            });
        }
    }
}
