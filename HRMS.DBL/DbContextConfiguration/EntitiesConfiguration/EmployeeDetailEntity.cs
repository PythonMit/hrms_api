using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeDetailEntity : IEntityTypeConfiguration<EmployeeDetail>
    {
        public void Configure(EntityTypeBuilder<EmployeeDetail> builder)
        {
            builder.ToTable("EmployeeDetails");

            builder.Property(e => e.Id).HasColumnName("Id");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.WorkEmail)
                .HasColumnName("WorkEmail")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.PersonalEmail)
                .HasColumnName("PersonalEmail")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.MobileNumber)
                .HasColumnName("MobileNumber")
                .IsUnicode(false);

            builder.Property(e => e.AlternateMobileNumber)
                .HasColumnName("AlternateMobileNumber")
                .IsUnicode(false);

            builder.Property(e => e.PreviousEmployeer)
                .HasColumnName("PreviousEmployeer")
                .IsUnicode(false);

            builder.Property(u => u.Experience)
                 .HasColumnName("Experience")
                 .IsUnicode(false);

            builder.Property(u => u.JoinDate)
                 .HasColumnName("JoinDate")
                 .IsUnicode(false);

            builder.Property(e => e.EndDate)
                 .HasColumnName("EndDate")
                 .IsUnicode(false);

            builder.Property(u => u.HasExited)
                 .HasColumnName("HasExited")
                 .IsUnicode(false);

            builder.Property(u => u.HasFNFSettled)
                 .HasColumnName("HasFNFSettled")
                 .IsUnicode(false);

            builder.Property(u => u.AllowEditPersonalDetails)
                 .HasColumnName("AllowEditPersonalDetails")
                 .IsUnicode(false);

            builder.Property(u => u.WorkingFormat)
                 .HasColumnName("WorkingFormat")
                 .IsUnicode(false);
        }
    }
}
