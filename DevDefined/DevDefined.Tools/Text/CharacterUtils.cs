// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterUtils.cs" company="">
//   
// </copyright>
// <summary>
//   The character utils.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Tools.Text
{
    using System.Collections.Generic;
    using System.Text;

    using DevDefined.Tools.Serialization;

    /// <summary>
    /// The character utils.
    /// </summary>
    public static class CharacterUtils
    {
        #region Constants

        /// <summary>
        /// The default characters per dash.
        /// </summary>
        private const int DefaultCharactersPerDash = 5;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The filter out dashes.
        /// </summary>
        /// <param name="sourceCharacters">
        /// The source characters.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<char> FilterOutDashes(IEnumerable<char> sourceCharacters)
        {
            foreach (char ch in sourceCharacters)
            {
                if (ch != '-')
                {
                    yield return ch;
                }
            }
        }

        /// <summary>
        /// The insert dashes.
        /// </summary>
        /// <param name="sourceCharacters">
        /// The source characters.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<char> InsertDashes(IEnumerable<char> sourceCharacters)
        {
            return InsertDashes(sourceCharacters, DefaultCharactersPerDash);
        }

        /// <summary>
        /// The insert dashes.
        /// </summary>
        /// <param name="sourceCharacters">
        /// The source characters.
        /// </param>
        /// <param name="charactersPerDash">
        /// The characters per dash.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<char> InsertDashes(IEnumerable<char> sourceCharacters, int charactersPerDash)
        {
            int count = 0;

            foreach (char ch in sourceCharacters)
            {
                if ((count > 0) && ((count % charactersPerDash) == 0))
                {
                    yield return '-';
                }

                yield return ch;
                count++;
            }
        }

        /// <summary>
        /// The pack bytes.
        /// </summary>
        /// <param name="sourceBytes">
        /// The source bytes.
        /// </param>
        /// <param name="bits">
        /// The bits.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
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

            if (!sink.IsEmpty)
            {
                yield return sink.ReadRemaining();
            }
        }

        /// <summary>
        /// The to array.
        /// </summary>
        /// <param name="sourceBytes">
        /// The source bytes.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public static byte[] ToArray(IEnumerable<byte> sourceBytes)
        {
            var buffer = new List<byte>();

            foreach (byte sourceByte in sourceBytes)
            {
                buffer.Add(sourceByte);
            }

            return buffer.ToArray();
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="sourceCharacters">
        /// The source characters.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToString(IEnumerable<char> sourceCharacters)
        {
            var builder = new StringBuilder();
            foreach (char ch in sourceCharacters)
            {
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// The unpack bytes.
        /// </summary>
        /// <param name="sourceBytes">
        /// The source bytes.
        /// </param>
        /// <param name="bits">
        /// The bits.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<byte> UnpackBytes(IEnumerable<byte> sourceBytes, int bits)
        {
            return UnpackBytes(sourceBytes, bits, true);
        }

        /// <summary>
        /// The unpack bytes.
        /// </summary>
        /// <param name="sourceBytes">
        /// The source bytes.
        /// </param>
        /// <param name="bits">
        /// The bits.
        /// </param>
        /// <param name="discardRemainder">
        /// The discard remainder.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
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
            {
                yield return sink.ReadRemaining();
            }
        }

        #endregion
    }
}