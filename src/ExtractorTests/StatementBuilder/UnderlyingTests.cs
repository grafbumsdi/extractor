using NUnit.Framework;

namespace ExtractorTests.StatementBuilder
{
    [TestFixture]
    public class UnderlyingTests
    {
        [Test]
        public void InsertStatementTest()
        {
            var isin = "DE00XXXGAXI";
            var underlyingStatementBuilder = new Extractor.StatementBuilder.Underlying(isin);
            Assert.That(
                underlyingStatementBuilder.InsertStatement(),
                Is.EqualTo(
                    $@"IF NOT EXISTS (SELECT * FROM [Underlying] WHERE  [ISIN] = '{isin}')
BEGIN
    INSERT INTO [Underlying]([ISIN],[ShortDescription],[LongDescription],[ShortDescriptionForDisplaying]," +
                    "[Status],[SecurityType],[WKN],[Shortcode],[RealtimeThresholdWeightage]," +
                    "[InvestmentUniverse],[UpdateDate],[Issuer],[ExpiryDate],[LastTradeDate])" + 
                    "VALUES({ISIN},{ShortDescription},{LongDescription},{ShortDescriptionForDisplaying}," +
                    "{Status},{SecurityType},{WKN},{Shortcode},{RealtimeThresholdWeightage}," +
                    "{InvestmentUniverse},{UpdateDate:RELATIVEDATETIME},{Issuer},{ExpiryDate:RELATIVEDATETIME}" +
                    @",{LastTradeDate:RELATIVEDATETIME})
END"));
        }
    }
}