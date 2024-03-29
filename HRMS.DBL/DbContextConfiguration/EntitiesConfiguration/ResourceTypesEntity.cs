using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration;
public class ResourcesCategorieEntity : IEntityTypeConfiguration<ResourceType>
{
    public void Configure(EntityTypeBuilder<ResourceType> builder)
    {
        builder.ToTable("ResourceTypes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.Description).HasColumnName("Description");
        builder.HasData(
             new ResourceType { Id = (int)ResourceTypes.CPU, Name = ResourceTypes.CPU.GetEnumDescriptionAttribute() },
             new ResourceType { Id = (int)ResourceTypes.Monitor, Name = ResourceTypes.Monitor.GetEnumDescriptionAttribute() },
             new ResourceType { Id = (int)ResourceTypes.Keyboard, Name = ResourceTypes.Keyboard.GetEnumDescriptionAttribute() },
             new ResourceType { Id = (int)ResourceTypes.Mouse, Name = ResourceTypes.Mouse.GetEnumDescriptionAttribute() },
             new ResourceType { Id = (int)ResourceTypes.Mobile, Name = ResourceTypes.Mobile.GetEnumDescriptionAttribute() },
             new ResourceType { Id = (int)ResourceTypes.HeadPhone, Name = ResourceTypes.HeadPhone.GetEnumDescriptionAttribute() },
             new ResourceType { Id = (int)ResourceTypes.Printer, Name = ResourceTypes.Printer.GetEnumDescriptionAttribute() });
    }
}
  