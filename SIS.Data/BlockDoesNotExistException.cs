// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlockDoesNotExistException.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   This Exception will get thrown when the requested block does not exist.
//   TODO: Make this implementation fully compliant with MSDN directives.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Data
{
    /// <summary>
    /// This Exception will get thrown when the requested block does not exist.
    /// TODO: Make this implementation fully compliant with MSDN directives.
    /// </summary>
    public class BlockDoesNotExistException : System.Exception
    {
        // The constructor that takes msg as a parameter
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockDoesNotExistException"/> class.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        public BlockDoesNotExistException(string msg)
            : base(msg)
        {
            // Nothing Special should be done for now.
        }

        #endregion
    }
}