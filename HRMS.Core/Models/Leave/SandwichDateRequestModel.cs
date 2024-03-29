using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models.Leave
{
    public class SandwichDateRequestModel
    {
        [DisallowNull]
        public DateTime LeaveStartDate { get; set; }
        [DisallowNull]
        public string EmployeeCode { get; set; }
        [DefaultValue(false)]
        public bool ConsiderAllSaturday { get; set; }
    }
}
