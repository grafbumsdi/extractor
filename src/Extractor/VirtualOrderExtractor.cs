using System;
using System.Collections.Generic;
using System.IO;

using Extractor.StatementBuilder;

namespace Extractor
{
    public class VirtualOrderExtractor
    {
        private readonly BasicExtractor extractor;

        public VirtualOrderExtractor(Guid wikifolioGuid, int limitOfVirtualOrderGroups = 50, bool createUnderlyings = true, bool createPhoneOrders = false)
        {
            var statementBuilders =
                new List<IStatementBuilder>()
                    {
                        new StatementBuilder.VirtualOrder(
                            wikifolioGuid,
                            limitOfVirtualOrderGroups)
                    };
            if (createUnderlyings)
            {

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
    }
}