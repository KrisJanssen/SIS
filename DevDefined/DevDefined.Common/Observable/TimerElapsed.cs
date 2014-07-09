// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimerElapsed.cs" company="">
//   
// </copyright>
// <summary>
//   The timer elapsed.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The timer elapsed.
    /// </summary>
    public class TimerElapsed
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerElapsed"/> class.
        /// </summary>
        /// <param name="time">
        /// The time.
        /// </param>
        public TimerElapsed(DateTime time)
        {
            this.Time = time;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the time.
        /// </summary>
        public DateTime Time { get; private set; }

        #endregion
    }
}