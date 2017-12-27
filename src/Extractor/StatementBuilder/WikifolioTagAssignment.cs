using System;

namespace Extractor.StatementBuilder
{
    public class WikifolioTagAssignment : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{Wikifolio}"),
                new Placeholder("{WikifolioTag}"),
                new Placeholder("{CreationDate:RELATIVEDATETIME}")
            };

        private readonly Guid wikifolioGuid;

        public WikifolioTagAssignment(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "WikifolioTagAssignment";

        public override string GetCondition() => $" [Wikifolio] = '{this.wikifolioGuid}'";
    }
}