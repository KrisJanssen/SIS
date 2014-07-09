// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDisposedEvent.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The DisposedEvent interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Base
{
    using System;

    /// <summary>
    /// The DisposedEvent interface.
    /// </summary>
    public interface IDisposedEvent
    {
        #region Public Events

        /// <summary>
        /// The disposed.
        /// </summary>
        event EventHandler Disposed;

        #endregion
    }
}