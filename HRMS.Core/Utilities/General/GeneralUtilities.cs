using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Contract;
using HRMS.Core.Models.General;
using HRMS.Core.Models.Salary;
using HRMS.Core.Models.SystemFlag;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HRMS.Core.Utilities.General
{
    /// <summary>
    /// General Utilities
    /// </summary>
    public class GeneralUtilities : IGeneralUtilities
    {
        /// <summary>
        /// Get ordinal number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string GetOrdinalNumber(int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }
        /// <summary>
        /// Calculating the fix Gross from CTC
        /// Basic 50 % of CTC
        /// DA 10 % of CTC - Not calculated
        /// HRA 25 % of Basic
        /// LTA 8.33% of Basic
        /// </summary>
        /// <param name="costToCompany"></param>
        /// <returns></returns>
        public FixGrossCalculationModel GetFixGrossCalculation(decimal costToCompany, IEnumerable<SystemFlagModel> systemFlags = null)
        {
            if (costToCompany <= 0)
            {
                return null;
            }

            double basicFlag = 50, hraFlag = 40, ltaFlag = 8.33, caFlag = 1600;
            if (systemFlags != null && systemFlags.Count() > 0)
            {
                basicFlag = systemFlags?.Where(x => x.Tags.ToLower().Contains("basic")).Select(x => Convert.ToDouble(x.Value)).FirstOrDefault() ?? basicFlag;
                hraFlag = systemFlags?.Where(x => x.Tags.ToLower().Contains("hra")).Select(x => Convert.ToDouble(x.Value)).FirstOrDefault() ?? hraFlag;
                ltaFlag = systemFlags?.Where(x => x.Tags.ToLower().Contains("lta")).Select(x => Convert.ToDouble(x.Value)).FirstOrDefault() ?? ltaFlag;
                caFlag = systemFlags?.Where(x => x.Tags.ToLower().Contains("conveyanceallowance")).Select(x => Convert.ToDouble(x.Value)).FirstOrDefault() ?? caFlag;
            }

            var result = new FixGrossCalculationModel();
            result.Basic = Math.Round(Convert.ToDouble(costToCompany) * basicFlag / 100);
            result.DA = 0; // Math.Round(Convert.ToDouble(result.Basic * 10 / 100));
            result.LTA = Math.Round(result.Basic * ltaFlag / 100); ;
            result.HRA = Math.Round(result.Basic * hraFlag / 100);

            //NOTE: New calculations
            var remaining = Convert.ToDouble(costToCompany) - (result.Basic + result.LTA + result.HRA + caFlag);
            result.ConveyanceAllowance = caFlag;
            result.OtherAllowance = remaining;

            //NOTE: Old calculation
            //var remaining = Math.Round(Convert.ToDouble(result.Basic * 15 / 100));
            //if (remaining <= 2000)
            //{
            //    result.ConveyanceAllowance = remaining;
            //}
            //else
            //{
            //    result.ConveyanceAllowance = 2000;
            //    result.OtherAllowance = (remaining - 2000);
            //}

            return result;
        }
        /// <summary>
        /// Calculating the earning gross from CTC
        /// </summary>
        /// <param name="costToCompany"></param>
        /// <param name="totalDays"></param>
        /// <param name="paidDays"></param>
        /// <returns></returns>
        public EarningGrossCalculationModel GetEarningGrossCalculation(EmployeeFixGrossModel model, double totalDays = 0, double paidDays = 0)
        {
            var result = new EarningGrossCalculationModel();
            if (totalDays == 0 || paidDays == 0)
            {
                return null;
            }

            if (model.DesignationTypeId == (int)DesignationTypes.ProjectTrainee)
            {
                model.CostToCompany = Convert.ToDecimal(model.StipendAmount);
            }

            var actualCTC = Math.Round(((Convert.ToDouble(model.CostToCompany) / totalDays) * paidDays));
            model.CostToCompany = Math.Round(Convert.ToDecimal(actualCTC));

            if (model.CostToCompany <= 0)
            {
                return null;
            }

            result.Basic = Math.Round((model.Basic / totalDays) * paidDays);
            result.DA = Math.Round((model.DA / totalDays) * paidDays);
            result.LTA = Math.Round((model.LTA / totalDays) * paidDays);
            result.HRA = Math.Round((model.HRA / totalDays) * paidDays);
            result.ConveyanceAllowance = Math.Round((model.ConveyanceAllowance / totalDays) * paidDays);
            result.OtherAllowance = Math.Round((model.OtherAllowance / totalDays) * paidDays);

            result.TotalEarning = (result.Basic + result.DA + result.LTA + result.HRA + result.ConveyanceAllowance + result.OtherAllowance);
            return result;
        }
        /// <summary>
        /// Get overtime amount calculation
        /// </summary>
        /// <param name="costToCompany"></param>
        /// <param name="totalDays"></param>
        /// <param name="overTimeMinutes"></param>
        /// <returns></returns>
        public decimal GetOverTimeAmountCalculation(decimal costToCompany, decimal totalDays, double overTimeMinutes)
        {
            decimal oneHourAmount = (costToCompany / totalDays / 8);
            return (oneHourAmount * 150 / 100) / 60 * Convert.ToDecimal(overTimeMinutes);
        }
        /// <summary>
        /// Get leave category
        /// </summary>
        /// <param name="leaveFor"></param>
        /// <returns></returns>
        public int GetLeaveCategory(string leaveFor)
        {
            if (leaveFor == LeaveForType.Unplanned_Urgent.GetEnumDescriptionAttribute())
            {
                return (int)LeaveCategoryType.CasualLeave;
            }
            else if (leaveFor == LeaveForType.Holiday_Vacation.GetEnumDescriptionAttribute())
            {
                return (int)LeaveCategoryType.PrivilegeLeave;
            }
            else if (leaveFor == LeaveForType.Sick.GetEnumDescriptionAttribute())
            {
                return (int)LeaveCategoryType.SickLeave;
            }
            return (int)LeaveTypes.LeaveWithoutPay;
        }
        /// <summary>
        /// Get leave for
        /// </summary>
        /// <param name="leaveType"></param>
        /// <returns></returns>
        public string GetLeaveFor(int leaveType)
        {
            switch (leaveType)
            {
                case (int)LeaveCategoryType.CasualLeave:
                    return LeaveForType.Unplanned_Urgent.GetEnumDescriptionAttribute();
                case (int)LeaveCategoryType.PrivilegeLeave:
                    return LeaveForType.Holiday_Vacation.GetEnumDescriptionAttribute();
                case (int)LeaveCategoryType.SickLeave:
                    return LeaveForType.Sick.GetEnumDescriptionAttribute();
                default:
                    return string.Empty;
            }
        }
        /// <summary>
        /// Get professional tax amount 
        /// </summary>
        /// <param name="earningGrossTotal"></param>
        /// <returns></returns>
        public int GetProfessionalTaxAmount(double earningGrossTotal)
        {
            if (earningGrossTotal > 12000)
                return 200;
            else return 0;

        }
        /// <summary>
        /// Get interval of dates based on given duration between two dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="fixIncentiveDuration"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> GetMonthlyDates(DateTime startDate, DateTime endDate, int fixIncentiveDuration = 1, int offset = 0)
        {
            var dates = new List<DateTime>();
            var runningDate = startDate;
            while (runningDate < endDate)
            {
                DateTime endOfThisPeriod = runningDate.AddMonths(fixIncentiveDuration);
                endOfThisPeriod = (endOfThisPeriod < endDate ? endOfThisPeriod : endDate);
                dates.Add(endOfThisPeriod.AddMonths(offset));
                runningDate = endOfThisPeriod;
            }

            return dates;
        }
        /// <summary>
        /// Get total month difference between two dates
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int GetTotalMonthsFrom(DateTime? start, DateTime? end, bool considerOffset = false)
        {
            DateTime? earlyDate = (start > end) ? end?.Date : start?.Date;
            DateTime? lateDate = (start > end) ? start?.Date : end?.Date;

            // Start with 1 month's difference and keep incrementing
            // until we overshoot the late date
            int monthsDiff = 1;
            while (earlyDate?.AddMonths(monthsDiff) <= lateDate)
            {
                monthsDiff++;
            }

            return monthsDiff - (considerOffset ? 1 : 0);
        }
        /// <summary>
        /// Get number of month from given date
        /// </summary>
        /// <param name="currentDate"></param>
        /// <param name="ToMonth"></param>
        /// <returns></returns>
        public IEnumerable<MonthYearModel> GetMonths(DateTime? currentDate = null, int? ToMonth = 3, bool toPrevious = false)
        {
            if (!currentDate.HasValue && ToMonth == null)
            {
                return null;
            }

            var months = (from i in Enumerable.Range(0, ToMonth.Value)
                          let now = (toPrevious ? currentDate?.AddMonths(-i) : currentDate?.AddMonths(i))
                          select new MonthYearModel
                          {
                              MonthLabel = now?.ToString("MMMM"),
                              Month = now?.Month ?? 0,
                              Year = now?.Year ?? 0
                          }).ToList();

            if (!months.Any(x => x.Month == currentDate?.Month && x.Year == currentDate?.Year))
            {
                months.Add(new MonthYearModel
                {
                    MonthLabel = currentDate?.ToString("MMMM"),
                    Month = currentDate?.Month ?? 0,
                    Year = currentDate?.Year ?? 0
                });
            }

            return months;
        }
        /// <summary>
        /// Get date range in between
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<DateTime?> GetDateRange(DateTime startDate, DateTime endDate, string dayTime = "")
        {
            var result = new List<DateTime?>();

            var isStartHalfDay = IsHalfDay(startDate, dayTime);
            if (isStartHalfDay)
            {
                result.Add(startDate);
                DateTime s = startDate;
                TimeSpan ts = new TimeSpan(13, 30, 0);
                s = s.Date + ts;

                startDate = s;
            }

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.Date == startDate.Date && !isStartHalfDay)
                {
                    result.Add(startDate);
                }
                else if (date.Date == endDate.Date)
                {
                    result.Add(endDate);
                }
                else
                {
                    result.Add(date);
                }
            }

            return result;
        }
        public bool IsHalfDay(DateTime? date, string dayTime = "")
        {
            var time = date.Value.TimeOfDay;
            return (time == TimeSpan.Parse(dayTime));
        }
        /// <summary>
        /// To get the first particular day of the month, start with the first day of the month: yyyy-mm-01. 
        /// Use whatever function is available to give a number corresponding to the day of the week; in C# this would be DateTime.DayOfWeek. 
        /// Subtract that number from the day you are looking for; for example, if the first day of the month is Wednesday (3) and you're looking for Friday (5), subtract 3 from 5, leaving 2. 
        /// If the answer is negative, add 7. Finally add that to the first of the month; for my example, the first Friday would be the 3rd.
        /// To get the last Friday of the month, find the first Friday of the next month and subtract 7 days.
        /// To get the 3rd Friday of the month, add 14 days to the first Friday.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="Day"></param>
        /// <param name="occurance"></param>
        /// <returns></returns>
        public int FindDay(int year, int month, DayOfWeek Day, int occurance)
        {
            if (occurance <= 0 || occurance > 5)
            {
                return 0;
            }

            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            //Substract first day of the month with the required day of the week 
            var daysneeded = (int)Day - (int)firstDayOfMonth.DayOfWeek;
            //if it is less than zero we need to get the next week day (add 7 days)
            if (daysneeded < 0) daysneeded = daysneeded + 7;
            //DayOfWeek is zero index based; multiply by the Occurance to get the day
            var resultedDay = (daysneeded + 1) + (7 * (occurance - 1));

            if (resultedDay > (firstDayOfMonth.AddMonths(1) - firstDayOfMonth).Days)
            {
                return 0;
            }

            return resultedDay;
        }
        /// <summary>
        /// Find exact day of the month
        /// </summary>
        /// <param name="date"></param>
        /// <param name="daysToAdd"></param>
        /// <returns></returns>
        public bool CheckDateInPreviousWeek(DateTime? date, int daysToAdd = 7)
        {
            if (date == null)
            {
                return false;
            }

            DateTime startOfLastWeek = date.Value.AddDays(-(int)date.Value.DayOfWeek);
            DateTime endOfLastWeek = startOfLastWeek.AddDays(daysToAdd);

            return date.Value.IsBewteenTwoDates(startOfLastWeek, endOfLastWeek);
        }
        /// <summary>
        /// Get available leave count based on fixed formula and total months
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="currentDate"></param>
        /// <param name="totalLeaves"></param>
        /// <param name="totalMonths"></param>
        /// <returns></returns>
        public double? GetAvailableLeaveCount(DateTime? startDate, DateTime? currentDate, double? totalLeaves, double totalMonths = 12)
        {
            var month = GetTotalMonthsFrom(startDate, currentDate);
            var monthlyActualNumber = (totalLeaves.HasValue ? (totalLeaves / totalMonths) : 0);

            return Math.Round(Convert.ToDouble(month * monthlyActualNumber));
        }
        /// <summary>
        /// Get role priority
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public int GetRolePriority(RoleTypes? role)
        {
            switch (role)
            {
                case RoleTypes.SuperAdmin:
                    return (int)RoleTypePriority.SuperAdmin;
                case RoleTypes.HRManager:
                    return (int)RoleTypePriority.HRManager;
                case RoleTypes.Employee:
                    return (int)RoleTypePriority.Employee;
                case RoleTypes.Manager:
                    return (int)RoleTypePriority.Manager;
                case RoleTypes.Guest:
                    return (int)RoleTypePriority.Guest;
                default:
                    return 0;
            }
        }
        public double GetActualContractPeriod(DateTime? start, DateTime? end, double probationPeriod = 0)
        {
            var months = GetTotalMonthsFrom(start, end);
            return (months - probationPeriod);
        }
        /// <summary>
        /// Get all year from start to end
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public IEnumerable<int> GetYearRange(int startYear, int endYear)
        {
            return Enumerable.Range(startYear, ((endYear - startYear) + 1));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public byte[] CreateCSV<T>(IEnumerable<T> list)
        {
            var stream = new MemoryStream();
            using (var sw = new StreamWriter(stream))
            {
                CreateRows(list, sw);
                sw.Flush();
                stream.Position = 0;
            }

            return stream.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sw"></param>
        private void CreateRows<T>(IEnumerable<T> list, StreamWriter sw)
        {
            foreach (var item in list)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    var prop = properties[i];
                    sw.Write(prop.GetValue(item) + ",");
                }
                var lastProp = properties[properties.Length - 1];
                sw.Write(lastProp.GetValue(item) + sw.NewLine);
            }
        }
    }
}
