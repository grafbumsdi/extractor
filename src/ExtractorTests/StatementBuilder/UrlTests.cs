using System;

using NUnit.Framework;

namespace ExtractorTests.StatementBuilder
{
    [TestFixture]
    public class UrlTests
    {
        [Test]
        public void InsertStatementTest()
        {
            var wikifolioGuid = default(Guid);
            var urlStatementBuilder = new Extractor.StatementBuilder.Url(wikifolioGuid);
            Assert.AreEqual(
                "INSERT INTO [Url]([ID],[Wikifolio],[Url],[Count],[IdentifierType])VALUES({ID},{Wikifolio},{Url},{Count},{IdentifierType})",
                urlStatementBuilder.InsertStatement());
        }

        [Test]
        public void QueryStatementTest()
        {
            var wikifolioGuid = new Guid("C03CD005-2A25-4A10-A127-903D2135DFB1");
            var urlStatementBuilder = new Extractor.StatementBuilder.Url(wikifolioGuid);
            Assert.AreEqual(
                "SELECT [ID],[Wikifolio],[Url],[Count],[IdentifierType] FROM dbo.[Url] WHERE  [Wikifolio] = 'c03cd005-2a25-4a10-a127-903d2135dfb1'",
                urlStatementBuilder.QueryStatement());
        }
    }
}