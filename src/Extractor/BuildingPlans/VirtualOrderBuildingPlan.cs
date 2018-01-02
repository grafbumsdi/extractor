using System;
using System.Collections.Generic;
using System.Linq;

using Extractor.StatementBuilder;

namespace Extractor.BuildingPlans
{
    public class VirtualOrderBuildingPlan : IBuildingPlan
    {
        private readonly IList<IStatementBuilder> statementBuilders;

        public VirtualOrderBuildingPlan(Guid wikifolioGuid, int limitOfVirtualOrderGroups = 50, bool createUnderlyings = true, bool createPhoneOrders = false)
        {
            var virtualOrderStatementBuilder =
                new StatementBuilder.VirtualOrder(wikifolioGuid, limitOfVirtualOrderGroups);
            this.statementBuilders = new List<IStatementBuilder>() { virtualOrderStatementBuilder };
            if (createUnderlyings)
            {
                foreach (var underlyingIsin in this.ReadUnderlyings(virtualOrderStatementBuilder))
                {
                    this.statementBuilders.Insert(0, new StatementBuilder.Underlying(underlyingIsin));
                }
            }

            if (createPhoneOrders)
            {
                throw new NotImplementedException("Phone Order creation not yet implemented");
            }
        }

        // TODO: avoid this readunderlyings in here. do it somewhere else
        private IEnumerable<string> ReadUnderlyings(StatementBuilder.VirtualOrder virtualOrderStatementBuilder)
        {
            var sqlReader = new SqlDataReader();
            foreach (var row in sqlReader.GetRows(virtualOrderStatementBuilder.GetUnderlyingsQueryStatement()).Distinct())
            {
                yield return (string)row[row.Keys.First()];
            }
        }

        public IList<IStatementBuilder> GetStatementBuilders() => this.statementBuilders;
    }
}