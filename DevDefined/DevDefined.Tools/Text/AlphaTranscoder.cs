// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlphaTranscoder.cs" company="">
//   
// </copyright>
// <summary>
//   Construct this class with a string containing 32 unique characters (preferrably just numbers and letters)
//   and you can then use it to convert between a number of bytes and those characters, useful when creating
//   things such as license key strings which contain an encoded payload of information.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Tools.Text
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Construct this class with a string containing 32 unique characters (preferrably just numbers and letters)
    /// and you can then use it to convert between a number of bytes and those characters, useful when creating
    /// things such as license key strings which contain an encoded payload of information.
    /// </summary>
    public class AlphaTranscoder
    {
        #region Constants

        /// <summary>
        /// The default key space.
        /// </summary>
        private const string DefaultKeySpace = "ABFNX5JCKTDL4HRS0UEMW712YGP98Z63";

        #endregion

        #region Fields

        /// <summary>
        /// The _key space.
        /// </summary>
        private readonly string _keySpace;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlphaTranscoder"/> class.
        /// </summary>
        public AlphaTranscoder()
            : this(DefaultKeySpace)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlphaTranscoder"/> class.
        /// </summary>
        /// <param name="keySpace">
        /// The key space.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public AlphaTranscoder(string keySpace)
        {
            if (keySpace.Length != 0x20)
            {
                throw new ArgumentOutOfRangeException(
                    "keySpace", 
                    "The keyspace string must contain exactly 32 characters");
            }

            this._keySpace = keySpace;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The convert to alphas.
        /// </summary>
        /// <param name="sourceBytes">
        /// The source bytes.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<char> ConvertToAlphas(IEnumerable<byte> sourceBytes)
        {
            foreach (byte sourceByte in sourceBytes)
            {
                yield return this.GetAlphaForValue(sourceByte);
            }
        }

        /// <summary>
        /// The convert to bytes.
        /// </summary>
        /// <param name="sourceCharacters">
        /// The source characters.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<byte> ConvertToBytes(IEnumerable<char> sourceCharacters)
        {
            foreach (char sourceCharacter in sourceCharacters)
            {
                yield return this.GetValueForAlpha(sourceCharacter);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get alpha for value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="char"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        private char GetAlphaForValue(byte value)
        {
            if (value >= 0x20)
            {
                throw new ArgumentOutOfRangeException("value", value, "The byte value must be less then 32");
            }

            return this._keySpace[value];
        }

        /// <summary>
        /// The get value for alpha.
        /// </summary>
        /// <param name="alphaNumeric">
        /// The alpha numeric.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte GetValueForAlpha(char alphaNumeric)
        {
            for (byte offset = 0; offset < 0x20; offset++)
            {
                if (this._keySpace[offset] == alphaNumeric)
                {
                    return offset;
                }
            }

            return byte.MaxValue;
        }

        #endregion
    }
}