using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Consts
{
    public enum LeaveCategoryType
    {
        [Description("Casual Leave")]
        CasualLeave = 1,
        [Description("Privilege Leave")]
        PrivilegeLeave = 2,
        [Description("Sick Leave")]
        SickLeave = 3,
        [Description("Leave Without Pay")]
        LeaveWithoutPay = 4
    }
}
