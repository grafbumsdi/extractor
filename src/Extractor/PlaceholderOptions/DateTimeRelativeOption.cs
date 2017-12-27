using System;

namespace Extractor.PlaceholderOptions
{
    public class DateTimeRelativeOption : IPlaceholderOption
    {
        public string GetIdentifier() => "RELATIVEDATETIME";

        public string RawValueToStringValue(object rawValue)
        {
            return (rawValue != null) ? ConvertToRelativeDateTime((DateTime)rawValue) : "NULL";
        }

        private static string ConvertToRelativeDateTime(DateTime time)
        {
            var diff = DateTime.Now.Date - time.Date;
            return
                $"DATEADD(MILLISECOND, {time.TimeOfDay.TotalMilliseconds}, DATEADD(DAY, DATEDIFF(DAY, {diff.TotalDays}, GETDATE()), 0))";
        }
    }
}