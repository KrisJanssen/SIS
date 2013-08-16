using System;
using System.Collections.Generic;

namespace DevDefined.Tools.Serialization
{
    /// <summary>
    /// Bit sink is a rather inefficient class for converting types to the corresponding bits, and allows
    /// for bit-level transformations over multi-byte arrays.
    /// </summary>
    public class BitSink
    {
        private readonly Queue<byte> _bits = new Queue<byte>();

        public bool IsEmpty
        {
            get { return _bits.Count <= 0; }
        }

        public void AddByte(Byte b)
        {
            AddBits(b, 8);
        }

        public void AddBytes(Byte[] bytes)
        {
            foreach (Byte b in bytes) AddByte(b);
        }

        public void AddBytes(Byte[] bytes, int length)
        {
            for (int i = 0; i < length; i++)
            {
                AddByte(bytes[i]);
            }
        }

        public void AddBits(Byte b, int bits)
        {
            for (int i = 0; i < bits; i++)
            {
                int val = (1 << i);
                AddBit((b & val) == val);
            }
        }

        public void AddBit(bool bit)
        {
            _bits.Enqueue(bit ? (byte) 1 : (byte) 0);
        }

        public void AddBit(Byte bit)
        {
            AddBit(bit != 0);
        }

        public bool CanRead(int bits)
        {
            return _bits.Count >= bits;
        }

        public byte Read(int bits)
        {
            byte b = 0;
            for (int i = 0; i < bits; i++)
            {
                var addition = (byte) (_bits.Dequeue() << i);
                b = (byte) (b | addition);
            }

            return b;
        }

        public byte ReadRemaining()
        {
            if (_bits.Count > 8)
                throw new InvalidOperationException("Can not read remaining bits when there are more then 8");

            return Read(_bits.Count);
        }

        public bool PeekBit(int position)
        {
            return (_bits.ToArray()[position] != 0);
        }

        public byte[] ReadAllBytes()
        {
            return ReadAllBytes(8);
        }

        public byte[] ReadAllBytes(int bitSize)
        {
            var bytes = new List<byte>();
            while (CanRead(8))
            {
                bytes.Add(Read(8));
            }
            if (!IsEmpty) bytes.Add(ReadRemaining());

            return bytes.ToArray();
        }
    }
}