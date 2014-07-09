// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="OHLCBarItem.cs">
//   
// </copyright>
// <summary>
//   Encapsulates a CandleStick curve type that displays a vertical (or horizontal)
//   line displaying the range of data values at each sample point, plus an starting
//   mark and an ending mark signifying the opening and closing value for the sample.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Encapsulates a CandleStick curve type that displays a vertical (or horizontal)
    /// line displaying the range of data values at each sample point, plus an starting
    /// mark and an ending mark signifying the opening and closing value for the sample.
    /// </summary>
    /// <remarks>For this type to work properly, your <see cref="IPointList" /> must contain
    /// <see cref="StockPt" /> objects, rather than ordinary <see cref="PointPair" /> types.
    /// This is because the <see cref="OHLCBarItem"/> type actually displays 5 data values
    /// but the <see cref="PointPair" /> only stores 3 data values.  The <see cref="StockPt" />
    /// stores <see cref="StockPt.Date" />, <see cref="StockPt.Close" />,
    /// <see cref="StockPt.Open" />, <see cref="StockPt.High" />, and
    /// <see cref="StockPt.Low" /> members.
    /// For a vertical CandleStick chart, the opening value is drawn as a horizontal line
    /// segment to the left of the vertical range bar, and the closing value is a horizontal
    /// line segment to the right.  The total length of these two line segments is controlled
    /// by the <see cref="ZedGraph.OHLCBar.Size" /> property, which is specified in
    /// points (1/72nd inch), and scaled according to <see cref="PaneBase.CalcScaleFactor" />.
    /// The candlesticks are drawn horizontally or vertically depending on the
    /// value of <see cref="BarSettings.Base"/>, which is a
    /// <see cref="ZedGraph.BarBase"/> enum type.</remarks>
    /// <author> John Champion </author>
    /// <version> $Revision: 3.4 $ $Date: 2007-12-31 00:23:05 $ </version>
    [Serializable]
    public class OHLCBarItem : CurveItem, ICloneable, ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema2 = 10;

        #endregion

        #region Fields

        /// <summary>
        /// Private field that stores a reference to the <see cref="ZedGraph.OHLCBar"/>
        /// class defined for this <see cref="OHLCBarItem"/>.  Use the public
        /// property <see cref="OHLCBar"/> to access this value.
        /// </summary>
        /// 
        protected OHLCBar _bar;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OHLCBarItem"/> class. 
        /// Create a new <see cref="OHLCBarItem"/>, specifying only the legend label.
        /// </summary>
        /// <param name="label">
        /// The label that will appear in the legend.
        /// </param>
        public OHLCBarItem(string label)
            : base(label)
        {
            this._bar = new OHLCBar();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OHLCBarItem"/> class. 
        /// Create a new <see cref="OHLCBarItem"/> using the specified properties.
        /// </summary>
        /// <param name="label">
        /// The _label that will appear in the legend.
        /// </param>
        /// <param name="points">
        /// An <see cref="IPointList"/> of double precision values that define
        /// the Date, Close, Open, High, and Low values for the curve.  Note that this
        /// <see cref="IPointList"/> should contain <see cref="StockPt"/> items rather
        /// than <see cref="PointPair"/> items.
        /// </param>
        /// <param name="color">
        /// The <see cref="System.Drawing.Color"/> to use for drawing the candlesticks.
        /// </param>
        public OHLCBarItem(string label, IPointList points, Color color)
            : base(label, points)
        {
            this._bar = new OHLCBar(color);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OHLCBarItem"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="OHLCBarItem"/> object from which to copy
        /// </param>
        public OHLCBarItem(OHLCBarItem rhs)
            : base(rhs)
        {
            this._bar = rhs._bar.Clone();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OHLCBarItem"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected OHLCBarItem(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema2");

            this._bar = (OHLCBar)info.GetValue("stick", typeof(OHLCBar));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a reference to the <see cref="OHLCBar"/> class defined
        /// for this <see cref="OHLCBarItem"/>.
        /// </summary>
        public OHLCBar Bar
        {
            get
            {
                return this._bar;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public OHLCBarItem Clone()
        {
            return new OHLCBarItem(this);
        }

        /// <summary>
        /// Do all rendering associated with this <see cref="OHLCBarItem"/> to the specified
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
        /// The ordinal position of the current <see cref="OHLCBarItem"/>
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
                this._bar.Draw(g, pane, this, this.BaseAxis(pane), this.ValueAxis(pane), scaleFactor);
            }
        }

        /// <summary>
        /// Draw a legend key entry for this <see cref="OHLCBarItem"/> at the specified location
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
            float pixBase, pixHigh, pixLow, pixOpen, pixClose;

            if (pane._barSettings.Base == BarBase.X)
            {
                pixBase = rect.Left + rect.Width / 2.0F;
                pixHigh = rect.Top;
                pixLow = rect.Bottom;
                pixOpen = pixHigh + rect.Height / 4;
                pixClose = pixLow - rect.Height / 4;
            }
            else
            {
                pixBase = rect.Top + rect.Height / 2.0F;
                pixHigh = rect.Right;
                pixLow = rect.Left;
                pixOpen = pixHigh - rect.Width / 4;
                pixClose = pixLow + rect.Width / 4;
            }

            float halfSize = 2.0f * scaleFactor;

            using (Pen pen = new Pen(this._bar.Color, this._bar._width))
            {
                this._bar.Draw(
                    g, 
                    pane, 
                    pane._barSettings.Base == BarBase.X, 
                    pixBase, 
                    pixHigh, 
                    pixLow, 
                    pixOpen, 
                    pixClose, 
                    halfSize, 
                    pen);
            }
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

            Axis valueAxis = this.ValueAxis(pane);
            Axis baseAxis = this.BaseAxis(pane);

            float halfSize = this._bar.Size * pane.CalcScaleFactor();

            PointPair pt = this._points[i];
            double date = pt.X;
            double high = pt.Y;
            double low = pt.Z;

            if (!pt.IsInvalid3D && (date > 0 || !baseAxis._scale.IsLog)
                && ((high > 0 && low > 0) || !valueAxis._scale.IsLog))
            {
                float pixBase, pixHigh, pixLow;
                pixBase = baseAxis.Scale.Transform(this._isOverrideOrdinal, i, date);
                pixHigh = valueAxis.Scale.Transform(this._isOverrideOrdinal, i, high);
                pixLow = valueAxis.Scale.Transform(this._isOverrideOrdinal, i, low);

                // Calculate the pixel location for the side of the bar (on the base axis)
                float pixSide = pixBase - halfSize;

                // Draw the bar
                if (baseAxis is XAxis || baseAxis is X2Axis)
                {
                    coords = string.Format(
                        "{0:f0},{1:f0},{2:f0},{3:f0}", 
                        pixSide, 
                        pixLow, 
                        pixSide + halfSize * 2, 
                        pixHigh);
                }
                else
                {
                    coords = string.Format(
                        "{0:f0},{1:f0},{2:f0},{3:f0}", 
                        pixLow, 
                        pixSide, 
                        pixHigh, 
                        pixSide + halfSize * 2);
                }

                return true;
            }

            return false;
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
            info.AddValue("stick", this._bar);
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
            return pane._barSettings.Base == BarBase.X;
        }

        /// <summary>
        /// Gets a flag indicating if the Z data range should be included in the axis scaling calculations.
        /// </summary>
        /// <remarks>
        /// IsZIncluded is true for <see cref="OHLCBarItem"/> objects, since the Y and Z
        /// values are defined as the High and Low values for the day.
        /// </remarks>
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
            return true;
        }

        #endregion
    }
}