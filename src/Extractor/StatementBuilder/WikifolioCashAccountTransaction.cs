using System;

namespace Extractor.StatementBuilder
{
    public class WikifolioCashAccountTransaction : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{ID}"),
                new Placeholder("{Wikifolio}"),
                new Placeholder("{CreationDate:RELATIVEDATETIME}"),
                new Placeholder("{ExecutionDate:RELATIVEDATETIME}"),
                new Placeholder("{Amount}"),
                new Placeholder("{Description}"),
                new Placeholder("{CatType}")
            };

        private readonly Guid wikifolioGuid;

        public WikifolioCashAccountTransaction(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "WikifolioCashAccountTransaction";

        public override string GetCondition() => $" [Wikifolio] = '{this.wikifolioGuid}'";
    }
}