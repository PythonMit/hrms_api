using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class SystemFlagEntity : IEntityTypeConfiguration<SystemFlag>
    {
        public void Configure(EntityTypeBuilder<SystemFlag> builder)
        {
            builder.ToTable("SystemFlags");
            builder.HasKey(x => x.Id);
        }
    }
}
