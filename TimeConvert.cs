using System;
using System.Globalization; // For culture-sensitive formatting

namespace cspWeather 
{
    public static class TimeConvert // Static class for utility methods
    {
        public static string FormatTime(DateTime dateTime, string format = "h:mm tt") // Default format
        {
            return dateTime.ToString(format);
        }

        public static string FormatDate(DateTime dateTime, string format = "MM/dd/yyyy") // Default format
        {
            return dateTime.ToString(format);
        }

        public static string FormatDateTime(DateTime dateTime, string format = "MM/dd/yyyy h:mm tt")
        {
            return dateTime.ToString(format);
        }


        // Culture-sensitive formatting (overloads)
        public static string FormatTime(DateTime dateTime, CultureInfo culture)
        {
            return dateTime.ToString("h:mm tt", culture);
        }

        public static string FormatDate(DateTime dateTime, CultureInfo culture)
        {
            return dateTime.ToString("MM/dd/yyyy", culture);
        }

        public static string FormatDateTime(DateTime dateTime, CultureInfo culture)
        {
            return dateTime.ToString("MM/dd/yyyy h:mm tt", culture);
        }

        // Getting time components (if needed)
        public static int GetHour(DateTime dateTime)
        {
            return dateTime.Hour;
        }

        public static int GetMinute(DateTime dateTime)
        {
            return dateTime.Minute;
        }

        public static string GetAmPm(DateTime dateTime)
        {
            return dateTime.ToString("tt");
        }

        public static int GetDay(DateTime dateTime)
        {
            return dateTime.Day;
        }

        public static int GetMonth(DateTime dateTime)
        {
            return dateTime.Month;
        }

        public static int GetYear(DateTime dateTime)
        {
            return dateTime.Year;
        }

        public static string GetMonthName(DateTime dateTime)
        {
            return dateTime.ToString("MMMM"); // Full month name
        }

        public static string GetDayOfWeek(DateTime dateTime)
        {
            return dateTime.ToString("dddd"); // Full day of the week name
        }

        public static DateTime ConvertFromUnixTimestamp(long unixTimeStamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).DateTime.ToLocalTime();
        }
    }
}
