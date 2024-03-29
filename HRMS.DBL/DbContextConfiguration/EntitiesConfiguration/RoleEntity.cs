using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class RoleEntity : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.HasData(new Role { Id = (int)RoleTypes.SuperAdmin, Name = RoleTypes.SuperAdmin.GetEnumDescriptionAttribute(), Priority = 1 },
             new Role { Id = (int)RoleTypes.Admin, Name = RoleTypes.Admin.GetEnumDescriptionAttribute(), Priority = 4 },
             new Role { Id = (int)RoleTypes.HRManager, Name = RoleTypes.HRManager.GetEnumDescriptionAttribute(), Priority = 7 },
             new Role { Id = (int)RoleTypes.Employee, Name = RoleTypes.Employee.GetEnumDescriptionAttribute(), Priority = 13 },
             new Role { Id = (int)RoleTypes.Manager, Name = RoleTypes.Manager.GetEnumDescriptionAttribute(), Priority = 10 },
             new Role { Id = (int)RoleTypes.Guest, Name = RoleTypes.Guest.GetEnumDescriptionAttribute(), Priority = 16 });
        }
    }
}
