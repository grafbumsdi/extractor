using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Extractor.StatementBuilder;

namespace Extractor
{
    public class WikifolioItemExtractor
    {
        private readonly BasicExtractor extractor;

        public WikifolioItemExtractor(Guid wikifolioGuid, bool createUnderlyings = true)
        {
            var wikifolioItemStatementBuilder =
                new StatementBuilder.WikifolioItem(wikifolioGuid);
            var statementBuilders = new List<IStatementBuilder>() { wikifolioItemStatementBuilder };
            if (createUnderlyings)
            {
                foreach (var underlyingIsin in this.ReadUnderlyings(wikifolioItemStatementBuilder))
                {
                    statementBuilders.Insert(0, new StatementBuilder.Underlying(underlyingIsin));
                }
            }
            this.extractor = new BasicExtractor(statementBuilders);
        }

        public IList<IStatementBuilder> GetStatementBuilders() => this.extractor.GetStatementBuilders();

        public void WriteInserts(TextWriter writer)
        {
            this.extractor.WriteInserts(writer);
        }

        private IEnumerable<string> ReadUnderlyings(StatementBuilder.WikifolioItem wikifolioItemStatementBuilder)
        {
            var sqlReader = new SqlDataReader();
            foreach (var row in sqlReader.GetRows(wikifolioItemStatementBuilder.GetUnderlyingsQueryStatement()).Distinct())
            {
                yield return (string)row[row.Keys.First()];
            }
        }
    }
}