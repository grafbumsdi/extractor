using System;
using System.Collections.Generic;

using Extractor;

using NUnit.Framework;

namespace ExtractorTests
{
    [TestFixture]
    public class ReplacerTests
    {
        private const string FreeText = "free text ' :option DateTIME anything { $%&$} wer";
        private const string FreeTextPreparedForInsert = "free text '' :option DateTIME anything { $%&$} wer";

        private const string FreeTextWithLineBreak = @"free text with line break
gaxi

uije";

        private const string FreeTextWithLineBreakPreparedForInsert =
            "free text with line break' + CHAR(13) + CHAR(10) + 'gaxi'"
            + " + CHAR(13) + CHAR(10) + '' + CHAR(13) + CHAR(10) + 'uije";

        private static readonly DateTime TestDateTime = new DateTime(2017, 11, 1, 14, 13, 02).AddMilliseconds(89);

        private static readonly Dictionary<string, object> DefaultDictionary =
            new Dictionary<string, object>()
                {
                    { "gaxi", null }, { "dateNull", null }, { "date", TestDateTime }, { "freeText", FreeText },
                    { "freeTextWithLineBreak", FreeTextWithLineBreak},
                    { "DBNull", DBNull.Value}, { "dateWithFractionalMilliseconds", TestDateTime.AddTicks(23) }
                };

        private int DiffToTestTimeInDays => (int)(DateTime.Now.Date - TestDateTime.Date).TotalDays;

        [Test]
        public void GetPlaceholdersNoMatches()
        {
            var replacer = new Replacer(DefaultDictionary, "INSERT INTO Wikifolio () VALUES ()");
            Assert.AreEqual(0, replacer.GetPlaceholders().Count);
        }

        [Test]
        public void GetPlaceholdersOneMatch()
        {
            var replacer = new Replacer(DefaultDictionary, "INSERT INTO Wikifolio () VALUES ({gaxi})");
            var placeHolders = replacer.GetPlaceholders();
            Assert.AreEqual(1, placeHolders.Count);
            Assert.AreEqual("{gaxi}", placeHolders[0].ExactPlaceHolderWithBrackets);
        }

        [Test]
        public void GetPlaceholdersMultipleMatches()
        {
            var replacer = new Replacer(DefaultDictionary, "INSERT INTO Wikifolio () VALUES ({gaxi},{haxi},{gaxi},{delay},{gaxi})");
            var placeHolders = replacer.GetPlaceholders();
            Assert.AreEqual(5, placeHolders.Count);
            Assert.AreEqual("{haxi}", placeHolders[1].ExactPlaceHolderWithBrackets);
            Assert.AreEqual("{gaxi}", placeHolders[4].ExactPlaceHolderWithBrackets);
        }

        [Test]
        public void GetPlaceholdersMultipleMatchesWithInvalidOptions()
        {
            var replacer = new Replacer(DefaultDictionary, "INSERT INTO Wikifolio () VALUES ({gaxi:123},{haxi:43},{gaxi},{delay},{gaxi})");
            Assert.Throws<InvalidOperationException>(() => replacer.GetPlaceholders());
        }

        [Test]
        public void GetFinalOutputOneMatch()
        {
            var replacer = new Replacer(DefaultDictionary, "FreeText: {freeText}");
            Assert.AreEqual($"FreeText: '{FreeTextPreparedForInsert}'", replacer.GetFinalOutput());
        }

        [Test]
        public void GetFinalOutputOneNullMatch()
        {
            var replacer = new Replacer(DefaultDictionary, "INSERT INTO Wikifolio () VALUES ({gaxi})");
            Assert.AreEqual("INSERT INTO Wikifolio () VALUES (NULL)", replacer.GetFinalOutput());
        }

        [Test]
        public void GetFinalOutputMultipleMatches()
        {
            var replacer = new Replacer(DefaultDictionary, "INSERT INTO Wikifolio () VALUES ({gaxi}, {gaxi})");
            Assert.AreEqual("INSERT INTO Wikifolio () VALUES (NULL, NULL)", replacer.GetFinalOutput());
        }

        [Test]
        public void GetFinalOutputDateMatches()
        {
            var replacer = new Replacer(DefaultDictionary, "haha: {date}");
            Assert.AreEqual("haha: '01.11.2017 14:13:02'", replacer.GetFinalOutput());
        }

        [Test]
        public void GetFinalOutputDateMatchesDateTime()
        {
            var replacer = new Replacer(DefaultDictionary, "SELECT {date:DATETIME}");
            Assert.AreEqual("SELECT '20171101 14:13:02.089'", replacer.GetFinalOutput());
        }

        [Test]
        public void GetFinalOutputDateMatchesRelativeDateTime()
        {
            var replacer = new Replacer(DefaultDictionary, "SELECT {date:RELATIVEDATETIME}");
            Assert.AreEqual(
                $"SELECT DATEADD(MILLISECOND, 51182089, DATEADD(DAY, DATEDIFF(DAY, {this.DiffToTestTimeInDays}, GETDATE()), 0))",
                replacer.GetFinalOutput());
        }

        [Test]
        public void GetFinalOutputDateMatchesRelativeDateTimeWithFractialMilliseconds()
        {
            var replacer = new Replacer(DefaultDictionary, "SELECT {dateWithFractionalMilliseconds:RELATIVEDATETIME}");
            Assert.AreEqual(
                $"SELECT DATEADD(MILLISECOND, 51182089, DATEADD(DAY, DATEDIFF(DAY, {this.DiffToTestTimeInDays}, GETDATE()), 0))",
                replacer.GetFinalOutput());
        }

        [Test]
        public void GetFinalOutputDateMatchesRelativeDateTimeUtc()
        {
            var replacer = new Replacer(DefaultDictionary, "SELECT {date:RELATIVEDATETIMEUTC}");
            Assert.AreEqual(
                $"SELECT DATEADD(MILLISECOND, 51182089, DATEADD(DAY, DATEDIFF(DAY, {this.DiffToTestTimeInDays}, GETUTCDATE()), 0))",
                replacer.GetFinalOutput());
        }

        [Test]
        public void GetFinalOutputDateMatchesRelativeDateTimeUtcWithFractialMilliseconds()
        {
            var replacer = new Replacer(DefaultDictionary, "SELECT {dateWithFractionalMilliseconds:RELATIVEDATETIMEUTC}");
            Assert.AreEqual(
                $"SELECT DATEADD(MILLISECOND, 51182089, DATEADD(DAY, DATEDIFF(DAY, {this.DiffToTestTimeInDays}, GETUTCDATE()), 0))",
                replacer.GetFinalOutput());
        }

        [Test]
        public void GetFinalOutputDbNull()
        {
            var replacer = new Replacer(DefaultDictionary, "SELECT {DBNull}, {DBNull:DATETIME}, {DBNull:RELATIVEDATETIME}, {DBNull:RELATIVEDATETIMEUTC}");
            Assert.AreEqual(
                $"SELECT NULL, NULL, NULL, NULL",
                replacer.GetFinalOutput());
        }

        [Test]
        public void GetFinalOutputFreeTextWithLineBreak()
        {
            var replacer = new Replacer(DefaultDictionary, "SELECT {freeTextWithLineBreak}");
            Assert.AreEqual(
                $"SELECT '{FreeTextWithLineBreakPreparedForInsert}'",
                replacer.GetFinalOutput());
        }
    }
}