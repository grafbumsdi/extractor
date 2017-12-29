using System;

namespace Extractor.StatementBuilder
{
    public class UserTradingExperience : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{User}"),
                new Placeholder("{RiskClass}"),
                new Placeholder("{TradingExperienceTime}"),
                new Placeholder("{TradingExperienceNumberOfTrades}")
            };

        private readonly Guid userGuid;

        public UserTradingExperience(Guid userGuid)
        {
            this.userGuid = userGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "UserTradingExperience";

        public override string GetCondition() => $"[User] = '{this.userGuid}'";
    }
}
