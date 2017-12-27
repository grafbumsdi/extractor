using System;

using Extractor;
using Extractor.PlaceholderOptions;

using NUnit.Framework;

namespace ExtractorTests
{
    [TestFixture]
    public class PlaceholderTests
    {
        [Test]
        public void PlaceholderThreeOptions()
        {
            var placeHolder = new Placeholder("{gaxi:DATETIME:RELATIVEDATETIME:RELATIVEDATETIMEUTC}");
            var options = placeHolder.Options;
            Assert.AreEqual(3, options.Count);
            Assert.AreEqual("DATETIME", options[0].GetIdentifier());
            Assert.AreEqual("RELATIVEDATETIME", options[1].GetIdentifier());
            Assert.AreEqual("RELATIVEDATETIMEUTC", options[2].GetIdentifier());
            Assert.IsTrue(placeHolder.IsDateTime);
            Assert.IsTrue(placeHolder.IsRelativeDateTime);
            Assert.IsTrue(placeHolder.IsRelativeDateTimeUtc);
        }

        [Test]
        public void PlaceholderInvalidOptions()
        {
            Assert.Throws<InvalidOperationException>(() => new Placeholder("{gaxi:OPTION1:OPTION2:ODATETIME}"));
        }

        [Test]
        public void PlaceholderOneOptions()
        {
            var placeHolder = new Placeholder("{gaxi:DATETIME}");
            var options = placeHolder.Options;
            Assert.AreEqual(1, options.Count);
            Assert.AreEqual("DATETIME", options[0].GetIdentifier());
            Assert.IsTrue(placeHolder.IsDateTime);
        }

        [Test]
        public void PlaceholderNoOptions()
        {
            var placeHolder = new Placeholder("{gaxiDATETIMEOPTION1OPTION2}");
            var options = placeHolder.Options;
            Assert.AreEqual(0, options.Count);
            Assert.IsFalse(placeHolder.IsDateTime);
        }

        [Test]
        public void PlaceholderOptionsRelativeDateTimeUtc()
        {
            var placeHolder = new Placeholder("{gaxiDATETIMEOPTION1OPTION2:RELATIVEDATETIMEUTC}");
            var options = placeHolder.Options;
            Assert.AreEqual(1, options.Count);
            Assert.IsFalse(placeHolder.IsDateTime);
            Assert.IsFalse(placeHolder.IsRelativeDateTime);
            Assert.IsTrue(placeHolder.IsRelativeDateTimeUtc);
        }

        [Test]
        public void PlaceholderConstructorWithOption()
        {
            var placeHolder = new Placeholder("Identifier", new DateTimeOption());
            var options = placeHolder.Options;
            Assert.AreEqual(1, options.Count);
            Assert.IsTrue(placeHolder.IsDateTime);
            Assert.IsFalse(placeHolder.IsRelativeDateTime);
            Assert.AreEqual("{Identifier:DATETIME}", placeHolder.ExactPlaceHolderWithBrackets);
            Assert.AreEqual("Identifier", placeHolder.ValueIdentifier);
        }
    }
}