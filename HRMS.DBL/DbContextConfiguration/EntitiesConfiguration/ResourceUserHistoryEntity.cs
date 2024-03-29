using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration;
public class ResourceUserHistoryEntity : IEntityTypeConfiguration<ResourceUserHistory>
{
    public void Configure(EntityTypeBuilder<ResourceUserHistory> builder)
    {
        builder.ToTable("ResourceUserHistory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Description).HasColumnName("Description");
        builder.Property(x => x.LogDateTime).HasColumnName("LogDateTime");
    }
}
