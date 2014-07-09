// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormEx.cs" company="">
//   
// </copyright>
// <summary>
//   Provides special methods and properties that must be implemented in a
//   system-specific manner. It is implemented as an object that is hosted
//   by the PdnBaseForm class. This way there is no inheritance hierarchy
//   extending into the SystemLayer assembly.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Systemlayer
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Provides special methods and properties that must be implemented in a
    /// system-specific manner. It is implemented as an object that is hosted
    /// by the PdnBaseForm class. This way there is no inheritance hierarchy 
    /// extending into the SystemLayer assembly.
    /// </summary>
    public sealed class FormEx : Control
    {
        #region Fields

        /// <summary>
        /// The force active title bar.
        /// </summary>
        private bool forceActiveTitleBar = false;

        /// <summary>
        /// The host.
        /// </summary>
        private Form host;

        /// <summary>
        /// The ignore nc activate.
        /// </summary>
        private int ignoreNcActivate = 0;

        /// <summary>
        /// The real parent wnd proc.
        /// </summary>
        private RealParentWndProcDelegate realParentWndProc;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormEx"/> class.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="realParentWndProc">
        /// The real parent wnd proc.
        /// </param>
        public FormEx(Form host, RealParentWndProcDelegate realParentWndProc)
        {
            this.host = host;
            this.realParentWndProc = realParentWndProc;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The process cmd key relay.
        /// </summary>
        public event EventHandler<ProcessCmdKeyEventArgs> ProcessCmdKeyRelay;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the titlebar rendering behavior for when the form is deactivated.
        /// </summary>
        /// <remarks>
        /// If this property is false, the titlebar will be rendered in a different color when the form
        /// is inactive as opposed to active. If this property is true, it will always render with the
        /// active style. If the whole application is deactivated, the title bar will still be drawn in
        /// an inactive state.
        /// </remarks>
        public bool ForceActiveTitleBar
        {
            get
            {
                return this.forceActiveTitleBar;
            }

            set
            {
                this.forceActiveTitleBar = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Manages some special handling of window messages.
        /// </summary>
        /// <param name="m">
        /// </param>
        /// <returns>
        /// true if the message was handled, false if the caller should handle the message.
        /// </returns>
        public bool HandleParentWndProc(ref Message m)
        {
            bool returnVal = true;

            switch (m.Msg)
            {
                case NativeConstants.WM_NCPAINT:
                    goto default;

                case NativeConstants.WM_NCACTIVATE:
                    if (this.forceActiveTitleBar && m.WParam == IntPtr.Zero)
                    {
                        if (this.ignoreNcActivate > 0)
                        {
                            --this.ignoreNcActivate;
                            goto default;
                        }
                        else if (Form.ActiveForm != this.host
                                 ||
                                 // Gets rid of: if you have the form active, then click on the desktop --> desktop refreshes
                                 !this.host.Visible)
                        {
                            // Gets rid of: desktop refresh on exit
                            goto default;
                        }
                        else
                        {
                            // Only 'lock' for the topmost form in the application. Otherwise you get the whole system
                            // refreshing (i.e. the dreaded "repaint the whole desktop 5 times" glitch) when you do things
                            // like minimize the window
                            // And only lock if we aren't minimized. Otherwise the desktop refreshes.
                            bool locked = false;
                            if (this.host.Owner == null && this.host.WindowState != FormWindowState.Minimized)
                            {
                                // UI.SetControlRedraw(this.host, false);
                                locked = true;
                            }

                            this.realParentWndProc(ref m);

                            SafeNativeMethods.SendMessageW(
                                this.host.Handle, 
                                NativeConstants.WM_NCACTIVATE, 
                                new IntPtr(1), 
                                IntPtr.Zero);

                            if (locked)
                            {
                                // UI.SetControlRedraw(this.host, true);
                                // this.host.Invalidate(true);
                            }

                            break;
                        }
                    }
                    else
                    {
                        goto default;
                    }

                case NativeConstants.WM_ACTIVATE:
                    goto default;

                case NativeConstants.WM_ACTIVATEAPP:
                    this.realParentWndProc(ref m);

                    // Check if the app is being deactivated
                    if (this.forceActiveTitleBar && m.WParam == IntPtr.Zero)
                    {
                        // If so, put our titlebar in the inactive state
                        SafeNativeMethods.PostMessageW(
                            this.host.Handle, 
                            NativeConstants.WM_NCACTIVATE, 
                            IntPtr.Zero, 
                            IntPtr.Zero);

                        ++this.ignoreNcActivate;
                    }

                    if (m.WParam == new IntPtr(1))
                    {
                        foreach (Form childForm in this.host.OwnedForms)
                        {
                            FormEx childFormEx = FindFormEx(childForm);

                            if (childFormEx != null)
                            {
                                if (childFormEx.ForceActiveTitleBar && childForm.IsHandleCreated)
                                {
                                    SafeNativeMethods.PostMessageW(
                                        childForm.Handle, 
                                        NativeConstants.WM_NCACTIVATE, 
                                        new IntPtr(1), 
                                        IntPtr.Zero);
                                }
                            }
                        }

                        FormEx ownerEx = FindFormEx(this.host.Owner);
                        if (ownerEx != null)
                        {
                            if (ownerEx.ForceActiveTitleBar && this.host.Owner.IsHandleCreated)
                            {
                                SafeNativeMethods.PostMessageW(
                                    this.host.Owner.Handle, 
                                    NativeConstants.WM_NCACTIVATE, 
                                    new IntPtr(1), 
                                    IntPtr.Zero);
                            }
                        }
                    }

                    break;

                default:
                    returnVal = false;
                    break;
            }

            GC.KeepAlive(this.host);
            return returnVal;
        }

        /// <summary>
        /// The relay process cmd key.
        /// </summary>
        /// <param name="keyData">
        /// The key data.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool RelayProcessCmdKey(Keys keyData)
        {
            bool handled = false;

            if (this.ProcessCmdKeyRelay != null)
            {
                ProcessCmdKeyEventArgs e = new ProcessCmdKeyEventArgs(keyData, false);
                this.ProcessCmdKeyRelay(this, e);
                handled = e.Handled;
            }

            return handled;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The find form ex.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <returns>
        /// The <see cref="FormEx"/>.
        /// </returns>
        internal static FormEx FindFormEx(Form host)
        {
            if (host != null)
            {
                ControlCollection controls = host.Controls;

                for (int i = 0; i < controls.Count; ++i)
                {
                    FormEx formEx = controls[i] as FormEx;

                    if (formEx != null)
                    {
                        return formEx;
                    }
                }
            }

            return null;
        }

        #endregion

        /// <summary>
        /// The process cmd key event args.
        /// </summary>
        public class ProcessCmdKeyEventArgs : EventArgs
        {
            #region Fields

            /// <summary>
            /// The handled.
            /// </summary>
            private bool handled;

            /// <summary>
            /// The key data.
            /// </summary>
            private Keys keyData;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="ProcessCmdKeyEventArgs"/> class.
            /// </summary>
            /// <param name="keyData">
            /// The key data.
            /// </param>
            /// <param name="handled">
            /// The handled.
            /// </param>
            public ProcessCmdKeyEventArgs(Keys keyData, bool handled)
            {
                this.keyData = keyData;
                this.handled = handled;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets or sets a value indicating whether handled.
            /// </summary>
            public bool Handled
            {
                get
                {
                    return this.handled;
                }

                set
                {
                    this.handled = value;
                }
            }

            /// <summary>
            /// Gets the key data.
            /// </summary>
            public Keys KeyData
            {
                get
                {
                    return this.keyData;
                }
            }

            #endregion
        }
    }
}