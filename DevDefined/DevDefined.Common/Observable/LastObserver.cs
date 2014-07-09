// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LastObserver.cs" company="">
//   
// </copyright>
// <summary>
//   The last observer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The last observer.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class LastObserver<T> : IObserver<T>
    {
        #region Fields

        /// <summary>
        /// The _inner observer.
        /// </summary>
        private readonly IObserver<T> _innerObserver;

        /// <summary>
        /// The _last.
        /// </summary>
        private T _last;

        /// <summary>
        /// The _set.
        /// </summary>
        private bool _set;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LastObserver{T}"/> class.
        /// </summary>
        /// <param name="innerObserver">
        /// The inner observer.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public LastObserver(IObserver<T> innerObserver)
        {
            if (innerObserver == null)
            {
                throw new ArgumentNullException("innerObserver");
            }

            this._innerObserver = innerObserver;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The on done.
        /// </summary>
        public void OnDone()
        {
            if (this._set)
            {
                this._innerObserver.OnNext(this._last);
            }

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
            this._last = item;
            this._set = true;
        }

        #endregion
    }
}