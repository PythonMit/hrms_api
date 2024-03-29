using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeOverTimeEntity : IEntityTypeConfiguration<EmployeeOverTime>
    {
        public void Configure(EntityTypeBuilder<EmployeeOverTime> builder)
        {
            builder.ToTable("EmployeeOverTimes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProjectName).HasColumnName("ProjectName");
            builder.Property(x => x.TaskDescription).HasColumnName("TaskDescription");
            builder.Property(x => x.OverTimeDate).HasColumnName("OverTimeDate");
            builder.Property(x => x.OverTimeMinutes).HasColumnName("OverTimeMinutes");
            builder.Property(x => x.OverTimeAmount).HasColumnName("OverTimeAmount");
            builder.Property(x => x.ApprovedBy).HasColumnName("ApprovedBy");
            builder.Property(x => x.ApprovedDate).HasColumnName("ApprovedDate");
            builder.Property(x => x.ApprovedMinutes).HasColumnName("ApprovedMinutes");
            builder.Property(x => x.Remarks).HasColumnName("Remarks");
        }
    }
}
