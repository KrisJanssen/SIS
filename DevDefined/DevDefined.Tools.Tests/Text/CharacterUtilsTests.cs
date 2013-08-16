using DevDefined.Tools.Text;
using NUnit.Framework;

namespace DevDefined.Tools.Tests.Text
{
    [TestFixture]
    public class CharacterUtilsTests
    {
        private readonly AlphaTranscoder _transcoder = new AlphaTranscoder();

        private byte[] ConvertKeyToBytes(string key)
        {
            return
                CharacterUtils.ToArray(
                    CharacterUtils.PackBytes(_transcoder.ConvertToBytes(CharacterUtils.FilterOutDashes(key)), 5));
        }

        private string ConvertBytesToKey(byte[] bytes)
        {
            return
                CharacterUtils.ToString(
                    CharacterUtils.InsertDashes(_transcoder.ConvertToAlphas(CharacterUtils.UnpackBytes(bytes, 5))));
        }

        [Test]
        public void ConvertBytesToKey()
        {
            var bytes = new byte[16];
            for (int i = 0; i < 13; i++) bytes[i] = (byte) i;
            Assert.AreEqual("AKAX0-B0A50-BRAXX-BDYFY-AAAAA", ConvertBytesToKey(bytes));
        }

        [Test]
        public void ConvertKeyToBytes()
        {
            byte[] bytes = ConvertKeyToBytes("ABCD-ABCDE-ABCDE-ABCDE-ABCDE");
            Assert.AreEqual(15, bytes.Length);
        }

        [Test]
        public void FilterOutDashes()
        {
            string output = CharacterUtils.ToString(CharacterUtils.FilterOutDashes("ABCDE-ABCDE-ABCDE-ABCDE"));
            Assert.AreEqual("ABCDEABCDEABCDEABCDE", output);
        }

        [Test]
        public void InsertDashes()
        {
            string output = CharacterUtils.ToString(CharacterUtils.InsertDashes("ABCDEABCDEABCDEABCDEABCDE"));
            Assert.AreEqual("ABCDE-ABCDE-ABCDE-ABCDE-ABCDE", output);
        }

        [Test]
        public void UnpackAndPack3Bits()
        {
            var input = new byte[] {0xFF, 0xF, 0xCC, 3};
            byte[] result = CharacterUtils.ToArray(CharacterUtils.PackBytes(CharacterUtils.UnpackBytes(input, 3), 3));
            Assert.AreEqual(input[0], result[0]);
            Assert.AreEqual(input[1], result[1]);
            Assert.AreEqual(input[2], result[2]);
            Assert.AreEqual(input[3], result[3]);
        }

        [Test]
        public void UnpackAndPackBytes()
        {
            var input = new byte[] {0xFF, 0xF, 0xCC, 3};
            byte[] result = CharacterUtils.ToArray(CharacterUtils.PackBytes(CharacterUtils.UnpackBytes(input, 8), 8));
            Assert.AreEqual(input[0], result[0]);
            Assert.AreEqual(input[1], result[1]);
            Assert.AreEqual(input[2], result[2]);
            Assert.AreEqual(input[3], result[3]);
        }
    }
}