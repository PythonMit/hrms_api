using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeEntity : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.EmployeeCode).HasColumnName("EmployeeCode");
            builder.Property(e => e.FirstName).HasColumnName("FirstName");
            builder.Property(e => e.MiddleName).HasColumnName("MiddleName");
            builder.Property(e => e.LastName).HasColumnName("LastName");
            builder.Property(e => e.Email).HasColumnName("Email");
            builder.Property(e => e.DateOfBirth).HasColumnName("DateOfBirth");
            builder.Property(e => e.Gender).HasColumnName("Gender");
            builder.Property(e => e.ImagekitDetailId).HasColumnName("ImagekitDetailId");
        }
    }
}
