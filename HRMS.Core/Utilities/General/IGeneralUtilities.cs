using HRMS.Core.Consts;
using HRMS.Core.Models.Contract;
using HRMS.Core.Models.General;
using HRMS.Core.Models.Salary;
using HRMS.Core.Models.SystemFlag;
using System;
using System.Collections.Generic;

namespace HRMS.Core.Utilities.General
{
    public interface IGeneralUtilities
    {
        string GetOrdinalNumber(int num);
        FixGrossCalculationModel GetFixGrossCalculation(decimal costToCompany, IEnumerable<SystemFlagModel> systemFlags = null);
        EarningGrossCalculationModel GetEarningGrossCalculation(EmployeeFixGrossModel model, double totalDays, double paidDays);
        decimal GetOverTimeAmountCalculation(decimal costToCompany, decimal totalDays, double overTimeMinutes);
        int GetLeaveCategory(string leaveFor);
        string GetLeaveFor(int leaveType);
        int GetProfessionalTaxAmount(double earningGrossTotal);
        IEnumerable<DateTime> GetMonthlyDates(DateTime startDate, DateTime endDate, int fixIncentiveDuration = 1, int offset = 0);
        IEnumerable<MonthYearModel> GetMonths(DateTime? currentDate = null, int? ToMonth = 3, bool toPrevious = false);
        int GetTotalMonthsFrom(DateTime? start, DateTime? end, bool considerOffset = false);
        IEnumerable<DateTime?> GetDateRange(DateTime startDate, DateTime endDate, string dayTime = "");
        bool IsHalfDay(DateTime? date, string dayTime = "");
        int FindDay(int year, int month, DayOfWeek Day, int occurance);
        bool CheckDateInPreviousWeek(DateTime? date, int daysToAdd = 7);
        double? GetAvailableLeaveCount(DateTime? startDate, DateTime? currentDate, double? totalLeaves, double totalMonths = 12);
        int GetRolePriority(RoleTypes? role);
        double GetActualContractPeriod(DateTime? start, DateTime? end, double probationPeriod = 0);
        IEnumerable<int> GetYearRange(int startYear, int endYear);
        byte[] CreateCSV<T>(IEnumerable<T> list);
    }
}
