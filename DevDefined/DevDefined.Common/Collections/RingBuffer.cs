// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RingBuffer.cs" company="">
//   
// </copyright>
// <summary>
//   The ring buffer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Collections
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// The ring buffer.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class RingBuffer<T> : IEnumerable<T>
    {
        #region Fields

        /// <summary>
        /// The _size.
        /// </summary>
        private readonly int _size;

        /// <summary>
        /// The _buffer.
        /// </summary>
        private T[] _buffer;

        /// <summary>
        /// The _end.
        /// </summary>
        private int _end;

        /// <summary>
        /// The _start.
        /// </summary>
        private int _start;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RingBuffer{T}"/> class.
        /// </summary>
        /// <param name="size">
        /// The size.
        /// </param>
        public RingBuffer(int size)
        {
            this._size = size;
            this.Clear();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                return this.IsEmpty
                           ? 0
                           : ((this._start > this._end)
                                  ? ((this._buffer.Length - this._start) + this._end)
                                  : (this._end - this._start));
            }
        }

        /// <summary>
        /// Gets a value indicating whether is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this._start == this._end;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is full.
        /// </summary>
        public bool IsFull
        {
            get
            {
                return this.Count == this._size;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Add(T item)
        {
            bool full = this.IsFull;

            if (full)
            {
                this._start = (this._start + 1) % this._buffer.Length;
            }

            this._buffer[this._end] = item;

            this._end = (this._end + 1) % this._buffer.Length;

            if (full)
            {
                Debug.Assert(this.Count == this._size, "Added item to full buffer, Count should be equal to size");
            }

            Debug.Assert(this.Count <= this._size, "Count should be less than or equal to size");
        }

        /// <summary>
        /// The clear.
        /// </summary>
        public void Clear()
        {
            this._start = 0;
            this._end = 0;
            this._buffer = new T[this._size + 1];
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (this._end == this._start)
            {
                yield break;
            }

            if (this._end < this._start)
            {
                for (int i = this._start; i < this._buffer.Length; i++)
                {
                    yield return this._buffer[i];
                }

                for (int i = 0; i < this._end; i++)
                {
                    yield return this._buffer[i];
                }
            }
            else
            {
                for (int i = this._start; i < this._end; i++)
                {
                    yield return this._buffer[i];
                }
            }
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}