using DevDefined.Common.Dsl;
using NUnit.Framework;

namespace DevDefined.Common.Tests.Dsl
{
    [TestFixture]
    public class DslExtensionsTests
    {
        [Test]
        public void IgnoreBatchSupport()
        {
            Batch batch = delegate { return null; };
            Assert.IsFalse(batch.IsIgnored());
            batch.Ignore();
            Assert.IsTrue(batch.IsIgnored());
        }
    }
}