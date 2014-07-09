// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PeriodicObserveable.cs" company="">
//   
// </copyright>
// <summary>
//   The periodic observeable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The periodic observeable.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class PeriodicObserveable<T> : AbstractObservableDecorator<T>
    {
        #region Fields

        /// <summary>
        /// The _period.
        /// </summary>
        private readonly TimeSpan _period;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodicObserveable{T}"/> class.
        /// </summary>
        /// <param name="innerObservable">
        /// The inner observable.
        /// </param>
        /// <param name="period">
        /// The period.
        /// </param>
        public PeriodicObserveable(IObservable<T> innerObservable, TimeSpan period)
            : base(innerObservable)
        {
            this._period = period;
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
        protected override IObserver<T> DecorateObserver(IObserver<T> observer)
        {
            return new PeriodicObserver<T>(observer, this._period);
        }

        #endregion
    }
}