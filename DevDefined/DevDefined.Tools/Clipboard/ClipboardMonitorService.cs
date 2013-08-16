using System;
using System.Windows.Forms;
using Castle.Core;
using DevDefined.Tools.Win32;

namespace DevDefined.Tools.Clipboard
{
    /// <summary>
    /// Service used to monitor the clipboard for changes. Create an instance of the monitor 
    /// service, passing it the main form for your application.
    /// </summary>
    /// <remarks>
    /// This class can be registered in the container, just be sure to register your main form as
    /// the default <see cref="IWin32Window" /> service.
    /// </remarks>
    public class ClipboardMonitorService : IClipboardMonitorService, IStartable
    {
        private readonly IntPtr _handle;
        private IntPtr _clipboardViewerNext;

        /// <summary>
        /// Initializes the service
        /// </summary>
        /// <param name="window"></param>
        public ClipboardMonitorService(IWin32Window window)
            : this(window.Handle)
        {
        }

        /// <summary>
        /// Initializes the service
        /// </summary>
        /// <param name="handle"></param>
        public ClipboardMonitorService(IntPtr handle)
        {
            _handle = handle;
        }

        #region IClipboardMonitorService Members

        public event EventHandler ClipboardChanged;

        #endregion

        #region IStartable Members

        /// <summary>
        /// Starts the service - will add the handler to the chain
        /// </summary>
        public void Start()
        {
            _clipboardViewerNext = User32.SetClipboardViewer(_handle);
        }

        /// <summary>
        /// Stops the service - will remove the handler from the chain
        /// </summary>
        public void Stop()
        {
            User32.ChangeClipboardChain(_handle, _clipboardViewerNext);
        }

        #endregion

        /// <summary>
        /// Call to process the WndProc message.
        /// </summary>
        /// <param name="m">The message to process</param>
        /// <returns>Will return <c>false</c> if the base WinProc needs to be called.</returns>
        public bool ProcessWndProcMessage(ref Message m)
        {
            switch ((Msgs) m.Msg)
            {
                case Msgs.WM_DRAWCLIPBOARD:

                    try
                    {
                        if (ClipboardChanged != null) ClipboardChanged(this, EventArgs.Empty);
                    }
                    finally
                    {
                        User32.SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    }
                    return true;

                case Msgs.WM_CHANGECBCHAIN:

                    if (m.WParam == _clipboardViewerNext)
                    {
                        _clipboardViewerNext = m.LParam;
                    }
                    else
                    {
                        User32.SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    }

                    return true;

                default:

                    return false;
            }
        }
    }
}