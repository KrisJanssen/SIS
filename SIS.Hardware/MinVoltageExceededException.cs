// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinVoltageExceededException.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   This Exception will get thrown when the requested voltage exceeds the global minimum.
//   TODO: Make this implementation fully compliant with MSDN directives.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    /// <summary>
    /// This Exception will get thrown when the requested voltage exceeds the global minimum.
    /// TODO: Make this implementation fully compliant with MSDN directives.
    /// </summary>
    public class MinVoltageExceededException : System.Exception
    {
        // The constructor that takes msg as a parameter
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MinVoltageExceededException"/> class.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        public MinVoltageExceededException(string msg)
            : base(msg)
        {
            // Nothing Special should be done for now.
        }

        #endregion
    }
}