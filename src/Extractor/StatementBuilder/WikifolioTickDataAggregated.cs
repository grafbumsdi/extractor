using System;

namespace Extractor.StatementBuilder
{
    public class WikifolioTickDataAggregated : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{Wikifolio}"),
                new Placeholder("{BeginDate:RELATIVEDATETIME}"),
                new Placeholder("{Duration}"),
                new Placeholder("{Open}"),
                new Placeholder("{Close}"),
                new Placeholder("{High}"),
                new Placeholder("{Low}"),
                new Placeholder("{NumberOfTicksAggregated}"),
                new Placeholder("{CalculationDate:RELATIVEDATETIME}"),
                new Placeholder("{Currency}")
            };

        private readonly Guid wikifolioGuid;

        public WikifolioTickDataAggregated(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "WikifolioTickDataAggregated";

        public override string GetCondition() => $" [Wikifolio] = '{this.wikifolioGuid}'";
    }
}