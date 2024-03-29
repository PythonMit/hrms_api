using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum EmployeeContractStatusType
    {
        [Description("Running")]
        Running = 1,
        [Description("Drop")]
        Drop = 2,
        [Description("Notice Period")]
        NoticePeriod = 3,
        [Description("Completed")]
        Completed = 4,
        [Description("Terminate")]
        Terminate = 5,
    }
}
