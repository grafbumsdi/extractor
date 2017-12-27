using System;

namespace Extractor.StatementBuilder
{
    public class Wikifolio : DefaultStatementBuilder, IStatementBuilder
    {
        private const string QueryString = 
@"SELECT [ID]
      ,[Isin]
      ,[Wkn]
      ,[NamePrefix]
      ,[Name]
      ,[NamePostfix]
      ,[ShortDescription]
      ,[LongDescription]
      ,[Keywords]
      ,[RatioContracts]
      ,[ContractsSold]
      ,[CashAccountCurrentBalance]
      ,[CashAccountStartBalance]
      ,[Editor]
      ,[RealMoneyInvestor]
      ,[Status]
      ,[BlockedFromEmissionProcess]
      ,[DailyFeeRelative]
      ,[PerformanceFeeRelative]
      ,[TradingReminderEmailToBeSent]
      ,[TestPeriodeOverEmailToBeSent]
      ,[MaxTickInterval]
      ,[MinTickInterval]
      ,[Url]
      ,[TransmitTickToCoreInterval]
      ,[MarkupRelative]
      ,[MarkdownRelative]
      ,[Watermark]
      ,[TermSheet]
      ,[RealMoneyInvestorSetDate]
      ,[BlockedFromEmissionProcessReason]
      ,[ContainsLeverageProducts]
      ,[WikifolioType]
      ,[CreationDate]
      ,[PublishingDate]
      ,[PublishingPrice]
      ,[EmissionDate]
      ,[EmissionPrice]
      ,[SavingPlan]
      ,[Version]
      ,[ContractsSoldDistinct]
      ,[NoIndex]
      ,[DefaultLanguage]
      ,[Currency]
  FROM [dbo].[Wikifolio]
  WHERE ";

        public override string InsertStatementColumnList() => @"INSERT INTO [dbo].[Wikifolio]
           ([ID]
           ,[Isin]
           ,[Wkn]
           ,[NamePrefix]
           ,[Name]
           ,[NamePostfix]
           ,[ShortDescription]
           ,[LongDescription]
           ,[Keywords]
           ,[RatioContracts]
           ,[ContractsSold]
           ,[CashAccountCurrentBalance]
           ,[CashAccountStartBalance]
           ,[Editor]
           ,[RealMoneyInvestor]
           ,[Status]
           ,[BlockedFromEmissionProcess]
           ,[DailyFeeRelative]
           ,[PerformanceFeeRelative]
           ,[TradingReminderEmailToBeSent]
           ,[TestPeriodeOverEmailToBeSent]
           ,[MaxTickInterval]
           ,[MinTickInterval]
           ,[Url]
           ,[TransmitTickToCoreInterval]
           ,[MarkupRelative]
           ,[MarkdownRelative]
           ,[Watermark]
           ,[TermSheet]
           ,[RealMoneyInvestorSetDate]
           ,[BlockedFromEmissionProcessReason]
           ,[ContainsLeverageProducts]
           ,[WikifolioType]
           ,[CreationDate]
           ,[PublishingDate]
           ,[PublishingPrice]
           ,[EmissionDate]
           ,[EmissionPrice]
           ,[SavingPlan]
           ,[ContractsSoldDistinct]
           ,[NoIndex]
           ,[DefaultLanguage]
           ,[Currency])";

        public override string InsertStatementValuesList() => @"({ID}
           ,{Isin}
           ,{Wkn}
           ,{NamePrefix}
           ,{Name}
           ,{NamePostfix}
           ,{ShortDescription}
           ,{LongDescription}
           ,{Keywords}
           ,{RatioContracts}
           ,{ContractsSold}
           ,{CashAccountCurrentBalance}
           ,{CashAccountStartBalance}
           ,'" + this.EditorGuid.ToString() + @"'
           ,{RealMoneyInvestor}
           ,{Status}
           ,{BlockedFromEmissionProcess}
           ,{DailyFeeRelative}
           ,{PerformanceFeeRelative}
           ,{TradingReminderEmailToBeSent}
           ,{TestPeriodeOverEmailToBeSent}
           ,{MaxTickInterval}
           ,{MinTickInterval}
           ,{Url}
           ,{TransmitTickToCoreInterval}
           ,{MarkupRelative}
           ,{MarkdownRelative}
           ,{Watermark}
           ,{TermSheet}
           ,{RealMoneyInvestorSetDate}
           ,{BlockedFromEmissionProcessReason}
           ,{ContainsLeverageProducts}
           ,{WikifolioType}
           ,{CreationDate:RELATIVEDATETIME}
           ,{PublishingDate:RELATIVEDATETIME}
           ,{PublishingPrice}
           ,{EmissionDate}
           ,{EmissionPrice}
           ,{SavingPlan}
           ,{ContractsSoldDistinct}
           ,{NoIndex}
           ,{DefaultLanguage}
           ,{Currency})";

        public Wikifolio(Guid wikifolioGuid, Guid userGuid)
        {
            this.Condition = $"ID = '{wikifolioGuid.ToString()}'";
            this.EditorGuid = userGuid;
        }

        public string Condition { get; private set; }

        public Guid EditorGuid { get; private set; }

        public override string QueryStatement() => QueryString + this.Condition;

        public override Placeholder[] Fields()
        {
            throw new NotImplementedException();
        }

        public override string TableIdentifier() => "Wikifolio";

        public override string GetCondition()
        {
            throw new NotImplementedException();
        }
    }
}
