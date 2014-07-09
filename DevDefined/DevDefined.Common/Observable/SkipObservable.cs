// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SkipObservable.cs" company="">
//   
// </copyright>
// <summary>
//   The skip observable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Observable
{
    /// <summary>
    /// The skip observable.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class SkipObservable<T> : AbstractObservableDecorator<T>
    {
        #region Fields

        /// <summary>
        /// The _number to skip.
        /// </summary>
        private readonly int _numberToSkip;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SkipObservable{T}"/> class.
        /// </summary>
        /// <param name="innerObservable">
        /// The inner observable.
        /// </param>
        /// <param name="numberToSkip">
        /// The number to skip.
        /// </param>
        public SkipObservable(IObservable<T> innerObservable, int numberToSkip)
            : base(innerObservable)
        {
            this._numberToSkip = numberToSkip;
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
        /// The <see cref="IObserver"/>.
        /// </returns>
        protected override IObserver<T> DecorateObserver(IObserver<T> observer)
        {
            return new SkipObserver<T>(observer, this._numberToSkip);
        }

        #endregion
    }
}