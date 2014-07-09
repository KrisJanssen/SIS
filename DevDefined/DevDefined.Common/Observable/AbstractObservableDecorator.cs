// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractObservableDecorator.cs" company="">
//   
// </copyright>
// <summary>
//   The abstract observable decorator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The abstract observable decorator.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class AbstractObservableDecorator<T> : IObservable<T>
    {
        #region Fields

        /// <summary>
        /// The _inner observable.
        /// </summary>
        protected readonly IObservable<T> _innerObservable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractObservableDecorator{T}"/> class.
        /// </summary>
        /// <param name="innerObservable">
        /// The inner observable.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        protected AbstractObservableDecorator(IObservable<T> innerObservable)
        {
            if (innerObservable == null)
            {
                throw new ArgumentNullException("innerObservable");
            }

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
        public virtual IDisposable Subscribe(IObserver<T> observer)
        {
            return this._innerObservable.Subscribe(this.DecorateObserver(observer));
        }

        #endregion

        #region Methods

        /// <summary>
        /// The decorate observer.
        /// </summary>
        /// <param name="observer">
        /// The observer.
        /// </param>
        /// <returns>
        /// The <see cref="IObserver{T}"/>.
        /// </returns>
        protected abstract IObserver<T> DecorateObserver(IObserver<T> observer);

        #endregion
    }
}