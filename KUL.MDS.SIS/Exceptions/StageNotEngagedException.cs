// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StageNotEngagedException.cs" company="">
//   
// </copyright>
// <summary>
//   This Exception will get thrown when the stage is not engaged properly when it should have been.
//   TODO: Make this implementation fully compliant with MSDN directives.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Exceptions
{
    /// <summary>
    /// This Exception will get thrown when the stage is not engaged properly when it should have been.
    /// TODO: Make this implementation fully compliant with MSDN directives.
    /// </summary>
    public class StageNotEngagedException : System.Exception
    {
        // The constructor that takes msg as a parameter
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StageNotEngagedException"/> class.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        public StageNotEngagedException(string msg)
            : base(msg)
        {
            // Nothing Special should be done for now.
        }

        #endregion
    }
}