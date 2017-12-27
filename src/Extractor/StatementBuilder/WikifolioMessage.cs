using System;

namespace Extractor.StatementBuilder
{
    public class WikifolioMessage : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{ID}"),
                new Placeholder("{Wikifolio}"),
                new Placeholder("{Message}"),
                new Placeholder("{CreationDate:RELATIVEDATETIME}"),
                new Placeholder("{Status}"),
                new Placeholder("{UnderlyingIsin}")
            };

        private readonly Guid wikifolioGuid;

        public WikifolioMessage(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "WikifolioMessage";

        public override string GetCondition() => $" [Wikifolio] = '{this.wikifolioGuid}'";
    }
}