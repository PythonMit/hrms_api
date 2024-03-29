using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeLeaveStatusEntity : IEntityTypeConfiguration<EmployeeLeaveStatus>
    {
        public void Configure(EntityTypeBuilder<EmployeeLeaveStatus> builder)
        {
            builder.ToTable("EmployeeLeaveStatus");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StatusType).HasColumnName("StatusType");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.HasData(
               new EmployeeLeaveStatus { Id = (int)EmployeeLeaveStatusType.Pending, StatusType = EmployeeLeaveStatusType.Pending.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
               new EmployeeLeaveStatus { Id = (int)EmployeeLeaveStatusType.Approved, StatusType = EmployeeLeaveStatusType.Approved.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
               new EmployeeLeaveStatus { Id = (int)EmployeeLeaveStatusType.Declined, StatusType = EmployeeLeaveStatusType.Declined.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
               new EmployeeLeaveStatus { Id = (int)EmployeeLeaveStatusType.LWP, StatusType = EmployeeLeaveStatusType.LWP.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active });
        }
    }
}
