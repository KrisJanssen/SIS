// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClipboardMonitorService.cs" company="">
//   
// </copyright>
// <summary>
//   Service used to monitor the clipboard for changes. Create an instance of the monitor
//   service, passing it the main form for your application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Tools.Clipboard
{
    using System;
    using System.Windows.Forms;

    using Castle.Core;

    using DevDefined.Tools.Win32;

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
        #region Fields

        /// <summary>
        /// The _handle.
        /// </summary>
        private readonly IntPtr _handle;

        /// <summary>
        /// The _clipboard viewer next.
        /// </summary>
        private IntPtr _clipboardViewerNext;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClipboardMonitorService"/> class. 
        /// Initializes the service
        /// </summary>
        /// <param name="window">
        /// </param>
        public ClipboardMonitorService(IWin32Window window)
            : this(window.Handle)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClipboardMonitorService"/> class. 
        /// Initializes the service
        /// </summary>
        /// <param name="handle">
        /// </param>
        public ClipboardMonitorService(IntPtr handle)
        {
            this._handle = handle;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The clipboard changed.
        /// </summary>
        public event EventHandler ClipboardChanged;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Call to process the WndProc message.
        /// </summary>
        /// <param name="m">
        /// The message to process
        /// </param>
        /// <returns>
        /// Will return <c>false</c> if the base WinProc needs to be called.
        /// </returns>
        public bool ProcessWndProcMessage(ref Message m)
        {
            switch ((Msgs)m.Msg)
            {
                case Msgs.WM_DRAWCLIPBOARD:

                    try
                    {
                        if (this.ClipboardChanged != null)
                        {
                            this.ClipboardChanged(this, EventArgs.Empty);
                        }
                    }
                    finally
                    {
                        User32.SendMessage(this._clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    }

                    return true;

                case Msgs.WM_CHANGECBCHAIN:

                    if (m.WParam == this._clipboardViewerNext)
                    {
                        this._clipboardViewerNext = m.LParam;
                    }
                    else
                    {
                        User32.SendMessage(this._clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    }

                    return true;

                default:

                    return false;
            }
        }

        /// <summary>
        /// Starts the service - will add the handler to the chain
        /// </summary>
        public void Start()
        {
            this._clipboardViewerNext = User32.SetClipboardViewer(this._handle);
        }

        /// <summary>
        /// Stops the service - will remove the handler from the chain
        /// </summary>
        public void Stop()
        {
            User32.ChangeClipboardChain(this._handle, this._clipboardViewerNext);
        }

        #endregion
    }
}