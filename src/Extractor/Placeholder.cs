using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Extractor.PlaceholderOptions;

namespace Extractor
{
    public class Placeholder
    {
        private const string OptionsDelimiter = ":";

        private static readonly IPlaceholderOption[] validOptions =
            { new DateTimeOption(), new DateTimeRelativeOption(), new DateTimeRelativeUtcOption() };

        public Placeholder(string placeHolderStringWithBrackets)
        {
            this.ExactPlaceHolderWithBrackets = placeHolderStringWithBrackets;

            var placeHolderWithoutBrackets = RemoveBrackets(placeHolderStringWithBrackets);

            this.Options = GetOptions(placeHolderWithoutBrackets);

            if (this.Options.Count > 0)
            {
                var positionOfDelimiter = placeHolderWithoutBrackets.IndexOf($"{OptionsDelimiter}", StringComparison.Ordinal);
                this.ValueIdentifier = placeHolderWithoutBrackets.Substring(0, positionOfDelimiter);
            }
            else
            {
                this.ValueIdentifier = placeHolderWithoutBrackets;
            }
        }

        public Placeholder(string identifier, IPlaceholderOption option)
        {
            this.ValueIdentifier = identifier;
            this.ExactPlaceHolderWithBrackets = $"{{{identifier}{OptionsDelimiter}{option.GetIdentifier()}}}";
            this.Options = new List<IPlaceholderOption>() { option };
        }

        public string ExactPlaceHolderWithBrackets { get; private set; }

        public string ValueIdentifier { get; private set; }

        public IList<IPlaceholderOption> Options { get; private set; }

        public bool IsDateTime => this.Options.Any(option => option is DateTimeOption);

        public bool IsRelativeDateTime => this.Options.Any(option => option is DateTimeRelativeOption);

        public bool IsRelativeDateTimeUtc => this.Options.Any(option => option is DateTimeRelativeUtcOption);

        private static string RemoveBrackets(string placeholderWithBrackets)
        {
            if (!placeholderWithBrackets.StartsWith("{"))
            {
                throw new Exception("Placeholder did not start with {. But: '" + placeholderWithBrackets + "'");
            }
            if (!placeholderWithBrackets.EndsWith("}"))
            {
                throw new Exception("Placeholder did not end with }. But: '" + placeholderWithBrackets + "'");
            }
            if (placeholderWithBrackets.LastIndexOf("{", StringComparison.Ordinal) > 0)
            {
                throw new Exception("Multiple occurences of { . Input was: '" + placeholderWithBrackets + "'");
            }
            if (placeholderWithBrackets.IndexOf("}", StringComparison.Ordinal) < placeholderWithBrackets.Length - 1)
            {
                throw new Exception("Multiple occurences of } . Input was: '" + placeholderWithBrackets + "'");
            }
            return placeholderWithBrackets.Replace("{", string.Empty).Replace("}", string.Empty);
        }

        private static IList<IPlaceholderOption> GetOptions(string placeholder)
        {
            return GetOptionStrings(placeholder).Select(
                (optionString) =>
                    {
                        return validOptions.Single(validOption => validOption.GetIdentifier().Equals(optionString));
                    }).ToList();
        }

        private static IList<string> GetOptionStrings(string placeholder)
        {
            var optionsRegex = new Regex($"{OptionsDelimiter}[^{OptionsDelimiter}]+");
            var options = new List<string>();
            foreach (Match optionMatch in optionsRegex.Matches(placeholder))
            {
                options.Add(optionMatch.Value.Substring(OptionsDelimiter.Length));
            }
            return options;
        }
    }
}
