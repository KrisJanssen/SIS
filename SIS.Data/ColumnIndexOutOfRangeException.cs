// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnIndexOutOfRangeException.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The column index out of range exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Data
{
    /// <summary>
    /// The column index out of range exception.
    /// </summary>
    internal class ColumnIndexOutOfRangeException : System.Exception
    {
        // The constructor that takes msg as a parameter
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnIndexOutOfRangeException"/> class.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        public ColumnIndexOutOfRangeException(string msg)
            : base(msg)
        {
            // Nothing Special should be done for now.
        }

        #endregion
    }
}