using System.ComponentModel;

namespace HRMS.Core.Consts;
public enum ResourceTypes
{
    [Description("CPU")]
    CPU = 1,
    [Description("Monitor")]
    Monitor = 2,
    [Description("Keyboard")]
    Keyboard = 3,
    [Description("Mouse")]
    Mouse = 4,
    [Description("Mobile")]
    Mobile = 5,
    [Description("HeadPhone")]
    HeadPhone = 6,
    [Description("Printer")]
    Printer = 7,
}
