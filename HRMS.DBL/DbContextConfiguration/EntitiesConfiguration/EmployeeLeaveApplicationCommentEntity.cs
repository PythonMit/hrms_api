using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeLeaveApplicationCommentEntity : IEntityTypeConfiguration<EmployeeLeaveApplicationComment>
    {
        public void Configure(EntityTypeBuilder<EmployeeLeaveApplicationComment> builder)
        {
            builder.ToTable("EmployeeLeaveApplicationComments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Comments).HasColumnName("Comments");
            builder.Property(x => x.CommentBy).HasColumnName("CommentBy");
            builder.Property(x => x.CommentDate).HasColumnName("CommentDate");
            builder.HasOne(x => x.EmployeeLeaveApplication)
                .WithMany(x => x.EmployeeLeaveApplicationComments)
                .HasForeignKey(x => x.EmployeeLeaveApplicationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
