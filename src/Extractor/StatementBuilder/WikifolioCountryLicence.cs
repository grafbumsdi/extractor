using System;

namespace Extractor.StatementBuilder
{
    public class WikifolioCountryLicence : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{Wikifolio}"),
                new Placeholder("{Country}"),
                new Placeholder("{ISIN}")
            };

        private readonly Guid wikifolioGuid;

        public WikifolioCountryLicence(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "WikifolioCountryLicence";

        public override string GetCondition() => $" [Wikifolio] = '{this.wikifolioGuid}'";
    }
}