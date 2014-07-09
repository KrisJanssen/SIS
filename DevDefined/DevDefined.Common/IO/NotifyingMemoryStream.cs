// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotifyingMemoryStream.cs" company="">
//   
// </copyright>
// <summary>
//   The notifying memory stream.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.IO
{
    using System.IO;

    /// <summary>
    /// The notifying memory stream.
    /// </summary>
    public class NotifyingMemoryStream : NotifyingStream
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyingMemoryStream"/> class.
        /// </summary>
        public NotifyingMemoryStream()
            : base(new MemoryStream())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyingMemoryStream"/> class.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        public NotifyingMemoryStream(byte[] buffer)
            : base(new MemoryStream(buffer))
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The to array.
        /// </summary>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public byte[] ToArray()
        {
            return ((MemoryStream)this.InnerStream).ToArray();
        }

        #endregion
    }
}