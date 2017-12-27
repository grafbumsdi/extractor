using System;

namespace Extractor.StatementBuilder
{
    public class WikifolioInvestmentUniverseAssignment : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{Wikifolio}"),
                new Placeholder("{InvestmentUniverse}")
            };

        private readonly Guid wikifolioGuid;

        public WikifolioInvestmentUniverseAssignment(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "WikifolioInvestmentUniverseAssignment";

        public override string GetCondition() => $" [Wikifolio] = '{this.wikifolioGuid}'";
    }
}