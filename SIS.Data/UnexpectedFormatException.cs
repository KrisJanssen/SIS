// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnexpectedFormatException.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The unexpected format exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Data
{
    using System;

    /// <summary>
    /// The unexpected format exception.
    /// </summary>
    internal class UnexpectedFormatException : Exception
    {
        // The constructor that takes msg as a parameter
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedFormatException"/> class.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        public UnexpectedFormatException(string msg)
            : base(msg)
        {
            // Nothing Special should be done for now.
        }

        #endregion
    }
}