// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IObserver.cs" company="">
//   
// </copyright>
// <summary>
//   The Observer interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The Observer interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IObserver<T>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The on done.
        /// </summary>
        void OnDone();

        /// <summary>
        /// The on exception.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        void OnException(Exception ex);

        /// <summary>
        /// The on next.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        void OnNext(T item);

        #endregion
    }
}