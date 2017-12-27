using System;

namespace Extractor.StatementBuilder
{
    public class WikifolioDescription : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{Wikifolio}"),
                new Placeholder("{Language}"),
                new Placeholder("{LongDescription}")
            };

        private readonly Guid wikifolioGuid;

        public WikifolioDescription(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "WikifolioDescription";

        public override string GetCondition() => $" [Wikifolio] = '{this.wikifolioGuid}'";
    }
}