using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeContractEntity : IEntityTypeConfiguration<EmployeeContract>
    {
        public void Configure(EntityTypeBuilder<EmployeeContract> builder)
        {
            builder.ToTable("EmployeeContracts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ContractStartDate).HasColumnName("ContractStartDate");
            builder.Property(x => x.ContractEndDate).HasColumnName("ContractEndDate");
            builder.Property(x => x.ProbationPeriod).HasColumnName("ProbationPeriod");
            builder.Property(x => x.TrainingPeriod).HasColumnName("TrainingPeriod");
            builder.Property(x => x.DropDate).HasColumnName("DropDate");
            builder.Property(x => x.NoticePeriodStartDate).HasColumnName("NoticePeriodStartDate");
            builder.Property(x => x.NoticePeriodEndDate).HasColumnName("NoticePeriodEndDate");
            builder.HasOne(x => x.Employee)
             .WithMany(x => x.EmployeeContracts)
             .HasForeignKey(x => x.EmployeeId);
        }
    }
}
