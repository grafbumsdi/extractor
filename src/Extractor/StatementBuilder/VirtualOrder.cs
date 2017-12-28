using System;

namespace Extractor.StatementBuilder
{
    public class VirtualOrder : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{ID}"),
                new Placeholder("{Wikifolio}"),
                new Placeholder("{Underlying}"),
                new Placeholder("{Stop}"),
                new Placeholder("{Limit}"),
                new Placeholder("{Amount}"),
                new Placeholder("{CreationDate:RELATIVEDATETIME}"),
                new Placeholder("{ValidUntilDate:RELATIVEDATETIME}"),
                new Placeholder("{Status}"),
                new Placeholder("{VirtualOrderGroup}"),
                new Placeholder("{ActivatingVirtualOrder}"),
                new Placeholder("{OrderType}"),
                new Placeholder("{ExecutionPrice}"),
                new Placeholder("{ExecutionDate:RELATIVEDATETIME}"),
                new Placeholder("{RealisedGainUnderlying}"),
                new Placeholder("{StatusErrorCode}"),
                new Placeholder("{PhoneOrder}"),
                new Placeholder("{OriginalVirtualOrder}"),
                new Placeholder("{UpdateDate:RELATIVEDATETIME}"),
                new Placeholder("{TakeProfitVirtualOrder}"),
                new Placeholder("{StopLossVirtualOrder}"),
                new Placeholder("{MainOrder}"),
                new Placeholder("{RowVersion}"),
                new Placeholder("{Weight}")
            };

        private readonly Guid wikifolioGuid;

        private readonly int limit;

        public VirtualOrder(Guid wikifolioGuid, int limit = 50)
        {
            this.wikifolioGuid = wikifolioGuid;
            this.limit = limit;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "VirtualOrder";

        public override string GetCondition()
        {
            return $@" [VirtualOrderGroup] IN
(
SELECT TOP {this.limit} [VirtualOrderGroup]
FROM
 (SELECT [VirtualOrderGroup], MIN([CreationDate]) as MinCreationDate
  FROM [VirtualOrder]
  WHERE [Wikifolio] = '{this.wikifolioGuid}'
  GROUP BY [VirtualOrderGroup]) AS VirtualOrderGroupsWithMinCreationDate
ORDER BY MinCreationDate DESC
) ORDER BY [CreationDate] ASC";
        }

        public string GetUnderlyings() => this.QueryStatement(fields: "DISTINCT [Underlying]");
    }
}