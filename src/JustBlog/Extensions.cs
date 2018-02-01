using System;
using System.Configuration;

namespace JustBlog
{
    public static class Extensions
    {
        public static string ToConfigLocalTime(this DateTime utcDT)
        {
            var istTZ = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["TimeZone"]);
            //var istTZ = TimeZone.CurrentTimeZone;
            return String.Format("{0} ({1})", TimeZoneInfo.ConvertTimeFromUtc(utcDT, istTZ).ToShortDateString(), ConfigurationManager.AppSettings["TimeZoneAbbr"]);
            //return TimeZoneInfo.GetSystemTimeZones().ToString();
        }
    }
}