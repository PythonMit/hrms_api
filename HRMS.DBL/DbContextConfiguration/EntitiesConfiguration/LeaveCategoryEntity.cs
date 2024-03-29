using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    internal class LeaveCategoryEntity : IEntityTypeConfiguration<LeaveCategory>
    {
        public void Configure(EntityTypeBuilder<LeaveCategory> builder)
        {
            builder.ToTable("LeaveCategories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Category).HasColumnName("Category");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.HasData(
                new LeaveCategory { Id = 1, Category = LeaveCategoryType.CasualLeave.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
                new LeaveCategory { Id = 2, Category = LeaveCategoryType.PrivilegeLeave.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
                new LeaveCategory { Id = 3, Category = LeaveCategoryType.SickLeave.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active });
        }
    }
}
