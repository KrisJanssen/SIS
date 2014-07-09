// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TakeObserver.cs" company="">
//   
// </copyright>
// <summary>
//   The take observer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Observable
{
    /// <summary>
    /// The take observer.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class TakeObserver<T> : AbstractObserverDecorator<T>
    {
        #region Fields

        /// <summary>
        /// The _max.
        /// </summary>
        private readonly int _max;

        /// <summary>
        /// The _count.
        /// </summary>
        private int _count;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TakeObserver{T}"/> class.
        /// </summary>
        /// <param name="innerObserver">
        /// The inner observer.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        public TakeObserver(IObserver<T> innerObserver, int max)
            : base(innerObserver)
        {
            this._max = max;
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
            if (this._count < this._max)
            {
                this._innerObserver.OnNext(item);
                this._count++;
            }
        }

        #endregion
    }
}