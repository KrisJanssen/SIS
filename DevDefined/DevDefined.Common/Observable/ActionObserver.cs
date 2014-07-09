// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionObserver.cs" company="">
//   
// </copyright>
// <summary>
//   The action observer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The action observer.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ActionObserver<T> : IObserver<T>
    {
        #region Fields

        /// <summary>
        /// The _on done.
        /// </summary>
        private readonly Action _onDone;

        /// <summary>
        /// The _on exception.
        /// </summary>
        private readonly Action<Exception> _onException;

        /// <summary>
        /// The _on next.
        /// </summary>
        private readonly Action<T> _onNext;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionObserver{T}"/> class.
        /// </summary>
        /// <param name="onNext">
        /// The on next.
        /// </param>
        public ActionObserver(Action<T> onNext)
            : this(onNext, DefaultExceptionHandler, DefaultDoneHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionObserver{T}"/> class.
        /// </summary>
        /// <param name="onNext">
        /// The on next.
        /// </param>
        /// <param name="onException">
        /// The on exception.
        /// </param>
        /// <param name="onDone">
        /// The on done.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public ActionObserver(Action<T> onNext, Action<Exception> onException, Action onDone)
        {
            if (onNext == null)
            {
                throw new ArgumentNullException("onNext");
            }

            if (onException == null)
            {
                throw new ArgumentNullException("onException");
            }

            if (onDone == null)
            {
                throw new ArgumentNullException("onDone");
            }

            this._onNext = onNext;
            this._onException = onException;
            this._onDone = onDone;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The on done.
        /// </summary>
        public void OnDone()
        {
            this._onDone();
        }

        /// <summary>
        /// The on exception.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public void OnException(Exception ex)
        {
            this._onException(ex);
        }

        /// <summary>
        /// The on next.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void OnNext(T item)
        {
            this._onNext(item);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The default done handler.
        /// </summary>
        private static void DefaultDoneHandler()
        {
        }

        /// <summary>
        /// The default exception handler.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        private static void DefaultExceptionHandler(Exception ex)
        {
            throw new Exception("Exception was thrown by Observable", ex);
        }

        #endregion
    }
}