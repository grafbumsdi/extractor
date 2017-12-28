using System;

using NUnit.Framework;

namespace ExtractorTests.StatementBuilder
{
    [TestFixture]
    public class VirtualOrderTests
    {
        [Test]
        public void QueryStatementTest()
        {
            var wikifolioGuid = new Guid("C03CD005-2A25-4A10-A127-903D2135DFB1");
            var virtualOrderStatementBuilder = new Extractor.StatementBuilder.VirtualOrder(wikifolioGuid);
            this.AssertQueryStatement(virtualOrderStatementBuilder, 50);

            var virtualOrderStatementBuilderWithLimit = new Extractor.StatementBuilder.VirtualOrder(wikifolioGuid, 10);
            this.AssertQueryStatement(virtualOrderStatementBuilderWithLimit, 10);
        }

        private void AssertQueryStatement(
            Extractor.StatementBuilder.VirtualOrder virtualOrderStatementBuilder, int limit)
        {
            Assert.AreEqual(
                "SELECT [ID],[Wikifolio],[Underlying],[Stop],[Limit],[Amount],[CreationDate],[ValidUntilDate],[Status],"
                + "[VirtualOrderGroup],[ActivatingVirtualOrder],[OrderType],[ExecutionPrice],[ExecutionDate],"
                + "[RealisedGainUnderlying],[StatusErrorCode],[PhoneOrder],[OriginalVirtualOrder],[UpdateDate],"
                + "[TakeProfitVirtualOrder],[StopLossVirtualOrder],[MainOrder],[RowVersion],[Weight] "
                + "FROM dbo.[VirtualOrder] WHERE " + $@" [VirtualOrderGroup] IN
(
SELECT TOP {limit} [VirtualOrderGroup]
FROM
 (SELECT [VirtualOrderGroup], MIN([CreationDate]) as MinCreationDate
  FROM [VirtualOrder]
  WHERE [Wikifolio] = 'c03cd005-2a25-4a10-a127-903d2135dfb1'
  GROUP BY [VirtualOrderGroup]) AS VirtualOrderGroupsWithMinCreationDate
ORDER BY MinCreationDate DESC
) ORDER BY [CreationDate] ASC",
                virtualOrderStatementBuilder.QueryStatement());
        }
    }
}