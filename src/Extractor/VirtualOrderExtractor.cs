using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Extractor.StatementBuilder;

namespace Extractor
{
    public class VirtualOrderExtractor
    {
        private readonly BasicExtractor extractor;

        public VirtualOrderExtractor(Guid wikifolioGuid, int limitOfVirtualOrderGroups = 50, bool createUnderlyings = true, bool createPhoneOrders = false)
        {
            var virtualOrderStatementBuilder =
                new StatementBuilder.VirtualOrder(wikifolioGuid, limitOfVirtualOrderGroups);
            var statementBuilders = new List<IStatementBuilder>() { virtualOrderStatementBuilder };
            if (createUnderlyings)
            {
                foreach (var underlyingIsin in this.ReadUnderlyings(virtualOrderStatementBuilder))
                {
                    statementBuilders.Add(new StatementBuilder.Underlying(underlyingIsin));
                }
            }

            if (createPhoneOrders)
            {
                throw new NotImplementedException("Phone Order creation not yet implemented");
            }

            this.extractor = new BasicExtractor(statementBuilders);
        }

        public void WriteInserts(TextWriter writer)
        {
            this.extractor.WriteInserts(writer);
        }

        private IEnumerable<string> ReadUnderlyings(StatementBuilder.VirtualOrder virtualOrderStatementBuilder)
        {
            var sqlReader = new SqlDataReader();
            foreach(var row in sqlReader.GetRows(virtualOrderStatementBuilder.GetUnderlyings()))
            {
                yield return (string)row[row.Keys.First()];
            }
        }
    }
}