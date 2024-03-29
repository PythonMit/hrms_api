using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeOverTimeStatusEntity : IEntityTypeConfiguration<EmployeeOverTimeStatus>
    {
        public void Configure(EntityTypeBuilder<EmployeeOverTimeStatus> builder)
        {
            builder.ToTable("EmployeeOverTimeStatus");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StatusType).HasColumnName("StatusType");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.HasData(
               new EmployeeOverTimeStatus { Id = (int)EmployeeOverTimeStatusType.Pending, StatusType = EmployeeOverTimeStatusType.Pending.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
               new EmployeeOverTimeStatus { Id = (int)EmployeeOverTimeStatusType.Approved, StatusType = EmployeeOverTimeStatusType.Approved.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
               new EmployeeOverTimeStatus { Id = (int)EmployeeOverTimeStatusType.Declined, StatusType = EmployeeOverTimeStatusType.Declined.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active });
        }
    }
}
