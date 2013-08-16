using System.Windows.Forms;
using DevDefined.Tools.Serialization;
using NUnit.Framework;

namespace DevDefined.Tools.Tests.Serialization
{
    [TestFixture]
    public class PaddingSerializerFixture
    {
        [Test]
        public void Serialize()
        {
            var padding = new Padding(10, 20, 30, 40);

            string encodedValue = PaddingSerializer.SerializePadding(padding);
            Assert.AreEqual("10,20,30,40", encodedValue);

            Padding padding2 = PaddingSerializer.DeserializePadding(encodedValue, Padding.Empty);
            Assert.AreEqual(padding, padding2);
        }
    }
}