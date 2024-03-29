using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeOverTimeManagerEntity : IEntityTypeConfiguration<EmployeeOverTimeManager>
    {
        public void Configure(EntityTypeBuilder<EmployeeOverTimeManager> builder)
        {
            builder.ToTable("EmployeeOverTimeManagers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EmployeeId).HasColumnName("EmployeeId");
            builder.Property(x => x.EmployeeOvertimeId).HasColumnName("EmployeeOvertimeId");
            builder.HasOne(x => x.Employee)
                .WithMany(x => x.EmployeeOverTimeManagers)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.EmployeeOverTime)
                .WithMany(x => x.EmployeeOverTimeManagers)
                .HasForeignKey(x => x.EmployeeOvertimeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
