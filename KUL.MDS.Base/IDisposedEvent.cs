// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDisposedEvent.cs" company="">
//   
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