using System;

using Extractor.PlaceholderOptions;

namespace Extractor.StatementBuilder
{
    public class WikifolioDecisionMakingAssignment : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{Wikifolio}"),
                new Placeholder("{DecisionMaking}"),
                new Placeholder("CreationDate", new DateTimeRelativeOption())
            };

        private readonly Guid wikifolioGuid;

        public WikifolioDecisionMakingAssignment(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "WikifolioDecisionMakingAssignment";

        public override string GetCondition() => $" [Wikifolio] = '{this.wikifolioGuid}'";
    }
}
