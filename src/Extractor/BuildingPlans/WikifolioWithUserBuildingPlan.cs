using System;
using System.Collections.Generic;
using System.Linq;

using Extractor.StatementBuilder;

namespace Extractor.BuildingPlans
{
    public class WikifolioWithUserBuildingPlan : IBuildingPlan
    {
        private readonly List<IStatementBuilder> statementBuilders;

        public WikifolioWithUserBuildingPlan(Guid wikifolioGuid, bool withTicks, bool withFees, bool withItems)
        {
            var userGuid = this.GetWikifolioEditorGuid(wikifolioGuid);
            var userBuildingPlan = new UserBuildingPlan(userGuid);
            var wikifolioBuildingPlan = new WikifolioBuildingPlan(wikifolioGuid, userGuid, withTicks, withFees, withItems);

            this.statementBuilders = new List<IStatementBuilder>();
            this.statementBuilders.AddRange(userBuildingPlan.GetStatementBuilders());
            this.statementBuilders.AddRange(wikifolioBuildingPlan.GetStatementBuilders());
        }

        private Guid GetWikifolioEditorGuid(Guid wikifolioGuid)
        {
            // expecting exactly 1 row (first single) and 1 column (2nd single)
            return new SqlDataReader()
                .GetRows($"SELECT [Editor] FROM [dbo].[Wikifolio] WHERE [ID] = '{wikifolioGuid.ToString()}'")
                .Single()
                .Values
                .Select((rawValue) => new Guid((string)rawValue?.ToString()))
                .Single();
        }

        public IList<IStatementBuilder> GetStatementBuilders() => this.statementBuilders;
    }
}