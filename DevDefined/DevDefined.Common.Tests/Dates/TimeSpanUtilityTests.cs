using System;
using DevDefined.Common.Dates;
using NUnit.Framework;

namespace DevDefined.Common.Tests.Dates
{
    [TestFixture]
    public class TimeSpanUtilityTests
    {
        [Test]
        public void Ago()
        {
            // NOTE: We round up on the quarter-to mark for timespans.

            Assert.AreEqual("a second ago", TimeSpanUtility.Ago(new TimeSpan(0, 0, 1)));
            Assert.AreEqual("3 seconds ago", TimeSpanUtility.Ago(new TimeSpan(0, 0, 3)));
            Assert.AreEqual("about a minute ago", TimeSpanUtility.Ago(new TimeSpan(0, 0, 60)));
            Assert.AreEqual("about a minute ago", TimeSpanUtility.Ago(new TimeSpan(0, 1, 59)));
            Assert.AreEqual("3 minutes ago", TimeSpanUtility.Ago(new TimeSpan(0, 3, 0)));
            Assert.AreEqual("3 minutes ago", TimeSpanUtility.Ago(new TimeSpan(0, 3, 30)));
            Assert.AreEqual("about an hour ago", TimeSpanUtility.Ago(new TimeSpan(1, 0, 0)));
            Assert.AreEqual("about 2 hours ago", TimeSpanUtility.Ago(new TimeSpan(1, 59, 0)));
            Assert.AreEqual("about 2 hours ago", TimeSpanUtility.Ago(new TimeSpan(2, 0, 0)));
            Assert.AreEqual("about 3 hours ago", TimeSpanUtility.Ago(new TimeSpan(2, 59, 0)));
            Assert.AreEqual("about 3 hours ago", TimeSpanUtility.Ago(new TimeSpan(3, 0, 0)));
            Assert.AreEqual("1 day ago", TimeSpanUtility.Ago(new TimeSpan(24, 0, 0)));
            Assert.AreEqual("1 day ago", TimeSpanUtility.Ago(new TimeSpan(47, 0, 0)));
            Assert.AreEqual("2 days ago", TimeSpanUtility.Ago(new TimeSpan(48, 0, 0)));
            Assert.AreEqual("22 days ago", TimeSpanUtility.Ago(new TimeSpan(22, 0, 0, 0)));
        }

        [Test]
        public void MajorMinorDuration()
        {
            // NOTE: we don't work beyond weeks.

            Assert.AreEqual("0 seconds", TimeSpanUtility.MajorMinorDuration(new TimeSpan(0, 0, 0)));
            Assert.AreEqual("1 second", TimeSpanUtility.MajorMinorDuration(new TimeSpan(0, 0, 1)));
            Assert.AreEqual("59 seconds", TimeSpanUtility.MajorMinorDuration(new TimeSpan(0, 0, 59)));
            Assert.AreEqual("1 minute", TimeSpanUtility.MajorMinorDuration(new TimeSpan(0, 1, 0)));
            Assert.AreEqual("3 minutes", TimeSpanUtility.MajorMinorDuration(new TimeSpan(0, 3, 0)));
            Assert.AreEqual("3 minutes, 1 second", TimeSpanUtility.MajorMinorDuration(new TimeSpan(0, 3, 1)));
            Assert.AreEqual("3 minutes, 45 seconds", TimeSpanUtility.MajorMinorDuration(new TimeSpan(0, 3, 45)));
            Assert.AreEqual("1 hour", TimeSpanUtility.MajorMinorDuration(new TimeSpan(1, 0, 0)));
            Assert.AreEqual("4 hours", TimeSpanUtility.MajorMinorDuration(new TimeSpan(4, 0, 0)));
            Assert.AreEqual("4 hours, 33 minutes", TimeSpanUtility.MajorMinorDuration(new TimeSpan(4, 33, 0)));
            Assert.AreEqual("1 day", TimeSpanUtility.MajorMinorDuration(new TimeSpan(1, 0, 0, 0)));
            Assert.AreEqual("1 day, 22 hours", TimeSpanUtility.MajorMinorDuration(new TimeSpan(1, 22, 0, 0)));
            Assert.AreEqual("5 days", TimeSpanUtility.MajorMinorDuration(new TimeSpan(5, 0, 2, 3)));
            Assert.AreEqual("6 days, 10 hours", TimeSpanUtility.MajorMinorDuration(new TimeSpan(6, 10, 2, 3)));
            Assert.AreEqual("1 week", TimeSpanUtility.MajorMinorDuration(new TimeSpan(7, 0, 0, 0)));
            Assert.AreEqual("1 week, 1 day", TimeSpanUtility.MajorMinorDuration(new TimeSpan(8, 0, 0, 0)));
            Assert.AreEqual("1 week, 4 days", TimeSpanUtility.MajorMinorDuration(new TimeSpan(11, 0, 0, 0)));
            Assert.AreEqual("2 weeks", TimeSpanUtility.MajorMinorDuration(new TimeSpan(14, 0, 0, 0)));
            Assert.AreEqual("3 weeks, 2 days", TimeSpanUtility.MajorMinorDuration(new TimeSpan(23, 0, 0, 0)));
        }
    }
}