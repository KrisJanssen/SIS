// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractObserverDecorator.cs" company="">
//   
// </copyright>
// <summary>
//   The abstract observer decorator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The abstract observer decorator.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class AbstractObserverDecorator<T> : IObserver<T>
    {
        #region Fields

        /// <summary>
        /// The _inner observer.
        /// </summary>
        protected readonly IObserver<T> _innerObserver;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractObserverDecorator{T}"/> class.
        /// </summary>
        /// <param name="innerObserver">
        /// The inner observer.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        protected AbstractObserverDecorator(IObserver<T> innerObserver)
        {
            if (innerObserver == null)
            {
                throw new ArgumentNullException("innerObserver");
            }

            this._innerObserver = innerObserver;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The on done.
        /// </summary>
        public void OnDone()
        {
            this._innerObserver.OnDone();
        }

        /// <summary>
        /// The on exception.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public void OnException(Exception ex)
        {
            this._innerObserver.OnException(ex);
        }

        /// <summary>
        /// The on next.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public abstract void OnNext(T item);

        #endregion
    }
}