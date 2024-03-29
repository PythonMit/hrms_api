using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeEarningGrossStatusEntity : IEntityTypeConfiguration<EmployeeEarningGrossStatus>
    {
        public void Configure(EntityTypeBuilder<EmployeeEarningGrossStatus> builder)
        {
            builder.ToTable("EmployeeEarningGrossStatus");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StatusType).HasColumnName("StatusType");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.HasData(
                new EmployeeEarningGrossStatus { Id = (int)EmployeeEarningGrossStatusType.InProcess, StatusType = EmployeeEarningGrossStatusType.InProcess.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
                new EmployeeEarningGrossStatus { Id = (int)EmployeeEarningGrossStatusType.Hold, StatusType = EmployeeEarningGrossStatusType.Hold.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
                new EmployeeEarningGrossStatus { Id = (int)EmployeeEarningGrossStatusType.Paid, StatusType = EmployeeEarningGrossStatusType.Paid.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
                new EmployeeEarningGrossStatus { Id = (int)EmployeeEarningGrossStatusType.PartiallyPaid, StatusType = EmployeeEarningGrossStatusType.PartiallyPaid.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active });
        }
    }
}
