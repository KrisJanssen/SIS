//using SIS.SystemLayer;
//using System;
//using System.Drawing;
//using System.Collections;
//using System.ComponentModel;
//using System.Windows.Forms;
//using Microsoft.Win32;

//namespace SIS.MDITemplate
//{

//    // TODO: move
//    internal delegate bool CmdKeysEventHandler(object sender, ref Message msg, Keys keyData);

//    internal class FloatingToolForm : BaseForm, ISnapObstacleHost
//    {
//        // Fields
//        private IContainer components;
//        private ControlEventHandler controlAddedDelegate;
//        private ControlEventHandler controlRemovedDelegate;
//        private KeyEventHandler keyUpDelegate;
//        private bool moving;
//        private Size movingCursorDelta = Size.Empty;
//        private SnapObstacleController snapObstacle;

//        // Events
//        public event CmdKeysEventHandler ProcessCmdKeyEvent;

//        public event EventHandler RelinquishFocus;

//        // Methods
//        public FloatingToolForm()
//        {
//            base.KeyPreview = true;
//            this.controlAddedDelegate = new ControlEventHandler(this.ControlAddedHandler);
//            this.controlRemovedDelegate = new ControlEventHandler(this.ControlRemovedHandler);
//            this.keyUpDelegate = new KeyEventHandler(this.KeyUpHandler);
//            base.ControlAdded += this.controlAddedDelegate;
//            base.ControlRemoved += this.controlRemovedDelegate;
//            this.InitializeComponent();
//            try
//            {
//                UserSessions.SessionChanged += new EventHandler(this.UserSessions_SessionChanged);
//                SystemEvents.DisplaySettingsChanged += new EventHandler(this.SystemEvents_DisplaySettingsChanged);
//            }
//            catch (Exception)
//            {
//            }
//        }

//        private void ControlAddedHandler(object sender, ControlEventArgs e)
//        {
//            e.Control.ControlAdded += this.controlAddedDelegate;
//            e.Control.ControlRemoved += this.controlRemovedDelegate;
//            e.Control.KeyUp += this.keyUpDelegate;
//        }

//        private void ControlRemovedHandler(object sender, ControlEventArgs e)
//        {
//            e.Control.ControlAdded -= this.controlAddedDelegate;
//            e.Control.ControlRemoved -= this.controlRemovedDelegate;
//            e.Control.KeyUp -= this.keyUpDelegate;
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                if (this.components != null)
//                {
//                    this.components.Dispose();
//                    this.components = null;
//                }
//                try
//                {
//                    UserSessions.SessionChanged -= new EventHandler(this.UserSessions_SessionChanged);
//                    SystemEvents.DisplaySettingsChanged -= new EventHandler(this.SystemEvents_DisplaySettingsChanged);
//                }
//                catch (Exception)
//                {
//                }
//            }
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            base.AutoScaleDimensions = new SizeF(96f, 96f);
//            base.AutoScaleMode = AutoScaleMode.Dpi;
//            base.ClientSize = new Size(0x124, 0x10f);
//            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
//            base.MaximizeBox = false;
//            base.MinimizeBox = false;
//            base.Name = "FloatingToolForm";
//            base.ShowInTaskbar = false;
//            base.SizeGripStyle = SizeGripStyle.Hide;
//            base.ForceActiveTitleBar = true;
//        }

//        private void KeyUpHandler(object sender, KeyEventArgs e)
//        {
//            if (!e.Handled)
//            {
//                this.OnKeyUp(e);
//            }
//        }

//        protected override void OnClick(EventArgs e)
//        {
//            this.OnRelinquishFocus();
//            base.OnClick(e);
//        }

//        protected override void OnEnabledChanged(EventArgs e)
//        {
//            if (this.snapObstacle != null)
//            {
//                this.snapObstacle.Enabled = base.Enabled;
//            }
//            base.OnEnabledChanged(e);
//        }

//        protected override void OnLoad(EventArgs e)
//        {
//            ISnapManagerHost smh = base.Owner as ISnapManagerHost;
//            if (smh != null)
//            {
//                smh.SnapManager.AddSnapObstacle(this);
//            }
//            base.OnLoad(e);
//        }

//        protected override void OnMove(EventArgs e)
//        {
//            this.UpdateSnapObstacleBounds();
//            base.OnMove(e);
//        }

//        protected override void OnMoving(MovingEventArgs mea)
//        {
//            ISnapManagerHost snapHost = base.Owner as ISnapManagerHost;
//            if (snapHost != null)
//            {
//                SnapManager sm = snapHost.SnapManager;
//                if (!this.moving)
//                {
//                    this.movingCursorDelta = new Size(Cursor.Position.X - mea.Rectangle.X, Cursor.Position.Y - mea.Rectangle.Y);
//                    this.moving = true;
//                }
//                mea.Rectangle = new Rectangle(Cursor.Position.X - this.movingCursorDelta.Width, Cursor.Position.Y - this.movingCursorDelta.Height, mea.Rectangle.Width, mea.Rectangle.Height);
//                this.snapObstacle.SetBounds(mea.Rectangle.ToInt32Rect());
//                Int32Point pt = mea.Rectangle.Location;
//                Int32Rect newRect = Int32RectUtil.From(sm.AdjustObstacleDestination(this.SnapObstacle, pt), mea.Rectangle.Size.ToInt32Size());
//                this.snapObstacle.SetBounds(newRect);
//                mea.Rectangle = newRect.ToGdipRectangle();
//            }
//            base.OnMoving(mea);
//        }

//        protected virtual void OnRelinquishFocus()
//        {
//            if (!MenuStripEx.IsAnyMenuActive && (this.RelinquishFocus != null))
//            {
//                this.RelinquishFocus(this, EventArgs.Empty);
//            }
//        }

//        protected override void OnResize(EventArgs e)
//        {
//            this.UpdateSnapObstacleBounds();
//            base.OnResize(e);
//            this.UpdateParking();
//        }

//        protected override void OnResizeBegin(EventArgs e)
//        {
//            this.UpdateSnapObstacleBounds();
//            this.UpdateParking();
//            base.OnResizeBegin(e);
//        }

//        protected override void OnResizeEnd(EventArgs e)
//        {
//            this.moving = false;
//            this.UpdateSnapObstacleBounds();
//            this.UpdateParking();
//            base.OnResizeEnd(e);
//            this.OnRelinquishFocus();
//        }

//        protected override void OnSizeChanged(EventArgs e)
//        {
//            this.UpdateSnapObstacleBounds();
//            this.UpdateParking();
//            base.OnSizeChanged(e);
//        }

//        protected override void OnVisibleChanged(EventArgs e)
//        {
//            if (base.Visible)
//            {
//                base.EnsureFormIsOnScreen();
//            }
//            base.OnVisibleChanged(e);
//        }

//        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
//        {
//            bool result = false;
//            if (keyData.IsArrowKey())
//            {
//                KeyEventArgs kea = new KeyEventArgs(keyData);
//                if (msg.Msg == 0x100)
//                {
//                    this.OnKeyDown(kea);
//                    return kea.Handled;
//                }
//            }
//            else if (this.ProcessCmdKeyEvent != null)
//            {
//                result = this.ProcessCmdKeyEvent(this, ref msg, keyData);
//            }
//            if (!result)
//            {
//                result = base.ProcessCmdKey(ref msg, keyData);
//            }
//            return result;
//        }

//        private void SnapObstacle_BoundsChangeRequested(object sender, HandledEventArgs<Rectangle> e)
//        {
//            base.Bounds = e.Data;
//        }

//        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
//        {
//            if (base.Visible && base.IsShown)
//            {
//                base.EnsureFormIsOnScreen();
//            }
//        }

//        private void UpdateParking()
//        {
//            if (((base.FormBorderStyle == FormBorderStyle.Fixed3D) || (base.FormBorderStyle == FormBorderStyle.FixedDialog)) || ((base.FormBorderStyle == FormBorderStyle.FixedSingle) || (base.FormBorderStyle == FormBorderStyle.FixedToolWindow)))
//            {
//                ISnapManagerHost ismh = base.Owner as ISnapManagerHost;
//                if (ismh != null)
//                {
//                    ismh.SnapManager.ReparkObstacle(this);
//                }
//            }
//        }

//        private void UpdateSnapObstacleBounds()
//        {
//            if (this.snapObstacle != null)
//            {
//                this.snapObstacle.SetBounds(this.Bounds());
//            }
//        }

//        private void UserSessions_SessionChanged(object sender, EventArgs e)
//        {
//            if (base.Visible && base.IsShown)
//            {
//                base.EnsureFormIsOnScreen();
//            }
//        }

//        // Properties
//        public SnapObstacle SnapObstacle
//        {
//            get
//            {
//                if (this.snapObstacle == null)
//                {
//                    int distancePadding = UI.GetExtendedFrameBounds(this);
//                    int distance = 3 + distancePadding;
//                    this.snapObstacle = new SnapObstacleController(base.Name, this.Bounds(), SnapRegion.Exterior, false, 15, distance);
//                    this.snapObstacle.BoundsChangeRequested += new HandledEventHandler<Int32Rect>(this.SnapObstacle_BoundsChangeRequested);
//                }
//                return this.snapObstacle;
//            }
//        }
//    }
//}
 
