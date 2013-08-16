using System;
using System.IO;

namespace DevDefined.Common.IO
{
    public class NotifyingStream : Stream
    {
        private readonly Stream _innerStream;

        public NotifyingStream(Stream innerStream)
        {
            if (innerStream == null)
                throw new ArgumentNullException("innerStream");

            _innerStream = innerStream;
        }

        public override bool CanRead
        {
            get { return _innerStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _innerStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _innerStream.CanWrite; }
        }

        public override long Length
        {
            get { return _innerStream.Length; }
        }

        public override long Position
        {
            get { return _innerStream.Position; }
            set { _innerStream.Position = value; }
        }

        protected Stream InnerStream
        {
            get { return _innerStream; }
        }

        public event EventHandler Closed;

        public override void Flush()
        {
            _innerStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _innerStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _innerStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _innerStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _innerStream.Write(buffer, offset, count);
        }

        protected virtual void OnClosed()
        {
            if (Closed != null) Closed(this, EventArgs.Empty);
        }

        public override void Close()
        {
            base.Close();
            OnClosed();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _innerStream.Dispose();
            }
        }
    }
}