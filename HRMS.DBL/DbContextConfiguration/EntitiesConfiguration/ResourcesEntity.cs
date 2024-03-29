using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration;
public class ResourcesEntity : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable("Resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Specification).HasColumnName("Specification");
        builder.Property(x => x.SystemName).HasColumnName("SystemName");
        builder.Property(x => x.PurchaseDate).HasColumnName("PurchaseDate");
        builder.Property(x => x.PhysicalLocation).HasColumnName("PhysicalLocation");
        builder.Property(x => x.Status).HasColumnName("Status");
        builder.Property(x => x.IsFree).HasColumnName("IsFree");
        builder.Property(x => x.Remarks).HasColumnName("Remarks");         
    }
}
