// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LastObservable.cs" company="">
//   
// </copyright>
// <summary>
//   The last observable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The last observable.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class LastObservable<T> : IObservable<T>
    {
        #region Fields

        /// <summary>
        /// The _inner observable.
        /// </summary>
        private readonly IObservable<T> _innerObservable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LastObservable{T}"/> class.
        /// </summary>
        /// <param name="innerObservable">
        /// The inner observable.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public LastObservable(IObservable<T> innerObservable)
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
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return this._innerObservable.Subscribe(new LastObserver<T>(observer));
        }

        #endregion
    }
}