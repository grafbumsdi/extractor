using System;
using System.Collections.Generic;

using Extractor.StatementBuilder;

namespace Extractor.BuildingPlans
{
    public class UserBuildingPlan : IBuildingPlan
    {
        private readonly Guid userGuid;

        public UserBuildingPlan(Guid userGuid)
        {
            this.userGuid = userGuid;
        }

        public IList<IStatementBuilder> GetStatementBuilders()
        {
            var userStatementBuilder = new StatementBuilder.User(this.userGuid);
            var statementBuilders = new List<IStatementBuilder>()
                                        {
                                            userStatementBuilder,
                                            new StatementBuilder.UserTradingExperience(this.userGuid),
                                            new StatementBuilder.UserNotificationGroup(this.userGuid)
                                        };
            return statementBuilders;
        }
    }
}