using System;
using System.Collections.Generic;

using Extractor.StatementBuilder;

namespace Extractor.BuildingPlans
{
    public class WikifolioBuildingPlan : IBuildingPlan
    {
        private readonly Guid wikifolioGuid;
        private readonly Guid userGuid;

        private readonly bool withTicks;
        private readonly bool withFees;
        private readonly bool withItems;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wikifolioGuid"></param>
        /// <param name="existingUserGuid">That will be used for Insert Statements</param>
        public WikifolioBuildingPlan(Guid wikifolioGuid, Guid existingUserGuid, bool withTicks, bool withFees, bool withItems)
        {
            this.wikifolioGuid = wikifolioGuid;
            this.userGuid = existingUserGuid;
            this.withTicks = withTicks;
            this.withFees = withFees;
            this.withItems = withItems;
        }

        public IList<IStatementBuilder> GetStatementBuilders()
        {
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
            if (this.withTicks)
            {
                statementBuilders.Add(new StatementBuilder.WikifolioTickDataAggregated(this.wikifolioGuid));
            }
            if (this.withFees)
            {
                statementBuilders.Add(new StatementBuilder.WikifolioFee(this.wikifolioGuid));
            }
            if (this.withItems)
            {
                var wikifolioItemBuildingPlan = new WikifolioItemBuildingPlan(this.wikifolioGuid);
                statementBuilders.AddRange(wikifolioItemBuildingPlan.GetStatementBuilders());
            }
            // TODO: WikifolioTransaction
            return statementBuilders;
        }
    }
}