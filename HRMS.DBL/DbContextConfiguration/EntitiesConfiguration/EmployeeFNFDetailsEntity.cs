using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeFNFDetailsEntity : IEntityTypeConfiguration<EmployeeFNFDetails>
    {
        public void Configure(EntityTypeBuilder<EmployeeFNFDetails> builder)
        {
            builder.ToTable("EmployeeFNFDetails");

            builder.Property(e => e.Id).HasColumnName("Id");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.EmployeeId)
                .HasColumnName("EmployeeId")
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasColumnName("Remarks")
                .HasMaxLength(int.MaxValue)
                .IsUnicode(true);

            builder.Property(e => e.ExitNote)
                .HasColumnName("ExitNote")
                .HasMaxLength(int.MaxValue)
                .IsUnicode(true);

            builder.Property(e => e.FNFDueDate)
                .HasColumnName("FNFDueDate")
                .IsUnicode(false);

            builder.Property(e => e.HasCertificateIssued)
                .HasColumnName("HasCertificateIssued")
                .IsUnicode(false);

            builder.Property(e => e.HasSalaryProceed)
                .HasColumnName("HasSalaryProceed")
                .IsUnicode(false);

            builder.Property(e => e.SettlementBy)
                .HasColumnName("SettlementBy")
                .IsUnicode(false);

            builder.Property(e => e.SettlementDate)
                .HasColumnName("SettlementDate")
                .IsUnicode(false);
        }
    }
}
