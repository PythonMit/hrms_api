using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class NotificationEntity : IEntityTypeConfiguration<Notification>
    {
        void IEntityTypeConfiguration<Notification>.Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notification");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EmployeeId).HasColumnName("EmployeeId");
            builder.Property(x => x.Title).HasColumnName("Title");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.Property(x => x.Type).HasColumnName("Type");
            builder.Property(x => x.HasRead).HasColumnName("HasRead");
        }
    }
}
