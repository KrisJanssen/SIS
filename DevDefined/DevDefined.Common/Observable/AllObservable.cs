// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AllObservable.cs" company="">
//   
// </copyright>
// <summary>
//   The all observable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Observable
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The all observable.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class AllObservable<T> : IObservable<IEnumerable<T>>
    {
        #region Fields

        /// <summary>
        /// The _inner observable.
        /// </summary>
        private readonly IObservable<T> _innerObservable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AllObservable{T}"/> class.
        /// </summary>
        /// <param name="innerObservable">
        /// The inner observable.
        /// </param>
        public AllObservable(IObservable<T> innerObservable)
        {
            this._innerObservable = innerObservable;
        }

        #endregion

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
        public IDisposable Subscribe(IObserver<IEnumerable<T>> observer)
        {
            return this._innerObservable.Subscribe(new AllObserver<T>(observer));
        }

        #endregion
    }
}