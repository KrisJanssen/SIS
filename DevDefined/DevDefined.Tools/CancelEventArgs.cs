using System;

namespace DevDefined.Tools
{
    /// <summary>
    /// Our version of the cancel event args (which can be overriden)
    /// </summary>
    public class CancelEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelEventArgs"/> class.
        /// </summary>
        public CancelEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelEventArgs"/> class.
        /// </summary>
        /// <param name="cancel">if set to <c>true</c> [cancel].</param>
        public CancelEventArgs(bool cancel)
        {
            InternalCancel = cancel;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the internal cancel flag is true or false.
        /// </summary>
        /// <value><c>true</c> if canceled; otherwise, <c>false</c>.</value>
        protected bool InternalCancel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CancelEventArgs"/> is canceled.
        /// </summary>
        /// <value><c>true</c> if canceled; otherwise, <c>false</c>.</value>
        public virtual bool Cancel
        {
            get { return InternalCancel; }
            set { InternalCancel = value; }
        }
    }

    /// <summary>
    /// Generic version of <see cref="CancelEventArgs" />.
    /// </summary>
    /// <typeparam name="T">The event value type</typeparam>
    public class CancelEventArgs<T> : CancelEventArgs
    {
        private T _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelEventArgs&lt;T&gt;"/> class.
        /// </summary>
        public CancelEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelEventArgs&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="cancel">if set to <c>true</c> action is cancelled.</param>
        public CancelEventArgs(bool cancel)
            : base(cancel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelEventArgs&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cancel">if set to <c>true</c> action is canceled.</param>
        public CancelEventArgs(T value, bool cancel)
            : base(cancel)
        {
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelEventArgs&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public CancelEventArgs(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public virtual T Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    /// <summary>
    /// Special case <see cref="CancelEventArgs" /> that will throw an exception if the user
    /// attempts to cancel it.
    /// </summary>
    public class CantCancelEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CancelEventArgs"/> is canceled.
        /// </summary>
        /// <value><c>true</c> if canceled; otherwise, <c>false</c>.</value>
        public override bool Cancel
        {
            get { return false; }
            set
            {
                if (value)
                    throw new ApplicationException(
                        "In the application's current environment you can not cancel this event");
            }
        }
    }

    /// <summary>
    /// Generic equivalent of <see cref="CantCancelEventArgs" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CantCancelEventArgs<T> : CantCancelEventArgs
    {
        private T _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="CantCancelEventArgs&lt;T&gt;"/> class.
        /// </summary>
        public CantCancelEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CantCancelEventArgs&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public CantCancelEventArgs(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public virtual T Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}