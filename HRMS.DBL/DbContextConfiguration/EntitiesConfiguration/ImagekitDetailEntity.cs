using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class ImagekitDetailEntity : IEntityTypeConfiguration<ImagekitDetail>
    {
        public void Configure(EntityTypeBuilder<ImagekitDetail> builder)
        {
            builder.ToTable("ImagekitDetails");
            builder.HasKey(x => x.Id);
        }
    }
}
