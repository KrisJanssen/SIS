// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangeObserver.cs" company="">
//   
// </copyright>
// <summary>
//   The change observer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The change observer.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ChangeObserver<T> : AbstractObserverDecorator<T>
        where T : IEquatable<T>
    {
        #region Fields

        /// <summary>
        /// The _has value.
        /// </summary>
        private bool _hasValue;

        /// <summary>
        /// The _last.
        /// </summary>
        private T _last;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeObserver{T}"/> class.
        /// </summary>
        /// <param name="innerObserver">
        /// The inner observer.
        /// </param>
        public ChangeObserver(IObserver<T> innerObserver)
            : base(innerObserver)
        {
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
            if (this._hasValue && this._last.Equals(item))
            {
                return;
            }

            this._last = item;
            this._hasValue = true;
            this._innerObserver.OnNext(item);
        }

        #endregion
    }
}