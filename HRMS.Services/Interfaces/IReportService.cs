using HRMS.Core.Models.Report;
using System.Threading.Tasks;

namespace HRMS.Services.Interfaces
{
    public interface IReportService
    {
        #region Leave
        Task GetLeaveSummary(ReportFilterModel filter);
        Task GetLeaveDetail(ReportFilterModel filter);
        #endregion Leave
        #region Overtime
        Task GetOvertimeSummary(ReportFilterModel filter);
        Task GetOvertimeDetail(ReportFilterModel filter);
        #endregion Overtime
    }
}
