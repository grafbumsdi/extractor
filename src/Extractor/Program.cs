using McMaster.Extensions.CommandLineUtils;

namespace Extractor
{
    [HelpOption,
    Subcommand("wikifolio", typeof(Commands.Wikifolio))]
    public class Program
    {
        public static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);
    }
}
