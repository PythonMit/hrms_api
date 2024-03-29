using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HRMS.Core.Models.Leave
{
    public class EmployeeLeaveBalanceListModel
    {
        public LeaveEmployeeDetailModel Employee { get; set; }
        [JsonPropertyName("LeavesV1"), JsonIgnore]
        public IEnumerable<EmployeeLeaveModel> Leaves { get; set; }
        [JsonPropertyName("Leaves")]
        public IEnumerable<EmployeeLeaveBalanceInfoModel> LeavesV2 { get; set; }
        public EmployeeContractOutlineModel Contract { get; set; }
    }
}
