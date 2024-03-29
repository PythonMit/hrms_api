using System.ComponentModel;

namespace HRMS.Core.Consts;
public enum  ResourceStatus
{
    [Description("Working")]
    Working = 1,
    [Description("Notworking")]
    Notworking = 2,
    [Description("Damange")]
    Damange = 3,
    [Description("Lost")]
    Lost = 4,
    [Description("InRepaire")]
    InRepaire = 5,
}

