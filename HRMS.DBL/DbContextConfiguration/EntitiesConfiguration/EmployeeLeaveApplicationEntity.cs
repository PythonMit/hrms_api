using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeLeaveApplicationEntity : IEntityTypeConfiguration<EmployeeLeaveApplication>
    {
        public void Configure(EntityTypeBuilder<EmployeeLeaveApplication> builder)
        {
            builder.ToTable("EmployeeLeaveApplications");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ApplyDate).HasColumnName("ApplyDate");
            builder.Property(x => x.LeaveFromDate).HasColumnName("LeaveFromDate");
            builder.Property(x => x.LeaveToDate).HasColumnName("LeaveToDate");
            builder.Property(x => x.NoOfDays).HasColumnName("NoOfDays");
            builder.Property(x => x.PurposeOfLeave).HasColumnName("PurposeOfLeave");
            builder.Property(x => x.ApprovedBy).HasColumnName("ApprovedBy");
            builder.Property(x => x.ApprovedDate).HasColumnName("ApprovedDate");
            builder.HasOne(x => x.EmployeeContract)
                 .WithMany(e => e.EmployeeLeaveApplications)
                 .HasForeignKey(x => x.EmployeeContractId);
        }
    }
}
