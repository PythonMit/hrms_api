using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeLeaveApplicationManagerEntity : IEntityTypeConfiguration<EmployeeLeaveApplicationManager>
    {
        public void Configure(EntityTypeBuilder<EmployeeLeaveApplicationManager> builder)
        {
            builder.ToTable("EmployeeLeaveApplicationManagers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EmployeeId).HasColumnName("EmployeeId");
            builder.Property(x => x.EmployeeLeaveApplicationId).HasColumnName("EmployeeLeaveApplicationId");
            builder.HasOne(x => x.Employee)
                    .WithMany(x => x.EmployeeLeaveApplicationManagers)
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.EmployeeLeaveApplication)
                    .WithMany(x => x.EmployeeLeaveApplicationManagers)
                    .HasForeignKey(x => x.EmployeeLeaveApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
