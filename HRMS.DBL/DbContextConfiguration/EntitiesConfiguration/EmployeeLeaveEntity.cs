using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeLeaveEntity : IEntityTypeConfiguration<EmployeeLeave>
    {
        public void Configure(EntityTypeBuilder<EmployeeLeave> builder)
        {
            builder.ToTable("EmployeeLeaves");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LeaveStartDate).HasColumnName("LeaveStartDate");
            builder.Property(x => x.LeaveEndDate).HasColumnName("LeaveEndDate");
            builder.Property(x => x.TotalLeaves).HasColumnName("TotalLeaves");
            builder.Property(x => x.TotalLeavesTaken).HasColumnName("TotalLeavesTaken");
            builder.HasOne(x => x.EmployeeContract)
                .WithMany(e => e.EmployeeLeaves)
                .HasForeignKey(x => x.EmployeeContractId);
        }
    }
}
