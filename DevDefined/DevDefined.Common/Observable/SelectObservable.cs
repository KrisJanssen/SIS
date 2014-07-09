// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectObservable.cs" company="">
//   
// </copyright>
// <summary>
//   The select observable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The select observable.
    /// </summary>
    /// <typeparam name="TInput">
    /// </typeparam>
    /// <typeparam name="TOutput">
    /// </typeparam>
    public class SelectObservable<TInput, TOutput> : IObservable<TOutput>
    {
        #region Fields

        /// <summary>
        /// The _inner observable.
        /// </summary>
        private readonly IObservable<TInput> _innerObservable;

        /// <summary>
        /// The _selector.
        /// </summary>
        private readonly Func<TInput, TOutput> _selector;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectObservable{TInput,TOutput}"/> class.
        /// </summary>
        /// <param name="innerObservable">
        /// The inner observable.
        /// </param>
        /// <param name="selector">
        /// The selector.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public SelectObservable(IObservable<TInput> innerObservable, Func<TInput, TOutput> selector)
        {
            if (innerObservable == null)
            {
                throw new ArgumentNullException("innerObservable");
            }

            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            this._innerObservable = innerObservable;
            this._selector = selector;
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
        public IDisposable Subscribe(IObserver<TOutput> observer)
        {
            return this._innerObservable.Subscribe(new SelectObserver<TInput, TOutput>(observer, this._selector));
        }

        #endregion
    }
}