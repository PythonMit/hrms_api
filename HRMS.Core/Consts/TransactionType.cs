using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum TransactionType
    {
        [Description("Carry Forward")]
        CFL = 1,
        [Description("Accrued")]
        ACC = 2,
        [Description("Use")]
        USE = 3,
        [Description("Leave Without Pay")]
        LWP = 4,
        [Description("Adjustment")]
        ADJ = 5,
    }
}
