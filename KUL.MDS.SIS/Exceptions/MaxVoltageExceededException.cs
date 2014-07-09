// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaxVoltageExceededException.cs" company="">
//   
// </copyright>
// <summary>
//   This Exception will get thrown when the requested voltage exceeds the global maximum.
//   TODO: Make this implementation fully compliant with MSDN directives.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Exceptions
{
    /// <summary>
    /// This Exception will get thrown when the requested voltage exceeds the global maximum.
    /// TODO: Make this implementation fully compliant with MSDN directives.
    /// </summary>
    public class MaxVoltageExceededException : System.Exception
    {
        // The constructor that takes msg as a parameter
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxVoltageExceededException"/> class.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        public MaxVoltageExceededException(string msg)
            : base(msg)
        {
            // Nothing Special should be done for now.
        }

        #endregion
    }
}