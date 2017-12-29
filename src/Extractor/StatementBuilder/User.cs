using System;

namespace Extractor.StatementBuilder
{
    public class User : DefaultStatementBuilder, IStatementBuilder
    {
        private static readonly Placeholder[] Flds =
            {
                new Placeholder("{ID}"),
                new Placeholder("{Firstname}"),
                new Placeholder("{Lastname}"),
                new Placeholder("{Address1}"),
                new Placeholder("{Address2}"),
                new Placeholder("{Zipcode}"),
                new Placeholder("{City}"),
                new Placeholder("{Country}"),
                new Placeholder("{Gender}"),
                new Placeholder("{DateOfBirth}"),
                new Placeholder("{Email}"),
                new Placeholder("{Telephone}"),
                new Placeholder("{Nickname}"),
                new Placeholder("{Password}"),
                new Placeholder("{PasswordReminderQuestion}"),
                new Placeholder("{PasswortReminderAnswer}"),
                new Placeholder("{Status}"),
                new Placeholder("{NewsletterAccepted}"),
                new Placeholder("{LastLoginDate:RELATIVEDATETIME}"),
                new Placeholder("{EditorProfile}"),
                new Placeholder("{UserActivationReminderEmailToBeSent}"),
                new Placeholder("{IsAgent}"),
                new Placeholder("{Legitimized}"),
                new Placeholder("{LegitimationPaperLocation}"),
                new Placeholder("{PriorityLogin}"),
                new Placeholder("{CompanyName}"),
                // TODO: is binary field new Placeholder("{ProfileLogo}"),
                new Placeholder("{UserType}"),
                new Placeholder("{LastContactDate:RELATIVEDATETIME}"),
                new Placeholder("{LegitimizationEmailSent}"),
                new Placeholder("{IssuingProcessPreparationEmailSent}"),
                new Placeholder("{RiskNotificationEnabled}"),
                new Placeholder("{Language}"),
                new Placeholder("{NoIndex}"),
                new Placeholder("{PromotionID}"),
                new Placeholder("{PreferredCountry}"),
                new Placeholder("{Referral}"),
                new Placeholder("{DidInitialWatchlisting}"),
                new Placeholder("{IsFollower}"),
                new Placeholder("{SiteCountryMode}"),
                new Placeholder("{SiteLanguageMode}"),
                new Placeholder("{TraderLifecyclePosition}"),
                new Placeholder("{CustomerLifecyclePosition}"),
                new Placeholder("{HideDashboardMyWikifolios}"),
                new Placeholder("{SendTradingReminder}")
            };

        private readonly Guid userGuid;

        public User(Guid userGuid)
        {
            this.userGuid = userGuid;
        }

        public override Placeholder[] Fields() => Flds;

        public override string TableIdentifier() => "User";

        public override string GetCondition() => $"[ID] = '{this.userGuid}'";
    }
}