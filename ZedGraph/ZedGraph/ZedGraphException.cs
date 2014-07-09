// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ZedGraphException.cs">
//   
// </copyright>
// <summary>
//   An exception thrown by ZedGraph.  A child class of <see cref="ApplicationException" />.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception thrown by ZedGraph.  A child class of <see cref="ApplicationException"/>.
    /// </summary>
    ///
    /// <author> Jerry Vos modified by John Champion</author>
    /// <version> $Revision: 3.2 $ $Date: 2006-06-24 20:26:44 $ </version>
    public class ZedGraphException : System.ApplicationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZedGraphException"/> class. 
        /// Initializes a new instance of the <see cref="Exception"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception.
        /// If the innerException parameter is not a null reference, the current exception is raised
        /// in a catch block that handles the inner exception.
        /// </param>
        public ZedGraphException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZedGraphException"/> class. 
        /// Initializes a new instance of the <see cref="Exception"/> class with a specified error message.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        public ZedGraphException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZedGraphException"/> class. 
        /// Initializes a new instance of the <see cref="Exception"/> class.
        /// </summary>
        public ZedGraphException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZedGraphException"/> class. 
        /// Initializes a new instance of the <see cref="ZedGraphException"/>
        /// class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="System.Runtime.Serialization.SerializationInfo"/>
        /// instance that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="System.Runtime.Serialization.StreamingContext"/>
        /// instance that contains contextual information about the source or destination.
        /// </param>
        protected ZedGraphException(
            SerializationInfo info, 
            StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}