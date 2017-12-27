using System;
using System.Collections.Generic;
using System.IO;

using Extractor.StatementBuilder;

namespace Extractor
{
    public class WikifolioExtractor
    {
        private readonly Guid wikifolioGuid;
        private readonly Guid userGuid;

        public WikifolioExtractor(Guid wikifolioGuid)
        {
            throw new NotImplementedException("wikifolio creation with user creation not implemented yet");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wikifolioGuid"></param>
        /// <param name="existingUserGuid">That will be used for Insert Statements</param>
        public WikifolioExtractor(Guid wikifolioGuid, Guid existingUserGuid, bool withTicks, bool withFees)
        {
            this.wikifolioGuid = wikifolioGuid;
            this.userGuid = existingUserGuid;
            this.StatementBuilders = new List<IStatementBuilder>()
                                         {
                                             new StatementBuilder.Wikifolio(
                                                 this.wikifolioGuid,
                                                 this.userGuid),
                                             new StatementBuilder.Url(this.wikifolioGuid),
                                             new StatementBuilder.WikifolioDecisionMakingAssignment(this.wikifolioGuid),
                                             new StatementBuilder.WikifolioDescription(this.wikifolioGuid),
                                             new StatementBuilder.WikifolioInvestmentUniverseAssignment(this.wikifolioGuid),
                                             new StatementBuilder.WikifolioRankingValues(this.wikifolioGuid),
                                             new StatementBuilder.WikifolioTagAssignment(this.wikifolioGuid),
                                             new StatementBuilder.WikifolioStatusHistory(this.wikifolioGuid),
                                             new StatementBuilder.WikifolioCountryLicence(this.wikifolioGuid),
                                             new StatementBuilder.WikifolioCashAccountTransaction(this.wikifolioGuid),
                                             new StatementBuilder.WikifolioMessage(this.wikifolioGuid)
                                         };
            if (withTicks)
            {
                this.StatementBuilders.Add(new StatementBuilder.WikifolioTickDataAggregated(this.wikifolioGuid));
            }
            if (withFees)
            {
                this.StatementBuilders.Add(new StatementBuilder.WikifolioFee(this.wikifolioGuid));
            }
        }

        private List<IStatementBuilder> StatementBuilders { get; set; }

        public void WriteInserts(TextWriter writer)
        {
            foreach (var statementBuilder in this.StatementBuilders)
            {
                this.WriteInsertsWithStatementBuilder(writer, statementBuilder);
            }
        }

        private void WriteInsertsWithStatementBuilder(TextWriter writer, IStatementBuilder statementBuilder)
        {
            writer.WriteLine($"-- INSERTS FOR: {statementBuilder.Identifier()}");
            this.ReadAndReplace(writer, statementBuilder);
            writer.WriteLine(string.Empty);
        }

        private void ReadAndReplace(TextWriter writer, IStatementBuilder statementBuilder)
        {
            foreach (var row in new SqlDataReader().GetRows(statementBuilder))
            {
                writer.WriteLine(new Replacer(row, statementBuilder.InsertStatement()).GetFinalOutput());
            }
        }
    }
}
