using System;
using JustBlog.Core.Objects;
using System.Configuration;
using System.Web.Mvc;

namespace JustBlog
{
    public static class Extensions
    {
        public static string ToConfigLocalTime(this DateTime utcDT)
        {
            var istTZ = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["TimeZone"]);
            return String.Format("{0} ({1})", TimeZoneInfo.ConvertTimeFromUtc(utcDT, istTZ).ToShortDateString(), ConfigurationManager.AppSettings["TimeZoneAbbr"]);
        }
    }
}