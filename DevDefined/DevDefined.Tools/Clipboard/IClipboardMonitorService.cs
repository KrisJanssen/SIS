// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClipboardMonitorService.cs" company="">
//   
// </copyright>
// <summary>
//   Service which monitors the clipboard for changes, and raises an event when it occurs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Tools.Clipboard
{
    using System;

    /// <summary>
    /// Service which monitors the clipboard for changes, and raises an event when it occurs.
    /// </summary>
    public interface IClipboardMonitorService
    {
        #region Public Events

        /// <summary>
        /// Event raised when the clipboard has changed
        /// </summary>
        event EventHandler ClipboardChanged;

        #endregion
    }
}