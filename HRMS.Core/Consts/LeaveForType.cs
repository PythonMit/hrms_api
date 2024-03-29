using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum LeaveForType
    {      
        [Description("Unplanned/Urgent")]
        Unplanned_Urgent = 1,
        [Description("Holiday/Vacation")]
        Holiday_Vacation = 2,
        [Description("Sick")]
        Sick = 3,
    }
}
