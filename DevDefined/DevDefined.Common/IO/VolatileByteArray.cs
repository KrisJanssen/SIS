using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace DevDefined.Common.IO
{
    /// <summary>
    /// Possible states of the Array of bytes
    /// </summary>
    public enum VolatileByteArrayState
    {
        Ok,
        Reading,
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
        private byte[] _array;
        private VolatileByteArrayState _state;

        public VolatileByteArray()
        {
        }

        public VolatileByteArray(byte[] array)
        {
            _array = array;
        }

        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public byte[] Array
        {
            get
            {
                AssertAccessible();
                return _array;
            }
            set
            {
                AssertAccessible();
                _array = value;
            }
        }

        public NotifyingMemoryStream AsReadableStream()
        {
            AssertReadable();
            var stream = new NotifyingMemoryStream(_array);
            stream.Closed += stream_Closed;
            _state = VolatileByteArrayState.Reading;
            return stream;
        }

        public NotifyingMemoryStream AsWritableStream()
        {
            AssertAccessible();
            var stream = new NotifyingMemoryStream();
            stream.Closed += stream_Closed;
            _state = VolatileByteArrayState.Writing;
            return stream;
        }

        public TextReader OpenReader()
        {
            return new StreamReader(AsReadableStream());
        }

        public TextWriter OpenWriter()
        {
            return new StreamWriter(AsWritableStream());
        }

        private void stream_Closed(object sender, EventArgs e)
        {
            if (_state == VolatileByteArrayState.Writing)
                _array = (sender as NotifyingMemoryStream).ToArray();
            _state = VolatileByteArrayState.Ok;
        }

        protected void AssertAccessible()
        {
            if (_state != VolatileByteArrayState.Ok) throw new Exception("The byte array is being overwritten");
        }

        protected void AssertReadable()
        {
            AssertAccessible();
            if (_array == null) throw new Exception("The byte array is NULL and can't be read");
        }
    }
}