// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PredicateObserver.cs" company="">
//   
// </copyright>
// <summary>
//   The predicate observer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The predicate observer.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class PredicateObserver<T> : AbstractObserverDecorator<T>
    {
        #region Fields

        /// <summary>
        /// The _match.
        /// </summary>
        private readonly Predicate<T> _match;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateObserver{T}"/> class.
        /// </summary>
        /// <param name="innerObserver">
        /// The inner observer.
        /// </param>
        /// <param name="match">
        /// The match.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public PredicateObserver(IObserver<T> innerObserver, Predicate<T> match)
            : base(innerObserver)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }

            this._match = match;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The on next.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public override void OnNext(T item)
        {
            if (this._match(item))
            {
                this._innerObserver.OnNext(item);
            }
        }

        #endregion
    }
}