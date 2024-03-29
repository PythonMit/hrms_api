using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class ActivityLogEntity : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.ToTable("ActivityLog");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EventType).HasColumnName("EventType");
            builder.Property(x => x.ActivityJson).HasColumnName("ActivityJson");
            builder.Property(x => x.EventLocation).HasColumnName("EventLocation");
        }
    }
}
