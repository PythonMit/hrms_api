using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class ProjectManagerEntity : IEntityTypeConfiguration<ProjectManager>
    {
        public void Configure(EntityTypeBuilder<ProjectManager> builder)
        {
            builder.ToTable("ProjectManagers");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Project)
                   .WithMany(e => e.ProjectManagers)
                   .HasForeignKey(x => x.ProjectId);
            builder.HasOne(x => x.Employee)
                   .WithMany(e => e.ProjectManagers)
                   .HasForeignKey(x => x.EmployeeId);
        }
    }
}
