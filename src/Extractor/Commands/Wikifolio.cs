using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

using Extractor.BuildingPlans;

using McMaster.Extensions.CommandLineUtils;

namespace Extractor.Commands
{
    [Command(
         Name = "wikifolio",
         Description = @"extracts a given wikifolio from production database in form of INSERT statements.",
         ExtendedHelpText = @"
Example Usage: 
wikifolio 9B7F994D-3C9A-45B9-BB15-001A7EF522AA --fixedWikifolioEditor A61696C7-3343-4761-8EA7-1EB39A27E431 --withFees --withWikifolioTickData --outputfile ""gaxi.txt""
wikifolio 9B7F994D-3C9A-45B9-BB15-001A7EF522AA
",
         ShowInHelpText = true), HelpOption]
    public class Wikifolio
    {
        private const string WikifolioGuidArgumentIdentifier = "WikifolioGuid";

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must specify the wikifolio guid")]
        [Argument(
            order: 0,
            name: WikifolioGuidArgumentIdentifier,
            description: "The guid of the wikifolio you want to extract - MUST BE SPECIFIED")]
        public string WikifolioGuidArgument { get; }

        [Option(
            template: "--fixedWikifolioEditor <USER_GUID>",
            optionType: CommandOptionType.SingleValue,
            description: "If given, the guid will be used as owner / editor of the wikifolio.")]
        public string FixedWikifolioEditor { get; }

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
            template: "--withItems",
            optionType: CommandOptionType.NoValue,
            Description = "If given, all wikifolio items (including underlyings) will also be extracted")]
        public bool WithItems { get; }

        [Option(
            template: "--withRecentVirtualOrderGroups <MAXIMUM_LIMIT_OF_VIRTUALORDERGROUPS>",
            optionType: CommandOptionType.SingleValue,
            Description = "If given, most recent virtual order groups of the wikifolio will also be extracted. " +
                          "Number of virtual order groups is limited by <MAXIMUM_LIMIT_OF_VIRTUALORDERGROUPS>")]
        public int? WithRecentVirtualOrderGroups { get; }

        [Option(
            template: "--outputfile",
            optionType: CommandOptionType.SingleValue,
            Description = "If given, the output will be written into the given file " + 
                          "(its content will be overwritten, if it exists already)")]
        public string OutputFile { get; }

        private void OnExecute(CommandLineApplication app, IConsole console)
        {
            TextWriter writer;
            if (!this.TryParseOutputFile(out writer))
            {
                writer = console.Out;
            }

            Guid wikifolioGuid;
            
            if (!this.TryParseGuid(this.WikifolioGuidArgument, out wikifolioGuid, WikifolioGuidArgumentIdentifier))
            {
                app.ShowHelp();
                return;
            }

            var extractor = new BasicExtractor();
            if (!string.IsNullOrEmpty(this.FixedWikifolioEditor))
            {
                Guid wikifolioOwnerGuid;
                if (!this.TryParseGuid(this.FixedWikifolioEditor, out wikifolioOwnerGuid, "--fixedWikifolioEditor"))
                {
                    app.ShowHelp();
                    return;
                }
                extractor.AddBuildingPlan(
                    new WikifolioBuildingPlan(
                        wikifolioGuid,
                        wikifolioOwnerGuid,
                        this.WithWikifolioTickData,
                        this.WithFees,
                        this.WithItems));
            }
            else
            {
                extractor.AddBuildingPlan(
                    new WikifolioWithUserBuildingPlan(
                        wikifolioGuid,
                        this.WithWikifolioTickData,
                        this.WithFees,
                        this.WithItems));
            }

            if (this.WithRecentVirtualOrderGroups.HasValue)
            {
                extractor.AddBuildingPlan(
                    new VirtualOrderBuildingPlan(wikifolioGuid, this.WithRecentVirtualOrderGroups.Value));
            }
            extractor.WriteInserts(writer);
            writer.WriteLine("-- DONE");
            writer.Close();
            writer.Dispose();
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