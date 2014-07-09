// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableTimer.cs" company="">
//   
// </copyright>
// <summary>
//   The observable timer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Observable
{
    using System;
    using System.Threading;

    /// <summary>
    /// The observable timer.
    /// </summary>
    public class ObservableTimer : AbstractObservable<TimerElapsed>
    {
        #region Fields

        /// <summary>
        /// The _stopped.
        /// </summary>
        private bool _stopped;

        /// <summary>
        /// The _timer.
        /// </summary>
        private Timer _timer;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Stop();
        }

        /// <summary>
        /// The start.
        /// </summary>
        /// <param name="periodInMilliseconds">
        /// The period in milliseconds.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public void Start(int periodInMilliseconds)
        {
            if (this._timer != null)
            {
                throw new InvalidOperationException("Observable timer has already been started");
            }

            this._timer = new Timer(this.Elapsed, null, 0, periodInMilliseconds);
        }

        /// <summary>
        /// The stop.
        /// </summary>
        public void Stop()
        {
            if (!this._stopped && this._timer != null)
            {
                this._timer.Dispose();
                this.OnDone();
                this._stopped = true;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The elapsed.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        private void Elapsed(object state)
        {
            var elapsed = new TimerElapsed(DateTime.Now);
            this.OnNext(elapsed);
        }

        #endregion
    }
}