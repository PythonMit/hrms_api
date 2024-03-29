using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Consts
{
    public enum MailerType
    {
        [Description("Smtp")]
        Smtp = 0,
        [Description("ThirdParty")]
        ThirdParty = 1,
    }
}
