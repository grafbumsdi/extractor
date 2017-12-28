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
            this.AssertQueryStatement(virtualOrderStatementBuilder, wikifolioGuid, 50);

            var virtualOrderStatementBuilderWithLimit = new Extractor.StatementBuilder.VirtualOrder(wikifolioGuid, 10);
            this.AssertQueryStatement(virtualOrderStatementBuilderWithLimit, wikifolioGuid, 10);
        }

        [Test]
        public void GetUnderlyingsTest()
        {
            var wikifolioGuid = new Guid("C03CD005-2A25-4A10-A127-903D2135DFB1");
            var virtualOrderStatementBuilder = new Extractor.StatementBuilder.VirtualOrder(wikifolioGuid, 20);
            Assert.That(
                virtualOrderStatementBuilder.GetUnderlyings(),
                Is.EqualTo("SELECT DISTINCT [Underlying] FROM dbo.[VirtualOrder] WHERE " + this.ExpectedCondition(20, wikifolioGuid)));
        }

        private void AssertQueryStatement(
            Extractor.StatementBuilder.VirtualOrder virtualOrderStatementBuilder,
            Guid wikifolioGuid,
            int limit)
        {
            Assert.AreEqual(
                "SELECT [ID],[Wikifolio],[Underlying],[Stop],[Limit],[Amount],[CreationDate],[ValidUntilDate],[Status],"
                + "[VirtualOrderGroup],[ActivatingVirtualOrder],[OrderType],[ExecutionPrice],[ExecutionDate],"
                + "[RealisedGainUnderlying],[StatusErrorCode],[PhoneOrder],[OriginalVirtualOrder],[UpdateDate],"
                + "[TakeProfitVirtualOrder],[StopLossVirtualOrder],[MainOrder],[RowVersion],[Weight] "
                + "FROM dbo.[VirtualOrder] WHERE " + this.ExpectedCondition(limit, wikifolioGuid),
                virtualOrderStatementBuilder.QueryStatement());
        }

        private string ExpectedCondition(int limit, Guid wikifolioGuid)
        {
            return $@" [VirtualOrderGroup] IN
(
SELECT TOP {limit} [VirtualOrderGroup]
FROM
 (SELECT [VirtualOrderGroup], MIN([CreationDate]) as MinCreationDate
  FROM [VirtualOrder]
  WHERE [Wikifolio] = '{wikifolioGuid.ToString().ToLower()}'
  GROUP BY [VirtualOrderGroup]) AS VirtualOrderGroupsWithMinCreationDate
ORDER BY MinCreationDate DESC
) ORDER BY [CreationDate] ASC";
        }
    }
}