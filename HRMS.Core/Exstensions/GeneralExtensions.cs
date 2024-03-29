using HRMS.Core.Consts;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace HRMS.Core.Exstensions
{
    public static class GeneralExtensions
    {
        /// <summary>
        /// Get enum description attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetEnumDescriptionAttribute<T>(this T source)
        {
            FieldInfo fi = source?.GetType().GetField(source?.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes?.Length > 0)
            {
                return attributes[0]?.Description;
            }
            else
            {
                return source?.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Nullable<T> ToNullable<T>(this string s) where T : struct
        {
            Nullable<T> result = new Nullable<T>();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(s);
                }
            }
            catch { }
            return result;
        }
        /// <summary>
        /// Convert string to given enum type
        /// Extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            value = value.Contains(" ") ? value.Replace(" ", "") : value;
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }
        /// <summary>
        /// Convert string to given enum type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(string value)
        {
            value = value.Contains(" ") ? value.Replace(" ", "") : value;
            return (T)Enum.Parse(typeof(T), value, true);
        }
        /// <summary>
        /// Get month number from given string of month
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetMonthNumber(string name)
        {
            if (string.IsNullOrEmpty(name)) return 0;
            return DateTime.ParseExact(name, "MMMM", CultureInfo.InvariantCulture).Month;
        }
        /// <summary>
        /// Get month name from given number of month
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetMonthName(int number)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(number);
        }
        /// <summary>
        /// Given date Bewteen two start and date dates
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool IsBewteenTwoDates(this DateTime dt, DateTime start, DateTime end)
        {
            return dt > start && dt <= end;
        }
    }
}
