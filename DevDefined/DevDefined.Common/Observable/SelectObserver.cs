// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectObserver.cs" company="">
//   
// </copyright>
// <summary>
//   The select observer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The select observer.
    /// </summary>
    /// <typeparam name="TInput">
    /// </typeparam>
    /// <typeparam name="TOutput">
    /// </typeparam>
    public class SelectObserver<TInput, TOutput> : IObserver<TInput>
    {
        #region Fields

        /// <summary>
        /// The _inner observer.
        /// </summary>
        private readonly IObserver<TOutput> _innerObserver;

        /// <summary>
        /// The _selector.
        /// </summary>
        private readonly Func<TInput, TOutput> _selector;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectObserver{TInput,TOutput}"/> class.
        /// </summary>
        /// <param name="innerObserver">
        /// The inner observer.
        /// </param>
        /// <param name="selector">
        /// The selector.
        /// </param>
        public SelectObserver(IObserver<TOutput> innerObserver, Func<TInput, TOutput> selector)
        {
            this._innerObserver = innerObserver;
            this._selector = selector;
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
        public void OnNext(TInput item)
        {
            this._innerObserver.OnNext(this._selector(item));
        }

        #endregion
    }
}