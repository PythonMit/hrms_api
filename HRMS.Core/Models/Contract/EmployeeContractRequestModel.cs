using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HRMS.Core.Models.Contract
{
    public class EmployeeContractRequestModel
    {
        [DefaultValue(0)]
        public int? Id { get; set; } = null;
        [Required, DefaultValue(0)]
        public int EmployeeId { get; set; }
        [Required, DefaultValue(0)]
        public int BranchId { get; set; }
        [Required, DefaultValue(null)]
        public DateTime ContractStartDate { get; set; }
        [Required, DefaultValue(null)]
        public DateTime ContractEndDate { get; set; }
        [Required, DefaultValue(0)]
        public int ProbationPeriod { get; set; }
        [Required, DefaultValue(0)]
        public int TrainingPeriod { get; set; }
        [Required, DefaultValue(0)]
        public int DesignationTypeId { get; set; }
        [Required, DefaultValue(0)]
        public int EmployeeContractStatusId { get; set; }
        [DefaultValue(false)]
        public bool IsRenew { get; set; }
        [DefaultValue(false)]
        public bool IsProjectTrainee { get; set; }
        public string Remarks { get; set; } = "";
        public DropInformationModel DropInformation { get; set; } = null;
        public NoticePeriodModel NoticePeriod { get; set; } = null;
        public FixGrossRequestModel FixGrossDetail { get; set; } = null;
        public string ImagekitDetailFileId { get; set; }
        public Guid? ImagekitDetailId { get; set; }
        public IFormFile FormFile { get; set; }
        public string FolderPath { get; set; }
        public bool HasIncentiveUpdate { get; set; }
        public TerminateModel Terminate { get; set; }
    }
}
