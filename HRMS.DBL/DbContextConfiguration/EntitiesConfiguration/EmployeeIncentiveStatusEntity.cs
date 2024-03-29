using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeIncentiveStatusEntity : IEntityTypeConfiguration<EmployeeIncentiveStatus>
    {
        public void Configure(EntityTypeBuilder<EmployeeIncentiveStatus> builder)
        {
            builder.ToTable("EmployeeIncentiveStatus");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StatusType).HasColumnName("StatusType");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.HasData(
                new EmployeeIncentiveStatus { Id = (int)EmployeeIncentiveStatusType.Pending, StatusType = EmployeeIncentiveStatusType.Pending.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
                new EmployeeIncentiveStatus { Id = (int)EmployeeIncentiveStatusType.Hold, StatusType = EmployeeIncentiveStatusType.Hold.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
                new EmployeeIncentiveStatus { Id = (int)EmployeeIncentiveStatusType.Paid, StatusType = EmployeeIncentiveStatusType.Paid.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active });
        }
    }
}
