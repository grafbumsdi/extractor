using System;

namespace Extractor.StatementBuilder
{
    public class WikifolioRankingValues : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{Wikifolio}"),
                new Placeholder("{PerformanceMonthValue}"),
                new Placeholder("{PerformanceMonthIndex}"),
                new Placeholder("{Performance3MonthsValue}"),
                new Placeholder("{Performance3MonthsIndex}"),
                new Placeholder("{Performance6MonthsValue}"),
                new Placeholder("{Performance6MonthsIndex}"),
                new Placeholder("{PerformanceYearValue}"),
                new Placeholder("{PerformanceYearIndex}"),
                new Placeholder("{PerformanceEverValue}"),
                new Placeholder("{PerformanceEverIndex}"),
                new Placeholder("{AveragePerformancePerMonthValue}"),
                new Placeholder("{AveragePerformancePerMonthIndex}"),
                new Placeholder("{TotalInvestmentsValue}"),
                new Placeholder("{TotalInvestmentsIndex}"),
                new Placeholder("{SharpeRatioValue}"),
                new Placeholder("{SharpeRatioIndex}"),
                new Placeholder("{LatestWikifoliosValue}"),
                new Placeholder("{LatestWikifoliosIndex}"),
                new Placeholder("{NumberOfBuyingInterestsValue}"),
                new Placeholder("{NumberOfBuyingInterestsIndex}"),
                new Placeholder("{MostTradedValue}"),
                new Placeholder("{MostTradedIndex}"),
                new Placeholder("{MaxDrawdownValue}"),
                new Placeholder("{MaxDrawdownIndex}"),
                new Placeholder("{EmissionDateValue}"),
                new Placeholder("{EmissionDateIndex}"),
                new Placeholder("{TotalBuyingInterestsValue}"),
                new Placeholder("{TotalBuyingInterestsIndex}"),
                new Placeholder("{TopWikifoliosValue}"),
                new Placeholder("{TopWikifoliosIndex}"),
                new Placeholder("{TradingVolumeValue}"),
                new Placeholder("{TradingVolumeIndex}"),
                new Placeholder("{ReturnSinceEmissionValue}"),
                new Placeholder("{ReturnSinceEmissionIndex}"),
                new Placeholder("{UpdateDate:RELATIVEDATETIME}"),
                new Placeholder("{PerformanceSinceEmissionValue}"),
                new Placeholder("{PerformanceSinceEmissionIndex}"),
                new Placeholder("{PerformanceYTDValue}"),
                new Placeholder("{PerformanceYTDIndex}"),
                new Placeholder("{Week52HighValue}"),
                new Placeholder("{Week52HighIndex}"),
                new Placeholder("{RiskValue}"),
                new Placeholder("{RiskIndex}"),
                new Placeholder("{LiquidationFigureValue}"),
                new Placeholder("{LiquidationFigureIndex}"),
                new Placeholder("{PerformanceIntradayIndex}"),
                new Placeholder("{PerformanceIntradayValue}")
            };

        private readonly Guid wikifolioGuid;

        public WikifolioRankingValues(Guid wikifolioGuid)
        {
            this.wikifolioGuid = wikifolioGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "WikifolioRankingValues";

        public override string GetCondition() => $" [Wikifolio] = '{this.wikifolioGuid}'";
    }
}