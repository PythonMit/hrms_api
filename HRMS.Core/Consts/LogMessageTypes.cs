using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum LogMessageTypes
    {
        [Description("Error")]
        Error = 1,
        [Description("Warning")]
        Warning = 2,
        [Description("Information")]
        Information = 3
    }
}
