using System;
using System.Collections.Generic;
using System.IO;

using Extractor.StatementBuilder;

namespace Extractor
{
    public class UserExtractor
    {
        private readonly Guid userGuid;
        private readonly BasicExtractor extractor;

        public UserExtractor(Guid userGuid)
        {
            this.userGuid = userGuid;
            var userStatementBuilder = new StatementBuilder.User(this.userGuid);
            var statementBuilders = new List<IStatementBuilder>()
                                        {
                                            userStatementBuilder,
                                            new StatementBuilder.UserTradingExperience(this.userGuid),
                                            new StatementBuilder.UserNotificationGroup(this.userGuid)
                                        };
            this.extractor = new BasicExtractor(statementBuilders);
        }

        public void WriteInserts(TextWriter writer)
        {
            this.extractor.WriteInserts(writer);
        }
    }
}