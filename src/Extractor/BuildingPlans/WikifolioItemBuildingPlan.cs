using System;
using System.Collections.Generic;
using System.Linq;

using Extractor.StatementBuilder;

namespace Extractor.BuildingPlans
{
    public class WikifolioItemBuildingPlan : IBuildingPlan
    {
        private readonly IList<IStatementBuilder> statementBuilders;

        public WikifolioItemBuildingPlan(Guid wikifolioGuid, bool createUnderlyings = true)
        {
            var wikifolioItemStatementBuilder =
                new StatementBuilder.WikifolioItem(wikifolioGuid);
            this.statementBuilders = new List<IStatementBuilder>() { wikifolioItemStatementBuilder };
            if (createUnderlyings)
            {
                foreach (var underlyingIsin in this.ReadUnderlyings(wikifolioItemStatementBuilder))
                {
                    this.statementBuilders.Insert(0, new StatementBuilder.Underlying(underlyingIsin));
                }
            }
        }

        // TODO: avoid this readunderlyings in here. do it somewhere else
        private IEnumerable<string> ReadUnderlyings(StatementBuilder.WikifolioItem wikifolioItemStatementBuilder)
        {
            var sqlReader = new SqlDataReader();
            foreach (var row in sqlReader.GetRows(wikifolioItemStatementBuilder.GetUnderlyingsQueryStatement()).Distinct())
            {
                yield return (string)row[row.Keys.First()];
            }
        }

        public IList<IStatementBuilder> GetStatementBuilders() => this.statementBuilders;
    }
}