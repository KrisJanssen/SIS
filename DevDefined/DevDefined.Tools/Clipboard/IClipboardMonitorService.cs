using System;

namespace DevDefined.Tools.Clipboard
{
    /// <summary>
    /// Service which monitors the clipboard for changes, and raises an event when it occurs.
    /// </summary>
    public interface IClipboardMonitorService
    {
        /// <summary>
        /// Event raised when the clipboard has changed
        /// </summary>
        event EventHandler ClipboardChanged;
    }
}