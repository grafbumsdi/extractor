using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Extractor
{
    public class Replacer
    {
        private readonly IDictionary<string, object> dictionary;

        private readonly string outputFormatString;

        public Replacer(IDictionary<string, object> dictionary, string outputFormatString)
        {
            this.dictionary = dictionary;
            this.outputFormatString = outputFormatString;
        }

        public string GetFinalOutput()
        {
            var returnString = this.outputFormatString;
            foreach (var placeholder in this.GetPlaceholders())
            {
                var valueForPlaceHolder = this.GetValueForPlaceHolder(placeholder);
                returnString = returnString.Replace(placeholder.ExactPlaceHolderWithBrackets, valueForPlaceHolder);
            }
            return returnString;
        }

        public string GetValueForPlaceHolder(Placeholder placeholder)
        {
            var rawValue = this.dictionary[placeholder.ValueIdentifier];
            if (rawValue == DBNull.Value)
            {
                rawValue = null;
            }
            if (placeholder.Options.Count > 0)
            {
                return placeholder.Options.FirstOrDefault()?.RawValueToStringValue(rawValue);
            }
            if (rawValue == null)
            {
                return "NULL";
            }
            if (rawValue is decimal)
            {
                rawValue = ((decimal)rawValue).ToString(CultureInfo.InvariantCulture);
            }
            if (rawValue is string)
            {
                rawValue = ((string)rawValue)
                    .Replace("'", "''")
                    .Replace("\r\n", "' + CHAR(13) + CHAR(10) + '")
                    .Replace("\r", "' + CHAR(13) + '")
                    .Replace("\n", "' + CHAR(13) + '");
            }
            return $"'{rawValue.ToString()}'";
        }

        public IList<Placeholder> GetPlaceholders()
        {
            var regexForPlaceholders = new Regex(@"{.*?}");
            var placeHolders = new List<Placeholder>();
            foreach (Match match in regexForPlaceholders.Matches(this.outputFormatString))
            {
                placeHolders.Add(new Placeholder(match.Value));
            }
            return placeHolders;
        }
    }
}
