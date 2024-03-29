using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeBankDetailEntity : IEntityTypeConfiguration<EmployeeBankDetail>
    {
        public void Configure(EntityTypeBuilder<EmployeeBankDetail> builder)
        {
            builder.ToTable("EmployeeBankDetails");
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BeneficiaryACNumber);            
            builder.Property(x => x.BeneficiaryEmail);
            builder.Property(x => x.BeneficiaryName);
            builder.Property(x => x.IFSCCode);
            builder.Property(x => x.TransactionType);
            builder.Property(x => x.BankName);
        }
    }
}
