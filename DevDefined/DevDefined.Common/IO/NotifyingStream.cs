// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotifyingStream.cs" company="">
//   
// </copyright>
// <summary>
//   The notifying stream.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.IO
{
    using System;
    using System.IO;

    /// <summary>
    /// The notifying stream.
    /// </summary>
    public class NotifyingStream : Stream
    {
        #region Fields

        /// <summary>
        /// The _inner stream.
        /// </summary>
        private readonly Stream _innerStream;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyingStream"/> class.
        /// </summary>
        /// <param name="innerStream">
        /// The inner stream.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public NotifyingStream(Stream innerStream)
        {
            if (innerStream == null)
            {
                throw new ArgumentNullException("innerStream");
            }

            this._innerStream = innerStream;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The closed.
        /// </summary>
        public event EventHandler Closed;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether can read.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return this._innerStream.CanRead;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can seek.
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                return this._innerStream.CanSeek;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can write.
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return this._innerStream.CanWrite;
            }
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        public override long Length
        {
            get
            {
                return this._innerStream.Length;
            }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public override long Position
        {
            get
            {
                return this._innerStream.Position;
            }

            set
            {
                this._innerStream.Position = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the inner stream.
        /// </summary>
        protected Stream InnerStream
        {
            get
            {
                return this._innerStream;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The close.
        /// </summary>
        public override void Close()
        {
            base.Close();
            this.OnClosed();
        }

        /// <summary>
        /// The flush.
        /// </summary>
        public override void Flush()
        {
            this._innerStream.Flush();
        }

        /// <summary>
        /// The read.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return this._innerStream.Read(buffer, offset, count);
        }

        /// <summary>
        /// The seek.
        /// </summary>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <param name="origin">
        /// The origin.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return this._innerStream.Seek(offset, origin);
        }

        /// <summary>
        /// The set length.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public override void SetLength(long value)
        {
            this._innerStream.SetLength(value);
        }

        /// <summary>
        /// The write.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            this._innerStream.Write(buffer, offset, count);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._innerStream.Dispose();
            }
        }

        /// <summary>
        /// The on closed.
        /// </summary>
        protected virtual void OnClosed()
        {
            if (this.Closed != null)
            {
                this.Closed(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}