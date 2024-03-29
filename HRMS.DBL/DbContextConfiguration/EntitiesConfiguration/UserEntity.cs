using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class UserEntity : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(e => e.Id).HasColumnName("Id");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.IsVerified)
                .HasDefaultValue(true);

            builder.Property(e => e.Disabled)
                .HasColumnName("Disabled")
                .HasMaxLength(5)
                .IsUnicode(false);

            builder.Property(e => e.Emailaddress)
                .HasColumnName("EmailAddress")
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Password)
                .HasColumnName("Password")
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Username)
                .HasColumnName("Username")
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(u => u.Salt)
                .HasMaxLength(32);
        }
    }
}
