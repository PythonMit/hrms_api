using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeSalaryStatusEntity : IEntityTypeConfiguration<EmployeeSalaryStatus>
    {
        public void Configure(EntityTypeBuilder<EmployeeSalaryStatus> builder)
        {
           builder.ToTable("EmployeeSalaryStatus");
           builder.HasKey(x => x.Id);
           builder.Property(x => x.StatusType).HasColumnName("StatusType");
           builder.Property(x => x.Description).HasColumnName("Description");
           builder.HasData(
                new EmployeeSalaryStatus { Id = (int)EmployeeSalaryStatusType.InProcess, StatusType = EmployeeSalaryStatusType.InProcess.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
                new EmployeeSalaryStatus { Id = (int)EmployeeSalaryStatusType.Hold, StatusType = EmployeeSalaryStatusType.Hold.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
                new EmployeeSalaryStatus { Id = (int)EmployeeSalaryStatusType.Paid, StatusType = EmployeeSalaryStatusType.Paid.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
                new EmployeeSalaryStatus { Id = (int)EmployeeSalaryStatusType.PartialPaid, StatusType = EmployeeSalaryStatusType.PartialPaid.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active });
        }
    }
}
