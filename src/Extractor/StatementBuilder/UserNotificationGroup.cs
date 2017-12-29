using System;

namespace Extractor.StatementBuilder
{
    public class UserNotificationGroup : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{UserId}"),
                new Placeholder("{NotificationGroupId}"),
                new Placeholder("{Subscribed}")
            };

        private readonly Guid userGuid;

        public UserNotificationGroup(Guid userGuid)
        {
            this.userGuid = userGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "UserNotificationGroup";

        public override string GetCondition() => $"[UserId] = '{this.userGuid}'";
    }
}