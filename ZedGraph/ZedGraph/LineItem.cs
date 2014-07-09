// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="LineItem.cs">
//   
// </copyright>
// <summary>
//   Encapsulates a curve type that is displayed as a line and/or a set of
//   symbols at each point.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Encapsulates a curve type that is displayed as a line and/or a set of
    /// symbols at each point.
    /// </summary>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 3.22 $ $Date: 2007-08-10 16:22:54 $ </version>
    [Serializable]
    public class LineItem : CurveItem, ICloneable, ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema2 = 10;

        #endregion

        #region Fields

        /// <summary>
        /// Private field that stores a reference to the <see cref="ZedGraph.Line"/>
        /// class defined for this <see cref="LineItem"/>.  Use the public
        /// property <see cref="Line"/> to access this value.
        /// </summary>
        protected Line _line;

        /// <summary>
        /// Private field that stores a reference to the <see cref="ZedGraph.Symbol"/>
        /// class defined for this <see cref="LineItem"/>.  Use the public
        /// property <see cref="Symbol"/> to access this value.
        /// </summary>
        protected Symbol _symbol;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LineItem"/> class. 
        /// Create a new <see cref="LineItem"/>, specifying only the legend <see cref="CurveItem.Label"/>.
        /// </summary>
        /// <param name="label">
        /// The _label that will appear in the legend.
        /// </param>
        public LineItem(string label)
            : base(label)
        {
            this._symbol = new Symbol();
            this._line = new Line();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineItem"/> class. 
        /// Create a new <see cref="LineItem"/> using the specified properties.
        /// </summary>
        /// <param name="label">
        /// The _label that will appear in the legend.
        /// </param>
        /// <param name="x">
        /// An array of double precision values that define
        /// the independent (X axis) values for this curve
        /// </param>
        /// <param name="y">
        /// An array of double precision values that define
        /// the dependent (Y axis) values for this curve
        /// </param>
        /// <param name="color">
        /// A <see cref="Color"/> value that will be applied to
        /// the <see cref="Line"/> and <see cref="Symbol"/> properties.
        /// </param>
        /// <param name="symbolType">
        /// A <see cref="SymbolType"/> enum specifying the
        /// type of symbol to use for this <see cref="LineItem"/>.  Use <see cref="SymbolType.None"/>
        /// to hide the symbols.
        /// </param>
        /// <param name="lineWidth">
        /// The width (in points) to be used for the <see cref="Line"/>.  This
        /// width is scaled based on <see cref="PaneBase.CalcScaleFactor"/>.  Use a value of zero to
        /// hide the line (see <see cref="ZedGraph.LineBase.IsVisible"/>).
        /// </param>
        public LineItem(string label, double[] x, double[] y, Color color, SymbolType symbolType, float lineWidth)
            : this(label, new PointPairList(x, y), color, symbolType, lineWidth)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineItem"/> class. 
        /// Create a new <see cref="LineItem"/> using the specified properties.
        /// </summary>
        /// <param name="label">
        /// The _label that will appear in the legend.
        /// </param>
        /// <param name="x">
        /// An array of double precision values that define
        /// the independent (X axis) values for this curve
        /// </param>
        /// <param name="y">
        /// An array of double precision values that define
        /// the dependent (Y axis) values for this curve
        /// </param>
        /// <param name="color">
        /// A <see cref="Color"/> value that will be applied to
        /// the <see cref="Line"/> and <see cref="Symbol"/> properties.
        /// </param>
        /// <param name="symbolType">
        /// A <see cref="SymbolType"/> enum specifying the
        /// type of symbol to use for this <see cref="LineItem"/>.  Use <see cref="SymbolType.None"/>
        /// to hide the symbols.
        /// </param>
        public LineItem(string label, double[] x, double[] y, Color color, SymbolType symbolType)
            : this(label, new PointPairList(x, y), color, symbolType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineItem"/> class. 
        /// Create a new <see cref="LineItem"/> using the specified properties.
        /// </summary>
        /// <param name="label">
        /// The _label that will appear in the legend.
        /// </param>
        /// <param name="points">
        /// A <see cref="IPointList"/> of double precision value pairs that define
        /// the X and Y values for this curve
        /// </param>
        /// <param name="color">
        /// A <see cref="Color"/> value that will be applied to
        /// the <see cref="Line"/> and <see cref="Symbol"/> properties.
        /// </param>
        /// <param name="symbolType">
        /// A <see cref="SymbolType"/> enum specifying the
        /// type of symbol to use for this <see cref="LineItem"/>.  Use <see cref="SymbolType.None"/>
        /// to hide the symbols.
        /// </param>
        /// <param name="lineWidth">
        /// The width (in points) to be used for the <see cref="Line"/>.  This
        /// width is scaled based on <see cref="PaneBase.CalcScaleFactor"/>.  Use a value of zero to
        /// hide the line (see <see cref="ZedGraph.LineBase.IsVisible"/>).
        /// </param>
        public LineItem(string label, IPointList points, Color color, SymbolType symbolType, float lineWidth)
            : base(label, points)
        {
            this._line = new Line(color);
            if (lineWidth == 0)
            {
                this._line.IsVisible = false;
            }
            else
            {
                this._line.Width = lineWidth;
            }

            this._symbol = new Symbol(symbolType, color);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineItem"/> class. 
        /// Create a new <see cref="LineItem"/> using the specified properties.
        /// </summary>
        /// <param name="label">
        /// The _label that will appear in the legend.
        /// </param>
        /// <param name="points">
        /// A <see cref="IPointList"/> of double precision value pairs that define
        /// the X and Y values for this curve
        /// </param>
        /// <param name="color">
        /// A <see cref="Color"/> value that will be applied to
        /// the <see cref="Line"/> and <see cref="Symbol"/> properties.
        /// </param>
        /// <param name="symbolType">
        /// A <see cref="SymbolType"/> enum specifying the
        /// type of symbol to use for this <see cref="LineItem"/>.  Use <see cref="SymbolType.None"/>
        /// to hide the symbols.
        /// </param>
        public LineItem(string label, IPointList points, Color color, SymbolType symbolType)
            : this(label, points, color, symbolType, global::ZedGraph.ZedGraph.LineBase.Default.Width)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineItem"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="LineItem"/> object from which to copy
        /// </param>
        public LineItem(LineItem rhs)
            : base(rhs)
        {
            this._symbol = new Symbol(rhs.Symbol);
            this._line = new Line(rhs.Line);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineItem"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected LineItem(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema2");

            this._symbol = (Symbol)info.GetValue("symbol", typeof(Symbol));
            this._line = (Line)info.GetValue("line", typeof(Line));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="ZedGraph.Line"/> class instance defined
        /// for this <see cref="LineItem"/>.
        /// </summary>
        public Line Line
        {
            get
            {
                return this._line;
            }

            set
            {
                this._line = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ZedGraph.Symbol"/> class instance defined
        /// for this <see cref="LineItem"/>.
        /// </summary>
        public Symbol Symbol
        {
            get
            {
                return this._symbol;
            }

            set
            {
                this._symbol = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public LineItem Clone()
        {
            return new LineItem(this);
        }

        /// <summary>
        /// Do all rendering associated with this <see cref="LineItem"/> to the specified
        /// <see cref="Graphics"/> device.  This method is normally only
        /// called by the Draw method of the parent <see cref="ZedGraph.CurveList"/>
        /// collection object.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="pos">
        /// The ordinal position of the current <see cref="Bar"/>
        /// curve.
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="ZedGraph.GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        public override void Draw(Graphics g, GraphPane pane, int pos, float scaleFactor)
        {
            if (this._isVisible)
            {
                this.Line.Draw(g, pane, this, scaleFactor);

                this.Symbol.Draw(g, pane, this, scaleFactor, this.IsSelected);
            }
        }

        /// <summary>
        /// Draw a legend key entry for this <see cref="LineItem"/> at the specified location
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="rect">
        /// The <see cref="RectangleF"/> struct that specifies the
        /// location for the legend key
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="ZedGraph.GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        public override void DrawLegendKey(Graphics g, GraphPane pane, RectangleF rect, float scaleFactor)
        {
            // Draw a sample curve to the left of the label text
            int xMid = (int)(rect.Left + rect.Width / 2.0F);
            int yMid = (int)(rect.Top + rect.Height / 2.0F);

            // RectangleF rect2 = rect;
            // rect2.Y = yMid;
            // rect2.Height = rect.Height / 2.0f;
            this._line.Fill.Draw(g, rect);

            this._line.DrawSegment(g, pane, rect.Left, yMid, rect.Right, yMid, scaleFactor);

            // Draw a sample symbol to the left of the label text				
            this._symbol.DrawSymbol(g, pane, xMid, yMid, scaleFactor, false, null);
        }

        /// <summary>
        /// Determine the coords for the rectangle associated with a specified point for 
        /// this <see cref="CurveItem"/>
        /// </summary>
        /// <param name="pane">
        /// The <see cref="GraphPane"/> to which this curve belongs
        /// </param>
        /// <param name="i">
        /// The index of the point of interest
        /// </param>
        /// <param name="coords">
        /// A list of coordinates that represents the "rect" for
        /// this point (used in an html AREA tag)
        /// </param>
        /// <returns>
        /// true if it's a valid point, false otherwise
        /// </returns>
        public override bool GetCoords(GraphPane pane, int i, out string coords)
        {
            coords = string.Empty;

            if (i < 0 || i >= this._points.Count)
            {
                return false;
            }

            PointPair pt = this._points[i];
            if (pt.IsInvalid)
            {
                return false;
            }

            double x, y, z;
            ValueHandler valueHandler = new ValueHandler(pane, false);
            valueHandler.GetValues(this, i, out x, out z, out y);

            Axis yAxis = this.GetYAxis(pane);
            Axis xAxis = this.GetXAxis(pane);

            PointF pixPt = new PointF(
                xAxis.Scale.Transform(this._isOverrideOrdinal, i, x), 
                yAxis.Scale.Transform(this._isOverrideOrdinal, i, y));

            if (!pane.Chart.Rect.Contains(pixPt))
            {
                return false;
            }

            float halfSize = this._symbol.Size * pane.CalcScaleFactor();

            coords = string.Format(
                "{0:f0},{1:f0},{2:f0},{3:f0}", 
                pixPt.X - halfSize, 
                pixPt.Y - halfSize, 
                pixPt.X + halfSize, 
                pixPt.Y + halfSize);

            return true;
        }

        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("schema2", schema2);
            info.AddValue("symbol", this._symbol);
            info.AddValue("line", this._line);
        }

        /// <summary>
        /// Loads some pseudo unique colors/symbols into this LineItem.  This
        /// is mainly useful for differentiating a set of new LineItems without
        /// having to pick your own colors/symbols.
        /// <seealso cref="CurveItem.MakeUnique( ColorSymbolRotator )"/>
        /// </summary>
        /// <param name="rotator">
        /// The <see cref="ColorSymbolRotator"/> that is used to pick the color
        ///  and symbol for this method call.
        /// </param>
        public override void MakeUnique(ColorSymbolRotator rotator)
        {
            this.Color = rotator.NextColor;
            this.Symbol.Type = rotator.NextSymbol;
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
        /// calling the typed version of <see cref="Clone" />
        /// </summary>
        /// <returns>A deep copy of this object</returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a flag indicating if the X axis is the independent axis for this <see cref="CurveItem"/>
        /// </summary>
        /// <param name="pane">
        /// The parent <see cref="GraphPane"/> of this <see cref="CurveItem"/>.
        /// </param>
        /// <value>
        /// true if the X axis is independent, false otherwise
        /// </value>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        internal override bool IsXIndependent(GraphPane pane)
        {
            return true;
        }

        /// <summary>
        /// Gets a flag indicating if the Z data range should be included in the axis scaling calculations.
        /// </summary>
        /// <param name="pane">
        /// The parent <see cref="GraphPane"/> of this <see cref="CurveItem"/>.
        /// </param>
        /// <value>
        /// true if the Z data are included, false otherwise
        /// </value>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        internal override bool IsZIncluded(GraphPane pane)
        {
            return false;
        }

        #endregion
    }
}