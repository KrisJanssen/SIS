// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangeObservable.cs" company="">
//   
// </copyright>
// <summary>
//   The change observable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The change observable.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ChangeObservable<T> : AbstractObservableDecorator<T>
        where T : IEquatable<T>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeObservable{T}"/> class.
        /// </summary>
        /// <param name="innerObservable">
        /// The inner observable.
        /// </param>
        public ChangeObservable(IObservable<T> innerObservable)
            : base(innerObservable)
        {
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
            return new ChangeObserver<T>(observer);
        }

        #endregion
    }
}