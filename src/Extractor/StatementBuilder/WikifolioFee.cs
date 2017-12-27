using System;

namespace Extractor.StatementBuilder
{
    public class WikifolioFee : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{ID}"),
                new Placeholder("{Wikifolio}"),
                new Placeholder("{FeeType}"),
                new Placeholder("{Value}"),
                new Placeholder("{ExecutionDate:RELATIVEDATETIME}"),
                new Placeholder("{FeeDate:RELATIVEDATETIME}")
            };

        private readonly Guid wikifolioGuid;

        public WikifolioFee(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "WikifolioFee";

        public override string GetCondition() => $" [Wikifolio] = '{this.wikifolioGuid}'";
    }
}