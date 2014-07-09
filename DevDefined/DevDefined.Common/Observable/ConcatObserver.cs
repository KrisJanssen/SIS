// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConcatObserver.cs" company="">
//   
// </copyright>
// <summary>
//   The concatenate observable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Observable
{
    using System;

    /// <summary>
    /// The concatenate observable.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ConcatenateObservable<T> : IObservable<T>
    {
        #region Fields

        /// <summary>
        /// The _first.
        /// </summary>
        private readonly IObservable<T> _first;

        /// <summary>
        /// The _second.
        /// </summary>
        private readonly IObservable<T> _second;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcatenateObservable{T}"/> class.
        /// </summary>
        /// <param name="first">
        /// The first.
        /// </param>
        /// <param name="second">
        /// The second.
        /// </param>
        public ConcatenateObservable(IObservable<T> first, IObservable<T> second)
        {
            this._first = first;
            this._second = second;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The subscribe.
        /// </summary>
        /// <param name="observer">
        /// The observer.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public IDisposable Subscribe(IObserver<T> observer)
        {
            IDisposable firstDisposable = this._first.Subscribe(observer);
            IDisposable secondDisposable = this._second.Subscribe(observer);
            return new DisposableAction(
                () =>
                    {
                        firstDisposable.Dispose();
                        secondDisposable.Dispose();
                    });
        }

        #endregion
    }
}