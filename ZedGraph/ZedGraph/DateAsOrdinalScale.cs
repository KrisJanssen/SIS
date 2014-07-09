// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="DateAsOrdinalScale.cs">
//   
// </copyright>
// <summary>
//   The DateAsOrdinalScale class inherits from the  class, and implements
//   the features specific to .
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// The DateAsOrdinalScale class inherits from the <see cref="Scale" /> class, and implements
    /// the features specific to <see cref="AxisType.DateAsOrdinal" />.
    /// </summary>
    /// <remarks>DateAsOrdinalScale is an ordinal axis that will have labels formatted with dates from the
    /// actual data values of the first <see cref="CurveItem" /> in the <see cref="CurveList" />.
    /// Although the tics are labeled with real data values, the actual points will be
    /// evenly-spaced in spite of the data values.  For example, if the X values of the first curve
    /// are 1, 5, and 100, then the tic labels will show 1, 5, and 100, but they will be equal
    /// distance from each other.
    /// </remarks>
    /// 
    /// <author> John Champion  </author>
    /// <version> $Revision: 1.13 $ $Date: 2007-11-28 02:38:22 $ </version>
    [Serializable]
    internal class DateAsOrdinalScale : Scale, ISerializable
    {
        // , ICloneable
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema2 = 10;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DateAsOrdinalScale"/> class. 
        /// Default constructor that defines the owner <see cref="Axis"/>
        /// (containing object) for this new object.
        /// </summary>
        /// <param name="owner">
        /// The owner, or containing object, of this instance
        /// </param>
        public DateAsOrdinalScale(Axis owner)
            : base(owner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateAsOrdinalScale"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="DateAsOrdinalScale"/> object from which to copy
        /// </param>
        /// <param name="owner">
        /// The <see cref="Axis"/> object that will own the
        /// new instance of <see cref="DateAsOrdinalScale"/>
        /// </param>
        public DateAsOrdinalScale(Scale rhs, Axis owner)
            : base(rhs, owner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateAsOrdinalScale"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected DateAsOrdinalScale(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema2");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the maximum value for this scale.
        /// </summary>
        /// <remarks>
        /// The set property is specifically adapted for <see cref="AxisType.DateAsOrdinal" /> scales,
        /// in that it automatically limits the value to the range of valid dates for the
        /// <see cref="XDate" /> struct.
        /// </remarks>
        public override double Max
        {
            get
            {
                return this._max;
            }

            set
            {
                this._max = XDate.MakeValidDate(value);
                this._maxAuto = false;
            }
        }

        /// <summary>
        /// Gets or sets the minimum value for this scale.
        /// </summary>
        /// <remarks>
        /// The set property is specifically adapted for <see cref="AxisType.DateAsOrdinal" /> scales,
        /// in that it automatically limits the value to the range of valid dates for the
        /// <see cref="XDate" /> struct.
        /// </remarks>
        public override double Min
        {
            get
            {
                return this._min;
            }

            set
            {
                this._min = XDate.MakeValidDate(value);
                this._minAuto = false;
            }
        }

        /// <summary>
        /// Return the <see cref="AxisType" /> for this <see cref="Scale" />, which is
        /// <see cref="AxisType.DateAsOrdinal" />.
        /// </summary>
        public override AxisType Type
        {
            get
            {
                return AxisType.DateAsOrdinal;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Create a new clone of the current item, with a new owner assignment
        /// </summary>
        /// <param name="owner">
        /// The new <see cref="Axis"/> instance that will be
        /// the owner of the new Scale
        /// </param>
        /// <returns>
        /// A new <see cref="Scale"/> clone.
        /// </returns>
        public override Scale Clone(Axis owner)
        {
            return new DateAsOrdinalScale(this, owner);
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
        }

        /// <summary>
        /// Select a reasonable ordinal axis scale given a range of data values, with the expectation that
        /// dates will be displayed.
        /// </summary>
        /// <remarks>
        /// This method only applies to <see cref="AxisType.DateAsOrdinal"/> type axes, and it
        /// is called by the general <see cref="PickScale"/> method.  For this type,
        /// the first curve is the "master", which contains the dates to be applied.
        /// <para>
        /// On Exit:
        /// </para>
        /// <para>
        /// <see cref="Scale.Min"/> is set to scale minimum (if <see cref="Scale.MinAuto"/> = true)
        /// </para>
        /// <para>
        /// <see cref="Scale.Max"/> is set to scale maximum (if <see cref="Scale.MaxAuto"/> = true)
        /// </para>
        /// <para>
        /// <see cref="Scale.MajorStep"/> is set to scale step size (if <see cref="Scale.MajorStepAuto"/> = true)
        /// </para>
        /// <para>
        /// <see cref="Scale.MinorStep"/> is set to scale minor step size (if <see cref="Scale.MinorStepAuto"/> = true)
        /// </para>
        /// <para>
        /// <see cref="Scale.Mag"/> is set to a magnitude multiplier according to the data
        /// </para>
        /// <para>
        /// <see cref="Scale.Format"/> is set to the display format for the values (this controls the
        /// number of decimal places, whether there are thousands separators, currency types, etc.)
        /// </para>
        /// </remarks>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object
        /// associated with this <see cref="Axis"/>
        /// </param>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <seealso cref="PickScale"/>
        /// <seealso cref="AxisType.Ordinal"/>
        public override void PickScale(GraphPane pane, Graphics g, float scaleFactor)
        {
            // call the base class first
            base.PickScale(pane, g, scaleFactor);

            /*			// First, get the date ranges from the first curve in the list
			double xMin; // = Double.MaxValue;
			double xMax; // = Double.MinValue;
			double yMin; // = Double.MaxValue;
			double yMax; // = Double.MinValue;
			double range = 1;

			foreach ( CurveItem curve in pane.CurveList )
			{
				if ( ( _ownerAxis is Y2Axis && curve.IsY2Axis ) ||
						( _ownerAxis is YAxis && !curve.IsY2Axis ) ||
						( _ownerAxis is X2Axis && curve.IsX2Axis ) ||
						( _ownerAxis is XAxis && !curve.IsX2Axis ) )
				{
					curve.GetRange( out xMin, out xMax, out yMin, out yMax, false, pane.IsBoundedRanges, pane );
					if ( _ownerAxis is XAxis || _ownerAxis is X2Axis )
						range = xMax - xMin;
					else
						range = yMax - yMin;
				}
			}
*/
            // Set the DateFormat by calling CalcDateStepSize
            // 			DateScale.CalcDateStepSize( range, Default.TargetXSteps, this );
            this.SetDateFormat(pane);

            // Now, set the axis range based on a ordinal scale
            base.PickScale(pane, g, scaleFactor);
            OrdinalScale.PickScale(pane, g, scaleFactor, this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Make a value label for an <see cref="AxisType.DateAsOrdinal"/> <see cref="Axis"/>.
        /// </summary>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="index">
        /// The zero-based, ordinal index of the label to be generated.  For example, a value of 2 would
        /// cause the third value label on the axis to be generated.
        /// </param>
        /// <param name="dVal">
        /// The numeric value associated with the label.  This value is ignored for log
        /// (<see cref="Scale.IsLog"/>)
        /// and text (<see cref="Scale.IsText"/>) type axes.
        /// </param>
        /// <returns>
        /// The resulting value label as a <see cref="string"/>
        /// </returns>
        internal override string MakeLabel(GraphPane pane, int index, double dVal)
        {
            if (this._format == null)
            {
                this._format = Scale.Default.Format;
            }

            double val;

            int tmpIndex = (int)dVal - 1;

            if (pane.CurveList.Count > 0 && pane.CurveList[0].Points.Count > tmpIndex)
            {
                if (this._ownerAxis is XAxis || this._ownerAxis is X2Axis)
                {
                    val = pane.CurveList[0].Points[tmpIndex].X;
                }
                else
                {
                    val = pane.CurveList[0].Points[tmpIndex].Y;
                }

                return XDate.ToString(val, this._format);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// The set date format.
        /// </summary>
        /// <param name="pane">
        /// The pane.
        /// </param>
        internal void SetDateFormat(GraphPane pane)
        {
            if (this._formatAuto)
            {
                double range = 10;

                if (pane.CurveList.Count > 0 && pane.CurveList[0].Points.Count > 1)
                {
                    double val1, val2;

                    PointPair pt1 = pane.CurveList[0].Points[0];
                    PointPair pt2 = pane.CurveList[0].Points[pane.CurveList[0].Points.Count - 1];
                    int p1 = 1;
                    int p2 = pane.CurveList[0].Points.Count;
                    if (pane.IsBoundedRanges)
                    {
                        p1 = (int)Math.Floor(this._ownerAxis.Scale.Min);
                        p2 = (int)Math.Ceiling(this._ownerAxis.Scale.Max);
                        p1 = Math.Min(Math.Max(p1, 1), pane.CurveList[0].Points.Count);
                        p2 = Math.Min(Math.Max(p2, 1), pane.CurveList[0].Points.Count);
                        if (p2 > p1)
                        {
                            pt1 = pane.CurveList[0].Points[p1 - 1];
                            pt2 = pane.CurveList[0].Points[p2 - 1];
                        }
                    }

                    if (this._ownerAxis is XAxis || this._ownerAxis is X2Axis)
                    {
                        val1 = pt1.X;
                        val2 = pt2.X;
                    }
                    else
                    {
                        val1 = pt1.Y;
                        val2 = pt2.Y;
                    }

                    if (val1 != PointPair.Missing && val2 != PointPair.Missing && !double.IsNaN(val1)
                        && !double.IsNaN(val2) && !double.IsInfinity(val1) && !double.IsInfinity(val2)
                        && Math.Abs(val2 - val1) > 1e-10)
                    {
                        range = Math.Abs(val2 - val1);
                    }
                }

                if (range > Default.RangeYearYear)
                {
                    this._format = Default.FormatYearYear;
                }
                else if (range > Default.RangeYearMonth)
                {
                    this._format = Default.FormatYearMonth;
                }
                else if (range > Default.RangeMonthMonth)
                {
                    this._format = Default.FormatMonthMonth;
                }
                else if (range > Default.RangeDayDay)
                {
                    this._format = Default.FormatDayDay;
                }
                else if (range > Default.RangeDayHour)
                {
                    this._format = Default.FormatDayHour;
                }
                else if (range > Default.RangeHourHour)
                {
                    this._format = Default.FormatHourHour;
                }
                else if (range > Default.RangeHourMinute)
                {
                    this._format = Default.FormatHourMinute;
                }
                else if (range > Default.RangeMinuteMinute)
                {
                    this._format = Default.FormatMinuteMinute;
                }
                else if (range > Default.RangeMinuteSecond)
                {
                    this._format = Default.FormatMinuteSecond;
                }
                else if (range > Default.RangeSecondSecond)
                {
                    this._format = Default.FormatSecondSecond;
                }
                else
                {
                    // MilliSecond
                    this._format = Default.FormatMillisecond;
                }
            }
        }

        #endregion
    }
}