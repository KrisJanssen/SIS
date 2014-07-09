// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventArgs.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Base
{
    using System;

    /// <summary>
    /// The event args.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class EventArgs<T> : EventArgs
    {
        #region Fields

        /// <summary>
        /// The data.
        /// </summary>
        private readonly T data;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs{T}"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public EventArgs(T data)
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