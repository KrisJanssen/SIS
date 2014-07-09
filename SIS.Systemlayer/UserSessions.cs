// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserSessions.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Encapsulates information and events about the current user session.
//   This relates to Terminal Services in Windows.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// Encapsulates information and events about the current user session.
    /// This relates to Terminal Services in Windows.
    /// </summary>
    public static class UserSessions
    {
        #region Static Fields

        /// <summary>
        /// The last remote session value.
        /// </summary>
        private static bool lastRemoteSessionValue;

        /// <summary>
        /// The lock object.
        /// </summary>
        private static object lockObject = new object();

        /// <summary>
        /// The message control.
        /// </summary>
        private static OurControl messageControl;

        /// <summary>
        /// The session changed.
        /// </summary>
        private static EventHandler sessionChanged;

        /// <summary>
        /// The session changed count.
        /// </summary>
        private static int sessionChangedCount;

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when the user changes between sessions. This event will only be
        /// raised when the value returned by IsRemote() changes.
        /// </summary>
        /// <remarks>
        /// For example, if the user is currently logged in at the console, and then
        /// switches to a remote session (they use Remote Desktop from another computer),
        /// then this event will be raised.
        /// Note to implementors: This may be implemented as a no-op.
        /// </remarks>
        public static event EventHandler SessionChanged
        {
            add
            {
                lock (lockObject)
                {
                    sessionChanged += value;
                    ++sessionChangedCount;

                    if (sessionChangedCount == 1)
                    {
                        messageControl = new OurControl();
                        messageControl.CreateControl(); // force the HWND to be created
                        messageControl.WmWtSessionChange += new EventHandler(SessionStrobeHandler);

                        SafeNativeMethods.WTSRegisterSessionNotification(
                            messageControl.Handle, 
                            NativeConstants.NOTIFY_FOR_ALL_SESSIONS);
                        lastRemoteSessionValue = IsRemote;
                    }
                }
            }

            remove
            {
                lock (lockObject)
                {
                    sessionChanged -= value;
                    int decremented = Interlocked.Decrement(ref sessionChangedCount);

                    if (decremented == 0)
                    {
                        try
                        {
                            SafeNativeMethods.WTSUnRegisterSessionNotification(messageControl.Handle);
                        }
                        catch (EntryPointNotFoundException)
                        {
                        }

                        messageControl.Dispose();
                        messageControl = null;
                    }
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Determines whether the user is running within a remoted session (Terminal Server, Remote Desktop).
        /// </summary>
        /// <returns>
        /// <b>true</b> if we're running in a remote session, <b>false</b> otherwise.
        /// </returns>
        /// <remarks>
        /// You can use this to optimize the presentation of visual elements. Remote sessions
        /// are often bandwidth limited and less suitable for complex drawing.
        /// Note to implementors: This may be implemented as a no op; in this case, always return false.
        /// </remarks>
        public static bool IsRemote
        {
            get
            {
                return 0 != SafeNativeMethods.GetSystemMetrics(NativeConstants.SM_REMOTESESSION);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on session changed.
        /// </summary>
        private static void OnSessionChanged()
        {
            if (sessionChanged != null)
            {
                sessionChanged(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// The session strobe handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void SessionStrobeHandler(object sender, EventArgs e)
        {
            if (IsRemote != lastRemoteSessionValue)
            {
                lastRemoteSessionValue = IsRemote;
                OnSessionChanged();
            }
        }

        #endregion

        /// <summary>
        /// The our control.
        /// </summary>
        private sealed class OurControl : Control
        {
            #region Public Events

            /// <summary>
            /// The wm wt session change.
            /// </summary>
            public event EventHandler WmWtSessionChange;

            #endregion

            #region Methods

            /// <summary>
            /// The wnd proc.
            /// </summary>
            /// <param name="m">
            /// The m.
            /// </param>
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case NativeConstants.WM_WTSSESSION_CHANGE:
                        this.OnWmWtSessionChange();
                        break;

                    default:
                        base.WndProc(ref m);
                        break;
                }
            }

            /// <summary>
            /// The on wm wt session change.
            /// </summary>
            private void OnWmWtSessionChange()
            {
                if (this.WmWtSessionChange != null)
                {
                    this.WmWtSessionChange(this, EventArgs.Empty);
                }
            }

            #endregion
        }
    }
}