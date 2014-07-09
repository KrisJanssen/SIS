// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AllObserver.cs" company="">
//   
// </copyright>
// <summary>
//   The all observer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The all observer.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class AllObserver<T> : IObserver<T>
    {
        #region Fields

        /// <summary>
        /// The _inner observer.
        /// </summary>
        private readonly IObserver<IEnumerable<T>> _innerObserver;

        /// <summary>
        /// The _items.
        /// </summary>
        private readonly List<T> _items = new List<T>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AllObserver{T}"/> class.
        /// </summary>
        /// <param name="innerObserver">
        /// The inner observer.
        /// </param>
        public AllObserver(IObserver<IEnumerable<T>> innerObserver)
        {
            this._innerObserver = innerObserver;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The on done.
        /// </summary>
        public void OnDone()
        {
            this._innerObserver.OnNext(this._items);
            this._innerObserver.OnDone();
        }

        /// <summary>
        /// The on exception.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public void OnException(Exception ex)
        {
            this._innerObserver.OnException(ex);
        }

        /// <summary>
        /// The on next.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void OnNext(T item)
        {
            this._items.Add(item);
        }

        #endregion
    }
}