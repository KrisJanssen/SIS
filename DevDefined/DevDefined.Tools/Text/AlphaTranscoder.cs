using System;
using System.Collections.Generic;

namespace DevDefined.Tools.Text
{
    /// <summary>
    /// Construct this class with a string containing 32 unique characters (preferrably just numbers and letters)
    /// and you can then use it to convert between a number of bytes and those characters, useful when creating
    /// things such as license key strings which contain an encoded payload of information.
    /// </summary>
    public class AlphaTranscoder
    {
        private const string DefaultKeySpace = "ABFNX5JCKTDL4HRS0UEMW712YGP98Z63";
        private readonly string _keySpace;

        public AlphaTranscoder()
            : this(DefaultKeySpace)
        {
        }

        public AlphaTranscoder(string keySpace)
        {
            if (keySpace.Length != 0x20)
            {
                throw new ArgumentOutOfRangeException("keySpace",
                                                      "The keyspace string must contain exactly 32 characters");
            }

            _keySpace = keySpace;
        }

        public IEnumerable<byte> ConvertToBytes(IEnumerable<char> sourceCharacters)
        {
            foreach (char sourceCharacter in sourceCharacters)
            {
                yield return GetValueForAlpha(sourceCharacter);
            }
        }

        public IEnumerable<char> ConvertToAlphas(IEnumerable<byte> sourceBytes)
        {
            foreach (byte sourceByte in sourceBytes)
            {
                yield return GetAlphaForValue(sourceByte);
            }
        }

        private byte GetValueForAlpha(char alphaNumeric)
        {
            for (byte offset = 0; offset < 0x20; offset++)
            {
                if (_keySpace[offset] == alphaNumeric)
                {
                    return offset;
                }
            }
            return byte.MaxValue;
        }

        private char GetAlphaForValue(byte value)
        {
            if (value >= 0x20)
                throw new ArgumentOutOfRangeException("value", value, "The byte value must be less then 32");

            return _keySpace[value];
        }
    }
}