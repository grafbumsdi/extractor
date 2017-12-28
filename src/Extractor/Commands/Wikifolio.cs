using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

using McMaster.Extensions.CommandLineUtils;

namespace Extractor.Commands
{
    [Command(
         Name = "wikifolio",
         Description = @"extracts a given wikifolio from production database in form of INSERT statements.",
         ExtendedHelpText = @"
Example Usage: 
wikifolio 9B7F994D-3C9A-45B9-BB15-001A7EF522AA A61696C7-3343-4761-8EA7-1EB39A27E431 --withFees --withWikifolioTickData --outputfile ""gaxi.txt""
wikifolio 9B7F994D-3C9A-45B9-BB15-001A7EF522AA A61696C7-3343-4761-8EA7-1EB39A27E431
",
         ShowInHelpText = true), HelpOption]
    public class Wikifolio
    {
        private const string WikifolioGuidArgumentIdentifier = "WikifolioGuid";
        private const string WikifolioOwnerGuidArgumentIdentifier = "WikifolioOwnerGuid";

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must specify the wikifolio guid")]
        [Argument(
            order: 0,
            name: WikifolioGuidArgumentIdentifier,
            description: "The guid of the wikifolio you want to extract - MUST BE SPECIFIED")]
        public string WikifolioGuidArgument { get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must specify an existing user guid for " + WikifolioOwnerGuidArgumentIdentifier)]
        [Argument(
            order: 1,
            name: WikifolioOwnerGuidArgumentIdentifier,
            description: "The guid of the user that will be the owner of the wikifolio - MUST BE SPECIFIED")]
        public string WikifolioOwnerGuidArgument { get; }

        [Option(
            template: "--withWikifolioTickData",
            optionType: CommandOptionType.NoValue,
            Description = "If given, all wikifolio tick data will also be extracted")]
        public bool WithWikifolioTickData { get; }

        [Option(
            template: "--withFees",
            optionType: CommandOptionType.NoValue,
            Description = "If given, all wikifolio fees will also be extracted")]
        public bool WithFees { get; }

        [Option(
            template: "--outputfile",
            optionType: CommandOptionType.SingleValue,
            Description = "If given, the output will be written into the given file " + 
                          "(its content will be overwritten, if it exists already)")]
        public string OutputFile { get; }

        private void OnExecute(CommandLineApplication app, IConsole console)
        {
            Guid wikifolioGuid;
            Guid wikifolioOwnerGuid;
            if (!this.TryParseGuid(this.WikifolioGuidArgument, out wikifolioGuid, WikifolioGuidArgumentIdentifier)
                || !this.TryParseGuid(this.WikifolioOwnerGuidArgument, out wikifolioOwnerGuid, WikifolioOwnerGuidArgumentIdentifier))
            {
                app.ShowHelp();
                return;
            }

            var wikifolioExtractor = new WikifolioExtractor(
                wikifolioGuid,
                wikifolioOwnerGuid,
                this.WithWikifolioTickData,
                this.WithFees);

            TextWriter writer;
            if (!this.TryParseOutputFile(out writer))
            {
                writer = console.Out;
            }
            wikifolioExtractor.WriteInserts(writer);
        }

        private bool TryParseGuid(string rawValue, out Guid outputGuid, string argumentName)
        {
            if (!Guid.TryParse(rawValue, out outputGuid))
            {
                Console.WriteLine(
                    $"Could not parse argument: '{argumentName}'. It was: '{rawValue}', but should be a valid Guid");
                return false;
            }

            return true;
        }

        private bool TryParseOutputFile(out TextWriter writer)
        {
            writer = null;
            if (string.IsNullOrEmpty(this.OutputFile))
            {
                return false;
            }

            try
            {
                writer = File.CreateText(this.OutputFile);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"error wrinter to output file: '{this.OutputFile}'");
                Console.WriteLine(e);
                return false;
            }
        }
    }
}