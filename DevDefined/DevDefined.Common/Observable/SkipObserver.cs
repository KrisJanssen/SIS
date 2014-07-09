// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SkipObserver.cs" company="">
//   
// </copyright>
// <summary>
//   The skip observer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    /// <summary>
    /// The skip observer.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class SkipObserver<T> : AbstractObserverDecorator<T>
    {
        #region Fields

        /// <summary>
        /// The _number to skip.
        /// </summary>
        private readonly int _numberToSkip;

        /// <summary>
        /// The _skipped so far.
        /// </summary>
        private int _skippedSoFar;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SkipObserver{T}"/> class.
        /// </summary>
        /// <param name="innerObserver">
        /// The inner observer.
        /// </param>
        /// <param name="numberToSkip">
        /// The number to skip.
        /// </param>
        public SkipObserver(IObserver<T> innerObserver, int numberToSkip)
            : base(innerObserver)
        {
            this._numberToSkip = numberToSkip;
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
            if (this._skippedSoFar < this._numberToSkip)
            {
                this._skippedSoFar++;
            }
            else
            {
                this._innerObserver.OnNext(item);
            }
        }

        #endregion
    }
}