using System;

namespace Extractor.StatementBuilder
{
    public class WikifolioItem: DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{Wikifolio}"),
                new Placeholder("{Underlying}"),
                new Placeholder("{Quantity}"),
                new Placeholder("{AveragePurchasePrice}")
            };

        private readonly Guid wikifolioGuid;

        public WikifolioItem(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "WikifolioItem";

        public override string GetCondition()
        {
            return $" [Wikifolio] = '{this.wikifolioGuid}'";
        }

        public string GetUnderlyingsQueryStatement() => this.QueryStatement(fields: "[Underlying]");
    }
}