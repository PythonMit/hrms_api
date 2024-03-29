using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class DocumentTypeEntity : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.ToTable("DocumentTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.HasData(
                new DocumentType { Id = (int)DocumentTypes.PAN, Name = DocumentTypes.PAN.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
               new DocumentType { Id = (int)DocumentTypes.AadhaarCard, Name = DocumentTypes.AadhaarCard.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
               new DocumentType { Id = (int)DocumentTypes.ElectionCard, Name = DocumentTypes.ElectionCard.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active });
        }
    }
}
