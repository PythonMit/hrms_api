using System.ComponentModel;

namespace HRMS.Core.Consts
{
    public enum DocumentTypes
    {
        [Description("PAN")]
        PAN = 1,
        [Description("AadhaarCard")]
        AadhaarCard = 2,
        [Description("ElectionCard")]
        ElectionCard = 3,
    }
}
