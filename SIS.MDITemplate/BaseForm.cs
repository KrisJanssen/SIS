// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseForm.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   This Form class is used to fix a few bugs in Windows Forms, and to add a few performance
//   enhancements, such as disabling opacity != 1.0 when running in a remote TS/RD session.
//   We derive from this class instead of Windows.Forms.Form directly.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Threading;
    using System.Windows.Forms;

    using SIS.Base;
    using SIS.Library;
    using SIS.MDITemplate.Moving;
    using SIS.MDITemplate.Snapping;
    using SIS.Resources;
    using SIS.Systemlayer;

    /// <summary>
    /// This Form class is used to fix a few bugs in Windows Forms, and to add a few performance
    /// enhancements, such as disabling opacity != 1.0 when running in a remote TS/RD session.
    /// We derive from this class instead of Windows.Forms.Form directly.
    /// </summary>
    public class BaseForm : Form, ISnapManagerHost
    {
        // This set keeps track of forms which cannot be the current modal form.
        #region Static Fields

        /// <summary>
        /// The enable opacity changed.
        /// </summary>
        public static EventHandler EnableOpacityChanged;

        /// <summary>
        /// The global enable opacity.
        /// </summary>
        private static bool globalEnableOpacity = true;

        /// <summary>
        /// The hotkey registrar.
        /// </summary>
        private static Dictionary<Keys, Function<bool, Keys>> hotkeyRegistrar = null;

        /// <summary>
        /// The parent modal forms.
        /// </summary>
        private static Stack<List<Form>> parentModalForms = new Stack<List<Form>>();

        #endregion

        #region Fields

        /// <summary>
        /// The components.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// The enable opacity.
        /// </summary>
        private bool enableOpacity = true;

        /// <summary>
        /// The form ex.
        /// </summary>
        private FormEx formEx;

        /// <summary>
        /// The instance enable opacity.
        /// </summary>
        private bool instanceEnableOpacity = true;

        /// <summary>
        /// The is shown.
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// The original priority.
        /// </summary>
        private ThreadPriority originalPriority;

        /// <summary>
        /// The our opacity.
        /// </summary>
        private double ourOpacity = 1.0;

        // store opacity setting so that when we go from disabled->enabled opacity we can set the correct value

        /// <summary>
        /// The process form hot key mutex.
        /// </summary>
        private bool processFormHotKeyMutex = false;

        // if we're already processing a form hotkey, don't let other hotkeys execute.

        /// <summary>
        /// The snap manager.
        /// </summary>
        private SnapManager snapManager = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="BaseForm"/> class.
        /// </summary>
        static BaseForm()
        {
            Application.EnterThreadModal += new EventHandler(Application_EnterThreadModal);
            Application.LeaveThreadModal += new EventHandler(Application_LeaveThreadModal);
            Application_EnterThreadModal(null, EventArgs.Empty);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseForm"/> class.
        /// </summary>
        public BaseForm()
        {
            this.originalPriority = Thread.CurrentThread.Priority;
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

            UI.InitScaling(this);

            this.SuspendLayout();
            this.InitializeComponent();

            this.formEx = new FormEx(this, new RealParentWndProcDelegate(this.RealWndProc));
            this.Controls.Add(this.formEx);
            this.formEx.Visible = false;
            this.DecideOpacitySetting();
            this.ResumeLayout(false);

            this.formEx.ProcessCmdKeyRelay += this.OnProcessCmdKeyRelay;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The moving.
        /// </summary>
        public event MovingEventHandler Moving;

        /// <summary>
        /// The query end session.
        /// </summary>
        public event CancelEventHandler QueryEndSession;

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the currently active modal form if the process is in the foreground and is active.
        /// </summary>
        /// <remarks>
        /// If Form.ActiveForm is modeless, we search up the chain of owner forms
        /// to find its modeless owner form.
        /// </remarks>
        public static Form CurrentModalForm
        {
            get
            {
                Form theForm = Form.ActiveForm;

                while (theForm != null && !theForm.Modal && theForm.Owner != null)
                {
                    theForm = theForm.Owner;
                }

                return theForm;
            }
        }

        /// <summary>
        /// Gets or sets a flag that enables or disables opacity for all PdnBaseForm instances.
        /// If a particular form's EnableInstanceOpacity property is false, that will override
        /// this property being 'true'.
        /// </summary>
        public static bool EnableOpacity
        {
            get
            {
                return globalEnableOpacity;
            }

            set
            {
                globalEnableOpacity = value;
                OnEnableOpacityChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether enable instance opacity.
        /// </summary>
        public bool EnableInstanceOpacity
        {
            get
            {
                return this.instanceEnableOpacity;
            }

            set
            {
                this.instanceEnableOpacity = value;
                this.DecideOpacitySetting();
            }
        }

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
                return this.formEx.ForceActiveTitleBar;
            }

            set
            {
                this.formEx.ForceActiveTitleBar = value;
            }
        }

        /// <summary>
        /// Gets whether the current form is the processes' top level modal form.
        /// </summary>
        public bool IsCurrentModalForm
        {
            get
            {
                if (IsInParentModalForms(this))
                {
                    return false;
                }

                if (this.ContainsFocus)
                {
                    return true;
                }

                foreach (Form ownedForm in this.OwnedForms)
                {
                    if (ownedForm.ContainsFocus)
                    {
                        return true;
                    }
                }

                return this == CurrentModalForm;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is shown.
        /// </summary>
        public bool IsShown
        {
            get
            {
                return this.isShown;
            }
        }

        /// <summary>
        /// Sets the opacity of the form.
        /// </summary>
        /// <param name="newOpacity">The new opacity value.</param>
        /// <remarks>
        /// Depending on the system configuration, this request may be ignored. For example,
        /// when running within a Terminal Service (or Remote Desktop) session, opacity will
        /// always be set to 1.0 for performance reasons.
        /// </remarks>
        public new double Opacity
        {
            get
            {
                return this.ourOpacity;
            }

            set
            {
                if (this.enableOpacity)
                {
                    // Bypassing Form.Opacity eliminates a "black flickering" that occurs when
                    // the form transitions from Opacity=1.0 to Opacity != 1.0, or vice versa.
                    // It appears to be a result of toggling the WS_EX_LAYERED style, or the
                    // fact that Form.Opacity re-applies visual styles when this value transition
                    // takes place.
                    UI.SetFormOpacity(this, value);
                }

                this.ourOpacity = value;
            }
        }

        /// <summary>
        /// Gets the screen aspect.
        /// </summary>
        public double ScreenAspect
        {
            get
            {
                Rectangle bounds = System.Windows.Forms.Screen.FromControl(this).Bounds;
                double aspect = (double)bounds.Width / (double)bounds.Height;
                return aspect;
            }
        }

        /// <summary>
        /// Gets the snap manager.
        /// </summary>
        public SnapManager SnapManager
        {
            get
            {
                if (this.snapManager == null)
                {
                    this.snapManager = new SnapManager();
                }

                return this.snapManager;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the strings resource manager.
        /// </summary>
        protected virtual ResourceManager StringsResourceManager
        {
            get
            {
                return Resources.Strings;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The ensure rect is on screen.
        /// </summary>
        /// <param name="screen">
        /// The screen.
        /// </param>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle EnsureRectIsOnScreen(Screen screen, Rectangle bounds)
        {
            Rectangle newBounds = bounds;
            Rectangle screenBounds = screen.WorkingArea;

            // Make sure the bottom and right do not fall off the edge, by moving the bounds
            if (newBounds.Right > screenBounds.Right)
            {
                newBounds.X -= newBounds.Right - screenBounds.Right;
            }

            if (newBounds.Bottom > screenBounds.Bottom)
            {
                newBounds.Y -= newBounds.Bottom - screenBounds.Bottom;
            }

            // Make sure the top and left haven't fallen off, by moving
            if (newBounds.Left < screenBounds.Left)
            {
                newBounds.X = screenBounds.Left;
            }

            if (newBounds.Top < screenBounds.Top)
            {
                newBounds.Y = screenBounds.Top;
            }

            // Make sure that we are not too wide / tall, by resizing
            if (newBounds.Right > screenBounds.Right)
            {
                newBounds.Width -= newBounds.Right - screenBounds.Right;
            }

            if (newBounds.Bottom > screenBounds.Bottom)
            {
                newBounds.Height -= newBounds.Bottom - screenBounds.Bottom;
            }

            // All done.
            return newBounds;
        }

        /// <summary>
        /// Registers a form-wide hot key, and a callback for when the key is pressed.
        /// The callback must be an instance method on a Control. Whatever Form the Control
        /// is on will process the hotkey, as long as the Form is derived from PdnBaseForm.
        /// </summary>
        /// <param name="keys">
        /// The keys.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        public static void RegisterFormHotKey(Keys keys, Function<bool, Keys> callback)
        {
            IComponent targetAsComponent = callback.Target as IComponent;
            IHotKeyTarget targetAsHotKeyTarget = callback.Target as IHotKeyTarget;

            if (targetAsComponent == null && targetAsHotKeyTarget == null)
            {
                throw new ArgumentException("target instance must implement IComponent or IHotKeyTarget", "callback");
            }

            if (hotkeyRegistrar == null)
            {
                hotkeyRegistrar = new Dictionary<Keys, Function<bool, Keys>>();
            }

            Function<bool, Keys> theDelegate = null;

            if (hotkeyRegistrar.ContainsKey(keys))
            {
                theDelegate = hotkeyRegistrar[keys];
                theDelegate += callback;
                hotkeyRegistrar[keys] = theDelegate;
            }
            else
            {
                theDelegate = new Function<bool, Keys>(callback);
                hotkeyRegistrar.Add(keys, theDelegate);
            }

            if (targetAsComponent != null)
            {
                targetAsComponent.Disposed += TargetAsComponent_Disposed;
            }
            else
            {
                targetAsHotKeyTarget.Disposed += TargetAsHotKeyTarget_Disposed;
            }
        }

        /// <summary>
        /// The unregister form hot key.
        /// </summary>
        /// <param name="keys">
        /// The keys.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        public static void UnregisterFormHotKey(Keys keys, Function<bool, Keys> callback)
        {
            if (hotkeyRegistrar != null)
            {
                Function<bool, Keys> theDelegate = hotkeyRegistrar[keys];
                theDelegate -= callback;
                hotkeyRegistrar[keys] = theDelegate;

                IComponent targetAsComponent = callback.Target as IComponent;
                if (targetAsComponent != null)
                {
                    targetAsComponent.Disposed -= TargetAsComponent_Disposed;
                }

                IHotKeyTarget targetAsHotKeyTarget = callback.Target as IHotKeyTarget;
                if (targetAsHotKeyTarget != null)
                {
                    targetAsHotKeyTarget.Disposed -= TargetAsHotKeyTarget_Disposed;
                }

                if (theDelegate.GetInvocationList().Length == 0)
                {
                    hotkeyRegistrar.Remove(keys);
                }

                if (hotkeyRegistrar.Count == 0)
                {
                    hotkeyRegistrar = null;
                }
            }
        }

        /// <summary>
        /// The update all forms.
        /// </summary>
        public static void UpdateAllForms()
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    try
                    {
                        form.Update();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        /// <summary>
        /// The client bounds to window bounds.
        /// </summary>
        /// <param name="clientBounds">
        /// The client bounds.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/>.
        /// </returns>
        public Rectangle ClientBoundsToWindowBounds(Rectangle clientBounds)
        {
            Rectangle currentBounds = this.Bounds;
            Rectangle currentClientBounds = this.RectangleToScreen(this.ClientRectangle);

            Rectangle newWindowBounds =
                new Rectangle(
                    clientBounds.Left - (currentClientBounds.Left - currentBounds.Left), 
                    clientBounds.Top - (currentClientBounds.Top - currentBounds.Top), 
                    clientBounds.Width + (currentBounds.Width - currentClientBounds.Width), 
                    clientBounds.Height + (currentBounds.Height - currentClientBounds.Height));

            return newWindowBounds;
        }

        /// <summary>
        /// The client size to window size.
        /// </summary>
        /// <param name="clientSize">
        /// The client size.
        /// </param>
        /// <returns>
        /// The <see cref="Size"/>.
        /// </returns>
        public Size ClientSizeToWindowSize(Size clientSize)
        {
            Size baseClientSize = this.ClientSize;
            Size baseWindowSize = this.Size;

            int extraWidth = baseWindowSize.Width - baseClientSize.Width;
            int extraHeight = baseWindowSize.Height - baseClientSize.Height;

            Size windowSize = new Size(clientSize.Width + extraWidth, clientSize.Height + extraHeight);
            return windowSize;
        }

        /// <summary>
        /// The ensure form is on screen.
        /// </summary>
        public void EnsureFormIsOnScreen()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                return;
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                return;
            }

            Screen ourScreen;

            try
            {
                ourScreen = Screen.FromControl(this);
            }
            catch (Exception)
            {
                ourScreen = null;
            }

            if (ourScreen == null)
            {
                ourScreen = Screen.PrimaryScreen;
            }

            Rectangle currentBounds = this.Bounds;
            Rectangle newBounds = EnsureRectIsOnScreen(ourScreen, currentBounds);
            this.Bounds = newBounds;
        }

        /// <summary>
        /// The flash.
        /// </summary>
        public void Flash()
        {
            UI.FlashForm(this);
        }

        /// <summary>
        /// The load resources.
        /// </summary>
        public virtual void LoadResources()
        {
            if (!this.DesignMode)
            {
                string stringName = this.Name + ".Localized";
                string stringValue = this.StringsResourceManager.GetString(stringName);

                if (stringValue != null)
                {
                    try
                    {
                        bool boolValue = bool.Parse(stringValue);

                        if (boolValue)
                        {
                            this.LoadLocalizedResources();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// The relay process cmd key.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="keyData">
        /// The key data.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool RelayProcessCmdKey(ref Message msg, Keys keyData)
        {
            return this.ProcessCmdKeyData(keyData);
        }

        /// <summary>
        /// The restore window.
        /// </summary>
        public void RestoreWindow()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                UI.RestoreWindow(this);
            }
        }

        /// <summary>
        /// The window bounds to client bounds.
        /// </summary>
        /// <param name="windowBounds">
        /// The window bounds.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/>.
        /// </returns>
        public Rectangle WindowBoundsToClientBounds(Rectangle windowBounds)
        {
            Rectangle currentBounds = this.Bounds;
            Rectangle currentClientBounds = this.RectangleToScreen(this.ClientRectangle);

            Rectangle newClientBounds =
                new Rectangle(
                    windowBounds.Left + (currentClientBounds.Left - currentBounds.Left), 
                    windowBounds.Top + (currentClientBounds.Top - currentBounds.Top), 
                    windowBounds.Width - (currentBounds.Width - currentClientBounds.Width), 
                    windowBounds.Height - (currentBounds.Height - currentClientBounds.Height));

            return newClientBounds;
        }

        /// <summary>
        /// The window size to client size.
        /// </summary>
        /// <param name="windowSize">
        /// The window size.
        /// </param>
        /// <returns>
        /// The <see cref="Size"/>.
        /// </returns>
        public Size WindowSizeToClientSize(Size windowSize)
        {
            Size baseClientSize = this.ClientSize;
            Size baseWindowSize = this.Size;

            int extraWidth = baseWindowSize.Width - baseClientSize.Width;
            int extraHeight = baseWindowSize.Height - baseClientSize.Height;
            Size clientSize = new Size(windowSize.Width - extraWidth, windowSize.Height - extraHeight);

            return clientSize;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                    this.components = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// The on closing.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!e.Cancel)
            {
                this.ForceActiveTitleBar = false;
            }
        }

        /// <summary>
        /// The on handle created.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            BaseForm.EnableOpacityChanged += new EventHandler(this.EnableOpacityChangedHandler);
            UserSessions.SessionChanged += new EventHandler(this.UserSessions_SessionChanged);
            this.DecideOpacitySetting();
        }

        /// <summary>
        /// The on handle destroyed.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);

            BaseForm.EnableOpacityChanged -= new EventHandler(this.EnableOpacityChangedHandler);
            UserSessions.SessionChanged -= new EventHandler(this.UserSessions_SessionChanged);
        }

        /// <summary>
        /// The on help requested.
        /// </summary>
        /// <param name="hevent">
        /// The hevent.
        /// </param>
        protected override void OnHelpRequested(HelpEventArgs hevent)
        {
            if (!hevent.Handled)
            {
                Utility.ShowHelp(this);
                hevent.Handled = true;
            }

            base.OnHelpRequested(hevent);
        }

        /// <summary>
        /// The on load.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnLoad(EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.LoadResources();
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// The on moving.
        /// </summary>
        /// <param name="mea">
        /// The mea.
        /// </param>
        protected virtual void OnMoving(MovingEventArgs mea)
        {
            if (this.Moving != null)
            {
                this.Moving(this, mea);
            }
        }

        /// <summary>
        /// The on query end session.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected virtual void OnQueryEndSession(CancelEventArgs e)
        {
            if (this.QueryEndSession != null)
            {
                this.QueryEndSession(this, e);
            }
        }

        /// <summary>
        /// The on scroll.
        /// </summary>
        /// <param name="se">
        /// The se.
        /// </param>
        protected override void OnScroll(ScrollEventArgs se)
        {
            Thread.CurrentThread.Priority = this.originalPriority;
            base.OnScroll(se);
        }

        /// <summary>
        /// The on shown.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnShown(EventArgs e)
        {
            this.isShown = true;
            Tracing.LogFeature("ShowDialog(" + this.GetType().FullName + ")");
            base.OnShown(e);
        }

        /// <summary>
        /// The process cmd key.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="keyData">
        /// The key data.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool processed = this.ProcessCmdKeyData(keyData);

            if (!processed)
            {
                processed = base.ProcessCmdKey(ref msg, keyData);
            }

            return processed;
        }

        /// <summary>
        /// The wnd proc.
        /// </summary>
        /// <param name="m">
        /// The m.
        /// </param>
        protected override void WndProc(ref Message m)
        {
            if (this.formEx == null)
            {
                base.WndProc(ref m);
            }
            else if (!this.formEx.HandleParentWndProc(ref m))
            {
                this.OurWndProc(ref m);
            }
        }

        /// <summary>
        /// The application_ enter thread modal.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void Application_EnterThreadModal(object sender, EventArgs e)
        {
            Form activeForm = Form.ActiveForm;
            List<Form> allPeerForms = GetAllPeerForms(activeForm);
            parentModalForms.Push(allPeerForms);
        }

        /// <summary>
        /// The application_ leave thread modal.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void Application_LeaveThreadModal(object sender, EventArgs e)
        {
            parentModalForms.Pop();
        }

        /// <summary>
        /// The get all peer forms.
        /// </summary>
        /// <param name="form">
        /// The form.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private static List<Form> GetAllPeerForms(Form form)
        {
            if (form == null)
            {
                return new List<Form>();
            }

            if (form.Owner != null)
            {
                return GetAllPeerForms(form.Owner);
            }

            List<Form> forms = new List<Form>();
            forms.Add(form);
            forms.AddRange(form.OwnedForms);

            return forms;
        }

        /// <summary>
        /// The get concrete target.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private static object GetConcreteTarget(object target)
        {
            Delegate asDelegate = target as Delegate;

            if (asDelegate == null)
            {
                return target;
            }
            else
            {
                return GetConcreteTarget(asDelegate.Target);
            }
        }

        /// <summary>
        /// The is in parent modal forms.
        /// </summary>
        /// <param name="form">
        /// The form.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsInParentModalForms(Form form)
        {
            foreach (List<Form> formList in parentModalForms)
            {
                foreach (Form parentModalForm in formList)
                {
                    if (parentModalForm == form)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// The on enable opacity changed.
        /// </summary>
        private static void OnEnableOpacityChanged()
        {
            if (EnableOpacityChanged != null)
            {
                EnableOpacityChanged(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// The remove disposed target.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        private static void RemoveDisposedTarget(object sender)
        {
            // Control was disposed, but it never unregistered for its hotkeys!
            List<Keys> keysList = new List<Keys>(hotkeyRegistrar.Keys);

            foreach (Keys keys in keysList)
            {
                Function<bool, Keys> theMultiDelegate = hotkeyRegistrar[keys];

                foreach (Delegate theDelegate in theMultiDelegate.GetInvocationList())
                {
                    if (object.ReferenceEquals(theDelegate.Target, sender))
                    {
                        UnregisterFormHotKey(keys, (Function<bool, Keys>)theDelegate);
                    }
                }
            }
        }

        /// <summary>
        /// The target as component_ disposed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void TargetAsComponent_Disposed(object sender, EventArgs e)
        {
            ((IComponent)sender).Disposed -= TargetAsComponent_Disposed;
            RemoveDisposedTarget(sender);
        }

        /// <summary>
        /// The target as hot key target_ disposed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void TargetAsHotKeyTarget_Disposed(object sender, EventArgs e)
        {
            ((IHotKeyTarget)sender).Disposed -= TargetAsHotKeyTarget_Disposed;
            RemoveDisposedTarget(sender);
        }

        /// <summary>
        /// Decides whether or not to have opacity be enabled.
        /// </summary>
        private void DecideOpacitySetting()
        {
            if (UserSessions.IsRemote || !BaseForm.globalEnableOpacity || !this.EnableInstanceOpacity)
            {
                if (this.enableOpacity)
                {
                    try
                    {
                        UI.SetFormOpacity(this, 1.0);
                    }

                        // This fails in certain odd situations (bug #746), so we just eat the exception.
                    catch (System.ComponentModel.Win32Exception)
                    {
                    }
                }

                this.enableOpacity = false;
            }
            else
            {
                if (!this.enableOpacity)
                {
                    // This fails in certain odd situations (bug #746), so we just eat the exception.
                    try
                    {
                        UI.SetFormOpacity(this, this.ourOpacity);
                    }
                    catch (System.ComponentModel.Win32Exception)
                    {
                    }
                }

                this.enableOpacity = true;
            }
        }

        /// <summary>
        /// The enable opacity changed handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void EnableOpacityChangedHandler(object sender, EventArgs e)
        {
            this.DecideOpacitySetting();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // BaseForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "BaseForm";
            this.ResumeLayout(false);
        }

        /// <summary>
        /// The is target form active.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsTargetFormActive(object target)
        {
            Control targetControl = null;

            if (targetControl == null)
            {
                Control asControl = target as Control;

                if (asControl != null)
                {
                    targetControl = asControl;
                }
            }

            if (targetControl == null)
            {
                IFormAssociate asIFormAssociate = target as IFormAssociate;

                if (asIFormAssociate != null)
                {
                    targetControl = asIFormAssociate.AssociatedForm;
                }
            }

            // target is not a control, or a type of non-control that we recognize as hosted by a control
            if (targetControl == null)
            {
                return false;
            }

            Form targetForm = targetControl.FindForm();

            // target is not on a form
            if (targetForm == null)
            {
                return false;
            }

            // is the target on the currently active form?
            Form activeModalForm = CurrentModalForm;

            if (targetForm == activeModalForm)
            {
                return true;
            }

            // Nope.
            return false;
        }

        /// <summary>
        /// The load localized resources.
        /// </summary>
        private void LoadLocalizedResources()
        {
            this.LoadLocalizedResources(this.Name, this);
        }

        /// <summary>
        /// The load localized resources.
        /// </summary>
        /// <param name="baseName">
        /// The base name.
        /// </param>
        /// <param name="control">
        /// The control.
        /// </param>
        private void LoadLocalizedResources(string baseName, Control control)
        {
            // Text
            string textStringName = baseName + ".Text";
            string textString = this.StringsResourceManager.GetString(textStringName);

            if (textString != null)
            {
                control.Text = textString;
            }

            // Location
            string locationStringName = baseName + ".Location";
            string locationString = this.StringsResourceManager.GetString(locationStringName);

            if (locationString != null)
            {
                try
                {
                    int x;
                    int y;

                    this.ParsePair(locationString, out x, out y);
                    control.Location = new Point(x, y);
                }
                catch (Exception ex)
                {
                    Tracing.Ping(
                        locationStringName + " is invalid: " + locationString + ", exception: " + ex.ToString());
                }
            }

            // Size
            string sizeStringName = baseName + ".Size";
            string sizeString = this.StringsResourceManager.GetString(sizeStringName);

            if (sizeString != null)
            {
                try
                {
                    int width;
                    int height;

                    this.ParsePair(sizeString, out width, out height);
                    control.Size = new Size(width, height);
                }
                catch (Exception ex)
                {
                    Tracing.Ping(sizeStringName + " is invalid: " + sizeString + ", exception: " + ex.ToString());
                }
            }

            // Recurse
            foreach (Control child in control.Controls)
            {
                if (child.Name == null || child.Name.Length > 0)
                {
                    string newBaseName = baseName + "." + child.Name;
                    this.LoadLocalizedResources(newBaseName, child);
                }
                else
                {
                    Tracing.Ping(
                        "Name property not set for an instance of " + child.GetType().Name + " within " + baseName);
                }
            }
        }

        /// <summary>
        /// The on process cmd key relay.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnProcessCmdKeyRelay(object sender, FormEx.ProcessCmdKeyEventArgs e)
        {
            bool handled = e.Handled;

            if (!handled)
            {
                handled = this.ProcessCmdKeyData(e.KeyData);
                e.Handled = handled;
            }
        }

        /// <summary>
        /// The our wnd proc.
        /// </summary>
        /// <param name="m">
        /// The m.
        /// </param>
        private void OurWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0216: // WM_MOVING
                    unsafe
                    {
                        int* p = (int*)m.LParam;
                        Rectangle rect = Rectangle.FromLTRB(p[0], p[1], p[2], p[3]);

                        MovingEventArgs mea = new MovingEventArgs(rect);
                        this.OnMoving(mea);

                        p[0] = mea.Rectangle.Left;
                        p[1] = mea.Rectangle.Top;
                        p[2] = mea.Rectangle.Right;
                        p[3] = mea.Rectangle.Bottom;

                        m.Result = new IntPtr(1);
                    }

                    break;

                    // WinForms doesn't handle this message correctly and wrongly returns 0 instead of 1.
                case 0x0011: // WM_QUERYENDSESSION
                    CancelEventArgs e = new CancelEventArgs();
                    this.OnQueryEndSession(e);
                    m.Result = e.Cancel ? IntPtr.Zero : new IntPtr(1);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>
        /// The parse pair.
        /// </summary>
        /// <param name="theString">
        /// The the string.
        /// </param>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        private void ParsePair(string theString, out int x, out int y)
        {
            string[] split = theString.Split(',');
            x = int.Parse(split[0]);
            y = int.Parse(split[1]);
        }

        /// <summary>
        /// The process cmd key data.
        /// </summary>
        /// <param name="keyData">
        /// The key data.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ProcessCmdKeyData(Keys keyData)
        {
            bool shouldHandle = this.ShouldProcessHotKey(keyData);

            if (shouldHandle)
            {
                bool processed = this.ProcessFormHotKey(keyData);
                return processed;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The process form hot key.
        /// </summary>
        /// <param name="keyData">
        /// The key data.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ProcessFormHotKey(Keys keyData)
        {
            bool processed = false;

            if (this.processFormHotKeyMutex)
            {
                processed = true;
            }
            else
            {
                this.processFormHotKeyMutex = true;

                try
                {
                    if (hotkeyRegistrar != null && hotkeyRegistrar.ContainsKey(keyData))
                    {
                        Function<bool, Keys> theDelegate = hotkeyRegistrar[keyData];
                        Delegate[] invokeList = theDelegate.GetInvocationList();

                        for (int i = invokeList.Length - 1; i >= 0; --i)
                        {
                            Function<bool, Keys> invokeMe = (Function<bool, Keys>)invokeList[i];
                            object concreteTarget = GetConcreteTarget(invokeMe.Target);

                            if (this.IsTargetFormActive(concreteTarget))
                            {
                                bool result = invokeMe(keyData);

                                if (result)
                                {
                                    // The callback handled the key.
                                    processed = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                finally
                {
                    this.processFormHotKeyMutex = false;
                }
            }

            return processed;
        }

        /// <summary>
        /// The real wnd proc.
        /// </summary>
        /// <param name="m">
        /// The m.
        /// </param>
        private void RealWndProc(ref Message m)
        {
            this.OurWndProc(ref m);
        }

        /// <summary>
        /// The should process hot key.
        /// </summary>
        /// <param name="keys">
        /// The keys.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ShouldProcessHotKey(Keys keys)
        {
            Keys keyOnly = keys & ~Keys.Modifiers;

            if (keyOnly == Keys.Back || keyOnly == Keys.Delete || keyOnly == Keys.Left || keyOnly == Keys.Right
                || keyOnly == Keys.Up || keyOnly == Keys.Down || keys == (Keys.Control | Keys.A) || // select all
                keys == (Keys.Control | Keys.Z) || // undo
                keys == (Keys.Control | Keys.Y) || // redo
                keys == (Keys.Control | Keys.X) || // cut
                keys == (Keys.Control | Keys.C) || // copy
                keys == (Keys.Control | Keys.V) || // paste
                keys == (Keys.Shift | Keys.Delete) || // cut (left-handed)
                keys == (Keys.Control | Keys.Insert) || // copy (left-handed)
                keys == (Keys.Shift | Keys.Insert) // paste (left-handed)
                )
            {
                Control focused = Utility.FindFocus();

                if (focused is TextBox || focused is ComboBox || focused is UpDownBase)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The user sessions_ session changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void UserSessions_SessionChanged(object sender, EventArgs e)
        {
            this.DecideOpacitySetting();
        }

        #endregion
    }
}