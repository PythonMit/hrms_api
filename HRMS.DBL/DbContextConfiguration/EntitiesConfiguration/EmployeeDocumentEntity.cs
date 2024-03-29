using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeDocumentEntity : IEntityTypeConfiguration<EmployeeDocument>
    {
        public void Configure(EntityTypeBuilder<EmployeeDocument> builder)
        {
            builder.ToTable("EmployeeDocuments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DocumentFront).HasColumnName("DocumentFront");
            builder.Property(x => x.DocumentBack).HasColumnName("DocumentBack");
            builder.HasOne(x => x.Employee)
                .WithMany(x => x.EmployeeDocuments)
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}
