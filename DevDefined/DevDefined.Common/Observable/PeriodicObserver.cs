// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PeriodicObserver.cs" company="">
//   
// </copyright>
// <summary>
//   The periodic observer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The periodic observer.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class PeriodicObserver<T> : AbstractObserverDecorator<T>
    {
        #region Fields

        /// <summary>
        /// The _period.
        /// </summary>
        private readonly TimeSpan _period;

        /// <summary>
        /// The _last.
        /// </summary>
        private DateTime? _last;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodicObserver{T}"/> class.
        /// </summary>
        /// <param name="innerObserver">
        /// The inner observer.
        /// </param>
        /// <param name="period">
        /// The period.
        /// </param>
        public PeriodicObserver(IObserver<T> innerObserver, TimeSpan period)
            : base(innerObserver)
        {
            this._period = period;
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
            DateTime now = DateTime.Now;

            if ((this._last == null) || now.Subtract(this._last.Value) > this._period)
            {
                this._last = now;
                this._innerObserver.OnNext(item);
            }
        }

        #endregion
    }
}