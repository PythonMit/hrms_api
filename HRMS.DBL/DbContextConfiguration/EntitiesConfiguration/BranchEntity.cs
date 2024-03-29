using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class BranchEntity : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code);
            builder.Property(x => x.Name);
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.HasData(
                new Branch { Id = 1, Code = BranchCode.ST.GetEnumDescriptionAttribute(), Name = BranchName.Surat.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
                new Branch { Id = 2, Code = BranchCode.AHM.GetEnumDescriptionAttribute(), Name = BranchName.Ahmedabad.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active });
        }
    }
}
