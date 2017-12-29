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
        private readonly BasicExtractor extractor;

        public WikifolioExtractor(Guid wikifolioGuid)
        {
            throw new NotImplementedException("wikifolio creation with user creation not implemented yet");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wikifolioGuid"></param>
        /// <param name="existingUserGuid">That will be used for Insert Statements</param>
        public WikifolioExtractor(Guid wikifolioGuid, Guid existingUserGuid, bool withTicks, bool withFees, bool withItems)
        {
            this.wikifolioGuid = wikifolioGuid;
            this.userGuid = existingUserGuid;
            var statementBuilders = new List<IStatementBuilder>()
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
                statementBuilders.Add(new StatementBuilder.WikifolioTickDataAggregated(this.wikifolioGuid));
            }
            if (withFees)
            {
                statementBuilders.Add(new StatementBuilder.WikifolioFee(this.wikifolioGuid));
            }
            if (withItems)
            {
                var wikifolioItemExtractor = new WikifolioItemExtractor(this.wikifolioGuid);
                statementBuilders.AddRange(wikifolioItemExtractor.GetStatementBuilders());
            }
            // TODO: WikifolioTransaction
            this.extractor = new BasicExtractor(statementBuilders);
        }

        public void WriteInserts(TextWriter writer)
        {
            this.extractor.WriteInserts(writer);
        }
    }
}
