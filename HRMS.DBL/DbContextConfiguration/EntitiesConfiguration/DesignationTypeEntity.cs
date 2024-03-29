using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace HRMS.DBL.DbContextConfiguration.EntitiesConfiguration
{
    public class DesignationTypeEntity : IEntityTypeConfiguration<DesignationType>
    {
        public void Configure(EntityTypeBuilder<DesignationType> builder)
        {
            builder.ToTable("DesignationTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.Property(x => x.Order).HasColumnName("Order");
            builder.HasData(DesignationTypeData());
        }

        private List<DesignationType> DesignationTypeData()
        {
            return new List<DesignationType> {
                new DesignationType { Id = (int)DesignationTypes.Director, Name = DesignationTypes.Director.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 1 },
                new DesignationType { Id = (int)DesignationTypes.HRExecutive, Name = DesignationTypes.HRExecutive.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 2 },
                new DesignationType { Id = (int)DesignationTypes.CTO, Name = DesignationTypes.CTO.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 3 },
                new DesignationType { Id = (int)DesignationTypes.ProjectManager, Name = DesignationTypes.ProjectManager.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 4 },
                new DesignationType { Id = (int)DesignationTypes.TeamLeader, Name = DesignationTypes.TeamLeader.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 5 },
                new DesignationType { Id = (int)DesignationTypes.BDM, Name = DesignationTypes.BDM.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 6 },
                new DesignationType { Id = (int)DesignationTypes.BDE, Name = DesignationTypes.BDE.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 7 },
                new DesignationType { Id = (int)DesignationTypes.FrontEndDeveloper, Name = DesignationTypes.FrontEndDeveloper.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 8 },
                new DesignationType { Id = (int)DesignationTypes.SrFrontEndDeveloper, Name = DesignationTypes.SrFrontEndDeveloper.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 9 },
                new DesignationType { Id = (int)DesignationTypes.BackEndDeveloper, Name = DesignationTypes.BackEndDeveloper.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 10 },
                new DesignationType { Id = (int)DesignationTypes.SrBackEndDeveloper, Name = DesignationTypes.SrBackEndDeveloper.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 11 },
                new DesignationType { Id = (int)DesignationTypes.FullStackDeveloper, Name = DesignationTypes.FullStackDeveloper.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 12 },
                new DesignationType { Id = (int)DesignationTypes.SrFullStackDeveloper, Name = DesignationTypes.SrFullStackDeveloper.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 13 },
                new DesignationType { Id = (int)DesignationTypes.MobileDeveloper, Name = DesignationTypes.MobileDeveloper.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 14 },
                new DesignationType { Id = (int)DesignationTypes.SrMobileDeveloper, Name = DesignationTypes.SrMobileDeveloper.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 15 },
                new DesignationType { Id = (int)DesignationTypes.UI_UXDesigner, Name = DesignationTypes.UI_UXDesigner.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 16 },
                new DesignationType { Id = (int)DesignationTypes.WebDesigner, Name = DesignationTypes.WebDesigner.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 17 },
                new DesignationType { Id = (int)DesignationTypes.SrWebDesigner, Name = DesignationTypes.SrWebDesigner.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 18 },
                new DesignationType { Id = (int)DesignationTypes.SoftwareEngineer, Name = DesignationTypes.SoftwareEngineer.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 19 },
                new DesignationType { Id = (int)DesignationTypes.PHPDeveloper, Name = DesignationTypes.PHPDeveloper.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 20 },
                new DesignationType { Id = (int)DesignationTypes.SrPHPDeveloper, Name = DesignationTypes.SrPHPDeveloper.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 21 },
                new DesignationType { Id = (int)DesignationTypes.Trainee, Name = DesignationTypes.Trainee.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 22 },
                new DesignationType { Id = (int)DesignationTypes.ProjectTrainee, Name = DesignationTypes.ProjectTrainee.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 23 },
                new DesignationType { Id = (int)DesignationTypes.SrSoftwareDeveloper, Name = DesignationTypes.SrSoftwareDeveloper.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 24 },
                new DesignationType { Id = (int)DesignationTypes.SoftwareDeveloper, Name = DesignationTypes.SoftwareDeveloper.GetEnumDescriptionAttribute(), RecordStatus = RecordStatus.Active, Order = 25 }
            };
        }
    }
}
