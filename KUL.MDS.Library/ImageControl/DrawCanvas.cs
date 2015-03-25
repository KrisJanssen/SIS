using System;
using System.Drawing;
using System.Windows.Forms;
using SIS.SystemLayer;

namespace SIS.Library
{
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class DrawCanvas : UserControl
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Members.

        private int m_iDrawHeight;
        private int m_iDrawWidth;
        private int m_iMouseXPos;
        private int m_iMouseYPos;
        private bool m_bZoomOnMouseWheel;
        private bool m_bPanMode;
        private bool m_bStretchImageToFit;
        private double m_dZoomFactor;
        private string m_sImageText;
        private Bitmap m_bmpImage;
        private MouseButtons m_mbtnPanMouseButton;
        private Pen m_penSelectPen;
        private Point m_ptOrigin;
        private Point m_ptStartPoint;
        private Point m_ptCenterPoint;
        private Point m_ptEndPoint;
        private Rectangle m_rctSourceRectangle;
        private Rectangle m_rctDestRectangle;
        private Rectangle m_rctSelectedRectangle;
        private Size m_szApparentImageSize;

        #endregion

        #region Properties.

        public Size ApparentImageSize
        {
            get
            {
                return this.m_szApparentImageSize;
            }
        }

        public Image Image
        {
            get
            {
                return this.m_bmpImage;
            }
            set
            {
                if (this.m_bmpImage != null)
                {
                    this.m_bmpImage.Dispose();
                    this.m_rctSelectedRectangle = Rectangle.Empty;
                    this.m_ptOrigin = new Point(0, 0);
                    this.m_szApparentImageSize = new Size(0, 0);
                    this.m_dZoomFactor = 1;
                }
                if (value == null)
                {
                    m_bmpImage = null;
                    this.Invalidate();
                }
                else
                {
                    Rectangle r = new Rectangle(0, 0, value.Width, value.Height);
                    this.m_bmpImage = new Bitmap(value);
                    this.Invalidate();
                }

            }
        }

        public string ImageText
        {
            get
            {
                return this.m_sImageText;
            }
            set
            {
                this.m_sImageText = value;
                this.Invalidate();
            }
        }

        public int MouseXPos
        {
            get
            {
                return this.m_iMouseXPos;
            }
        }

        public int MouseYPos
        {
            get
            {
                return this.m_iMouseYPos;
            }
        }

        public Point Origin
        {
            get
            {
                return this.m_ptOrigin;
            }
            set
            {
                this.m_ptOrigin = value;
                this.Invalidate();
            }
        }
        
        public MouseButtons PanButton
        {
            get
            {
                return this.m_mbtnPanMouseButton;
            }
            set
            {
                this.m_mbtnPanMouseButton = value;
            }
        }

        public bool PanMode
        {
            get
            {
                return this.m_bPanMode;
            }
            set
            {
                this.m_bPanMode = value;
            }
        }

        Rectangle SelectedRectangle
        {
            get
            {
                return this.m_rctSelectedRectangle;
            }
            set
            {
                this.m_rctSelectedRectangle = value;
                this.Invalidate();
            }
        }

        public bool StretchImageToFit
        {
            get
            {
                return this.m_bStretchImageToFit;
            }
            set
            {
                this.m_bStretchImageToFit = value;
                this.Invalidate();
            }
        }

        public double ZoomFactor
        {
            get
            {
                return this.m_dZoomFactor;
            }
            set
            {
                this.m_dZoomFactor = value;

                if ((this.m_dZoomFactor > 20))
                {
                    this.m_dZoomFactor = 20;
                }
                if ((m_dZoomFactor < 0.05))
                {
                    this.m_dZoomFactor = 0.05;
                }
                if (!(this.m_bmpImage == null))
                {
                    this.m_szApparentImageSize.Height = (int)(this.m_bmpImage.Height * this.m_dZoomFactor);
                    this.m_szApparentImageSize.Width = (int)(this.m_bmpImage.Width * this.m_dZoomFactor);
                    ComputeDrawingArea();
                    CheckBounds();
                }
                this.Invalidate();
            }
        }

        public bool ZoomOnMouseWheel
        {
            get
            {
                return this.m_bZoomOnMouseWheel;
            }
            set
            {
                this.m_bZoomOnMouseWheel = value;
            }
        }

        #endregion

        #region Events.

        public event EventHandler OnScrollPositionsChanged;
        public event EventHandler OnMousePositionChanged;
        public event EventHandler OnPositionSelected;

        #endregion

        #region Methods.

        public DrawCanvas()
        {
            //  This call is required by the Windows Form Designer.
            InitializeComponent();

            //  Add any initialization after the InitializeComponent() call.
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);

            this.m_ptOrigin = new System.Drawing.Point(0, 0);
            this.m_rctSourceRectangle = Rectangle.Empty;
            this.m_rctDestRectangle = Rectangle.Empty;

            // Initialize some members.
            this.m_dZoomFactor = 1;
            this.m_szApparentImageSize = new Size(0, 0);
            this.m_bPanMode = true;
            this.m_bStretchImageToFit = false;
            this.m_bZoomOnMouseWheel = true;
            this.m_penSelectPen = new Pen(Color.Blue, 2);
            this.m_mbtnPanMouseButton = MouseButtons.Left;
        }

        private void CheckBounds()
        {
            if (!(this.m_bmpImage == null))
            {

                // Make sure we don't go out of bounds
                if ((this.m_ptOrigin.X < 0))
                {
                    this.m_ptOrigin.X = 0;
                }
                if ((this.m_ptOrigin.Y < 0))
                {
                    this.m_ptOrigin.Y = 0;
                }
                if ((this.m_ptOrigin.X
                            > (this.m_bmpImage.Width
                            - (this.ClientSize.Width / this.m_dZoomFactor))))
                {
                    this.m_ptOrigin.X = (m_bmpImage.Width
                                - (int)(this.ClientSize.Width / this.m_dZoomFactor));
                }
                if ((this.m_ptOrigin.Y
                            > (this.m_bmpImage.Height
                            - (int)(this.ClientSize.Height / this.m_dZoomFactor))))
                {
                    this.m_ptOrigin.Y = (m_bmpImage.Height
                                - (int)(this.ClientSize.Height / this.m_dZoomFactor));
                }
                if ((this.m_ptOrigin.X < 0))
                {
                    this.m_ptOrigin.X = 0;
                }
                if ((this.m_ptOrigin.Y < 0))
                {
                    this.m_ptOrigin.Y = 0;
                }
            }
        }

        private void DrawRectangle(MouseEventArgs __mevargsE)
        {
            if (new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height).Contains(PointToClient(Cursor.Position)))
            {
                int _iWidth = System.Math.Abs((this.m_ptStartPoint.X - __mevargsE.X));
                int _iHeight = System.Math.Abs((this.m_ptStartPoint.Y - __mevargsE.Y));
                Point _ptUpperLeft;

                // need to determine the  upper left corner of the rectangel regardless of whether it's 
                // the start point or the end point, or other.
                _ptUpperLeft = new Point(System.Math.Min(this.m_ptStartPoint.X, __mevargsE.X), System.Math.Min(this.m_ptStartPoint.Y, __mevargsE.Y));
                this.m_rctSelectedRectangle = new System.Drawing.Rectangle(_ptUpperLeft.X, _ptUpperLeft.Y, _iWidth, _iHeight);
            }
        }

        public void FitToScreen()
        {
            this.m_bStretchImageToFit = false;
            this.m_ptOrigin = new Point(0, 0);

            if (!(this.m_bmpImage == null))
            {
                double _dZoomFactor;

                double _dXZoom = (double)this.ClientSize.Width / (double)this.m_bmpImage.Width;
                double _dYZoom = (double)this.ClientSize.Height / (double)this.m_bmpImage.Height;

                _dZoomFactor = Math.Min(_dXZoom, _dYZoom);

                if ((Math.Abs((_dZoomFactor - this.m_dZoomFactor)) > 0.01))
                {
                    this.ZoomFactor = _dZoomFactor;
                }
            }
        }

        private void ZoomImage(bool ZoomIn)
        {
            //  Get center point
            this.m_ptCenterPoint.X = (
                this.m_ptOrigin.X + (int)Math.Round(((double)this.m_rctSourceRectangle.Width / (double)2), 0));
            this.m_ptCenterPoint.Y = (
                this.m_ptOrigin.Y + (int)Math.Round(((double)this.m_rctSourceRectangle.Height / (double)2), 0));

            // set new zoomfactor
            if (ZoomIn)
            {
                this.ZoomFactor = Math.Round((ZoomFactor * 1.1), 2);
            }
            else
            {
                this.ZoomFactor = Math.Round((ZoomFactor * 0.9), 2);
            }

            // Reset the origin to maintain center point
            this.m_ptOrigin.X = (
                m_ptCenterPoint.X - (int)Math.Round(((double)ClientSize.Width / (m_dZoomFactor / (double)2)), 0));
            this.m_ptOrigin.Y = (
                m_ptCenterPoint.Y - (int)Math.Round(((double)ClientSize.Height / (m_dZoomFactor / (double)2)), 0));

            CheckBounds();

            if (this.OnScrollPositionsChanged != null)
            {
                this.OnScrollPositionsChanged(this, new EventArgs());
                Tracing.Ping("OnScrollPositionsChanged raised here!");
            }
        }

        public void ZoomIn()
        {
            this.ZoomImage(true);
        }

        public void ZoomOut()
        {
            this.ZoomImage(false);
        }

        private void ZoomSelection()
        {
            if (!(this.m_bmpImage == null || this.m_rctSelectedRectangle == Rectangle.Empty))
            {
                try
                {
                    Point _ptNewOrigin = new Point(
                        (this.Origin.X + (int)(this.m_rctSelectedRectangle.X / this.m_dZoomFactor)),
                        (this.Origin.Y + (int)(this.m_rctSelectedRectangle.Y / this.m_dZoomFactor)));

                    double _dNewZoomFactor;

                    if ((this.m_rctSelectedRectangle.Width > this.m_rctSelectedRectangle.Height))
                    {
                        _dNewZoomFactor = ((double)ClientSize.Width / ((double)SelectedRectangle.Width / ZoomFactor));
                    }
                    else
                    {
                        _dNewZoomFactor = ((double)ClientSize.Height / ((double)SelectedRectangle.Height / ZoomFactor));
                    }

                    this.m_ptOrigin = _ptNewOrigin;
                    this.m_dZoomFactor = _dNewZoomFactor;
                }

                catch (Exception ex)
                {
                    throw;
                }

                this.m_rctSelectedRectangle = Rectangle.Empty;
            }
        }

        private void ComputeDrawingArea()
        {
            this.m_iDrawHeight = (int)Math.Round(((double)this.ClientSize.Height / m_dZoomFactor), 0);
            this.m_iDrawWidth = (int)Math.Round(((double)this.ClientSize.Width / m_dZoomFactor), 0);
        }

        #region Mouspanning.

        // TODO: Clean this up and make it work!
        private void DrawCanvas_MouseDown(object __oSender, MouseEventArgs __mevargsE)
        {
            if (!(this.m_bmpImage == null))
            {
                this.m_ptEndPoint = Point.Empty;
                this.m_rctSelectedRectangle = Rectangle.Empty;
                this.m_ptStartPoint = new Point(__mevargsE.X, __mevargsE.Y);
                this.Focus();
            }
        }

        private void DrawCanvas_MouseUp(object sender, MouseEventArgs __mevargsE)
        {
            if (!(m_bmpImage == null))
            {
                if (!this.m_bPanMode)
                {
                    this.m_ptEndPoint = new Point(__mevargsE.X, __mevargsE.Y);

                    if (!(this.m_rctSelectedRectangle == null))
                    {
                        ZoomSelection();
                    }
                }
            }
        }

        #endregion

        public void RotateFlip(RotateFlipType __rftpRotateFlipType)
        {
            if (!(this.m_bmpImage == null))
            {
                this.m_bmpImage.RotateFlip(__rftpRotateFlipType);
                this.Invalidate();
            }
        }

        protected override void OnMouseEnter(EventArgs __evargsE)
        {
            //this.Focus();
            base.OnMouseEnter(__evargsE);
        }

        protected override void OnMouseMove(MouseEventArgs __mevargsE)
        {
            if (!(this.m_bmpImage == null))
            {
                if ((__mevargsE.Button == this.m_mbtnPanMouseButton))
                {
                    int _iDeltaX = (this.m_ptStartPoint.X - __mevargsE.X);
                    int _iDeltaY = (this.m_ptStartPoint.Y - __mevargsE.Y);

                    if (this.m_bPanMode)
                    {
                        // Set the origin of the new image
                        this.m_ptOrigin.X = (
                            this.m_ptOrigin.X + (int)Math.Round(((double)_iDeltaX / this.m_dZoomFactor), 0));
                        this.m_ptOrigin.Y = (
                            this.m_ptOrigin.Y + (int)Math.Round(((double)_iDeltaY / this.m_dZoomFactor), 0));

                        CheckBounds();

                        // reset the startpoints
                        this.m_ptStartPoint.X = __mevargsE.X;
                        this.m_ptStartPoint.Y = __mevargsE.Y;

                        // Force a paint
                        this.Invalidate();

                        if (this.OnScrollPositionsChanged != null)
                        {
                            this.OnScrollPositionsChanged(this, new EventArgs());
                            Tracing.Ping("OnScrollPositionsChanged raised here!");
                        }
                    }
                    else
                    {
                        DrawRectangle(__mevargsE);
                    }
                }
            }

            this.m_iMouseXPos = __mevargsE.X;
            this.m_iMouseYPos = __mevargsE.Y;

            if (this.OnMousePositionChanged != null)
            {
                this.OnMousePositionChanged(this, new EventArgs());
            }

            base.OnMouseMove(__mevargsE);
        }

        protected override void OnMouseWheel(MouseEventArgs __mevargsE)
        {
            if (this.m_bZoomOnMouseWheel)
            {
                // set new zoomfactor
                if ((__mevargsE.Delta > 0))
                {
                    this.ZoomImage(true);
                }
                else if ((__mevargsE.Delta < 0))
                {
                    this.ZoomImage(false);
                }

                base.OnMouseWheel(__mevargsE);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);

            if ((this.m_bmpImage == null))
            {
                return;
            }

            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            if (this.m_bStretchImageToFit)
            {
                this.m_rctSourceRectangle = new System.Drawing.Rectangle(
                    0,
                    0,
                    this.m_bmpImage.Width,
                    this.m_bmpImage.Height);
            }
            else
            {
                this.m_rctSourceRectangle = new System.Drawing.Rectangle(
                    this.m_ptOrigin.X,
                    this.m_ptOrigin.Y,
                    this.m_iDrawWidth,
                    this.m_iDrawHeight);
            }

            e.Graphics.DrawImage(
                this.m_bmpImage,
                this.m_rctDestRectangle,
                this.m_rctSourceRectangle,
                GraphicsUnit.Pixel);

            if (!this.m_bPanMode && this.m_rctSelectedRectangle != null)
            {
                e.Graphics.DrawRectangle(
                    this.m_penSelectPen,
                    this.m_rctSelectedRectangle);
            }

            Point _ptStringPosition = new Point(1, 1);

            System.Drawing.Brush _brshBrush = Brushes.White;

            e.Graphics.DrawString(
                this.m_sImageText,
                System.Drawing.SystemFonts.DefaultFont,
                _brshBrush,
                _ptStringPosition.X,
                _ptStringPosition.Y);


            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.m_rctDestRectangle = new System.Drawing.Rectangle(
                0,
                0,
                this.ClientSize.Width,
                this.ClientSize.Height);

            this.ComputeDrawingArea();
            base.OnSizeChanged(e);
        }

        private void DrawCanvas_Resize(object __oSender,EventArgs __evargsE)
        {
            this.ComputeDrawingArea();

            if (this.StretchImageToFit)
            {
                this.Invalidate();
            }
        }

        private void DrawCanvas_MouseDoubleClick(object _oSender, MouseEventArgs __mevargsE)
        {
            if (this.OnPositionSelected != null)
            {
                this.OnPositionSelected(this, new EventArgs());
            }
        }

        #endregion  
    }

}