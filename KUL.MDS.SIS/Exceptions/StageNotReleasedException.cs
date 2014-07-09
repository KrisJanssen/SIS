// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StageNotReleasedException.cs" company="">
//   
// </copyright>
// <summary>
//   This Exception will get thrown when the stage is not released properly when it should have been.
//   TODO: Make this implementation fully compliant with MSDN directives.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Exceptions
{
    /// <summary>
    /// This Exception will get thrown when the stage is not released properly when it should have been.
    /// TODO: Make this implementation fully compliant with MSDN directives.
    /// </summary>
    public class StageNotReleasedException : System.Exception
    {
        // The constructor that takes msg as a parameter
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StageNotReleasedException"/> class.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        public StageNotReleasedException(string msg)
            : base(msg)
        {
            // Nothing Special should be done for now.
        }

        #endregion
    }
}