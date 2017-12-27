using System;

namespace Extractor.StatementBuilder
{
    public class Url : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{ID}"),
                new Placeholder("{Wikifolio}"),
                new Placeholder("{Url}"),
                new Placeholder("{Count}"),
                new Placeholder("{IdentifierType}")
            };

        private readonly Guid wikifolioGuid;

        public Url(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "Url";

        public override string GetCondition()
        {
            return $" [Wikifolio] = '{this.wikifolioGuid}'";
        }
    }
}
