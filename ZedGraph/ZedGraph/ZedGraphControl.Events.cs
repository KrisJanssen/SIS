// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ZedGraphControl.Events.cs">
//   
// </copyright>
// <summary>
//   The zed graph control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// The zed graph control.
    /// </summary>
    partial class ZedGraphControl
    {
        #region Constants

        /// <summary>
        /// The zoom resolution.
        /// </summary>
        private const double ZoomResolution = 1e-300;

        #endregion

        #region Delegates

        /// <summary>
        /// A delegate that allows subscribing methods to append or modify the context menu.
        /// </summary>
        /// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
        /// <param name="menuStrip">A reference to the <see cref="ContextMenuStrip"/> object
        /// that contains the context menu.
        /// </param>
        /// <param name="mousePt">The point at which the mouse was clicked</param>
        /// <param name="objState">The current context menu state</param>
        /// <seealso cref="ContextMenuBuilder" />
        public delegate void ContextMenuBuilderEventHandler(
            ZedGraphControl sender, 
            ContextMenuStrip menuStrip, 
            Point mousePt, 
            ContextMenuObjectState objState);

        /// <summary>
        /// A delegate that allows custom formatting of the cursor value tooltips
        /// </summary>
        /// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
        /// <param name="pane">The <see cref="GraphPane"/> object that contains the cursor of interest</param>
        /// <param name="mousePt">The <see cref="Point"/> object that represents the cursor value location</param>
        /// <seealso cref="CursorValueEvent" />
        public delegate string CursorValueHandler(ZedGraphControl sender, GraphPane pane, Point mousePt);

        /// <summary>
        /// A delegate that allows notification of clicks on ZedGraph objects that have
        /// active links enabled
        /// </summary>
        /// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
        /// <param name="pane">The source <see cref="GraphPane" /> in which the click
        /// occurred.
        /// </param>
        /// <param name="source">The source object which was clicked.  This is typically
        /// a type of <see cref="CurveItem" /> if a curve point was clicked, or
        /// a type of <see cref="GraphObj" /> if a graph object was clicked.
        /// </param>
        /// <param name="link">The <see cref="Link" /> object, belonging to
        /// <paramref name="source" />, that contains the link information
        /// </param>
        /// <param name="index">An index value, typically used if a <see cref="CurveItem" />
        /// was clicked, indicating the ordinal value of the actual point that was clicked.
        /// </param>
        /// <returns>
        /// Return true if you have handled the LinkEvent entirely, and you do not
        /// want the <see cref="ZedGraphControl"/> to do any further action.
        /// Return false if ZedGraph should go ahead and process the LinkEvent.
        /// </returns>
        public delegate bool LinkEventHandler(
            ZedGraphControl sender, 
            GraphPane pane, 
            object source, 
            Link link, 
            int index);

        /// <summary>
        /// A delegate that receives notification after a point-edit operation is completed.
        /// </summary>
        /// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
        /// <param name="pane">The <see cref="GraphPane"/> object that contains the
        /// point that has been edited</param>
        /// <param name="curve">The <see cref="CurveItem"/> object that contains the point
        /// that has been edited</param>
        /// <param name="iPt">The integer index of the edited <see cref="PointPair"/> within the
        /// <see cref="IPointList"/> of the selected <see cref="CurveItem"/>
        /// </param>
        /// <seealso cref="PointValueEvent" />
        public delegate string PointEditHandler(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt);

        /// <summary>
        /// A delegate that allows custom formatting of the point value tooltips
        /// </summary>
        /// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
        /// <param name="pane">The <see cref="GraphPane"/> object that contains the point value of interest</param>
        /// <param name="curve">The <see cref="CurveItem"/> object that contains the point value of interest</param>
        /// <param name="iPt">The integer index of the selected <see cref="PointPair"/> within the
        /// <see cref="IPointList"/> of the selected <see cref="CurveItem"/></param>
        /// <seealso cref="PointValueEvent" />
        public delegate string PointValueHandler(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt);

        /// <summary>
        /// A delegate that allows notification of scroll events.
        /// </summary>
        /// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
        /// <param name="scrollBar">The source <see cref="ScrollBar"/> object</param>
        /// <param name="oldState">A <see cref="ZoomState"/> object that corresponds to the state of the
        /// <see cref="GraphPane"/> before the scroll event.</param>
        /// <param name="newState">A <see cref="ZoomState"/> object that corresponds to the state of the
        /// <see cref="GraphPane"/> after the scroll event</param>
        /// <seealso cref="ZoomEvent" />
        public delegate void ScrollDoneHandler(
            ZedGraphControl sender, 
            ScrollBar scrollBar, 
            ZoomState oldState, 
            ZoomState newState);

        /// <summary>
        /// A delegate that allows notification of scroll events.
        /// </summary>
        /// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
        /// <param name="scrollBar">The source <see cref="ScrollBar"/> object</param>
        /// <param name="oldState">A <see cref="ZoomState"/> object that corresponds to the state of the
        /// <see cref="GraphPane"/> before the scroll event.</param>
        /// <param name="newState">A <see cref="ZoomState"/> object that corresponds to the state of the
        /// <see cref="GraphPane"/> after the scroll event</param>
        /// <seealso cref="ZoomEvent" />
        public delegate void ScrollProgressHandler(
            ZedGraphControl sender, 
            ScrollBar scrollBar, 
            ZoomState oldState, 
            ZoomState newState);

        /// <summary>
        /// A delegate that allows notification of mouse events on Graph objects.
        /// </summary>
        /// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
        /// <param name="e">A <see cref="MouseEventArgs" /> corresponding to this event</param>
        /// <seealso cref="MouseDownEvent" />
        /// <returns>
        /// Return true if you have handled the mouse event entirely, and you do not
        /// want the <see cref="ZedGraphControl"/> to do any further action (e.g., starting
        /// a zoom operation).  Return false if ZedGraph should go ahead and process the
        /// mouse event.
        /// </returns>
        public delegate bool ZedMouseEventHandler(ZedGraphControl sender, MouseEventArgs e);

        /// <summary>
        /// A delegate that allows notification of zoom and pan events.
        /// </summary>
        /// <param name="sender">The source <see cref="ZedGraphControl"/> object</param>
        /// <param name="oldState">A <see cref="ZoomState"/> object that corresponds to the state of the
        /// <see cref="GraphPane"/> before the zoom or pan event.</param>
        /// <param name="newState">A <see cref="ZoomState"/> object that corresponds to the state of the
        /// <see cref="GraphPane"/> after the zoom or pan event</param>
        /// <seealso cref="ZoomEvent" />
        public delegate void ZoomEventHandler(ZedGraphControl sender, ZoomState oldState, ZoomState newState);

        #endregion

        #region Public Events

        /// <summary>
        /// Subscribe to this event to be able to modify the ZedGraph context menu.
        /// </summary>
        /// <remarks>
        /// The context menu is built on the fly after a right mouse click.  You can add menu items
        /// to this menu by simply modifying the <see paramref="menu"/> parameter.
        /// </remarks>
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe to this event to be able to modify the ZedGraph context menu")]
        public event ContextMenuBuilderEventHandler ContextMenuBuilder;

        /// <summary>
        /// Subscribe to this event to provide custom formatting for the cursor value tooltips
        /// </summary>
        /// <example>
        /// <para>To subscribe to this event, use the following in your FormLoad method:</para>
        /// <code>zedGraphControl1.CursorValueEvent +=
        /// new ZedGraphControl.CursorValueHandler( MyCursorValueHandler );</code>
        /// <para>Add this method to your Form1.cs:</para>
        /// <code>
        ///    private string MyCursorValueHandler( object sender, GraphPane pane, Point mousePt )
        ///    {
        ///    #region
        ///		double x, y;
        ///		pane.ReverseTransform( mousePt, out x, out y );
        ///		return "( " + x.ToString( "f2" ) + ", " + y.ToString( "f2" ) + " )";
        ///    #endregion
        ///    }</code>
        /// </example>
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe to this event to provide custom-formatting for cursor value tooltips")]
        public event CursorValueHandler CursorValueEvent;

        /// <summary>
        /// Subscribe to this event to provide notification of Double Clicks on graph
        /// objects
        /// </summary>
        /// <remarks>
        /// This event provides for a notification when the mouse is double-clicked on an object
        /// within any <see cref="GraphPane"/> of the <see cref="MasterPane"/> associated
        /// with this <see cref="ZedGraphControl" />.  This event will use the
        /// <see cref="ZedGraph.MasterPane.FindNearestPaneObject"/> method to determine which object
        /// was clicked.  The boolean value that you return from this handler determines whether
        /// or not the <see cref="ZedGraphControl"/> will do any further handling of the
        /// DoubleClick event (see <see cref="ZedMouseEventHandler" />).  Return true if you have
        /// handled the DoubleClick event entirely, and you do not
        /// want the <see cref="ZedGraphControl"/> to do any further action. 
        /// Return false if ZedGraph should go ahead and process the
        /// DoubleClick event.
        /// </remarks>
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe to be notified when the left mouse button is double-clicked")]
        public event ZedMouseEventHandler DoubleClickEvent;

        /// <summary>
        /// Subscribe to this event to be able to respond to mouse clicks within linked
        /// objects.
        /// </summary>
        /// <remarks>
        /// Linked objects are typically either <see cref="GraphObj" /> type objects or
        /// <see cref="CurveItem" /> type objects.  These object types can include
        /// hyperlink information allowing for "drill-down" type operation.  
        /// </remarks>
        /// <seealso cref="LinkEventHandler"/>
        /// <seealso cref="Link" />
        /// <seealso cref="CurveItem.Link">CurveItem.Link</seealso>
        /// <seealso cref="GraphObj.Link">GraphObj.Link</seealso>
        // /// <seealso cref="ZedGraph.Web.IsImageMap" />
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe to be notified when a link-enabled item is clicked")]
        public event LinkEventHandler LinkEvent;

        /// <summary>
        /// Hide the standard control MouseDown event so that the ZedGraphControl.MouseDownEvent
        /// can be used.  This is so that the user must return true/false in order to indicate
        /// whether or not we should respond to the event.
        /// </summary>
        [Bindable(false)]
        [Browsable(false)]
        public new event MouseEventHandler MouseDown;

        /// <summary>
        /// Subscribe to this event to provide notification of MouseDown clicks on graph
        /// objects
        /// </summary>
        /// <remarks>
        /// This event provides for a notification when the mouse is clicked on an object
        /// within any <see cref="GraphPane"/> of the <see cref="MasterPane"/> associated
        /// with this <see cref="ZedGraphControl" />.  This event will use the
        /// <see cref="ZedGraph.MasterPane.FindNearestPaneObject"/> method to determine which object
        /// was clicked.  The boolean value that you return from this handler determines whether
        /// or not the <see cref="ZedGraphControl"/> will do any further handling of the
        /// MouseDown event (see <see cref="ZedMouseEventHandler" />).  Return true if you have
        /// handled the MouseDown event entirely, and you do not
        /// want the <see cref="ZedGraphControl"/> to do any further action (e.g., starting
        /// a zoom operation).  Return false if ZedGraph should go ahead and process the
        /// MouseDown event.
        /// </remarks>
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe to be notified when the left mouse button is clicked down")]
        public event ZedMouseEventHandler MouseDownEvent;

        /// <summary>
        /// Subscribe to this event to provide notification of MouseMove events over graph
        /// objects
        /// </summary>
        /// <remarks>
        /// This event provides for a notification when the mouse is moving over on the control.
        /// The boolean value that you return from this handler determines whether
        /// or not the <see cref="ZedGraphControl"/> will do any further handling of the
        /// MouseMove event (see <see cref="ZedMouseEventHandler" />).  Return true if you
        /// have handled the MouseMove event entirely, and you do not
        /// want the <see cref="ZedGraphControl"/> to do any further action.
        /// Return false if ZedGraph should go ahead and process the MouseMove event.
        /// </remarks>
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe to be notified when the mouse is moved inside the control")]
        public event ZedMouseEventHandler MouseMoveEvent;

        /// <summary>
        /// Hide the standard control MouseUp event so that the ZedGraphControl.MouseUpEvent
        /// can be used.  This is so that the user must return true/false in order to indicate
        /// whether or not we should respond to the event.
        /// </summary>
        [Bindable(false)]
        [Browsable(false)]
        public new event MouseEventHandler MouseUp;

        /// <summary>
        /// Subscribe to this event to provide notification of MouseUp clicks on graph
        /// objects
        /// </summary>
        /// <remarks>
        /// This event provides for a notification when the mouse is clicked on an object
        /// within any <see cref="GraphPane"/> of the <see cref="MasterPane"/> associated
        /// with this <see cref="ZedGraphControl" />.  This event will use the
        /// <see cref="ZedGraph.MasterPane.FindNearestPaneObject"/> method to determine which object
        /// was clicked.  The boolean value that you return from this handler determines whether
        /// or not the <see cref="ZedGraphControl"/> will do any further handling of the
        /// MouseUp event (see <see cref="ZedMouseEventHandler" />).  Return true if you have
        /// handled the MouseUp event entirely, and you do not
        /// want the <see cref="ZedGraphControl"/> to do any further action (e.g., starting
        /// a zoom operation).  Return false if ZedGraph should go ahead and process the
        /// MouseUp event.
        /// </remarks>
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe to be notified when the left mouse button is released")]
        public event ZedMouseEventHandler MouseUpEvent;

        /// <summary>
        /// Subscribe to this event to receive notifcation and/or respond after a data
        /// point has been edited via <see cref="IsEnableHEdit" /> and <see cref="IsEnableVEdit" />.
        /// </summary>
        /// <example>
        /// <para>To subscribe to this event, use the following in your Form_Load method:</para>
        /// <code>zedGraphControl1.PointEditEvent +=
        /// new ZedGraphControl.PointEditHandler( MyPointEditHandler );</code>
        /// <para>Add this method to your Form1.cs:</para>
        /// <code>
        ///    private string MyPointEditHandler( object sender, GraphPane pane, CurveItem curve, int iPt )
        ///    {
        ///        PointPair pt = curve[iPt];
        ///        return "This value is " + pt.Y.ToString("f2") + " gallons";
        ///    }</code>
        /// </example>
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe to this event to respond to data point edit actions")]
        public event PointEditHandler PointEditEvent;

        /// <summary>
        /// Subscribe to this event to provide custom formatting for the tooltips
        /// </summary>
        /// <example>
        /// <para>To subscribe to this event, use the following in your FormLoad method:</para>
        /// <code>zedGraphControl1.PointValueEvent +=
        /// new ZedGraphControl.PointValueHandler( MyPointValueHandler );</code>
        /// <para>Add this method to your Form1.cs:</para>
        /// <code>
        ///    private string MyPointValueHandler( object sender, GraphPane pane, CurveItem curve, int iPt )
        ///    {
        ///    #region
        ///        PointPair pt = curve[iPt];
        ///        return "This value is " + pt.Y.ToString("f2") + " gallons";
        ///    #endregion
        ///    }</code>
        /// </example>
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe to this event to provide custom-formatting for data point tooltips")]
        public event PointValueHandler PointValueEvent;

        /// <summary>
        /// Subscribe to this event to be notified when the <see cref="GraphPane"/> is scrolled by the user
        /// using the scrollbars.
        /// </summary>
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe this event to be notified when a scroll operation using the scrollbars is completed")]
        public event ScrollDoneHandler ScrollDoneEvent;

        /// <summary>
        /// Subscribe to this event to be notified when the <see cref="GraphPane"/> is scrolled by the user
        /// using the scrollbars.
        /// </summary>
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe this event to be notified of general scroll events")]
        public event ScrollEventHandler ScrollEvent;

        /// <summary>
        /// Subscribe to this event to be notified when the <see cref="GraphPane"/> is scrolled by the user
        /// using the scrollbars.
        /// </summary>
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe this event to be notified continuously as a scroll operation is taking place")]
        public event ScrollProgressHandler ScrollProgressEvent;

        /// <summary>
        /// Subscribe to this event to be notified when the <see cref="GraphPane"/> is zoomed or panned by the user,
        /// either via a mouse drag operation or by the context menu commands.
        /// </summary>
        [Bindable(true)]
        [Category("Events")]
        [Description("Subscribe to this event to be notified when the graph is zoomed or panned")]
        public event ZoomEventHandler ZoomEvent;

        #endregion

        #region Events

        /// <summary>
        /// Hide the standard control MouseMove event so that the ZedGraphControl.MouseMoveEvent
        /// can be used.  This is so that the user must return true/false in order to indicate
        /// whether or not we should respond to the event.
        /// </summary>
        [Bindable(false)]
        [Browsable(false)]
        private new event MouseEventHandler MouseMove;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Zoom a specified pane in or out according to the specified zoom fraction.
        /// </summary>
        /// <remarks>
        /// The zoom will occur on the <see cref="XAxis"/>, <see cref="YAxis"/>, and
        /// <see cref="Y2Axis"/> only if the corresponding flag, <see cref="IsEnableHZoom"/> or
        /// <see cref="IsEnableVZoom"/>, is true.  Note that if there are multiple Y or Y2 axes, all of
        /// them will be zoomed.
        /// </remarks>
        /// <param name="pane">
        /// The <see cref="GraphPane"/> instance to be zoomed.
        /// </param>
        /// <param name="zoomFraction">
        /// The fraction by which to zoom, less than 1 to zoom in, greater than
        /// 1 to zoom out.  For example, 0.9 will zoom in such that the scale is 90% of what it was
        /// originally.
        /// </param>
        /// <param name="centerPt">
        /// The screen position about which the zoom will be centered.  This
        /// value is only used if <see paramref="isZoomOnCenter"/> is true.
        /// </param>
        /// <param name="isZoomOnCenter">
        /// true to cause the zoom to be centered on the point
        /// <see paramref="centerPt"/>, false to center on the <see cref="Chart.Rect"/>.
        /// </param>
        public void ZoomPane(GraphPane pane, double zoomFraction, PointF centerPt, bool isZoomOnCenter)
        {
            this.ZoomPane(pane, zoomFraction, centerPt, isZoomOnCenter, true);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Make a string label that corresponds to a user scale value.
        /// </summary>
        /// <param name="axis">
        /// The axis from which to obtain the scale value.  This determines
        /// if it's a date value, linear, log, etc.
        /// </param>
        /// <param name="val">
        /// The value to be made into a label
        /// </param>
        /// <param name="iPt">
        /// The ordinal position of the value
        /// </param>
        /// <param name="isOverrideOrdinal">
        /// true to override the ordinal settings of the axis,
        /// and prefer the actual value instead.
        /// </param>
        /// <returns>
        /// The string label.
        /// </returns>
        protected string MakeValueLabel(Axis axis, double val, int iPt, bool isOverrideOrdinal)
        {
            if (axis != null)
            {
                if (axis.Scale.IsDate || axis.Scale.Type == AxisType.DateAsOrdinal)
                {
                    return XDate.ToString(val, this._pointDateFormat);
                }
                else if (axis._scale.IsText && axis._scale._textLabels != null)
                {
                    int i = iPt;
                    if (isOverrideOrdinal)
                    {
                        i = (int)(val - 0.5);
                    }

                    if (i >= 0 && i < axis._scale._textLabels.Length)
                    {
                        return axis._scale._textLabels[i];
                    }
                    else
                    {
                        return (i + 1).ToString();
                    }
                }
                else if (axis.Scale.IsAnyOrdinal && axis.Scale.Type != AxisType.LinearAsOrdinal && !isOverrideOrdinal)
                {
                    return iPt.ToString(this._pointValueFormat);
                }
                else
                {
                    return val.ToString(this._pointValueFormat);
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Handle a panning operation for the specified <see cref="Axis"/>.
        /// </summary>
        /// <param name="axis">
        /// The <see cref="Axis"/> to be panned
        /// </param>
        /// <param name="startVal">
        /// The value where the pan started.  The scale range
        /// will be shifted by the difference between <see paramref="startVal"/> and
        /// <see paramref="endVal"/>.
        /// </param>
        /// <param name="endVal">
        /// The value where the pan ended.  The scale range
        /// will be shifted by the difference between <see paramref="startVal"/> and
        /// <see paramref="endVal"/>.
        /// </param>
        protected void PanScale(Axis axis, double startVal, double endVal)
        {
            if (axis != null)
            {
                Scale scale = axis._scale;
                double delta = scale.Linearize(startVal) - scale.Linearize(endVal);

                scale._minLinearized += delta;
                scale._maxLinearized += delta;

                scale._minAuto = false;
                scale._maxAuto = false;

                /*
								if ( axis.Type == AxisType.Log )
								{
									axis._scale._min *= startVal / endVal;
									axis._scale._max *= startVal / endVal;
								}
								else
								{
									axis._scale._min += startVal - endVal;
									axis._scale._max += startVal - endVal;
								}
				*/
            }
        }

        /// <summary>
        /// Set the cursor according to the current mouse location.
        /// </summary>
        protected void SetCursor()
        {
            this.SetCursor(this.PointToClient(Control.MousePosition));
        }

        /// <summary>
        /// Set the cursor according to the current mouse location.
        /// </summary>
        /// <param name="mousePt">
        /// The mouse Pt.
        /// </param>
        protected void SetCursor(Point mousePt)
        {
            if (this._masterPane != null)
            {
                GraphPane pane = this._masterPane.FindChartRect(mousePt);
                if ((this._isEnableHPan || this._isEnableVPan)
                    && (Control.ModifierKeys == Keys.Shift || this._isPanning) && (pane != null || this._isPanning))
                {
                    this.Cursor = Cursors.Hand;
                }
                else if ((this._isEnableVZoom || this._isEnableHZoom) && (pane != null || this._isZooming))
                {
                    this.Cursor = Cursors.Cross;
                }
                else if (this._isEnableSelection && (pane != null || this._isSelecting))
                {
                    this.Cursor = Cursors.Cross;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }

                // 			else if ( isZoomMode || isPanMode )
                // 				this.Cursor = Cursors.No;
            }
        }

        /// <summary>
        /// Handle the Key Events so ZedGraph can Escape out of a panning or zooming operation.
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        protected void ZedGraphControl_KeyDown(object sender, KeyEventArgs e)
        {
            this.SetCursor();

            if (e.KeyCode == Keys.Escape)
            {
                if (this._isPanning)
                {
                    this.HandlePanCancel();
                }

                if (this._isZooming)
                {
                    this.HandleZoomCancel();
                }

                if (this._isEditing)
                {
                    this.HandleEditCancel();
                }

                // if ( _isSelecting )
                // Esc always cancels the selection
                this.HandleSelectionCancel();

                this._isZooming = false;
                this._isPanning = false;
                this._isEditing = false;
                this._isSelecting = false;

                this.Refresh();
            }
        }

        /// <summary>
        /// Handle a KeyUp event
        /// </summary>
        /// <param name="sender">
        /// The <see cref="ZedGraphControl"/> in which the KeyUp occurred.
        /// </param>
        /// <param name="e">
        /// A <see cref="KeyEventArgs"/> instance.
        /// </param>
        protected void ZedGraphControl_KeyUp(object sender, KeyEventArgs e)
        {
            this.SetCursor();
        }

        /// <summary>
        /// Handle a MouseDown event in the <see cref="ZedGraphControl"/>
        /// </summary>
        /// <param name="sender">
        /// A reference to the <see cref="ZedGraphControl"/>
        /// </param>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> instance
        /// </param>
        protected void ZedGraphControl_MouseDown(object sender, MouseEventArgs e)
        {
            this._isPanning = false;
            this._isZooming = false;
            this._isEditing = false;
            this._isSelecting = false;
            this._dragPane = null;

            Point mousePt = new Point(e.X, e.Y);

            // Callback for doubleclick events
            if (this._masterPane != null && e.Clicks > 1 && this.DoubleClickEvent != null)
            {
                if (this.DoubleClickEvent(this, e))
                {
                    return;
                }
            }

            // Provide Callback for MouseDown events
            if (this._masterPane != null && this.MouseDownEvent != null)
            {
                if (this.MouseDownEvent(this, e))
                {
                    return;
                }
            }

            if (e.Clicks > 1 || this._masterPane == null)
            {
                return;
            }

            // First, see if the click is within a Linkable object within any GraphPane
            GraphPane pane = this.MasterPane.FindPane(mousePt);
            if (pane != null && e.Button == this._linkButtons && Control.ModifierKeys == this._linkModifierKeys)
            {
                object source;
                Link link;
                int index;
                using (Graphics g = this.CreateGraphics())
                {
                    float scaleFactor = pane.CalcScaleFactor();
                    if (pane.FindLinkableObject(mousePt, g, scaleFactor, out source, out link, out index))
                    {
                        if (this.LinkEvent != null && this.LinkEvent(this, pane, source, link, index))
                        {
                            return;
                        }

                        string url;
                        CurveItem curve = source as CurveItem;

                        if (curve != null)
                        {
                            url = link.MakeCurveItemUrl(pane, curve, index);
                        }
                        else
                        {
                            url = link._url;
                        }

                        if (url != string.Empty)
                        {
                            System.Diagnostics.Process.Start(url);

                            // linkable objects override any other actions with mouse
                            return;
                        }
                    }

                    // g.Dispose();
                }
            }

            // Second, Check to see if it's within a Chart Rect
            pane = this.MasterPane.FindChartRect(mousePt);

            // Rectangle rect = new Rectangle( mousePt, new Size( 1, 1 ) );
            if (pane != null && (this._isEnableHPan || this._isEnableVPan)
                && ((e.Button == this._panButtons && Control.ModifierKeys == this._panModifierKeys)
                    || (e.Button == this._panButtons2 && Control.ModifierKeys == this._panModifierKeys2)))
            {
                this._isPanning = true;
                this._dragStartPt = mousePt;
                this._dragPane = pane;

                // _zoomState = new ZoomState( _dragPane, ZoomState.StateType.Pan );
                this.ZoomStateSave(this._dragPane, ZoomState.StateType.Pan);
            }
            else if (pane != null && (this._isEnableHZoom || this._isEnableVZoom)
                     && ((e.Button == this._zoomButtons && Control.ModifierKeys == this._zoomModifierKeys)
                         || (e.Button == this._zoomButtons2 && Control.ModifierKeys == this._zoomModifierKeys2)))
            {
                this._isZooming = true;
                this._dragStartPt = mousePt;
                this._dragEndPt = mousePt;
                this._dragEndPt.Offset(1, 1);
                this._dragPane = pane;
                this.ZoomStateSave(this._dragPane, ZoomState.StateType.Zoom);
            }
                
                // Revision: JCarpenter 10/06
            else if (pane != null && this._isEnableSelection && e.Button == this._selectButtons
                     && (Control.ModifierKeys == this._selectModifierKeys
                         || Control.ModifierKeys == this._selectAppendModifierKeys))
            {
                this._isSelecting = true;
                this._dragStartPt = mousePt;
                this._dragEndPt = mousePt;
                this._dragEndPt.Offset(1, 1);
                this._dragPane = pane;
            }
            else if (pane != null && (this._isEnableHEdit || this._isEnableVEdit)
                     && (e.Button == this.EditButtons && Control.ModifierKeys == this.EditModifierKeys))
            {
                // find the point that was clicked, and make sure the point list is editable
                // and that it's a primary Y axis (the first Y or Y2 axis)
                if (pane.FindNearestPoint(mousePt, out this._dragCurve, out this._dragIndex)
                    && this._dragCurve.Points is IPointListEdit)
                {
                    this._isEditing = true;
                    this._dragPane = pane;
                    this._dragStartPt = mousePt;
                    this._dragStartPair = this._dragCurve[this._dragIndex];
                }
            }
        }

        /// <summary>
        /// protected method for handling MouseMove events to display tooltips over
        /// individual datapoints.
        /// </summary>
        /// <param name="sender">
        /// A reference to the control that has the MouseMove event.
        /// </param>
        /// <param name="e">
        /// A MouseEventArgs object.
        /// </param>
        protected void ZedGraphControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._masterPane != null)
            {
                Point mousePt = new Point(e.X, e.Y);

                // Provide Callback for MouseMove events
                if (this.MouseMoveEvent != null && this.MouseMoveEvent(this, e))
                {
                    return;
                }

                // Point tempPt = this.PointToClient( Control.MousePosition );
                this.SetCursor(mousePt);

                // If the mouse is being dragged,
                // undraw and redraw the rectangle as the mouse moves.
                if (this._isZooming)
                {
                    this.HandleZoomDrag(mousePt);
                }
                else if (this._isPanning)
                {
                    this.HandlePanDrag(mousePt);
                }
                else if (this._isEditing)
                {
                    this.HandleEditDrag(mousePt);
                }
                else if (this._isShowCursorValues)
                {
                    this.HandleCursorValues(mousePt);
                }
                else if (this._isShowPointValues)
                {
                    this.HandlePointValues(mousePt);
                }
                    
                    // Revision: JCarpenter 10/06
                else if (this._isSelecting)
                {
                    this.HandleZoomDrag(mousePt);
                }
            }
        }

        /// <summary>
        /// Handle a MouseUp event in the <see cref="ZedGraphControl"/>
        /// </summary>
        /// <param name="sender">
        /// A reference to the <see cref="ZedGraphControl"/>
        /// </param>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> instance
        /// </param>
        protected void ZedGraphControl_MouseUp(object sender, MouseEventArgs e)
        {
            // Provide Callback for MouseUp events
            if (this._masterPane != null && this.MouseUpEvent != null)
            {
                if (this.MouseUpEvent(this, e))
                {
                    return;
                }
            }

            if (this._masterPane != null && this._dragPane != null)
            {
                // If the MouseUp event occurs, the user is done dragging.
                if (this._isZooming)
                {
                    this.HandleZoomFinish(sender, e);
                }
                else if (this._isPanning)
                {
                    this.HandlePanFinish();
                }
                else if (this._isEditing)
                {
                    this.HandleEditFinish();
                }
                    
                    // Revision: JCarpenter 10/06
                else if (this._isSelecting)
                {
                    this.HandleSelectionFinish(sender, e);
                }
            }

            // Reset the rectangle.
            // dragStartPt = new Rectangle( 0, 0, 0, 0 );
            this._dragPane = null;
            this._isZooming = false;
            this._isPanning = false;
            this._isEditing = false;
            this._isSelecting = false;

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Handle a MouseWheel event in the <see cref="ZedGraphControl"/>
        /// </summary>
        /// <param name="sender">
        /// A reference to the <see cref="ZedGraphControl"/>
        /// </param>
        /// <param name="e">
        /// A <see cref="MouseEventArgs"/> instance
        /// </param>
        protected void ZedGraphControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((this._isEnableVZoom || this._isEnableHZoom) && this._isEnableWheelZoom && this._masterPane != null)
            {
                GraphPane pane = this.MasterPane.FindChartRect(new PointF(e.X, e.Y));
                if (pane != null && e.Delta != 0)
                {
                    ZoomState oldState = this.ZoomStateSave(pane, ZoomState.StateType.WheelZoom);

                    // ZoomState oldState = pane.ZoomStack.Push( pane, ZoomState.StateType.Zoom );
                    PointF centerPoint = new PointF(e.X, e.Y);
                    double zoomFraction = 1 + (e.Delta < 0 ? 1.0 : -1.0) * this.ZoomStepFraction;

                    this.ZoomPane(pane, zoomFraction, centerPoint, this._isZoomOnMouseCenter, false);

                    this.ApplyToAllPanes(pane);

                    using (Graphics g = this.CreateGraphics())
                    {
                        // always AxisChange() the dragPane
                        pane.AxisChange(g);

                        foreach (GraphPane tempPane in this._masterPane._paneList)
                        {
                            if (tempPane != pane && (this._isSynchronizeXAxes || this._isSynchronizeYAxes))
                            {
                                tempPane.AxisChange(g);
                            }
                        }
                    }

                    this.ZoomStatePush(pane);

                    // Provide Callback to notify the user of zoom events
                    if (this.ZoomEvent != null)
                    {
                        this.ZoomEvent(this, oldState, new ZoomState(pane, ZoomState.StateType.WheelZoom));
                    }

                    this.Refresh();
                }
            }
        }

        /// <summary>
        /// Zoom a specified pane in or out according to the specified zoom fraction.
        /// </summary>
        /// <remarks>
        /// The zoom will occur on the <see cref="XAxis"/>, <see cref="YAxis"/>, and
        /// <see cref="Y2Axis"/> only if the corresponding flag, <see cref="IsEnableHZoom"/> or
        /// <see cref="IsEnableVZoom"/>, is true.  Note that if there are multiple Y or Y2 axes, all of
        /// them will be zoomed.
        /// </remarks>
        /// <param name="pane">
        /// The <see cref="GraphPane"/> instance to be zoomed.
        /// </param>
        /// <param name="zoomFraction">
        /// The fraction by which to zoom, less than 1 to zoom in, greater than
        /// 1 to zoom out.  For example, 0.9 will zoom in such that the scale is 90% of what it was
        /// originally.
        /// </param>
        /// <param name="centerPt">
        /// The screen position about which the zoom will be centered.  This
        /// value is only used if <see paramref="isZoomOnCenter"/> is true.
        /// </param>
        /// <param name="isZoomOnCenter">
        /// true to cause the zoom to be centered on the point
        /// <see paramref="centerPt"/>, false to center on the <see cref="Chart.Rect"/>.
        /// </param>
        /// <param name="isRefresh">
        /// true to force a refresh of the control, false to leave it unrefreshed
        /// </param>
        protected void ZoomPane(
            GraphPane pane, 
            double zoomFraction, 
            PointF centerPt, 
            bool isZoomOnCenter, 
            bool isRefresh)
        {
            double x;
            double x2;
            double[] y;
            double[] y2;

            pane.ReverseTransform(centerPt, out x, out x2, out y, out y2);

            if (this._isEnableHZoom)
            {
                this.ZoomScale(pane.XAxis, zoomFraction, x, isZoomOnCenter);
                this.ZoomScale(pane.X2Axis, zoomFraction, x2, isZoomOnCenter);
            }

            if (this._isEnableVZoom)
            {
                for (int i = 0; i < pane.YAxisList.Count; i++)
                {
                    this.ZoomScale(pane.YAxisList[i], zoomFraction, y[i], isZoomOnCenter);
                }

                for (int i = 0; i < pane.Y2AxisList.Count; i++)
                {
                    this.ZoomScale(pane.Y2AxisList[i], zoomFraction, y2[i], isZoomOnCenter);
                }
            }

            using (Graphics g = this.CreateGraphics())
            {
                pane.AxisChange(g);

                // g.Dispose();
            }

            this.SetScroll(this.hScrollBar1, pane.XAxis, this._xScrollRange.Min, this._xScrollRange.Max);
            this.SetScroll(this.vScrollBar1, pane.YAxis, this._yScrollRangeList[0].Min, this._yScrollRangeList[0].Max);

            if (isRefresh)
            {
                this.Refresh();
            }
        }

        /// <summary>
        /// Zoom the specified axis by the specified amount, with the center of the zoom at the
        /// (optionally) specified point.
        /// </summary>
        /// <remarks>
        /// This method is used for MouseWheel zoom operations
        /// </remarks>
        /// <param name="axis">
        /// The <see cref="Axis"/> to be zoomed.
        /// </param>
        /// <param name="zoomFraction">
        /// The zoom fraction, less than 1.0 to zoom in, greater than 1.0 to
        /// zoom out.  That is, a value of 0.9 will zoom in such that the scale length is 90% of what
        /// it previously was.
        /// </param>
        /// <param name="centerVal">
        /// The location for the center of the zoom.  This is only used if
        /// <see paramref="IsZoomOnMouseCenter"/> is true.
        /// </param>
        /// <param name="isZoomOnCenter">
        /// true if the zoom is to be centered at the
        /// <see paramref="centerVal"/> screen position, false for the zoom to be centered within
        /// the <see cref="Chart.Rect"/>.
        /// </param>
        protected void ZoomScale(Axis axis, double zoomFraction, double centerVal, bool isZoomOnCenter)
        {
            if (axis != null && zoomFraction > 0.0001 && zoomFraction < 1000.0)
            {
                Scale scale = axis._scale;

                /*
								if ( axis.Scale.IsLog )
								{
									double ratio = Math.Sqrt( axis._scale._max / axis._scale._min * zoomFraction );

									if ( !isZoomOnCenter )
										centerVal = Math.Sqrt( axis._scale._max * axis._scale._min );

									axis._scale._min = centerVal / ratio;
									axis._scale._max = centerVal * ratio;
								}
								else
								{
				*/
                double minLin = axis._scale._minLinearized;
                double maxLin = axis._scale._maxLinearized;
                double range = (maxLin - minLin) * zoomFraction / 2.0;

                if (!isZoomOnCenter)
                {
                    centerVal = (maxLin + minLin) / 2.0;
                }

                axis._scale._minLinearized = centerVal - range;
                axis._scale._maxLinearized = centerVal + range;

                // 				}
                axis._scale._minAuto = false;
                axis._scale._maxAuto = false;
            }
        }

        /// <summary>
        /// The bound point to rect.
        /// </summary>
        /// <param name="mousePt">
        /// The mouse pt.
        /// </param>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        private PointF BoundPointToRect(Point mousePt, RectangleF rect)
        {
            PointF newPt = new PointF(mousePt.X, mousePt.Y);

            if (mousePt.X < rect.X)
            {
                newPt.X = rect.X;
            }

            if (mousePt.X > rect.Right)
            {
                newPt.X = rect.Right;
            }

            if (mousePt.Y < rect.Y)
            {
                newPt.Y = rect.Y;
            }

            if (mousePt.Y > rect.Bottom)
            {
                newPt.Y = rect.Bottom;
            }

            return newPt;
        }

        /// <summary>
        /// The calc screen rect.
        /// </summary>
        /// <param name="mousePt1">
        /// The mouse pt 1.
        /// </param>
        /// <param name="mousePt2">
        /// The mouse pt 2.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/>.
        /// </returns>
        private Rectangle CalcScreenRect(Point mousePt1, Point mousePt2)
        {
            Point screenPt = this.PointToScreen(mousePt1);
            Size size = new Size(mousePt2.X - mousePt1.X, mousePt2.Y - mousePt1.Y);
            Rectangle rect = new Rectangle(screenPt, size);

            if (this._isZooming)
            {
                Rectangle chartRect = Rectangle.Round(this._dragPane.Chart._rect);

                Point chartPt = this.PointToScreen(chartRect.Location);

                if (!this._isEnableVZoom)
                {
                    rect.Y = chartPt.Y;
                    rect.Height = chartRect.Height + 1;
                }
                else if (!this._isEnableHZoom)
                {
                    rect.X = chartPt.X;
                    rect.Width = chartRect.Width + 1;
                }
            }

            return rect;
        }

        /// <summary>
        /// The handle cursor values.
        /// </summary>
        /// <param name="mousePt">
        /// The mouse pt.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        private Point HandleCursorValues(Point mousePt)
        {
            GraphPane pane = this._masterPane.FindPane(mousePt);
            if (pane != null && pane.Chart._rect.Contains(mousePt))
            {
                // Provide Callback for User to customize the tooltips
                if (this.CursorValueEvent != null)
                {
                    string label = this.CursorValueEvent(this, pane, mousePt);
                    if (label != null && label.Length > 0)
                    {
                        this.pointToolTip.SetToolTip(this, label);
                        this.pointToolTip.Active = true;
                    }
                    else
                    {
                        this.pointToolTip.Active = false;
                    }
                }
                else
                {
                    double x, x2, y, y2;
                    pane.ReverseTransform(mousePt, out x, out x2, out y, out y2);
                    string xStr = this.MakeValueLabel(pane.XAxis, x, -1, true);
                    string yStr = this.MakeValueLabel(pane.YAxis, y, -1, true);
                    string y2Str = this.MakeValueLabel(pane.Y2Axis, y2, -1, true);

                    this.pointToolTip.SetToolTip(this, "( " + xStr + ", " + yStr + ", " + y2Str + " )");
                    this.pointToolTip.Active = true;
                }
            }
            else
            {
                this.pointToolTip.Active = false;
            }

            return mousePt;
        }

        /// <summary>
        /// The handle edit cancel.
        /// </summary>
        private void HandleEditCancel()
        {
            if (this._isEditing)
            {
                IPointListEdit list = this._dragCurve.Points as IPointListEdit;
                if (list != null)
                {
                    list[this._dragIndex] = this._dragStartPair;
                }

                this._isEditing = false;
                this.Refresh();
            }
        }

        /// <summary>
        /// The handle edit drag.
        /// </summary>
        /// <param name="mousePt">
        /// The mouse pt.
        /// </param>
        private void HandleEditDrag(Point mousePt)
        {
            // get the scale values that correspond to the current point
            double curX, curY;
            this._dragPane.ReverseTransform(
                mousePt, 
                this._dragCurve.IsX2Axis, 
                this._dragCurve.IsY2Axis, 
                this._dragCurve.YAxisIndex, 
                out curX, 
                out curY);
            double startX, startY;
            this._dragPane.ReverseTransform(
                this._dragStartPt, 
                this._dragCurve.IsX2Axis, 
                this._dragCurve.IsY2Axis, 
                this._dragCurve.YAxisIndex, 
                out startX, 
                out startY);

            // calculate the new scale values for the point
            PointPair newPt = new PointPair(this._dragStartPair);

            Scale xScale = this._dragCurve.GetXAxis(this._dragPane)._scale;
            if (this._isEnableHEdit)
            {
                newPt.X =
                    xScale.DeLinearize(xScale.Linearize(newPt.X) + xScale.Linearize(curX) - xScale.Linearize(startX));
            }

            Scale yScale = this._dragCurve.GetYAxis(this._dragPane)._scale;
            if (this._isEnableVEdit)
            {
                newPt.Y =
                    yScale.DeLinearize(yScale.Linearize(newPt.Y) + yScale.Linearize(curY) - yScale.Linearize(startY));
            }

            // save the data back to the point list
            IPointListEdit list = this._dragCurve.Points as IPointListEdit;
            if (list != null)
            {
                list[this._dragIndex] = newPt;
            }

            // force a redraw
            this.Refresh();
        }

        /// <summary>
        /// The handle edit finish.
        /// </summary>
        private void HandleEditFinish()
        {
            if (this.PointEditEvent != null)
            {
                this.PointEditEvent(this, this._dragPane, this._dragCurve, this._dragIndex);
            }
        }

        /// <summary>
        /// The handle pan cancel.
        /// </summary>
        private void HandlePanCancel()
        {
            if (this._isPanning)
            {
                if (this._zoomState != null && this._zoomState.IsChanged(this._dragPane))
                {
                    this.ZoomStateRestore(this._dragPane);

                    // _zoomState.ApplyState( _dragPane );
                    // _zoomState = null;
                }

                this._isPanning = false;
                this.Refresh();

                this.ZoomStateClear();
            }
        }

        /// <summary>
        /// The handle pan drag.
        /// </summary>
        /// <param name="mousePt">
        /// The mouse pt.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        private Point HandlePanDrag(Point mousePt)
        {
            double x1, x2, xx1, xx2;
            double[] y1, y2, yy1, yy2;

            // PointF endPoint = mousePt;
            // PointF startPoint = ( (Control)sender ).PointToClient( this.dragRect.Location );
            this._dragPane.ReverseTransform(this._dragStartPt, out x1, out xx1, out y1, out yy1);
            this._dragPane.ReverseTransform(mousePt, out x2, out xx2, out y2, out yy2);

            if (this._isEnableHPan)
            {
                this.PanScale(this._dragPane.XAxis, x1, x2);
                this.PanScale(this._dragPane.X2Axis, xx1, xx2);
                this.SetScroll(this.hScrollBar1, this._dragPane.XAxis, this._xScrollRange.Min, this._xScrollRange.Max);
            }

            if (this._isEnableVPan)
            {
                for (int i = 0; i < y1.Length; i++)
                {
                    this.PanScale(this._dragPane.YAxisList[i], y1[i], y2[i]);
                }

                for (int i = 0; i < yy1.Length; i++)
                {
                    this.PanScale(this._dragPane.Y2AxisList[i], yy1[i], yy2[i]);
                }

                this.SetScroll(
                    this.vScrollBar1, 
                    this._dragPane.YAxis, 
                    this._yScrollRangeList[0].Min, 
                    this._yScrollRangeList[0].Max);
            }

            this.ApplyToAllPanes(this._dragPane);

            this.Refresh();

            this._dragStartPt = mousePt;

            return mousePt;
        }

        /// <summary>
        /// The handle pan finish.
        /// </summary>
        private void HandlePanFinish()
        {
            // push the prior saved zoomstate, since the scale ranges have already been changed on
            // the fly during the panning operation
            if (this._zoomState != null && this._zoomState.IsChanged(this._dragPane))
            {
                // _dragPane.ZoomStack.Push( _zoomState );
                this.ZoomStatePush(this._dragPane);

                // Provide Callback to notify the user of pan events
                if (this.ZoomEvent != null)
                {
                    this.ZoomEvent(this, this._zoomState, new ZoomState(this._dragPane, ZoomState.StateType.Pan));
                }

                this._zoomState = null;
            }
        }

        /// <summary>
        /// The handle point values.
        /// </summary>
        /// <param name="mousePt">
        /// The mouse pt.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        private Point HandlePointValues(Point mousePt)
        {
            int iPt;
            GraphPane pane;
            object nearestObj;

            using (Graphics g = this.CreateGraphics())
            {
                if (this._masterPane.FindNearestPaneObject(mousePt, g, out pane, out nearestObj, out iPt))
                {
                    if (nearestObj is CurveItem && iPt >= 0)
                    {
                        CurveItem curve = (CurveItem)nearestObj;

                        // Provide Callback for User to customize the tooltips
                        if (this.PointValueEvent != null)
                        {
                            string label = this.PointValueEvent(this, pane, curve, iPt);
                            if (label != null && label.Length > 0)
                            {
                                this.pointToolTip.SetToolTip(this, label);
                                this.pointToolTip.Active = true;
                            }
                            else
                            {
                                this.pointToolTip.Active = false;
                            }
                        }
                        else
                        {
                            if (curve is PieItem)
                            {
                                this.pointToolTip.SetToolTip(
                                    this, 
                                    ((PieItem)curve).Value.ToString(this._pointValueFormat));
                            }
                                
                                // 							else if ( curve is OHLCBarItem || curve is JapaneseCandleStickItem )
                                // 							{
                                // 								StockPt spt = (StockPt)curve.Points[iPt];
                                // 								this.pointToolTip.SetToolTip( this, ( (XDate) spt.Date ).ToString( "MM/dd/yyyy" ) + "\nOpen: $" +
                                // 								spt.Open.ToString( "N2" ) +
                                // 								"\nHigh: $" +
                                // 								spt.High.ToString( "N2" ) + "\nLow: $" +
                                // 								spt.Low.ToString( "N2" ) + "\nClose: $" +
                                // 								spt.Close.ToString
                                // 								( "N2" ) );
                                // 							}
                            else
                            {
                                PointPair pt = curve.Points[iPt];

                                if (pt.Tag is string)
                                {
                                    this.pointToolTip.SetToolTip(this, (string)pt.Tag);
                                }
                                else
                                {
                                    double xVal, yVal, lowVal;
                                    ValueHandler valueHandler = new ValueHandler(pane, false);
                                    if ((curve is BarItem || curve is ErrorBarItem || curve is HiLowBarItem)
                                        && pane.BarSettings.Base != BarBase.X)
                                    {
                                        valueHandler.GetValues(curve, iPt, out yVal, out lowVal, out xVal);
                                    }
                                    else
                                    {
                                        valueHandler.GetValues(curve, iPt, out xVal, out lowVal, out yVal);
                                    }

                                    string xStr = this.MakeValueLabel(
                                        curve.GetXAxis(pane), 
                                        xVal, 
                                        iPt, 
                                        curve.IsOverrideOrdinal);
                                    string yStr = this.MakeValueLabel(
                                        curve.GetYAxis(pane), 
                                        yVal, 
                                        iPt, 
                                        curve.IsOverrideOrdinal);

                                    this.pointToolTip.SetToolTip(this, "( " + xStr + ", " + yStr + " )");

                                    // this.pointToolTip.SetToolTip( this,
                                    // 	curve.Points[iPt].ToString( this.pointValueFormat ) );
                                }
                            }

                            this.pointToolTip.Active = true;
                        }
                    }
                    else
                    {
                        this.pointToolTip.Active = false;
                    }
                }
                else
                {
                    this.pointToolTip.Active = false;
                }

                // g.Dispose();
            }

            return mousePt;
        }

        /// <summary>
        /// The handle selection cancel.
        /// </summary>
        private void HandleSelectionCancel()
        {
            this._isSelecting = false;

            this._selection.ClearSelection(this._masterPane);

            this.Refresh();
        }

        /// <summary>
        /// Perform selection on curves within the drag pane, or under the mouse click.
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        private void HandleSelectionFinish(object sender, MouseEventArgs e)
        {
            if (e.Button != this._selectButtons)
            {
                this.Refresh();
                return;
            }

            PointF mousePtF = this.BoundPointToRect(new Point(e.X, e.Y), this._dragPane.Chart._rect);

            PointF mousePt = this.BoundPointToRect(new Point(e.X, e.Y), this._dragPane.Rect);

            Point curPt = ((Control)sender).PointToScreen(Point.Round(mousePt));

            // Only accept a drag if it covers at least 5 pixels in each direction
            // Point curPt = ( (Control)sender ).PointToScreen( Point.Round( mousePt ) );
            if ((Math.Abs(mousePtF.X - this._dragStartPt.X) > 4) && (Math.Abs(mousePtF.Y - this._dragStartPt.Y) > 4))
            {
                double x1, x2, xx1, xx2;
                double[] y1, y2, yy1, yy2;
                PointF startPoint =
                    ((Control)sender).PointToClient(
                        new Point(Convert.ToInt32(this._dragPane.Rect.X), Convert.ToInt32(this._dragPane.Rect.Y)));

                this._dragPane.ReverseTransform(this._dragStartPt, out x1, out xx1, out y1, out yy1);
                this._dragPane.ReverseTransform(mousePtF, out x2, out xx2, out y2, out yy2);

                CurveList objects = new CurveList();

                double left = Math.Min(x1, x2);
                double right = Math.Max(x1, x2);

                double top = 0;
                double bottom = 0;

                for (int i = 0; i < y1.Length; i++)
                {
                    bottom = Math.Min(y1[i], y2[i]);
                    top = Math.Max(y1[i], y2[i]);
                }

                for (int i = 0; i < yy1.Length; i++)
                {
                    bottom = Math.Min(bottom, yy2[i]);
                    bottom = Math.Min(yy1[i], bottom);
                    top = Math.Max(top, yy2[i]);
                    top = Math.Max(yy1[i], top);
                }

                double w = right - left;
                double h = bottom - top;

                RectangleF rF = new RectangleF((float)left, (float)top, (float)w, (float)h);

                this._dragPane.FindContainedObjects(rF, this.CreateGraphics(), out objects);

                if (Control.ModifierKeys == this._selectAppendModifierKeys)
                {
                    this._selection.AddToSelection(this._masterPane, objects);
                }
                else
                {
                    this._selection.Select(this._masterPane, objects);
                }

                // 				this.Select( objects );

                // Graphics g = this.CreateGraphics();
                // this._dragPane.AxisChange( g );
                // g.Dispose();
            }
            else
            {
                // It's a single-select

                // Point mousePt = new Point( e.X, e.Y );
                int iPt;
                GraphPane pane;
                object nearestObj;

                using (Graphics g = this.CreateGraphics())
                {
                    if (this.MasterPane.FindNearestPaneObject(mousePt, g, out pane, out nearestObj, out iPt))
                    {
                        if (nearestObj is CurveItem && iPt >= 0)
                        {
                            if (Control.ModifierKeys == this._selectAppendModifierKeys)
                            {
                                this._selection.AddToSelection(this._masterPane, nearestObj as CurveItem);
                            }
                            else
                            {
                                this._selection.Select(this._masterPane, nearestObj as CurveItem);
                            }
                        }
                        else
                        {
                            this._selection.ClearSelection(this._masterPane);
                        }

                        this.Refresh();
                    }
                    else
                    {
                        this._selection.ClearSelection(this._masterPane);
                    }
                }
            }

            using (Graphics g = this.CreateGraphics())
            {
                // always AxisChange() the dragPane
                this._dragPane.AxisChange(g);

                foreach (GraphPane pane in this._masterPane._paneList)
                {
                    if (pane != this._dragPane && (this._isSynchronizeXAxes || this._isSynchronizeYAxes))
                    {
                        pane.AxisChange(g);
                    }
                }
            }

            this.Refresh();
        }

        /// <summary>
        /// The handle zoom cancel.
        /// </summary>
        private void HandleZoomCancel()
        {
            if (this._isZooming)
            {
                this._isZooming = false;
                this.Refresh();

                this.ZoomStateClear();
            }
        }

        /// <summary>
        /// The handle zoom drag.
        /// </summary>
        /// <param name="mousePt">
        /// The mouse pt.
        /// </param>
        private void HandleZoomDrag(Point mousePt)
        {
            // Hide the previous rectangle by calling the
            // DrawReversibleFrame method with the same parameters.
            Rectangle rect = this.CalcScreenRect(this._dragStartPt, this._dragEndPt);
            ControlPaint.DrawReversibleFrame(rect, this.BackColor, FrameStyle.Dashed);

            // Bound the zoom to the ChartRect
            this._dragEndPt = Point.Round(this.BoundPointToRect(mousePt, this._dragPane.Chart._rect));
            rect = this.CalcScreenRect(this._dragStartPt, this._dragEndPt);

            // Draw the new rectangle by calling DrawReversibleFrame again.
            ControlPaint.DrawReversibleFrame(rect, this.BackColor, FrameStyle.Dashed);
        }

        /// <summary>
        /// The handle zoom finish.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void HandleZoomFinish(object sender, MouseEventArgs e)
        {
            PointF mousePtF = this.BoundPointToRect(new Point(e.X, e.Y), this._dragPane.Chart._rect);

            // Only accept a drag if it covers at least 5 pixels in each direction
            // Point curPt = ( (Control)sender ).PointToScreen( Point.Round( mousePt ) );
            if ((Math.Abs(mousePtF.X - this._dragStartPt.X) > 4 || !this._isEnableHZoom)
                && (Math.Abs(mousePtF.Y - this._dragStartPt.Y) > 4 || !this._isEnableVZoom))
            {
                // Draw the rectangle to be evaluated. Set a dashed frame style
                // using the FrameStyle enumeration.
                // ControlPaint.DrawReversibleFrame( this.dragRect,
                // 	this.BackColor, FrameStyle.Dashed );
                double x1, x2, xx1, xx2;
                double[] y1, y2, yy1, yy2;

                // PointF startPoint = ( (Control)sender ).PointToClient( this.dragRect.Location );
                this._dragPane.ReverseTransform(this._dragStartPt, out x1, out xx1, out y1, out yy1);
                this._dragPane.ReverseTransform(mousePtF, out x2, out xx2, out y2, out yy2);

                bool zoomLimitExceeded = false;

                if (this._isEnableHZoom)
                {
                    double min1 = Math.Min(x1, x2);
                    double max1 = Math.Max(x1, x2);
                    double min2 = Math.Min(xx1, xx2);
                    double max2 = Math.Max(xx1, xx2);

                    if (Math.Abs(x1 - x2) < ZoomResolution || Math.Abs(xx1 - xx2) < ZoomResolution)
                    {
                        zoomLimitExceeded = true;
                    }
                }

                if (this._isEnableVZoom && !zoomLimitExceeded)
                {
                    for (int i = 0; i < y1.Length; i++)
                    {
                        if (Math.Abs(y1[i] - y2[i]) < ZoomResolution)
                        {
                            zoomLimitExceeded = true;
                            break;
                        }
                    }

                    for (int i = 0; i < yy1.Length; i++)
                    {
                        if (Math.Abs(yy1[i] - yy2[i]) < ZoomResolution)
                        {
                            zoomLimitExceeded = true;
                            break;
                        }
                    }
                }

                if (!zoomLimitExceeded)
                {
                    this.ZoomStatePush(this._dragPane);

                    // ZoomState oldState = _dragPane.ZoomStack.Push( _dragPane,
                    // 			ZoomState.StateType.Zoom );
                    if (this._isEnableHZoom)
                    {
                        this._dragPane.XAxis._scale._min = Math.Min(x1, x2);
                        this._dragPane.XAxis._scale._minAuto = false;
                        this._dragPane.XAxis._scale._max = Math.Max(x1, x2);
                        this._dragPane.XAxis._scale._maxAuto = false;

                        this._dragPane.X2Axis._scale._min = Math.Min(xx1, xx2);
                        this._dragPane.X2Axis._scale._minAuto = false;
                        this._dragPane.X2Axis._scale._max = Math.Max(xx1, xx2);
                        this._dragPane.X2Axis._scale._maxAuto = false;
                    }

                    if (this._isEnableVZoom)
                    {
                        for (int i = 0; i < y1.Length; i++)
                        {
                            this._dragPane.YAxisList[i]._scale._min = Math.Min(y1[i], y2[i]);
                            this._dragPane.YAxisList[i]._scale._max = Math.Max(y1[i], y2[i]);
                            this._dragPane.YAxisList[i]._scale._minAuto = false;
                            this._dragPane.YAxisList[i]._scale._maxAuto = false;
                        }

                        for (int i = 0; i < yy1.Length; i++)
                        {
                            this._dragPane.Y2AxisList[i]._scale._min = Math.Min(yy1[i], yy2[i]);
                            this._dragPane.Y2AxisList[i]._scale._max = Math.Max(yy1[i], yy2[i]);
                            this._dragPane.Y2AxisList[i]._scale._minAuto = false;
                            this._dragPane.Y2AxisList[i]._scale._maxAuto = false;
                        }
                    }

                    this.SetScroll(
                        this.hScrollBar1, 
                        this._dragPane.XAxis, 
                        this._xScrollRange.Min, 
                        this._xScrollRange.Max);
                    this.SetScroll(
                        this.vScrollBar1, 
                        this._dragPane.YAxis, 
                        this._yScrollRangeList[0].Min, 
                        this._yScrollRangeList[0].Max);

                    this.ApplyToAllPanes(this._dragPane);

                    // Provide Callback to notify the user of zoom events
                    if (this.ZoomEvent != null)
                    {
                        this.ZoomEvent(
                            this, 
                            this._zoomState, 
                            // oldState,
                            new ZoomState(this._dragPane, ZoomState.StateType.Zoom));
                    }

                    using (Graphics g = this.CreateGraphics())
                    {
                        // always AxisChange() the dragPane
                        this._dragPane.AxisChange(g);

                        foreach (GraphPane pane in this._masterPane._paneList)
                        {
                            if (pane != this._dragPane && (this._isSynchronizeXAxes || this._isSynchronizeYAxes))
                            {
                                pane.AxisChange(g);
                            }
                        }
                    }
                }

                this.Refresh();
            }
        }

        #endregion
    }
}