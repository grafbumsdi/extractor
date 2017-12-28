namespace Extractor.StatementBuilder
{
    public class Underlying : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{ISIN}"),
                new Placeholder("{ShortDescription}"),
                new Placeholder("{LongDescription}"),
                new Placeholder("{ShortDescriptionForDisplaying}"),
                new Placeholder("{Status}"),
                new Placeholder("{SecurityType}"),
                new Placeholder("{WKN}"),
                new Placeholder("{Shortcode}"),
                new Placeholder("{RealtimeThresholdWeightage}"),
                new Placeholder("{InvestmentUniverse}"),
                new Placeholder("{UpdateDate:RELATIVEDATETIME}"),
                new Placeholder("{Issuer}"),
                new Placeholder("{ExpiryDate:RELATIVEDATETIME}"),
                new Placeholder("{LastTradeDate:RELATIVEDATETIME}")
            };

        private readonly string isin;

        public Underlying(string isin)
        {
            this.isin = isin;
        }

        public override string InsertStatement()
        {
            var insertStatement = base.InsertStatement();
            return
$@"IF NOT EXISTS (SELECT * FROM [{this.TableIdentifier()}] WHERE {this.GetCondition()})
BEGIN
    {insertStatement}
END";
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "Underlying";

        public override string GetCondition() => $" [ISIN] = '{this.isin}'";
    }
}