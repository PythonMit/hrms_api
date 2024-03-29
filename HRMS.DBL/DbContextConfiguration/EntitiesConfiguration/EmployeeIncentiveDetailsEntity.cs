using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeIncentiveDetailsEntity : IEntityTypeConfiguration<EmployeeIncentiveDetails>
    {
        public void Configure(EntityTypeBuilder<EmployeeIncentiveDetails> builder)
        {
            builder.ToTable("EmployeeIncentiveDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EmployeeContractId).HasColumnName("EmployeeContractId");
            builder.Property(x => x.IncentiveAmount).HasColumnName("IncentiveAmount");
            builder.Property(x => x.IncentiveDate).HasColumnName("IncentiveDate");
            builder.Property(x => x.EmployeeIncentiveStatusId).HasColumnName("EmployeeIncentiveStatusId");
            builder.Property(x => x.Remarks).HasColumnName("Remarks");
            builder.HasOne(x => x.EmployeeContract)
                .WithMany(e => e.EmployeeIncentiveDetails)
                .HasForeignKey(x => x.EmployeeContractId);
        }
    }
}
