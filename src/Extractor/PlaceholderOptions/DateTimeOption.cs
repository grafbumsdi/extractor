using System;

namespace Extractor.PlaceholderOptions
{
    public class DateTimeOption : IPlaceholderOption
    {
        public const string Identifier = "DATETIME";

        public string GetIdentifier() => Identifier;

        public string RawValueToStringValue(object rawValue)
        {
            return (rawValue != null) ? $"'{ConvertDateToFuckingMsSqlDateTimeFormat((DateTime)rawValue)}'" : "NULL";
        }

        private static string ConvertDateToFuckingMsSqlDateTimeFormat(DateTime time)
        {
            // all of this fucking stuff not working
            // return time.ToString("yyyy-MM-dd HH:mm:ss");
            // return time.ToString("O");
            // return time.ToString("u");
            return time.Year + time.Month.ToString("D2") + time.Day.ToString("D2") + " " + time.Hour.ToString("D2")
                   + ":" + time.Minute.ToString("D2") + ":" + time.Second.ToString("D2") + "."
                   + time.Millisecond.ToString("D3");
        }
    }
}
