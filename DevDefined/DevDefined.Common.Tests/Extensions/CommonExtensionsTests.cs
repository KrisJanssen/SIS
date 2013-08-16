using System.Collections.Generic;
using DevDefined.Common.Extensions;
using NUnit.Framework;

namespace DevDefined.Common.Tests.Extensions
{
    [TestFixture]
    public class CommonExtensionsTests
    {
        [Test]
        public void LoopTo()
        {
            10.LoopTo(20)(i => i.PrintLine());
        }

        [Test]
        public void ToProjectedDictionaryOfLists()
        {
            var postTag = new[]
                              {
                                  new {PostUrl = "/blog/firstpost/", Tag = ".Net"},
                                  new {PostUrl = "/blog/firstpost/", Tag = "Tools"},
                                  new {PostUrl = "/blog/secondpost/", Tag = "Travel"}
                              };


            IDictionary<string, IList<string>> organisedByTag = postTag.ToProjectedDictionaryOfLists
                (
                pt => pt.PostUrl,
                pt => pt.Tag
                );

            Assert.AreEqual(2, organisedByTag["/blog/firstpost/"].Count);
            Assert.IsTrue(organisedByTag["/blog/firstpost/"].Contains(".Net"));
            Assert.IsTrue(organisedByTag["/blog/firstpost/"].Contains("Tools"));

            Assert.AreEqual(1, organisedByTag["/blog/secondpost/"].Count);
            Assert.IsTrue(organisedByTag["/blog/secondpost/"].Contains("Travel"));
        }
    }
}