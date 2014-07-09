// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="JapaneseCandleStick.cs">
//   
// </copyright>
// <summary>
//   This class handles the drawing of the curve  objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// This class handles the drawing of the curve <see cref="JapaneseCandleStick"/> objects.
    /// </summary>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 3.10 $ $Date: 2007-04-16 00:03:02 $ </version>
    [Serializable]
    public class JapaneseCandleStick : OHLCBar, ICloneable, ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema2 = 11;

        #endregion

        #region Fields

        /// <summary>
        /// Private field that stores the CandleStick color when the <see cref="StockPt.Close" /> 
        /// value is less than the <see cref="StockPt.Open" /> value.  Use the public
        /// property <see cref="FallingColor"/> to access this value.
        /// </summary>
        protected Color _fallingColor;

        /// <summary>
        /// Private field to store the <see cref="Border" /> class to be used for drawing the
        /// candlestick "bars" when the <see cref="StockPt.Close" /> value is less than
        /// the <see cref="StockPt.Open" /> value.  See the public property
        /// <see cref="FallingBorder" /> to access this value.
        /// </summary>
        private Border _fallingBorder;

        /// <summary>
        /// Private field to store the <see cref="Fill" /> class to be used for filling the
        /// candlestick "bars" when the <see cref="StockPt.Close" /> value is less than
        /// the <see cref="StockPt.Open" /> value.  See the public property
        /// <see cref="FallingFill" /> to access this value.
        /// </summary>
        private Fill _fallingFill;

        /// <summary>
        /// Private field to store the <see cref="Border" /> class to be used for drawing the
        /// candlestick "bars" when the <see cref="StockPt.Close" /> value is greater than
        /// the <see cref="StockPt.Open" /> value.  See the public property
        /// <see cref="RisingBorder" /> to access this value.
        /// </summary>
        private Border _risingBorder;

        /// <summary>
        /// Private field to store the <see cref="Fill" /> class to be used for filling the
        /// candlestick "bars" when the <see cref="StockPt.Close" /> value is greater than
        /// the <see cref="StockPt.Open" /> value.  See the public property
        /// <see cref="RisingFill" /> to access this value.
        /// </summary>
        private Fill _risingFill;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JapaneseCandleStick"/> class. 
        /// Default constructor that sets all <see cref="JapaneseCandleStick"/> properties to
        /// default values as defined in the <see cref="Default"/> class.
        /// </summary>
        public JapaneseCandleStick()
            : base()
        {
            this._risingFill = new Fill(Default.RisingColor);
            this._fallingFill = new Fill(Default.FallingColor);

            this._risingBorder = new Border(Default.RisingBorder, LineBase.Default.Width);
            this._fallingBorder = new Border(Default.FallingBorder, LineBase.Default.Width);

            this._fallingColor = Default.FallingColor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JapaneseCandleStick"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="JapaneseCandleStick"/> object from which to copy
        /// </param>
        public JapaneseCandleStick(JapaneseCandleStick rhs)
            : base(rhs)
        {
            this._risingFill = rhs._risingFill.Clone();
            this._fallingFill = rhs._fallingFill.Clone();

            this._risingBorder = rhs._risingBorder.Clone();
            this._fallingBorder = rhs._fallingBorder.Clone();

            this._fallingColor = rhs._fallingColor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JapaneseCandleStick"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected JapaneseCandleStick(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema2");

            this._risingFill = (Fill)info.GetValue("risingFill", typeof(Fill));
            this._fallingFill = (Fill)info.GetValue("fallingFill", typeof(Fill));
            this._risingBorder = (Border)info.GetValue("risingBorder", typeof(Border));
            this._fallingBorder = (Border)info.GetValue("fallingBorder", typeof(Border));

            if (schema2 >= 11)
            {
                this._fallingColor = (Color)info.GetValue("fallingColor", typeof(Color));
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The <see cref="Border" /> instance to be used for drawing the border frame of
        /// the candlestick "bars" when the <see cref="StockPt.Close" /> value is less than the
        /// <see cref="StockPt.Open" /> value.
        /// </summary>
        public Border FallingBorder
        {
            get
            {
                return this._fallingBorder;
            }

            set
            {
                this._fallingBorder = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Drawing.Color"/> data for this
        /// <see cref="JapaneseCandleStick"/> when the value of the candlestick is
        /// falling.
        /// </summary>
        /// <remarks>This property only controls the color of
        /// the vertical line when the value is falling.  The rising color is controlled
        /// by the <see cref="LineBase.Color" /> property.
        /// </remarks>
        public Color FallingColor
        {
            get
            {
                return this._fallingColor;
            }

            set
            {
                this._fallingColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Fill" /> class that is used to fill the candlestick
        /// "bars" when the <see cref="StockPt.Close" /> value is less than the
        /// <see cref="StockPt.Open" /> value.
        /// </summary>
        public Fill FallingFill
        {
            get
            {
                return this._fallingFill;
            }

            set
            {
                this._fallingFill = value;
            }
        }

        /// <summary>
        /// The <see cref="Border" /> instance to be used for drawing the border frame of
        /// the candlestick "bars" when the <see cref="StockPt.Close" /> value is greater than the
        /// <see cref="StockPt.Open" /> value.
        /// </summary>
        public Border RisingBorder
        {
            get
            {
                return this._risingBorder;
            }

            set
            {
                this._risingBorder = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Fill" /> class that is used to fill the candlestick
        /// "bars" when the <see cref="StockPt.Close" /> value is greater than the
        /// <see cref="StockPt.Open" /> value.
        /// </summary>
        public Fill RisingFill
        {
            get
            {
                return this._risingFill;
            }

            set
            {
                this._risingFill = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public new JapaneseCandleStick Clone()
        {
            return new JapaneseCandleStick(this);
        }

        /// <summary>
        /// Draw the <see cref="JapaneseCandleStick"/> to the specified <see cref="Graphics"/>
        /// device at the specified location.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="isXBase">
        /// boolean value that indicates if the "base" axis for this
        /// <see cref="JapaneseCandleStick"/> is the X axis.  True for an <see cref="XAxis"/> base,
        /// false for a <see cref="YAxis"/> or <see cref="Y2Axis"/> base.
        /// </param>
        /// <param name="pixBase">
        /// The independent axis position of the center of the candlestick in
        /// pixel units
        /// </param>
        /// <param name="pixHigh">
        /// The high value position of the candlestick in
        /// pixel units
        /// </param>
        /// <param name="pixLow">
        /// The low value position of the candlestick in
        /// pixel units
        /// </param>
        /// <param name="pixOpen">
        /// The opening value position of the candlestick in
        /// pixel units
        /// </param>
        /// <param name="pixClose">
        /// The closing value position of the candlestick in
        /// pixel units
        /// </param>
        /// <param name="halfSize">
        /// The scaled width of one-half of a bar, in pixels
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor for the features of the graph based on the <see cref="PaneBase.BaseDimension"/>.  This
        /// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
        /// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
        /// </param>
        /// <param name="pen">
        /// A pen with the <see cref="Color"/> attribute for this
        /// <see cref="JapaneseCandleStick"/>
        /// </param>
        /// <param name="fill">
        /// The <see cref="Fill"/> instance to be used for filling this
        /// <see cref="JapaneseCandleStick"/>
        /// </param>
        /// <param name="border">
        /// The <see cref="Border"/> instance to be used for drawing the
        /// border around the <see cref="JapaneseCandleStick"/> filled box
        /// </param>
        /// <param name="pt">
        /// The <see cref="PointPair"/> to be used for determining the
        /// <see cref="Fill"/>, just in case it's a <see cref="FillType.GradientByX"/>,
        /// <see cref="FillType.GradientByY"/>, or
        /// <see cref="FillType.GradientByZ"/> <see cref="FillType"/>
        /// </param>
        public void Draw(
            Graphics g, 
            GraphPane pane, 
            bool isXBase, 
            float pixBase, 
            float pixHigh, 
            float pixLow, 
            float pixOpen, 
            float pixClose, 
            float halfSize, 
            float scaleFactor, 
            Pen pen, 
            Fill fill, 
            Border border, 
            PointPair pt)
        {
            // float halfSize = (int) ( _size * scaleFactor / 2.0f + 0.5f );
            if (pixBase != PointPair.Missing && Math.Abs(pixLow) < 1000000 && Math.Abs(pixHigh) < 1000000)
            {
                RectangleF rect;
                if (isXBase)
                {
                    rect = new RectangleF(
                        pixBase - halfSize, 
                        Math.Min(pixOpen, pixClose), 
                        halfSize * 2.0f, 
                        Math.Abs(pixOpen - pixClose));

                    g.DrawLine(pen, pixBase, pixHigh, pixBase, pixLow);
                }
                else
                {
                    rect = new RectangleF(
                        Math.Min(pixOpen, pixClose), 
                        pixBase - halfSize, 
                        Math.Abs(pixOpen - pixClose), 
                        halfSize * 2.0f);

                    g.DrawLine(pen, pixHigh, pixBase, pixLow, pixBase);
                }

                if (this._isOpenCloseVisible && Math.Abs(pixOpen) < 1000000 && Math.Abs(pixClose) < 1000000)
                {
                    if (rect.Width == 0)
                    {
                        rect.Width = 1;
                    }

                    if (rect.Height == 0)
                    {
                        rect.Height = 1;
                    }

                    fill.Draw(g, rect, pt);
                    border.Draw(g, pane, scaleFactor, rect);
                }
            }
        }

        /// <summary>
        /// Draw all the <see cref="JapaneseCandleStick"/>'s to the specified <see cref="Graphics"/>
        /// device as a candlestick at each defined point.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="curve">
        /// A <see cref="JapaneseCandleStickItem"/> object representing the
        /// <see cref="JapaneseCandleStick"/>'s to be drawn.
        /// </param>
        /// <param name="baseAxis">
        /// The <see cref="Axis"/> class instance that defines the base (independent)
        /// axis for the <see cref="JapaneseCandleStick"/>
        /// </param>
        /// <param name="valueAxis">
        /// The <see cref="Axis"/> class instance that defines the value (dependent)
        /// axis for the <see cref="JapaneseCandleStick"/>
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        public void Draw(
            Graphics g, 
            GraphPane pane, 
            JapaneseCandleStickItem curve, 
            Axis baseAxis, 
            Axis valueAxis, 
            float scaleFactor)
        {
            // ValueHandler valueHandler = new ValueHandler( pane, false );
            float pixBase, pixHigh, pixLow, pixOpen, pixClose;

            if (curve.Points != null)
            {
                // float halfSize = _size * scaleFactor;
                float halfSize = this.GetBarWidth(pane, baseAxis, scaleFactor);

                Color tColor = this._color;
                Color tFallingColor = this._fallingColor;
                float tPenWidth = this._width;
                Fill tRisingFill = this._risingFill;
                Fill tFallingFill = this._fallingFill;
                Border tRisingBorder = this._risingBorder;
                Border tFallingBorder = this._fallingBorder;
                if (curve.IsSelected)
                {
                    tColor = Selection.Border.Color;
                    tFallingColor = Selection.Border.Color;
                    tPenWidth = Selection.Border.Width;
                    tRisingFill = Selection.Fill;
                    tFallingFill = Selection.Fill;
                    tRisingBorder = Selection.Border;
                    tFallingBorder = Selection.Border;
                }

                using (Pen risingPen = new Pen(tColor, tPenWidth))
                using (Pen fallingPen = new Pen(tFallingColor, tPenWidth))
                {
                    // Loop over each defined point							
                    for (int i = 0; i < curve.Points.Count; i++)
                    {
                        PointPair pt = curve.Points[i];
                        double date = pt.X;
                        double high = pt.Y;
                        double low = pt.Z;
                        double open = PointPair.Missing;
                        double close = PointPair.Missing;
                        if (pt is StockPt)
                        {
                            open = (pt as StockPt).Open;
                            close = (pt as StockPt).Close;
                        }

                        // Any value set to double max is invalid and should be skipped
                        // This is used for calculated values that are out of range, divide
                        // by zero, etc.
                        // Also, any value <= zero on a log scale is invalid
                        if (!curve.Points[i].IsInvalid3D && (date > 0 || !baseAxis._scale.IsLog)
                            && ((high > 0 && low > 0) || !valueAxis._scale.IsLog))
                        {
                            pixBase = (int)(baseAxis.Scale.Transform(curve.IsOverrideOrdinal, i, date) + 0.5);

                            // pixBase = baseAxis.Scale.Transform( curve.IsOverrideOrdinal, i, date );
                            pixHigh = valueAxis.Scale.Transform(curve.IsOverrideOrdinal, i, high);
                            pixLow = valueAxis.Scale.Transform(curve.IsOverrideOrdinal, i, low);
                            if (PointPair.IsValueInvalid(open))
                            {
                                pixOpen = float.MaxValue;
                            }
                            else
                            {
                                pixOpen = valueAxis.Scale.Transform(curve.IsOverrideOrdinal, i, open);
                            }

                            if (PointPair.IsValueInvalid(close))
                            {
                                pixClose = float.MaxValue;
                            }
                            else
                            {
                                pixClose = valueAxis.Scale.Transform(curve.IsOverrideOrdinal, i, close);
                            }

                            if (!curve.IsSelected && this._gradientFill.IsGradientValueType)
                            {
                                using (Pen tPen = this.GetPen(pane, scaleFactor, pt))
                                    this.Draw(
                                        g, 
                                        pane, 
                                        baseAxis is XAxis || baseAxis is X2Axis, 
                                        pixBase, 
                                        pixHigh, 
                                        pixLow, 
                                        pixOpen, 
                                        pixClose, 
                                        halfSize, 
                                        scaleFactor, 
                                        tPen, 
                                        close > open ? tRisingFill : tFallingFill, 
                                        close > open ? tRisingBorder : tFallingBorder, 
                                        pt);
                            }
                            else
                            {
                                this.Draw(
                                    g, 
                                    pane, 
                                    baseAxis is XAxis || baseAxis is X2Axis, 
                                    pixBase, 
                                    pixHigh, 
                                    pixLow, 
                                    pixOpen, 
                                    pixClose, 
                                    halfSize, 
                                    scaleFactor, 
                                    close > open ? risingPen : fallingPen, 
                                    close > open ? tRisingFill : tFallingFill, 
                                    close > open ? tRisingBorder : tFallingBorder, 
                                    pt);
                            }
                        }
                    }
                }
            }
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
            info.AddValue("risingFill", this._risingFill);
            info.AddValue("fallingFill", this._fallingFill);
            info.AddValue("risingBorder", this._risingBorder);
            info.AddValue("fallingBorder", this._fallingBorder);
            info.AddValue("fallingColor", this._fallingColor);
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

        /// <summary>
        /// A simple struct that defines the
        /// default property values for the <see cref="ZedGraph.JapaneseCandleStick"/> class.
        /// </summary>
        public new struct Default
        {
            #region Static Fields

            /// <summary>
            /// The default color for the border of the falling CandleSticks
            /// (<see cref="JapaneseCandleStick.FallingBorder" /> property).
            /// </summary>
            public static Color FallingBorder = Color.Black;

            /// <summary>
            /// The default fillcolor for drawing the falling case CandleSticks
            /// (<see cref="JapaneseCandleStick.FallingFill"/> property).
            /// </summary>
            public static Color FallingColor = Color.Black;

            /// <summary>
            /// The default color for the border of the rising CandleSticks
            /// (<see cref="JapaneseCandleStick.RisingBorder" /> property).
            /// </summary>
            public static Color RisingBorder = Color.Black;

            /// <summary>
            /// The default fillcolor for drawing the rising case CandleSticks
            /// (<see cref="JapaneseCandleStick.RisingFill"/> property).
            /// </summary>
            public static Color RisingColor = Color.White;

            #endregion
        }
    }
}