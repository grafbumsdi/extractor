using System;

namespace Extractor.PlaceholderOptions
{
    public class DateTimeRelativeUtcOption : IPlaceholderOption
    {
        public string GetIdentifier() => "RELATIVEDATETIMEUTC";

        public string RawValueToStringValue(object rawValue)
        {
            return (rawValue != null) ? ConvertToRelativeDateTimeUtc((DateTime)rawValue) : "NULL";
        }

        private static string ConvertToRelativeDateTimeUtc(DateTime timeInUtc)
        {
            var diff = DateTime.UtcNow.Date - timeInUtc.Date;
            return
                $"DATEADD(MILLISECOND, {timeInUtc.TimeOfDay.TotalMilliseconds}, DATEADD(DAY, DATEDIFF(DAY, {diff.TotalDays}, GETUTCDATE()), 0))";
        }
    }
}