// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableSink.cs" company="">
//   
// </copyright>
// <summary>
//   The observable sink.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Observable
{
    using System;
    using System.Collections.Generic;

    using DevDefined.Common.Extensions;

    /// <summary>
    /// The observable sink.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ObservableSink<T> : AbstractObservable<T>, IDisposable
    {
        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.OnDone();
        }

        /// <summary>
        /// The pump.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// The <see cref="ObservableSink"/>.
        /// </returns>
        public ObservableSink<T> Pump(IEnumerable<T> source)
        {
            source.ForEach(this.OnNext);
            return this;
        }

        /// <summary>
        /// The pump.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// The <see cref="ObservableSink"/>.
        /// </returns>
        public ObservableSink<T> Pump(params T[] source)
        {
            source.ForEach(this.OnNext);
            return this;
        }

        /// <summary>
        /// The pump.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="ObservableSink"/>.
        /// </returns>
        public ObservableSink<T> Pump(T item)
        {
            this.OnNext(item);
            return this;
        }

        #endregion
    }
}