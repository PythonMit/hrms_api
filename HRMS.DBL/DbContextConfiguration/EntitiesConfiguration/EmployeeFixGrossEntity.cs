using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeFixGrossEntity : IEntityTypeConfiguration<EmployeeFixGross>
    {
        public void Configure(EntityTypeBuilder<EmployeeFixGross> builder)
        {
            builder.ToTable("EmployeeFixGross");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CostToCompany).HasColumnName("CostToCompany");
            builder.Property(x => x.StipendAmount).HasColumnName("StipendAmount");
            builder.Property(x => x.Basic).HasColumnName("Basic");
            builder.Property(x => x.DA).HasColumnName("DA");
            builder.Property(x => x.HRA).HasColumnName("HRA");
            builder.Property(x => x.ConveyanceAllowance).HasColumnName("ConveyanceAllowance");
            builder.Property(x => x.IsFixIncentive).HasColumnName("IsFixIncentive");
            builder.Property(x => x.FixIncentiveDuration).HasColumnName("FixIncentiveDuration");
            builder.Property(x => x.FixIncentiveRemarks).HasColumnName("FixIncentiveRemarks");
            builder.Property(x => x.Remarks).HasColumnName("Remarks");
            builder.Property(x => x.IsDelete).HasColumnName("IsDelete");
            builder.HasOne(a => a.EmployeeContract)
                .WithOne(b => b.EmployeeFixGross)
                .HasForeignKey<EmployeeFixGross>(b => b.EmployeeContractId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
