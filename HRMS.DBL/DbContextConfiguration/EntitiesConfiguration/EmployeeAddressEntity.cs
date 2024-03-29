using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeAddressEntity : IEntityTypeConfiguration<EmployeeAddress>
    {
        public void Configure(EntityTypeBuilder<EmployeeAddress> builder)
        {
            builder.ToTable("EmployeeAddresses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AddressLine1).HasColumnName("AddressLine1");
            builder.Property(x => x.AddressLine2).HasColumnName("AddressLine2");
            builder.Property(x => x.City).HasColumnName("City");
            builder.Property(x => x.Country).HasColumnName("Country");
            builder.Property(x => x.State).HasColumnName("State");
            builder.Property(x => x.Pincode).HasColumnName("Pincode");
        }
    }
}
