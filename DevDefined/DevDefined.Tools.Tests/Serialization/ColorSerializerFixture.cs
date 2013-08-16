using System.Drawing;
using DevDefined.Tools.Serialization;
using NUnit.Framework;

namespace DevDefined.Tools.Tests.Serialization
{
    [TestFixture]
    public class ColorSerializerFixture
    {
        [Test]
        public void SerializeArgbColor()
        {
            Color color = Color.FromArgb(255, 128, 64, 32);

            string encodedColor = ColorSerializer.SerializeColor(color);

            Assert.AreEqual("ARGBColor:255:128:64:32", encodedColor);

            Color color2 = ColorSerializer.DeserializeColor(encodedColor);

            Assert.AreEqual(color, color2);
        }

        [Test]
        public void SerializeNamedColor()
        {
            Color color = Color.DodgerBlue;

            string encodedColor = ColorSerializer.SerializeColor(color);

            Assert.AreEqual("NamedColor:DodgerBlue", encodedColor);

            Color color2 = ColorSerializer.DeserializeColor(encodedColor);

            Assert.AreEqual(color, color2);
        }
    }
}