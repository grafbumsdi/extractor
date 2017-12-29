using System;
using System.IO;
using System.Linq;

namespace Extractor
{
    public class WikifolioWithUserExtractor
    {
        private readonly UserExtractor userExtractor;

        private readonly WikifolioExtractor wikifolioExtractor;

        public WikifolioWithUserExtractor(Guid wikifolioGuid, bool withTicks, bool withFees, bool withItems)
        {
            var userGuid = this.GetWikifolioEditorGuid(wikifolioGuid);
            this.userExtractor = new UserExtractor(userGuid);
            this.wikifolioExtractor = new WikifolioExtractor(wikifolioGuid, userGuid, withTicks, withFees, withItems);
        }

        public void WriteInserts(TextWriter writer)
        {
            this.userExtractor.WriteInserts(writer);
            this.wikifolioExtractor.WriteInserts(writer);
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
    }
}