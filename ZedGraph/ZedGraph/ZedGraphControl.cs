// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ZedGraphControl.cs">
//   
// </copyright>
// <summary>
//   The ZedGraphControl class provides a UserControl interface to the
//   <see cref="ZedGraph" /> class library.  This allows ZedGraph to be installed
//   as a control in the Visual Studio toolbox.  You can use the control by simply
//   dragging it onto a form in the Visual Studio form editor.  All graph
//   attributes are accessible via the <see cref="ZedGraphControl.GraphPane" />
//   property.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Reflection;
    using System.Resources;
    using System.Windows.Forms;

    /*
	/// <summary>
	/// 
	/// </summary>
	public struct DrawingThreadData
	{
		/// <summary>
		/// 
		/// </summary>
		public Graphics _g;
		/// <summary>
		/// 
		/// </summary>
		public MasterPane _masterPane;

//		public DrawingThread( Graphics g, MasterPane masterPane )
//		{
//			_g = g;
//			_masterPane = masterPane;
//		}
	}
*/

    /// <summary>
    /// The ZedGraphControl class provides a UserControl interface to the
    /// <see cref="ZedGraph"/> class library.  This allows ZedGraph to be installed
    /// as a control in the Visual Studio toolbox.  You can use the control by simply
    /// dragging it onto a form in the Visual Studio form editor.  All graph
    /// attributes are accessible via the <see cref="ZedGraphControl.GraphPane"/>
    /// property.
    /// </summary>
    /// <author> John Champion revised by Jerry Vos </author>
    /// <version> $Revision: 3.86 $ $Date: 2007-11-03 04:41:29 $ </version>
    public partial class ZedGraphControl : UserControl
    {
        #region Constants

        /// <summary>
        /// The _ scroll control span.
        /// </summary>
        private const int _ScrollControlSpan = int.MaxValue;

        // The ratio of the largeChange to the smallChange for the scroll bars
        /// <summary>
        /// The _ scroll small ratio.
        /// </summary>
        private const int _ScrollSmallRatio = 10;

        #endregion

        #region Fields

        /// <summary>
        /// The _menu click pt.
        /// </summary>
        internal Point _menuClickPt;

        /// <summary>
        /// The _drag curve.
        /// </summary>
        private CurveItem _dragCurve;

        /// <summary>
        /// The _drag end pt.
        /// </summary>
        private Point _dragEndPt;

        /// <summary>
        /// The _drag index.
        /// </summary>
        private int _dragIndex;

        /// <summary>
        /// Internal variable that stores the <see cref="GraphPane"/> reference for the Pane that is
        /// currently being zoomed or panned.
        /// </summary>
        private GraphPane _dragPane = null;

        /// <summary>
        /// The _drag start pair.
        /// </summary>
        private PointPair _dragStartPair;

        /// <summary>
        /// Internal variable that stores a rectangle which is either the zoom rectangle, or the incremental
        /// pan amount since the last mousemove event.
        /// </summary>
        private Point _dragStartPt;

        /// <summary>
        /// Gets or sets a value that determines which Mouse button will be used to edit point
        /// data values
        /// </summary>
        /// <remarks>
        /// This setting only applies if <see cref="IsEnableHEdit" /> and/or
        /// <see cref="IsEnableVEdit" /> are true.
        /// </remarks>
        /// <seealso cref="EditModifierKeys" />
        private MouseButtons _editButtons = MouseButtons.Right;

        /// <summary>
        /// Gets or sets a value that determines which modifier keys will be used to edit point
        /// data values
        /// </summary>
        /// <remarks>
        /// This setting only applies if <see cref="IsEnableHEdit" /> and/or
        /// <see cref="IsEnableVEdit" /> are true.
        /// </remarks>
        /// <seealso cref="EditButtons" />
        private Keys _editModifierKeys = Keys.Alt;

        /// <summary>
        /// The _is auto scroll range.
        /// </summary>
        private bool _isAutoScrollRange = false;

        /// <summary>
        /// Internal variable that indicates a point value is currently being edited.
        /// </summary>
        private bool _isEditing = false;

        /// <summary>
        /// private value that determines whether or not point editing is enabled in the
        /// horizontal direction.  Use the public property <see cref="IsEnableHEdit"/> to access this
        /// value.
        /// </summary>
        private bool _isEnableHEdit = false;

        /// <summary>
        /// private value that determines whether or not panning is allowed for the control in the
        /// horizontal direction.  Use the
        /// public property <see cref="IsEnableHPan"/> to access this value.
        /// </summary>
        private bool _isEnableHPan = true;

        /// <summary>
        /// private value that determines whether or not zooming is enabled for the control in the
        /// horizontal direction.  Use the public property <see cref="IsEnableHZoom"/> to access this
        /// value.
        /// </summary>
        private bool _isEnableHZoom = true;

        // Revision: JCarpenter 10/06
        /// <summary>
        /// Internal variable that indicates if the control can manage selections. 
        /// </summary>
        private bool _isEnableSelection = false;

        /// <summary>
        /// private value that determines whether or not point editing is enabled in the
        /// vertical direction.  Use the public property <see cref="IsEnableVEdit"/> to access this
        /// value.
        /// </summary>
        private bool _isEnableVEdit = false;

        /// <summary>
        /// private value that determines whether or not panning is allowed for the control in the
        /// vertical direction.  Use the
        /// public property <see cref="IsEnableVPan"/> to access this value.
        /// </summary>
        private bool _isEnableVPan = true;

        /// <summary>
        /// private value that determines whether or not zooming is enabled for the control in the
        /// vertical direction.  Use the public property <see cref="IsEnableVZoom"/> to access this
        /// value.
        /// </summary>
        private bool _isEnableVZoom = true;

        /// <summary>
        /// private value that determines whether or not zooming is enabled with the mousewheel.
        /// Note that this property is used in combination with the <see cref="IsEnableHZoom"/> and
        /// <see cref="IsEnableVZoom" /> properties to control zoom options.
        /// </summary>
        private bool _isEnableWheelZoom = true;

        /// <summary>
        /// Internal variable that indicates the control is currently being panned.
        /// </summary>
        private bool _isPanning = false;

        /// <summary>
        /// private field that determines whether or not the <see cref="MasterPane" />
        /// <see cref="PaneBase.Rect" /> dimensions will be expanded to fill the
        /// available space when printing this <see cref="ZedGraphControl" />.
        /// </summary>
        /// <remarks>
        /// If <see cref="IsPrintKeepAspectRatio" /> is also true, then the <see cref="MasterPane" />
        /// <see cref="PaneBase.Rect" /> dimensions will be expanded to fit as large
        /// a space as possible while still honoring the visible aspect ratio.
        /// </remarks>
        private bool _isPrintFillPage = true;

        /// <summary>
        /// private field that determines whether or not the visible aspect ratio of the
        /// <see cref="MasterPane" /> <see cref="PaneBase.Rect" /> will be preserved
        /// when printing this <see cref="ZedGraphControl" />.
        /// </summary>
        private bool _isPrintKeepAspectRatio = true;

        /// <summary>
        /// private field that determines whether the settings of
        /// <see cref="ZedGraph.PaneBase.IsFontsScaled" /> and <see cref="PaneBase.IsPenWidthScaled" />
        /// will be overridden to true during printing operations.
        /// </summary>
        /// <remarks>
        /// Printing involves pixel maps that are typically of a dramatically different dimension
        /// than on-screen pixel maps.  Therefore, it becomes more important to scale the fonts and
        /// lines to give a printed image that looks like what is shown on-screen.  The default
        /// setting for <see cref="ZedGraph.PaneBase.IsFontsScaled" /> is true, but the default
        /// setting for <see cref="PaneBase.IsPenWidthScaled" /> is false.
        /// </remarks>
        /// <value>
        /// A value of true will cause both <see cref="ZedGraph.PaneBase.IsFontsScaled" /> and
        /// <see cref="PaneBase.IsPenWidthScaled" /> to be temporarily set to true during
        /// printing operations.
        /// </value>
        private bool _isPrintScaleAll = true;

        /// <summary>
        /// Internal variable that indicates the control is currently using selection. 
        /// </summary>
        private bool _isSelecting = false;

        /// <summary>
        /// private field that determines whether or not the context menu will be available.  Use the
        /// public property <see cref="IsShowContextMenu"/> to access this value.
        /// </summary>
        private bool _isShowContextMenu = true;

        /// <summary>
        /// private field that determines whether or not a message box will be shown in response to
        /// a context menu "Copy" command.  Use the
        /// public property <see cref="IsShowCopyMessage"/> to access this value.
        /// </summary>
        /// <remarks>
        /// Note that, if this value is set to false, the user will receive no indicative feedback
        /// in response to a Copy action.
        /// </remarks>
        private bool _isShowCopyMessage = true;

        /// <summary>
        /// private field that determines whether or not tooltips will be displayed
        /// showing the scale values while the mouse is located within the ChartRect.
        /// Use the public property <see cref="IsShowCursorValues"/> to access this value.
        /// </summary>
        private bool _isShowCursorValues = false;

        /// <summary>
        /// The _is show h scroll bar.
        /// </summary>
        private bool _isShowHScrollBar = false;

        /// <summary>
        /// private field that determines whether or not tooltips will be displayed
        /// when the mouse hovers over data values.  Use the public property
        /// <see cref="IsShowPointValues"/> to access this value.
        /// </summary>
        private bool _isShowPointValues = false;

        /// <summary>
        /// The _is show v scroll bar.
        /// </summary>
        private bool _isShowVScrollBar = false;

        // private bool		isScrollY2 = false;

        /// <summary>
        /// The _is synchronize x axes.
        /// </summary>
        private bool _isSynchronizeXAxes = false;

        /// <summary>
        /// The _is synchronize y axes.
        /// </summary>
        private bool _isSynchronizeYAxes = false;

        // private System.Windows.Forms.HScrollBar hScrollBar1;
        // private System.Windows.Forms.VScrollBar vScrollBar1;

        // The range of values to use the scroll control bars

        /// <summary>
        /// The _is zoom on mouse center.
        /// </summary>
        private bool _isZoomOnMouseCenter = false;

        /// <summary>
        /// Internal variable that indicates the control is currently being zoomed. 
        /// </summary>
        private bool _isZooming = false;

        /// <summary>
        /// Gets or sets a value that determines which Mouse button will be used to click on
        /// linkable objects
        /// </summary>
        /// <seealso cref="LinkModifierKeys" />
        private MouseButtons _linkButtons = MouseButtons.Left;

        /// <summary>
        /// Gets or sets a value that determines which modifier keys will be used to click
        /// on linkable objects
        /// </summary>
        /// <seealso cref="LinkButtons" />
        private Keys _linkModifierKeys = Keys.Alt;

        /// <summary>
        /// This private field contains the instance for the MasterPane object of this control.
        /// You can access the MasterPane object through the public property
        /// <see cref="ZedGraphControl.MasterPane"/>. This is nulled when this Control is
        /// disposed.
        /// </summary>
        private MasterPane _masterPane;

        /// <summary>
        /// Gets or sets a value that determines which Mouse button will be used to perform
        /// panning operations
        /// </summary>
        /// <remarks>
        /// This setting only applies if <see cref="IsEnableHPan" /> and/or
        /// <see cref="IsEnableVPan" /> are true.  A Pan operation (dragging the graph with
        /// the mouse) should not be confused with a scroll operation (using a scroll bar to
        /// move the graph).
        /// </remarks>
        /// <seealso cref="PanModifierKeys" />
        /// <seealso cref="PanButtons2" />
        /// <seealso cref="PanModifierKeys2" />
        private MouseButtons _panButtons = MouseButtons.Left;

        /// <summary>
        /// Gets or sets a value that determines which Mouse button will be used as a
        /// secondary option to perform panning operations
        /// </summary>
        /// <remarks>
        /// This setting only applies if <see cref="IsEnableHPan" /> and/or
        /// <see cref="IsEnableVPan" /> are true.  A Pan operation (dragging the graph with
        /// the mouse) should not be confused with a scroll operation (using a scroll bar to
        /// move the graph).
        /// </remarks>
        /// <seealso cref="PanModifierKeys2" />
        /// <seealso cref="PanButtons" />
        /// <seealso cref="PanModifierKeys" />
        private MouseButtons _panButtons2 = MouseButtons.Middle;

        /// <summary>
        /// Gets or sets a value that determines which modifier keys will be used to perform
        /// panning operations
        /// </summary>
        /// <remarks>
        /// This setting only applies if <see cref="IsEnableHPan" /> and/or
        /// <see cref="IsEnableVPan" /> are true.  A Pan operation (dragging the graph with
        /// the mouse) should not be confused with a scroll operation (using a scroll bar to
        /// move the graph).
        /// </remarks>
        /// <seealso cref="PanButtons" />
        /// <seealso cref="PanButtons2" />
        /// <seealso cref="PanModifierKeys2" />
        private Keys _panModifierKeys = Keys.Control;

        /// <summary>
        /// Gets or sets a value that determines which modifier keys will be used as a
        /// secondary option to perform panning operations
        /// </summary>
        /// <remarks>
        /// This setting only applies if <see cref="IsEnableHPan" /> and/or
        /// <see cref="IsEnableVPan" /> are true.  A Pan operation (dragging the graph with
        /// the mouse) should not be confused with a scroll operation (using a scroll bar to
        /// move the graph).
        /// </remarks>
        /// <seealso cref="PanButtons2" />
        /// <seealso cref="PanButtons" />
        /// <seealso cref="PanModifierKeys" />
        private Keys _panModifierKeys2 = Keys.None;

        /// <summary>
        /// private field that stores a <see cref="PrintDocument" /> instance, which maintains
        /// a persistent selection of printer options.
        /// </summary>
        /// <remarks>
        /// This is needed so that a "Print" action utilizes the settings from a prior
        /// "Page Setup" action.</remarks>
        private PrintDocument _pdSave = null;

        /// <summary>
        /// private field that determines the format for displaying tooltip date values.
        /// This format is passed to <see cref="XDate.ToString(string)"/>.
        /// Use the public property <see cref="PointDateFormat"/> to access this
        /// value.
        /// </summary>
        private string _pointDateFormat = XDate.DefaultFormatStr;

        /// <summary>
        /// private field that determines the format for displaying tooltip values.
        /// This format is passed to <see cref="PointPairBase.ToString(string)"/>.
        /// Use the public property <see cref="PointValueFormat"/> to access this
        /// value.
        /// </summary>
        private string _pointValueFormat = PointPair.DefaultFormat;

        /// <summary>
        /// The _resource manager.
        /// </summary>
        private ResourceManager _resourceManager;

        /// <summary>
        /// The _save file dialog.
        /// </summary>
        private SaveFileDialog _saveFileDialog = new SaveFileDialog();

        /// <summary>
        /// The _scroll grace.
        /// </summary>
        private double _scrollGrace = 0.00; // 0.05;

        /// <summary>
        /// The _select append modifier keys.
        /// </summary>
        private Keys _selectAppendModifierKeys = Keys.Shift | Keys.Control;

        /// <summary>
        /// Gets or sets a value that determines which mouse button will be used to select
        /// <see cref="CurveItem" />'s.
        /// </summary>
        /// <remarks>
        /// This setting only applies if <see cref="IsEnableSelection" /> is true.
        /// </remarks>
        /// <seealso cref="SelectModifierKeys" />
        private MouseButtons _selectButtons = MouseButtons.Left;

        /// <summary>
        /// Gets or sets a value that determines which modifier keys will be used to select
        /// <see cref="CurveItem" />'s.
        /// </summary>
        /// <remarks>
        /// This setting only applies if <see cref="IsEnableSelection" /> is true.
        /// </remarks>
        /// <seealso cref="SelectButtons" />
        private Keys _selectModifierKeys = Keys.Shift;

        /// <summary>
        /// This private field contains a list of selected CurveItems.
        /// </summary>
        // private List<CurveItem> _selection = new List<CurveItem>();
        private Selection _selection = new Selection();

        /// <summary>
        /// The _x scroll range.
        /// </summary>
        private ScrollRange _xScrollRange;

        /// <summary>
        /// The _y 2 scroll range list.
        /// </summary>
        private ScrollRangeList _y2ScrollRangeList;

        /// <summary>
        /// The _y scroll range list.
        /// </summary>
        private ScrollRangeList _yScrollRangeList;

        /// <summary>
        /// Gets or sets a value that determines which Mouse button will be used to perform
        /// zoom operations
        /// </summary>
        /// <remarks>
        /// This setting only applies if <see cref="IsEnableHZoom" /> and/or
        /// <see cref="IsEnableVZoom" /> are true.
        /// </remarks>
        /// <seealso cref="ZoomModifierKeys" />
        /// <seealso cref="ZoomButtons2" />
        /// <seealso cref="ZoomModifierKeys2" />
        private MouseButtons _zoomButtons = MouseButtons.Left;

        /// <summary>
        /// Gets or sets a value that determines which Mouse button will be used as a
        /// secondary option to perform zoom operations
        /// </summary>
        /// <remarks>
        /// This setting only applies if <see cref="IsEnableHZoom" /> and/or
        /// <see cref="IsEnableVZoom" /> are true.
        /// </remarks>
        /// <seealso cref="ZoomModifierKeys2" />
        /// <seealso cref="ZoomButtons" />
        /// <seealso cref="ZoomModifierKeys" />
        private MouseButtons _zoomButtons2 = MouseButtons.None;

        /// <summary>
        /// Gets or sets a value that determines which modifier keys will be used to perform
        /// zoom operations
        /// </summary>
        /// <remarks>
        /// This setting only applies if <see cref="IsEnableHZoom" /> and/or
        /// <see cref="IsEnableVZoom" /> are true.
        /// </remarks>
        /// <seealso cref="ZoomButtons" />
        /// <seealso cref="ZoomButtons2" />
        /// <seealso cref="ZoomModifierKeys2" />
        private Keys _zoomModifierKeys = Keys.None;

        /// <summary>
        /// Gets or sets a value that determines which modifier keys will be used as a
        /// secondary option to perform zoom operations
        /// </summary>
        /// <remarks>
        /// This setting only applies if <see cref="IsEnableHZoom" /> and/or
        /// <see cref="IsEnableVZoom" /> are true.
        /// </remarks>
        /// <seealso cref="ZoomButtons" />
        /// <seealso cref="ZoomButtons2" />
        /// <seealso cref="ZoomModifierKeys2" />
        private Keys _zoomModifierKeys2 = Keys.None;

        /// <summary>
        /// private field that stores the state of the scale ranges prior to starting a panning action.
        /// </summary>
        private ZoomState _zoomState;

        /// <summary>
        /// The _zoom state stack.
        /// </summary>
        private ZoomStateStack _zoomStateStack;

        /// <summary>
        /// The _zoom step fraction.
        /// </summary>
        private double _zoomStepFraction = 0.1;

        #endregion

        // temporarily save the location of a context menu click so we can use it for reference
        // Note that Control.MousePosition ends up returning the position after the mouse has
        // moved to the menu item within the context menu.  Therefore, this point is saved so
        // that we have the point at which the context menu was first right-clicked
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZedGraphControl"/> class. 
        /// Default Constructor
        /// </summary>
        public ZedGraphControl()
        {
            this.InitializeComponent();

            // These commands do nothing, but they get rid of the compiler warnings for
            // unused events
            bool b = this.MouseDown == null || this.MouseUp == null || this.MouseMove == null;

            // Link in these events from the base class, since we disable them from this class.
            base.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ZedGraphControl_MouseDown);
            base.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ZedGraphControl_MouseUp);
            base.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ZedGraphControl_MouseMove);

            // this.MouseWheel += new System.Windows.Forms.MouseEventHandler( this.ZedGraphControl_MouseWheel );

            // Use double-buffering for flicker-free updating:
            this.SetStyle(
                ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer
                | ControlStyles.ResizeRedraw, 
                true);

            // isTransparentBackground = false;
            // SetStyle( ControlStyles.Opaque, false );
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // this.BackColor = Color.Transparent;
            this._resourceManager = new ResourceManager(
                "ZedGraph.ZedGraph.ZedGraphLocale", 
                Assembly.GetExecutingAssembly());

            Rectangle rect = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
            this._masterPane = new MasterPane(string.Empty, rect);
            this._masterPane.Margin.All = 0;
            this._masterPane.Title.IsVisible = false;

            string titleStr = this._resourceManager.GetString("title_def");
            string xStr = this._resourceManager.GetString("x_title_def");
            string yStr = this._resourceManager.GetString("y_title_def");

            // GraphPane graphPane = new GraphPane( rect, "Title", "X Axis", "Y Axis" );
            GraphPane graphPane = new GraphPane(rect, titleStr, xStr, yStr);
            using (Graphics g = this.CreateGraphics())
            {
                graphPane.AxisChange(g);

                // g.Dispose();
            }

            this._masterPane.Add(graphPane);

            this.hScrollBar1.Minimum = 0;
            this.hScrollBar1.Maximum = 100;
            this.hScrollBar1.Value = 0;

            this.vScrollBar1.Minimum = 0;
            this.vScrollBar1.Maximum = 100;
            this.vScrollBar1.Value = 0;

            this._xScrollRange = new ScrollRange(true);
            this._yScrollRangeList = new ScrollRangeList();
            this._y2ScrollRangeList = new ScrollRangeList();

            this._yScrollRangeList.Add(new ScrollRange(true));
            this._y2ScrollRangeList.Add(new ScrollRange(false));

            this._zoomState = null;
            this._zoomStateStack = new ZoomStateStack();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>This performs an axis change command on the graphPane.
        /// </summary>
        /// <remarks>
        /// This is the same as
        /// <c>ZedGraphControl.GraphPane.AxisChange( ZedGraphControl.CreateGraphics() )</c>, however,
        /// this method also calls <see cref="SetScrollRangeFromData" /> if <see cref="IsAutoScrollRange" />
        /// is true.
        /// </remarks>
        public virtual void AxisChange()
        {
            lock (this)
            {
                if (this.BeenDisposed || this._masterPane == null)
                {
                    return;
                }

                using (Graphics g = this.CreateGraphics())
                {
                    this._masterPane.AxisChange(g);

                    // g.Dispose();
                }

                if (this._isAutoScrollRange)
                {
                    this.SetScrollRangeFromData();
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// true if the components should be
        /// disposed, false otherwise
        /// </param>
        protected override void Dispose(bool disposing)
        {
            lock (this)
            {
                if (disposing)
                {
                    if (this.components != null)
                    {
                        this.components.Dispose();
                    }
                }

                base.Dispose(disposing);

                this._masterPane = null;
            }
        }

        /// <summary>
        /// Called by the system to update the control on-screen
        /// </summary>
        /// <param name="e">
        /// A PaintEventArgs object containing the Graphics specifications
        /// for this Paint event.
        /// </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            lock (this)
            {
                if (this.BeenDisposed || this._masterPane == null || this.GraphPane == null)
                {
                    return;
                }

                if (this.hScrollBar1 != null && this.GraphPane != null && this.vScrollBar1 != null
                    && this._yScrollRangeList != null)
                {
                    this.SetScroll(
                        this.hScrollBar1, 
                        this.GraphPane.XAxis, 
                        this._xScrollRange.Min, 
                        this._xScrollRange.Max);
                    this.SetScroll(
                        this.vScrollBar1, 
                        this.GraphPane.YAxis, 
                        this._yScrollRangeList[0].Min, 
                        this._yScrollRangeList[0].Max);
                }

                base.OnPaint(e);

                // Add a try/catch pair since the users of the control can't catch this one
                try
                {
                    this._masterPane.Draw(e.Graphics);
                }
                catch
                {
                }
            }

            /*
			// first, see if an old thread is still running
			if ( t != null && t.IsAlive )
			{
				t.Abort();
			}

			//dt = new DrawingThread( e.Graphics, _masterPane );
			//g = e.Graphics;

			// Fire off the new thread
			t = new Thread( new ParameterizedThreadStart( DoDrawingThread ) );
			//ct.ApartmentState = ApartmentState.STA;
			//ct.SetApartmentState( ApartmentState.STA );
			DrawingThreadData dtd;
			dtd._g = e.Graphics;
			dtd._masterPane = _masterPane;

			t.Start( dtd );
			//ct.Join();
*/
        }

        // 		Thread t = null;
        // DrawingThread dt = null;

        /*
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtdobj"></param>
		public void DoDrawingThread( object dtdobj )
		{
			try
			{
				DrawingThreadData dtd = (DrawingThreadData) dtdobj;

				if ( dtd._g != null && dtd._masterPane != null )
					dtd._masterPane.Draw( dtd._g );

				//				else
				//				{
				//					using ( Graphics g2 = CreateGraphics() )
				//						_masterPane.Draw( g2 );
				//				}
			}
			catch
			{

			}
		}
*/

        /// <summary>
        /// Called when the control has been resized.
        /// </summary>
        /// <param name="sender">
        /// A reference to the control that has been resized.
        /// </param>
        /// <param name="e">
        /// An EventArgs object.
        /// </param>
        protected void ZedGraphControl_ReSize(object sender, EventArgs e)
        {
            lock (this)
            {
                if (this.BeenDisposed || this._masterPane == null)
                {
                    return;
                }

                Size newSize = this.Size;

                if (this._isShowHScrollBar)
                {
                    this.hScrollBar1.Visible = true;
                    newSize.Height -= this.hScrollBar1.Size.Height;
                    this.hScrollBar1.Location = new Point(0, newSize.Height);
                    this.hScrollBar1.Size = new Size(newSize.Width, this.hScrollBar1.Height);
                }
                else
                {
                    this.hScrollBar1.Visible = false;
                }

                if (this._isShowVScrollBar)
                {
                    this.vScrollBar1.Visible = true;
                    newSize.Width -= this.vScrollBar1.Size.Width;
                    this.vScrollBar1.Location = new Point(newSize.Width, 0);
                    this.vScrollBar1.Size = new Size(this.vScrollBar1.Width, newSize.Height);
                }
                else
                {
                    this.vScrollBar1.Visible = false;
                }

                using (Graphics g = this.CreateGraphics())
                {
                    this._masterPane.ReSize(g, new RectangleF(0, 0, newSize.Width, newSize.Height));

                    // g.Dispose();
                }

                this.Invalidate();
            }
        }

        /// <summary>
        /// Clear the collection of saved states.
        /// </summary>
        private void ZoomStateClear()
        {
            this._zoomStateStack.Clear();
            this._zoomState = null;
        }

        /// <summary>
        /// Clear all states from the undo stack for each GraphPane.
        /// </summary>
        private void ZoomStatePurge()
        {
            foreach (GraphPane pane in this._masterPane._paneList)
            {
                pane.ZoomStack.Clear();
            }
        }

        /// <summary>
        /// Place the previously saved states of the GraphPanes on the individual GraphPane
        /// <see cref="ZedGraph.GraphPane.ZoomStack"/> collections.  This provides for an
        /// option to undo the state change at a later time.  Save a single
        /// (<see paramref="primaryPane"/>) GraphPane if the panes are not synchronized
        /// (see <see cref="IsSynchronizeXAxes"/> and <see cref="IsSynchronizeYAxes"/>),
        /// or save a list of states for all GraphPanes if the panes are synchronized.
        /// </summary>
        /// <param name="primaryPane">
        /// The primary GraphPane on which zoom/pan/scroll operations
        /// are taking place
        /// </param>
        private void ZoomStatePush(GraphPane primaryPane)
        {
            if (this._isSynchronizeXAxes || this._isSynchronizeYAxes)
            {
                for (int i = 0; i < this._masterPane._paneList.Count; i++)
                {
                    if (i < this._zoomStateStack.Count)
                    {
                        this._masterPane._paneList[i].ZoomStack.Add(this._zoomStateStack[i]);
                    }
                }
            }
            else if (this._zoomState != null)
            {
                primaryPane.ZoomStack.Add(this._zoomState);
            }

            this.ZoomStateClear();
        }

        /// <summary>
        /// Restore the states of the GraphPanes to a previously saved condition (via
        /// <see cref="ZoomStateSave"/>.  This is essentially an "undo" for live
        /// pan and scroll actions.  Restores a single
        /// (<see paramref="primaryPane"/>) GraphPane if the panes are not synchronized
        /// (see <see cref="IsSynchronizeXAxes"/> and <see cref="IsSynchronizeYAxes"/>),
        /// or save a list of states for all GraphPanes if the panes are synchronized.
        /// </summary>
        /// <param name="primaryPane">
        /// The primary GraphPane on which zoom/pan/scroll operations
        /// are taking place
        /// </param>
        private void ZoomStateRestore(GraphPane primaryPane)
        {
            if (this._isSynchronizeXAxes || this._isSynchronizeYAxes)
            {
                for (int i = 0; i < this._masterPane._paneList.Count; i++)
                {
                    if (i < this._zoomStateStack.Count)
                    {
                        this._zoomStateStack[i].ApplyState(this._masterPane._paneList[i]);
                    }
                }
            }
            else if (this._zoomState != null)
            {
                this._zoomState.ApplyState(primaryPane);
            }

            this.ZoomStateClear();
        }

        /// <summary>
        /// Save the current states of the GraphPanes to a separate collection.  Save a single
        /// (<see paramref="primaryPane"/>) GraphPane if the panes are not synchronized
        /// (see <see cref="IsSynchronizeXAxes"/> and <see cref="IsSynchronizeYAxes"/>),
        /// or save a list of states for all GraphPanes if the panes are synchronized.
        /// </summary>
        /// <param name="primaryPane">
        /// The primary GraphPane on which zoom/pan/scroll operations
        /// are taking place
        /// </param>
        /// <param name="type">
        /// The <see cref="ZoomState.StateType"/> that describes the
        /// current operation
        /// </param>
        /// <returns>
        /// The <see cref="ZoomState"/> that corresponds to the
        /// <see paramref="primaryPane"/>.
        /// </returns>
        private ZoomState ZoomStateSave(GraphPane primaryPane, ZoomState.StateType type)
        {
            this.ZoomStateClear();

            if (this._isSynchronizeXAxes || this._isSynchronizeYAxes)
            {
                foreach (GraphPane pane in this._masterPane._paneList)
                {
                    ZoomState state = new ZoomState(pane, type);
                    if (pane == primaryPane)
                    {
                        this._zoomState = state;
                    }

                    this._zoomStateStack.Add(state);
                }
            }
            else
            {
                this._zoomState = new ZoomState(primaryPane, type);
            }

            return this._zoomState;
        }

        #endregion
    }
}