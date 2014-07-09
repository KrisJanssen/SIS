// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VolatileByteArray.cs" company="">
//   
// </copyright>
// <summary>
//   Possible states of the Array of bytes
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    /// <summary>
    /// Possible states of the Array of bytes
    /// </summary>
    public enum VolatileByteArrayState
    {
        /// <summary>
        /// The ok.
        /// </summary>
        Ok, 

        /// <summary>
        /// The reading.
        /// </summary>
        Reading, 

        /// <summary>
        /// The writing.
        /// </summary>
        Writing
    }

    /// <summary>
    /// A little convenience class that allows a byte array to be written to and
    /// read from like a stream.
    /// 
    /// Also provides some very basic locking semantics to ensure it can't be read
    /// while being written / dirty.
    /// </summary>
    [Serializable]
    public class VolatileByteArray
    {
        #region Fields

        /// <summary>
        /// The _array.
        /// </summary>
        private byte[] _array;

        /// <summary>
        /// The _state.
        /// </summary>
        private VolatileByteArrayState _state;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VolatileByteArray"/> class.
        /// </summary>
        public VolatileByteArray()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VolatileByteArray"/> class.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        public VolatileByteArray(byte[] array)
        {
            this._array = array;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the array.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public byte[] Array
        {
            get
            {
                this.AssertAccessible();
                return this._array;
            }

            set
            {
                this.AssertAccessible();
                this._array = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The as readable stream.
        /// </summary>
        /// <returns>
        /// The <see cref="NotifyingMemoryStream"/>.
        /// </returns>
        public NotifyingMemoryStream AsReadableStream()
        {
            this.AssertReadable();
            var stream = new NotifyingMemoryStream(this._array);
            stream.Closed += this.stream_Closed;
            this._state = VolatileByteArrayState.Reading;
            return stream;
        }

        /// <summary>
        /// The as writable stream.
        /// </summary>
        /// <returns>
        /// The <see cref="NotifyingMemoryStream"/>.
        /// </returns>
        public NotifyingMemoryStream AsWritableStream()
        {
            this.AssertAccessible();
            var stream = new NotifyingMemoryStream();
            stream.Closed += this.stream_Closed;
            this._state = VolatileByteArrayState.Writing;
            return stream;
        }

        /// <summary>
        /// The open reader.
        /// </summary>
        /// <returns>
        /// The <see cref="TextReader"/>.
        /// </returns>
        public TextReader OpenReader()
        {
            return new StreamReader(this.AsReadableStream());
        }

        /// <summary>
        /// The open writer.
        /// </summary>
        /// <returns>
        /// The <see cref="TextWriter"/>.
        /// </returns>
        public TextWriter OpenWriter()
        {
            return new StreamWriter(this.AsWritableStream());
        }

        #endregion

        #region Methods

        /// <summary>
        /// The assert accessible.
        /// </summary>
        /// <exception cref="Exception">
        /// </exception>
        protected void AssertAccessible()
        {
            if (this._state != VolatileByteArrayState.Ok)
            {
                throw new Exception("The byte array is being overwritten");
            }
        }

        /// <summary>
        /// The assert readable.
        /// </summary>
        /// <exception cref="Exception">
        /// </exception>
        protected void AssertReadable()
        {
            this.AssertAccessible();
            if (this._array == null)
            {
                throw new Exception("The byte array is NULL and can't be read");
            }
        }

        /// <summary>
        /// The stream_ closed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void stream_Closed(object sender, EventArgs e)
        {
            if (this._state == VolatileByteArrayState.Writing)
            {
                this._array = (sender as NotifyingMemoryStream).ToArray();
            }

            this._state = VolatileByteArrayState.Ok;
        }

        #endregion
    }
}