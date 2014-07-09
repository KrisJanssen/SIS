// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandledEventArgs`1.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The handled event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate.Events
{
    using System.ComponentModel;

    /// <summary>
    /// The handled event args.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class HandledEventArgs<T> : HandledEventArgs
    {
        #region Fields

        /// <summary>
        /// The data.
        /// </summary>
        private T data;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HandledEventArgs{T}"/> class.
        /// </summary>
        /// <param name="handled">
        /// The handled.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public HandledEventArgs(bool handled, T data)
            : base(handled)
        {
            this.data = data;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the data.
        /// </summary>
        public T Data
        {
            get
            {
                return this.data;
            }
        }

        #endregion
    }
}