using System;

namespace Extractor.StatementBuilder
{
    public class WikifolioStatusHistory : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{Wikifolio}"),
                new Placeholder("{Status}"),
                new Placeholder("{UpdateDate:RELATIVEDATETIME}")
            };

        private readonly Guid wikifolioGuid;

        public WikifolioStatusHistory(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "WikifolioStatusHistory";

        public override string GetCondition() => $" [Wikifolio] = '{this.wikifolioGuid}'";
    }
}