using DevDefined.Tools.Serialization;
using NUnit.Framework;

namespace DevDefined.Tools.Tests.Serialization
{
    [TestFixture]
    public class BitSinkTests
    {
        [Test]
        public void AddBytesThenReadBytes()
        {
            var sink = new BitSink();
            sink.AddByte(0xFF);
            sink.AddByte(0x00);
            sink.AddByte(0x0F);
            sink.AddByte(0xF0);
            sink.AddByte(0xFF);

            Assert.AreEqual(0xFF, sink.Read(8));
            Assert.AreEqual(0x00, sink.Read(8));
            Assert.AreEqual(0x0F, sink.Read(8));
            Assert.AreEqual(0xF0, sink.Read(8));
            Assert.AreEqual(0xFF, sink.Read(8));
        }

        [Test]
        public void AddByteThenReadNibble()
        {
            var sink = new BitSink();
            sink.AddByte(0xFE);
            Assert.IsTrue(sink.CanRead(4));
            Assert.AreEqual(0xE, sink.Read(4));
            Assert.IsTrue(sink.CanRead(4));
            Assert.AreEqual(0xF, sink.Read(4));
            Assert.IsFalse(sink.CanRead(4));
        }

        [Test]
        public void AddNiblesThenReadByte()
        {
            var sink = new BitSink();
            sink.AddBits(0xE, 4);
            sink.AddBits(0xF, 4);
            Assert.AreEqual(0xFE, sink.Read(8));
        }
    }
}