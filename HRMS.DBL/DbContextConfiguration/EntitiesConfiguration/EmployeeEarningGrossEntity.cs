using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeEarningGrossEntity : IEntityTypeConfiguration<EmployeeEarningGross>
    {
        public void Configure(EntityTypeBuilder<EmployeeEarningGross> builder)
        {
            builder.ToTable("EmployeeEarningGross");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SalaryMonth).HasColumnName("SalaryMonth");
            builder.Property(x => x.TotalDays).HasColumnName("TotalDays");
            builder.Property(x => x.LWP).HasColumnName("LWP");
            builder.Property(x => x.PaidDays).HasColumnName("PaidDays");
            builder.Property(x => x.Basic).HasColumnName("Basic");
            builder.Property(x => x.DA).HasColumnName("DA");
            builder.Property(x => x.HRA).HasColumnName("HRA");
            builder.Property(x => x.ConveyanceAllowance).HasColumnName("ConveyanceAllowance");
            builder.Property(x => x.OtherAllowance).HasColumnName("OtherAllowance");
            builder.Property(x => x.FixIncentive).HasColumnName("FixIncentive");
            builder.Property(x => x.PT).HasColumnName("PT");
            builder.Property(x => x.TDS).HasColumnName("TDS");
            builder.Property(x => x.EmployeePF).HasColumnName("EmployeePF");
            builder.Property(x => x.EmployerPF).HasColumnName("EmployerPF");
            builder.Property(x => x.OverTimeAmount).HasColumnName("OverTimeAmount");
            builder.Property(x => x.NetSalary).HasColumnName("NetSalary");
            builder.Property(x => x.Remarks).HasColumnName("Remarks");
            builder.Property(x => x.PaidDate).HasColumnName("PaidDate");
            builder.Property(x => x.CreatedBy).HasColumnName("CreatedBy");
        }
    }
}
