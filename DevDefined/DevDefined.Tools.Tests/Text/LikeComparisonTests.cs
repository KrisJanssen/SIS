using DevDefined.Tools.Text;
using NUnit.Framework;

namespace DevDefined.Tools.Tests.Text
{
    [TestFixture]
    public class LikeComparisonTests
    {
        [Test]
        public void TestBackwardsCharSetRangeCaseInsensitive()
        {
            var comparison = new LikeComparison("[n-a]rg", true);
            Assert.IsTrue(comparison.Compare("arg"));
            Assert.IsTrue(comparison.Compare("brg"));
            Assert.IsTrue(comparison.Compare("frg"));
            Assert.IsTrue(comparison.Compare("grg"));
            Assert.IsFalse(comparison.Compare("org"));
            Assert.IsTrue(comparison.Compare("ARG"));
            Assert.IsFalse(comparison.Compare("FAIL"));
            Assert.IsFalse(comparison.Compare("zarg"));
        }

        [Test]
        public void TestBasicExpressionCaseInsensitive()
        {
            var comparison = new LikeComparison("TEST", true);
            Assert.IsTrue(comparison.Compare("TEST"));
            Assert.IsTrue(comparison.Compare("test"));
            Assert.IsTrue(comparison.Compare("TeSt"));
            Assert.IsFalse(comparison.Compare("FAIL"));
            Assert.IsFalse(comparison.Compare("TEST1"));
        }

        [Test]
        public void TestBasicExpressionCaseSensitive()
        {
            var comparison = new LikeComparison("TEST", false);
            Assert.IsTrue(comparison.Compare("TEST"));
            Assert.IsFalse(comparison.Compare("test"));
            Assert.IsFalse(comparison.Compare("TeSt"));
            Assert.IsFalse(comparison.Compare("FAIL"));
            Assert.IsFalse(comparison.Compare("TEST1"));
        }

        [Test]
        public void TestCharSetCaseInsensitive()
        {
            var comparison = new LikeComparison("[fbc]an", true);
            Assert.IsTrue(comparison.Compare("fan"));
            Assert.IsTrue(comparison.Compare("ban"));
            Assert.IsTrue(comparison.Compare("can"));
            Assert.IsTrue(comparison.Compare("FAN"));
            Assert.IsTrue(comparison.Compare("BAN"));
            Assert.IsTrue(comparison.Compare("CAN"));
            Assert.IsFalse(comparison.Compare("FAIL"));
            Assert.IsFalse(comparison.Compare("zan"));
            Assert.IsFalse(comparison.Compare("canada"));
        }

        [Test]
        public void TestCharSetCaseSensitive()
        {
            var comparison = new LikeComparison("[fbc]an", false);
            Assert.IsTrue(comparison.Compare("fan"));
            Assert.IsTrue(comparison.Compare("ban"));
            Assert.IsTrue(comparison.Compare("can"));
            Assert.IsFalse(comparison.Compare("FAN"));
            Assert.IsFalse(comparison.Compare("BAN"));
            Assert.IsFalse(comparison.Compare("CAN"));
            Assert.IsFalse(comparison.Compare("FAIL"));
            Assert.IsFalse(comparison.Compare("zan"));
            Assert.IsFalse(comparison.Compare("canada"));
            var comparison2 = new LikeComparison("[fBc]an", false);
            Assert.IsTrue(comparison2.Compare("Ban"));
            Assert.IsFalse(comparison2.Compare("ban"));
            Assert.IsTrue(comparison2.Compare("can"));
            Assert.IsFalse(comparison2.Compare("Can"));
        }

        [Test]
        public void TestCharSetRangeCaseInsensitive()
        {
            var comparison = new LikeComparison("[a-n]rg", true);
            Assert.IsTrue(comparison.Compare("arg"));
            Assert.IsTrue(comparison.Compare("brg"));
            Assert.IsTrue(comparison.Compare("frg"));
            Assert.IsTrue(comparison.Compare("grg"));
            Assert.IsFalse(comparison.Compare("org"));
            Assert.IsTrue(comparison.Compare("ARG"));
            Assert.IsFalse(comparison.Compare("FAIL"));
            Assert.IsFalse(comparison.Compare("zarg"));
        }

        [Test]
        public void TestCharSetRangesPlusSinglesCaseInsensitive()
        {
            var comparison = new LikeComparison("[acfn-pz-t]rg", true);
            string passChars = "acfnoptuvwxyz";
            for (char c = 'a'; c <= 'z'; c++)
            {
                string stmt = "" + c + "rg";
                string stmt2 = "" + char.ToUpper(c) + "rG";
                if (passChars.IndexOf(c) < 0)
                {
                    Assert.IsFalse(comparison.Compare(stmt), stmt);
                    Assert.IsFalse(comparison.Compare(stmt2), stmt2);
                }
                else
                {
                    Assert.IsTrue(comparison.Compare(stmt), stmt);
                    Assert.IsTrue(comparison.Compare(stmt2), stmt2);
                }
            }
            Assert.IsFalse(comparison.Compare("FAIL"));
        }

        [Test]
        public void TestCharSetRangeStartAndEndsWithRangeMarkerCaseInsensitive()
        {
            var comparison = new LikeComparison("test[-z-]", true);
            // it should pass for these scenarios (note that the range is counted
            // as a normal character, and so an allowable value in the set)
            Assert.IsTrue(comparison.Compare("test-"));
            Assert.IsTrue(comparison.Compare("TESTZ"));
            Assert.IsTrue(comparison.Compare("testZ"));

            // we should fail for all these
            Assert.IsFalse(comparison.Compare("test[-z-]"));
            Assert.IsFalse(comparison.Compare("test-z"));
            Assert.IsFalse(comparison.Compare("test-z-"));
            Assert.IsTrue(comparison.Compare("testz"));
            Assert.IsFalse(comparison.Compare("testzssszzz!"));
        }

        [Test]
        public void TestClassicWildCardTermInStringCaseInsensitive()
        {
            var comparison = new LikeComparison("%sonic%", true);
            Assert.IsTrue(comparison.Compare("sonic youth"));
            Assert.IsTrue(comparison.Compare("when sonic youth arrive"));
            Assert.IsTrue(comparison.Compare("the hedgehogs name was sonic"));
            Assert.IsFalse(comparison.Compare("s0nic"));
            Assert.IsFalse(comparison.Compare("s o n i c"));
        }

        [Test]
        public void TestComplexExpression()
        {
            // an expression we make more and more complex, to see if there's any issues with all
            // the expressions playing nicely together.	 Not a catch all so much as a sanity check			
            var comparison = new LikeComparison("a \\% b_[f-hi-k]", '\\', false);
            Assert.IsTrue(comparison.Compare("a % big"));
            comparison = new LikeComparison("a \\% b_[f-hi-k]%_dog", '\\', false);
            Assert.IsTrue(comparison.Compare("a % big dog"));
            Assert.IsTrue(comparison.Compare("a % big black dog"));
            Assert.IsFalse(comparison.Compare("a % big black cat"));
            comparison = new LikeComparison("a \\% b_[f-hi-k]%_dog_is not a \\_cat", '\\', false);
            Assert.IsTrue(comparison.Compare("a % big dog is not a _cat"));
            Assert.IsTrue(comparison.Compare("a % big black dog is not a _cat"));
            Assert.IsFalse(comparison.Compare("a % big black cat is not a _cat"));
            Assert.IsFalse(comparison.Compare("a % big black dog is not a _dog"));
            comparison = new LikeComparison("a \\% b_[f-hi-k]%_dog_is not a \\_cat%not a [^az-nr-c-]@t", '\\', false);
            Assert.IsTrue(comparison.Compare("a % big dog is not a _cat and not a b@t"));
            comparison = new LikeComparison("a \\% b_[f-hi-k]%_dog_is not a \\_cat%not a [nb][@a][tT]", '\\', false);
            Assert.IsTrue(comparison.Compare("a % big dog is not a _cat and not a b@t"));
            Assert.IsTrue(comparison.Compare("a % big dog is not a _cat and not a b@T"));
            Assert.IsTrue(comparison.Compare("a % big dog is not a _cat and not a baT"));
            Assert.IsTrue(comparison.Compare("a % big dog is not a _cat and not a bat"));
            Assert.IsTrue(comparison.Compare("a % big dog is not a _cat and not a nat"));
            Assert.IsFalse(comparison.Compare("a % big dog is not a _cat and not a cat"));
            Assert.IsFalse(comparison.Compare("a % big dog is not a _cat and not a b@tter"));
        }

        [Test]
        public void TestNotInCharSetRangesPlusSinglesCaseInsensitive()
        {
            var comparison = new LikeComparison("[^acfn-pz-t]rg", true);
            string failChars = "acfnoptuvwxyz";
            for (char c = 'a'; c <= 'z'; c++)
            {
                string stmt = "" + c + "rg";
                string stmt2 = "" + char.ToUpper(c) + "rG";
                if (failChars.IndexOf(c) < 0)
                {
                    Assert.IsTrue(comparison.Compare(stmt), stmt);
                    Assert.IsTrue(comparison.Compare(stmt2), stmt2);
                }
                else
                {
                    Assert.IsFalse(comparison.Compare(stmt), stmt);
                    Assert.IsFalse(comparison.Compare(stmt2), stmt2);
                }
            }
            Assert.IsFalse(comparison.Compare("FAIL"));
        }

        [Test]
        public void TestSingleCharacterAndWildCardWorkProperly()
        {
            // we implement a normalising feature to ensure that a single character
            // following a wild card doesn't give funny answers (because we only evaluate
            // the next atom for a match, as opposed to the whole of the remaining expression...
            // if you disable the normalising function in the LikeComparison class you should see
            // this test fail on the first comparison..
            var comparison = new LikeComparison("test%_end", true);
            Assert.IsTrue(comparison.Compare("test_my_end"));
            // will fail here if you dont call LikeComparison.NormaliseWildCards from the Initialise function.
            Assert.IsTrue(comparison.Compare("test_end"));
            Assert.IsFalse(comparison.Compare("testend"));
            Assert.IsTrue(comparison.Compare("test end"));
            Assert.IsFalse(comparison.Compare("testending"));
            Assert.IsTrue(comparison.Compare("test                       END"));
        }

        [Test]
        public void TestSingleCharacterWildCaseInsensitive()
        {
            var comparison = new LikeComparison("te_t", true);
            for (char c = 'a'; c <= 'z'; c++)
            {
                // this is the only pass scenario we expect
                Assert.IsTrue(comparison.Compare("te" + c + "t"));
                // and some likely trip-up ones we want to ensure aren't returning false positives...
                Assert.IsFalse(comparison.Compare("ta" + c + "t"));
                Assert.IsFalse(comparison.Compare("te" + c + "s"));
                Assert.IsFalse(comparison.Compare("te" + c + "tz"));
            }
        }

        [Test]
        public void TestWildcardExpressionCaseInsensitive()
        {
            var comparison = new LikeComparison("TE%", true);
            Assert.IsTrue(comparison.Compare("TE"));
            Assert.IsTrue(comparison.Compare("TEST"));
            Assert.IsTrue(comparison.Compare("test"));
            Assert.IsTrue(comparison.Compare("TeSt"));
            Assert.IsFalse(comparison.Compare("FAIL"));
            Assert.IsTrue(comparison.Compare("TEST1"));
            Assert.IsFalse(comparison.Compare("TAKE"));
            Assert.IsFalse(comparison.Compare(" TAKE"));
        }

        [Test]
        public void TestWildcardExpressionCaseSensitive()
        {
            var comparison = new LikeComparison("TE%", false);
            Assert.IsTrue(comparison.Compare("TE"));
            Assert.IsTrue(comparison.Compare("TEST"));
            Assert.IsFalse(comparison.Compare("test"));
            Assert.IsFalse(comparison.Compare("TeSt"));
            Assert.IsTrue(comparison.Compare("TEst"));
            Assert.IsFalse(comparison.Compare("FAIL"));
            Assert.IsTrue(comparison.Compare("TEST1"));
            Assert.IsFalse(comparison.Compare("TAKE"));
            Assert.IsFalse(comparison.Compare(" TAKE"));
        }

        [Test]
        public void TestWildCardMatchesEmptyStringAndOtherValuesButNotNull()
        {
            // we used to let null values compare to a single wildcard as being a a valid "pass"
            // however, after testing the same thing in sql 2000 I've decided it would be a deviation
            // which could only cause to confuse... 
            // ie.
            //
            //	select * from
            //	(
            //		select 1 jid, convert(varchar,null) zid
            //	) n
            //	where n.zid like '%'
            //
            // returns 0 rows, so we now fail the last assertion to ensure the LikeComparison is compliant with the sql
            // expectations.
            var comparison = new LikeComparison("%", true);
            Assert.IsTrue(comparison.Compare(string.Empty));
            Assert.IsTrue(comparison.Compare("PASS"));
            Assert.IsTrue(comparison.Compare("%"));
            Assert.IsTrue(comparison.Compare(" "));
            Assert.IsFalse(comparison.Compare(null));
        }
    }
}