using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeStatusEntity : IEntityTypeConfiguration<EmployeeStatus>
    {
        public void Configure(EntityTypeBuilder<EmployeeStatus> builder)
        {
            builder.ToTable("EmployeeStatus");
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Status).HasColumnName("Status");
            builder.HasData(new EmployeeStatus { Id = (int)StatusType.Active, Status = StatusType.Active.GetEnumDescriptionAttribute() },
                           new EmployeeStatus { Id = (int)StatusType.InActive, Status = StatusType.InActive.GetEnumDescriptionAttribute() });
        }
    }
}
