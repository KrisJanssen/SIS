// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitSink.cs" company="">
//   
// </copyright>
// <summary>
//   Bit sink is a rather inefficient class for converting types to the corresponding bits, and allows
//   for bit-level transformations over multi-byte arrays.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Tools.Serialization
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Bit sink is a rather inefficient class for converting types to the corresponding bits, and allows
    /// for bit-level transformations over multi-byte arrays.
    /// </summary>
    public class BitSink
    {
        #region Fields

        /// <summary>
        /// The _bits.
        /// </summary>
        private readonly Queue<byte> _bits = new Queue<byte>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this._bits.Count <= 0;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add bit.
        /// </summary>
        /// <param name="bit">
        /// The bit.
        /// </param>
        public void AddBit(bool bit)
        {
            this._bits.Enqueue(bit ? (byte)1 : (byte)0);
        }

        /// <summary>
        /// The add bit.
        /// </summary>
        /// <param name="bit">
        /// The bit.
        /// </param>
        public void AddBit(byte bit)
        {
            this.AddBit(bit != 0);
        }

        /// <summary>
        /// The add bits.
        /// </summary>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <param name="bits">
        /// The bits.
        /// </param>
        public void AddBits(byte b, int bits)
        {
            for (int i = 0; i < bits; i++)
            {
                int val = 1 << i;
                AddBit((b & val) == val);
            }
        }

        /// <summary>
        /// The add byte.
        /// </summary>
        /// <param name="b">
        /// The b.
        /// </param>
        public void AddByte(byte b)
        {
            this.AddBits(b, 8);
        }

        /// <summary>
        /// The add bytes.
        /// </summary>
        /// <param name="bytes">
        /// The bytes.
        /// </param>
        public void AddBytes(byte[] bytes)
        {
            foreach (byte b in bytes)
            {
                this.AddByte(b);
            }
        }

        /// <summary>
        /// The add bytes.
        /// </summary>
        /// <param name="bytes">
        /// The bytes.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        public void AddBytes(byte[] bytes, int length)
        {
            for (int i = 0; i < length; i++)
            {
                this.AddByte(bytes[i]);
            }
        }

        /// <summary>
        /// The can read.
        /// </summary>
        /// <param name="bits">
        /// The bits.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CanRead(int bits)
        {
            return this._bits.Count >= bits;
        }

        /// <summary>
        /// The peek bit.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool PeekBit(int position)
        {
            return this._bits.ToArray()[position] != 0;
        }

        /// <summary>
        /// The read.
        /// </summary>
        /// <param name="bits">
        /// The bits.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        public byte Read(int bits)
        {
            byte b = 0;
            for (int i = 0; i < bits; i++)
            {
                var addition = (byte)(this._bits.Dequeue() << i);
                b = (byte)(b | addition);
            }

            return b;
        }

        /// <summary>
        /// The read all bytes.
        /// </summary>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public byte[] ReadAllBytes()
        {
            return this.ReadAllBytes(8);
        }

        /// <summary>
        /// The read all bytes.
        /// </summary>
        /// <param name="bitSize">
        /// The bit size.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public byte[] ReadAllBytes(int bitSize)
        {
            var bytes = new List<byte>();
            while (this.CanRead(8))
            {
                bytes.Add(this.Read(8));
            }

            if (!this.IsEmpty)
            {
                bytes.Add(this.ReadRemaining());
            }

            return bytes.ToArray();
        }

        /// <summary>
        /// The read remaining.
        /// </summary>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public byte ReadRemaining()
        {
            if (this._bits.Count > 8)
            {
                throw new InvalidOperationException("Can not read remaining bits when there are more then 8");
            }

            return this.Read(this._bits.Count);
        }

        #endregion
    }
}