using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class EmployeeContractStatusEntity : IEntityTypeConfiguration<EmployeeContractStatus>
    {
        public void Configure(EntityTypeBuilder<EmployeeContractStatus> builder)
        {
            builder.ToTable("EmployeeContractStatus");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StatusType).HasColumnName("StatusType");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.HasData(
                new EmployeeContractStatus { Id = (int)EmployeeContractStatusType.Running, StatusType = EmployeeContractStatusType.Running.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
               new EmployeeContractStatus { Id = (int)EmployeeContractStatusType.Drop, StatusType = EmployeeContractStatusType.Drop.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
               new EmployeeContractStatus { Id = (int)EmployeeContractStatusType.NoticePeriod, StatusType = EmployeeContractStatusType.NoticePeriod.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
               new EmployeeContractStatus { Id = (int)EmployeeContractStatusType.Completed, StatusType = EmployeeContractStatusType.Completed.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active },
               new EmployeeContractStatus { Id = (int)EmployeeContractStatusType.Terminate, StatusType = EmployeeContractStatusType.Terminate.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active });
        }
    }
}
