using System.Collections.Generic;
using System.Text;
using DevDefined.Tools.Serialization;

namespace DevDefined.Tools.Text
{
    public static class CharacterUtils
    {
        private const int DefaultCharactersPerDash = 5;

        public static string ToString(IEnumerable<char> sourceCharacters)
        {
            var builder = new StringBuilder();
            foreach (char ch in sourceCharacters) builder.Append(ch);
            return builder.ToString();
        }

        public static byte[] ToArray(IEnumerable<byte> sourceBytes)
        {
            var buffer = new List<byte>();

            foreach (byte sourceByte in sourceBytes)
            {
                buffer.Add(sourceByte);
            }

            return buffer.ToArray();
        }

        public static IEnumerable<char> FilterOutDashes(IEnumerable<char> sourceCharacters)
        {
            foreach (char ch in sourceCharacters)
                if (ch != '-') yield return ch;
        }

        public static IEnumerable<char> InsertDashes(IEnumerable<char> sourceCharacters)
        {
            return InsertDashes(sourceCharacters, DefaultCharactersPerDash);
        }

        public static IEnumerable<char> InsertDashes(IEnumerable<char> sourceCharacters, int charactersPerDash)
        {
            int count = 0;

            foreach (char ch in sourceCharacters)
            {
                if ((count > 0) && ((count%charactersPerDash) == 0)) yield return '-';
                yield return ch;
                count++;
            }
        }

        public static IEnumerable<byte> PackBytes(IEnumerable<byte> sourceBytes, int bits)
        {
            var sink = new BitSink();

            foreach (byte sourceByte in sourceBytes)
            {
                sink.AddBits(sourceByte, bits);

                while (sink.CanRead(8))
                {
                    yield return sink.Read(8);
                }
            }

            if (!sink.IsEmpty) yield return sink.ReadRemaining();
        }

        public static IEnumerable<byte> UnpackBytes(IEnumerable<byte> sourceBytes, int bits)
        {
            return UnpackBytes(sourceBytes, bits, true);
        }

        public static IEnumerable<byte> UnpackBytes(IEnumerable<byte> sourceBytes, int bits, bool discardRemainder)
        {
            var sink = new BitSink();

            foreach (byte sourceByte in sourceBytes)
            {
                sink.AddByte(sourceByte);

                while (sink.CanRead(bits))
                {
                    yield return sink.Read(bits);
                }
            }

            if (!(sink.IsEmpty || discardRemainder))
                yield return sink.ReadRemaining();
        }
    }
}