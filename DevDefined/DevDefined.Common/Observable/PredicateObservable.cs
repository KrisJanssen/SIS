// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PredicateObservable.cs" company="">
//   
// </copyright>
// <summary>
//   The predicate observable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The predicate observable.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class PredicateObservable<T> : AbstractObservableDecorator<T>
    {
        #region Fields

        /// <summary>
        /// The _match.
        /// </summary>
        private readonly Predicate<T> _match;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateObservable{T}"/> class.
        /// </summary>
        /// <param name="innerObservable">
        /// The inner observable.
        /// </param>
        /// <param name="match">
        /// The match.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public PredicateObservable(IObservable<T> innerObservable, Predicate<T> match)
            : base(innerObservable)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }

            this._match = match;
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
            return new PredicateObserver<T>(observer, this._match);
        }

        #endregion
    }
}