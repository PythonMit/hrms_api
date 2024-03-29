using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class LeaveTypeEntity : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
           builder.ToTable("LeaveTypes");
           builder.HasKey(x => x.Id);
           builder.Property(x => x.Name).HasColumnName("Name");
           builder.Property(x => x.Description).HasColumnName("Description");
           builder.Property(x => x.TotalLeaves);
           builder.HasData(
               new LeaveType { Id = 1, Name = LeaveTypes.CarryForward.GetEnumDescriptionAttribute(), TotalLeaves = (int)LeaveTally.CF, RecordStatus = RecordStatus.Active },
               new LeaveType { Id = 2, Name = LeaveTypes.FlatLeave.GetEnumDescriptionAttribute(), TotalLeaves = (int)LeaveTally.FL, RecordStatus = RecordStatus.Active },
               new LeaveType { Id = 3, Name = LeaveTypes.LeaveWithoutPay.GetEnumDescriptionAttribute(), TotalLeaves = (int)LeaveTally.LWP, RecordStatus = RecordStatus.Active });
        }
    }
}
