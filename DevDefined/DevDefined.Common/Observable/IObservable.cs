// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IObservable.cs" company="">
//   
// </copyright>
// <summary>
//   The Observable interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The Observable interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IObservable<T>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The subscribe.
        /// </summary>
        /// <param name="observer">
        /// The observer.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        IDisposable Subscribe(IObserver<T> observer);

        #endregion
    }
}