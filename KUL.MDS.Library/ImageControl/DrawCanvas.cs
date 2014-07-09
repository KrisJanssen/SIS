// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DrawCanvas.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The draw canvas.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Library.ImageControl
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using log4net;

    using SIS.Systemlayer;

    /// <summary>
    /// The draw canvas.
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class DrawCanvas : UserControl
    {
        #region Static Fields

        /// <summary>
        /// The _logger.
        /// </summary>
        private static readonly ILog _logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Fields

        /// <summary>
        /// The m_b pan mode.
        /// </summary>
        private bool m_bPanMode;

        /// <summary>
        /// The m_b stretch image to fit.
        /// </summary>
        private bool m_bStretchImageToFit;

        /// <summary>
        /// The m_b zoom on mouse wheel.
        /// </summary>
        private bool m_bZoomOnMouseWheel;

        /// <summary>
        /// The m_bmp image.
        /// </summary>
        private Bitmap m_bmpImage;

        /// <summary>
        /// The m_d zoom factor.
        /// </summary>
        private double m_dZoomFactor;

        /// <summary>
        /// The m_i draw height.
        /// </summary>
        private int m_iDrawHeight;

        /// <summary>
        /// The m_i draw width.
        /// </summary>
        private int m_iDrawWidth;

        /// <summary>
        /// The m_i mouse x pos.
        /// </summary>
        private int m_iMouseXPos;

        /// <summary>
        /// The m_i mouse y pos.
        /// </summary>
        private int m_iMouseYPos;

        /// <summary>
        /// The m_mbtn pan mouse button.
        /// </summary>
        private MouseButtons m_mbtnPanMouseButton;

        /// <summary>
        /// The m_pen select pen.
        /// </summary>
        private Pen m_penSelectPen;

        /// <summary>
        /// The m_pt center point.
        /// </summary>
        private Point m_ptCenterPoint;

        /// <summary>
        /// The m_pt end point.
        /// </summary>
        private Point m_ptEndPoint;

        /// <summary>
        /// The m_pt origin.
        /// </summary>
        private Point m_ptOrigin;

        /// <summary>
        /// The m_pt start point.
        /// </summary>
        private Point m_ptStartPoint;

        /// <summary>
        /// The m_rct dest rectangle.
        /// </summary>
        private Rectangle m_rctDestRectangle;

        /// <summary>
        /// The m_rct selected rectangle.
        /// </summary>
        private Rectangle m_rctSelectedRectangle;

        /// <summary>
        /// The m_rct source rectangle.
        /// </summary>
        private Rectangle m_rctSourceRectangle;

        /// <summary>
        /// The m_s image text.
        /// </summary>
        private string m_sImageText;

        /// <summary>
        /// The m_sz apparent image size.
        /// </summary>
        private Size m_szApparentImageSize;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawCanvas"/> class.
        /// </summary>
        public DrawCanvas()
        {
            // This call is required by the Windows Form Designer.
            this.InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
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

        #endregion

        #region Public Events

        /// <summary>
        /// The on mouse position changed.
        /// </summary>
        public event EventHandler OnMousePositionChanged;

        /// <summary>
        /// The on position selected.
        /// </summary>
        public event EventHandler OnPositionSelected;

        /// <summary>
        /// The on scroll positions changed.
        /// </summary>
        public event EventHandler OnScrollPositionsChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the apparent image size.
        /// </summary>
        public Size ApparentImageSize
        {
            get
            {
                return this.m_szApparentImageSize;
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
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
                    this.m_bmpImage = null;
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

        /// <summary>
        /// Gets or sets the image text.
        /// </summary>
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

        /// <summary>
        /// Gets the mouse x pos.
        /// </summary>
        public int MouseXPos
        {
            get
            {
                return this.m_iMouseXPos;
            }
        }

        /// <summary>
        /// Gets the mouse y pos.
        /// </summary>
        public int MouseYPos
        {
            get
            {
                return this.m_iMouseYPos;
            }
        }

        /// <summary>
        /// Gets or sets the origin.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the pan button.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether pan mode.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether stretch image to fit.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the zoom factor.
        /// </summary>
        public double ZoomFactor
        {
            get
            {
                return this.m_dZoomFactor;
            }

            set
            {
                this.m_dZoomFactor = value;

                if (this.m_dZoomFactor > 20)
                {
                    this.m_dZoomFactor = 20;
                }

                if (this.m_dZoomFactor < 0.05)
                {
                    this.m_dZoomFactor = 0.05;
                }

                if (!(this.m_bmpImage == null))
                {
                    this.m_szApparentImageSize.Height = (int)(this.m_bmpImage.Height * this.m_dZoomFactor);
                    this.m_szApparentImageSize.Width = (int)(this.m_bmpImage.Width * this.m_dZoomFactor);
                    this.ComputeDrawingArea();
                    this.CheckBounds();
                }

                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether zoom on mouse wheel.
        /// </summary>
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

        #region Properties

        /// <summary>
        /// Gets or sets the selected rectangle.
        /// </summary>
        private Rectangle SelectedRectangle
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

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The fit to screen.
        /// </summary>
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

                if (Math.Abs(_dZoomFactor - this.m_dZoomFactor) > 0.01)
                {
                    this.ZoomFactor = _dZoomFactor;
                }
            }
        }

        /// <summary>
        /// The rotate flip.
        /// </summary>
        /// <param name="__rftpRotateFlipType">
        /// The __rftp rotate flip type.
        /// </param>
        public void RotateFlip(RotateFlipType __rftpRotateFlipType)
        {
            if (!(this.m_bmpImage == null))
            {
                this.m_bmpImage.RotateFlip(__rftpRotateFlipType);
                this.Invalidate();
            }
        }

        /// <summary>
        /// The zoom in.
        /// </summary>
        public void ZoomIn()
        {
            this.ZoomImage(true);
        }

        /// <summary>
        /// The zoom out.
        /// </summary>
        public void ZoomOut()
        {
            this.ZoomImage(false);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on mouse enter.
        /// </summary>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
        protected override void OnMouseEnter(EventArgs __evargsE)
        {
            // this.Focus();
            base.OnMouseEnter(__evargsE);
        }

        /// <summary>
        /// The on mouse move.
        /// </summary>
        /// <param name="__mevargsE">
        /// The __mevargs e.
        /// </param>
        protected override void OnMouseMove(MouseEventArgs __mevargsE)
        {
            if (!(this.m_bmpImage == null))
            {
                if (__mevargsE.Button == this.m_mbtnPanMouseButton)
                {
                    int _iDeltaX = this.m_ptStartPoint.X - __mevargsE.X;
                    int _iDeltaY = this.m_ptStartPoint.Y - __mevargsE.Y;

                    if (this.m_bPanMode)
                    {
                        // Set the origin of the new image
                        this.m_ptOrigin.X = this.m_ptOrigin.X
                                             + (int)Math.Round(((double)_iDeltaX / this.m_dZoomFactor), 0);
                        this.m_ptOrigin.Y = this.m_ptOrigin.Y
                                             + (int)Math.Round(((double)_iDeltaY / this.m_dZoomFactor), 0);

                        this.CheckBounds();

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
                        this.DrawRectangle(__mevargsE);
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

        /// <summary>
        /// The on mouse wheel.
        /// </summary>
        /// <param name="__mevargsE">
        /// The __mevargs e.
        /// </param>
        protected override void OnMouseWheel(MouseEventArgs __mevargsE)
        {
            if (this.m_bZoomOnMouseWheel)
            {
                // set new zoomfactor
                if (__mevargsE.Delta > 0)
                {
                    this.ZoomImage(true);
                }
                else if (__mevargsE.Delta < 0)
                {
                    this.ZoomImage(false);
                }

                base.OnMouseWheel(__mevargsE);
            }
        }

        /// <summary>
        /// The on paint.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);

            if (this.m_bmpImage == null)
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
                e.Graphics.DrawRectangle(this.m_penSelectPen, this.m_rctSelectedRectangle);
            }

            Point _ptStringPosition = new Point(1, 1);

            Brush _brshBrush = Brushes.White;

            e.Graphics.DrawString(
                this.m_sImageText, 
                System.Drawing.SystemFonts.DefaultFont, 
                _brshBrush, 
                _ptStringPosition.X, 
                _ptStringPosition.Y);

            base.OnPaint(e);
        }

        /// <summary>
        /// The on size changed.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnSizeChanged(EventArgs e)
        {
            this.m_rctDestRectangle = new System.Drawing.Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);

            this.ComputeDrawingArea();
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// The check bounds.
        /// </summary>
        private void CheckBounds()
        {
            if (!(this.m_bmpImage == null))
            {
                // Make sure we don't go out of bounds
                if (this.m_ptOrigin.X < 0)
                {
                    this.m_ptOrigin.X = 0;
                }

                if (this.m_ptOrigin.Y < 0)
                {
                    this.m_ptOrigin.Y = 0;
                }

                if (this.m_ptOrigin.X > (this.m_bmpImage.Width - (this.ClientSize.Width / this.m_dZoomFactor)))
                {
                    this.m_ptOrigin.X = this.m_bmpImage.Width - (int)(this.ClientSize.Width / this.m_dZoomFactor);
                }

                if (this.m_ptOrigin.Y > (this.m_bmpImage.Height - (int)(this.ClientSize.Height / this.m_dZoomFactor)))
                {
                    this.m_ptOrigin.Y = this.m_bmpImage.Height - (int)(this.ClientSize.Height / this.m_dZoomFactor);
                }

                if (this.m_ptOrigin.X < 0)
                {
                    this.m_ptOrigin.X = 0;
                }

                if (this.m_ptOrigin.Y < 0)
                {
                    this.m_ptOrigin.Y = 0;
                }
            }
        }

        /// <summary>
        /// The compute drawing area.
        /// </summary>
        private void ComputeDrawingArea()
        {
            this.m_iDrawHeight = (int)Math.Round((double)this.ClientSize.Height / this.m_dZoomFactor, 0);
            this.m_iDrawWidth = (int)Math.Round((double)this.ClientSize.Width / this.m_dZoomFactor, 0);
        }

        /// <summary>
        /// The draw canvas_ mouse double click.
        /// </summary>
        /// <param name="_oSender">
        /// The _o sender.
        /// </param>
        /// <param name="__mevargsE">
        /// The __mevargs e.
        /// </param>
        private void DrawCanvas_MouseDoubleClick(object _oSender, MouseEventArgs __mevargsE)
        {
            if (this.OnPositionSelected != null)
            {
                this.OnPositionSelected(this, new EventArgs());
            }
        }

        /// <summary>
        /// The draw canvas_ mouse down.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="__mevargsE">
        /// The __mevargs e.
        /// </param>
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

        /// <summary>
        /// The draw canvas_ mouse up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="__mevargsE">
        /// The __mevargs e.
        /// </param>
        private void DrawCanvas_MouseUp(object sender, MouseEventArgs __mevargsE)
        {
            if (!(this.m_bmpImage == null))
            {
                if (!this.m_bPanMode)
                {
                    this.m_ptEndPoint = new Point(__mevargsE.X, __mevargsE.Y);

                    if (!(this.m_rctSelectedRectangle == null))
                    {
                        this.ZoomSelection();
                    }
                }
            }
        }

        /// <summary>
        /// The draw canvas_ resize.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
        private void DrawCanvas_Resize(object __oSender, EventArgs __evargsE)
        {
            this.ComputeDrawingArea();

            if (this.StretchImageToFit)
            {
                this.Invalidate();
            }
        }

        /// <summary>
        /// The draw rectangle.
        /// </summary>
        /// <param name="__mevargsE">
        /// The __mevargs e.
        /// </param>
        private void DrawRectangle(MouseEventArgs __mevargsE)
        {
            if (
                new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height).Contains(
                    this.PointToClient(Cursor.Position)))
            {
                int _iWidth = System.Math.Abs(this.m_ptStartPoint.X - __mevargsE.X);
                int _iHeight = System.Math.Abs(this.m_ptStartPoint.Y - __mevargsE.Y);
                Point _ptUpperLeft;

                // need to determine the  upper left corner of the rectangel regardless of whether it's 
                // the start point or the end point, or other.
                _ptUpperLeft = new Point(
                    System.Math.Min(this.m_ptStartPoint.X, __mevargsE.X), 
                    System.Math.Min(this.m_ptStartPoint.Y, __mevargsE.Y));
                this.m_rctSelectedRectangle = new System.Drawing.Rectangle(
                    _ptUpperLeft.X, 
                    _ptUpperLeft.Y, 
                    _iWidth, 
                    _iHeight);
            }
        }

        /// <summary>
        /// The zoom image.
        /// </summary>
        /// <param name="ZoomIn">
        /// The zoom in.
        /// </param>
        private void ZoomImage(bool ZoomIn)
        {
            // Get center point
            this.m_ptCenterPoint.X = this.m_ptOrigin.X
                                     + (int)Math.Round((double)this.m_rctSourceRectangle.Width / (double)2, 0);
            this.m_ptCenterPoint.Y = this.m_ptOrigin.Y
                                     + (int)Math.Round((double)this.m_rctSourceRectangle.Height / (double)2, 0);

            // set new zoomfactor
            if (ZoomIn)
            {
                this.ZoomFactor = Math.Round(this.ZoomFactor * 1.1, 2);
            }
            else
            {
                this.ZoomFactor = Math.Round(this.ZoomFactor * 0.9, 2);
            }

            // Reset the origin to maintain center point
            this.m_ptOrigin.X = this.m_ptCenterPoint.X
                                - (int)Math.Round((double)this.ClientSize.Width / (this.m_dZoomFactor / (double)2), 0);
            this.m_ptOrigin.Y = this.m_ptCenterPoint.Y
                                - (int)
                                  Math.Round((double)this.ClientSize.Height / (this.m_dZoomFactor / (double)2), 0);

            this.CheckBounds();

            if (this.OnScrollPositionsChanged != null)
            {
                this.OnScrollPositionsChanged(this, new EventArgs());
                Tracing.Ping("OnScrollPositionsChanged raised here!");
            }
        }

        /// <summary>
        /// The zoom selection.
        /// </summary>
        private void ZoomSelection()
        {
            if (!(this.m_bmpImage == null || this.m_rctSelectedRectangle == Rectangle.Empty))
            {
                try
                {
                    Point _ptNewOrigin =
                        new Point(
                            this.Origin.X + (int)(this.m_rctSelectedRectangle.X / this.m_dZoomFactor), 
                            this.Origin.Y + (int)(this.m_rctSelectedRectangle.Y / this.m_dZoomFactor));

                    double _dNewZoomFactor;

                    if (this.m_rctSelectedRectangle.Width > this.m_rctSelectedRectangle.Height)
                    {
                        _dNewZoomFactor = (double)this.ClientSize.Width
                                           / ((double)this.SelectedRectangle.Width / this.ZoomFactor);
                    }
                    else
                    {
                        _dNewZoomFactor = (double)this.ClientSize.Height
                                           / ((double)this.SelectedRectangle.Height / this.ZoomFactor);
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

        #endregion
    }
}