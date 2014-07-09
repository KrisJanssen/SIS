// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TakeObservable.cs" company="">
//   
// </copyright>
// <summary>
//   The take observable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Observable
{
    /// <summary>
    /// The take observable.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class TakeObservable<T> : AbstractObservableDecorator<T>
    {
        #region Fields

        /// <summary>
        /// The _max.
        /// </summary>
        private readonly int _max;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TakeObservable{T}"/> class.
        /// </summary>
        /// <param name="innerObservable">
        /// The inner observable.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        public TakeObservable(IObservable<T> innerObservable, int max)
            : base(innerObservable)
        {
            this._max = max;
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
            return new TakeObserver<T>(observer, this._max);
        }

        #endregion
    }
}