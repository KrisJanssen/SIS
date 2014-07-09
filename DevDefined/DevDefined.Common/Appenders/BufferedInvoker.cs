// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BufferedInvoker.cs" company="">
//   
// </copyright>
// <summary>
//   The buffered invoker.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Appenders
{
    using System;
    using System.Timers;

    /// <summary>
    /// The buffered invoker.
    /// </summary>
    public class BufferedInvoker
    {
        #region Fields

        /// <summary>
        /// The _action.
        /// </summary>
        private readonly Action _action;

        /// <summary>
        /// The _max rate in millseconds.
        /// </summary>
        private readonly double _maxRateInMillseconds;

        /// <summary>
        /// The _timer.
        /// </summary>
        private readonly Timer _timer;

        /// <summary>
        /// The _last invocation.
        /// </summary>
        private DateTime? _lastInvocation;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferedInvoker"/> class.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="maxRateInMillseconds">
        /// The max rate in millseconds.
        /// </param>
        public BufferedInvoker(Action action, double maxRateInMillseconds)
        {
            this._action = action;
            this._maxRateInMillseconds = maxRateInMillseconds;
            this._timer = new Timer(maxRateInMillseconds) { Enabled = false };
            this._timer.Elapsed += this.TimerElapsed;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The invoke.
        /// </summary>
        public void Invoke()
        {
            if (!this._timer.Enabled)
            {
                if (this._lastInvocation.HasValue)
                {
                    if (DateTime.Now.Subtract(this._lastInvocation.Value).TotalMilliseconds > this._maxRateInMillseconds)
                    {
                        this.FireInvoke();
                    }
                    else
                    {
                        this._timer.Start();
                    }
                }
                else
                {
                    this.FireInvoke();
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The fire invoke.
        /// </summary>
        private void FireInvoke()
        {
            this._lastInvocation = DateTime.Now;
            this._timer.Enabled = false;
            this._action();
        }

        /// <summary>
        /// The timer elapsed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            this._timer.Enabled = false;
            this._lastInvocation = DateTime.Now;
            this._action();
        }

        #endregion
    }
}