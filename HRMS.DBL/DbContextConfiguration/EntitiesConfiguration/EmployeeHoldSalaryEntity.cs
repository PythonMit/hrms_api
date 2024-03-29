using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    internal class EmployeeHoldSalaryEntity : IEntityTypeConfiguration<EmployeeHoldSalary>
    {
        public void Configure(EntityTypeBuilder<EmployeeHoldSalary> builder)
        {
            builder.ToTable("EmployeeHoldSalary");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EmployeeEarningGrossId).HasColumnName("EmployeeEarningGrossId");
            builder.Property(x => x.EmployeeSalaryStatusId).HasColumnName("EmployeeSalaryStatusId");
            builder.Property(x => x.Remarks).HasColumnName("Remarks");            
            builder.Property(x => x.HoldAmount).HasColumnName("HoldAmount");
            builder.Property(x => x.PaidDate).HasColumnName("PaidDate");
        }
    }
}
