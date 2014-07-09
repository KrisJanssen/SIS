// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractObservable.cs" company="">
//   
// </copyright>
// <summary>
//   The abstract observable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The abstract observable.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class AbstractObservable<T> : IObservable<T>
    {
        #region Fields

        /// <summary>
        /// The _observers.
        /// </summary>
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

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
        public IDisposable Subscribe(IObserver<T> observer)
        {
            this._observers.Add(observer);
            return new DisposableAction(() => this._observers.Remove(observer));
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on done.
        /// </summary>
        protected virtual void OnDone()
        {
            this._observers.ForEach(o => o.OnDone());
        }

        /// <summary>
        /// The on exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        protected virtual void OnException(Exception exception)
        {
            this._observers.ForEach(o => o.OnException(exception));
        }

        /// <summary>
        /// The on next.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        protected virtual void OnNext(T item)
        {
            this._observers.ForEach(o => o.OnNext(item));
        }

        #endregion
    }
}