using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum LeaveTypes
    {
        [Description("Carry Forward")]
        CarryForward = 1,
        [Description("Flat Leave")]
        FlatLeave = 2,
        [Description("Leave Without Pay")]
        LeaveWithoutPay = 3,
    }
}
