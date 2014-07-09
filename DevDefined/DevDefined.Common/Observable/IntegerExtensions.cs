// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntegerExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The integer extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The integer extensions.
    /// </summary>
    public static class IntegerExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The milliseconds.
        /// </summary>
        /// <param name="milliseconds">
        /// The milliseconds.
        /// </param>
        /// <returns>
        /// The <see cref="TimeSpan"/>.
        /// </returns>
        public static TimeSpan Milliseconds(this int milliseconds)
        {
            return new TimeSpan(0, 0, 0, 0, milliseconds);
        }

        #endregion
    }
}