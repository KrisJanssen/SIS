// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockPt.cs" company="">
//   
// </copyright>
// <summary>
//   The basic <see cref="PointPair" /> class holds three data values (X, Y, Z).  This
//   class extends the basic PointPair to contain five data values (X, Y, Z, Open, Close).
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// The basic <see cref="PointPair" /> class holds three data values (X, Y, Z).  This
    /// class extends the basic PointPair to contain five data values (X, Y, Z, Open, Close).
    /// </summary>
    /// <remarks>
    /// The values are remapped to <see cref="Date" />, <see cref="High" />,
    /// <see cref="Low" />, <see cref="Open" />, and <see cref="Close" />.
    /// </remarks>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 3.4 $ $Date: 2007-02-07 07:46:46 $ </version>
    [Serializable]
    public class StockPt : PointPair, ISerializable
    {
        // member variable mapping:
        // Date = X
        // High = Y
        // Low = Z
        // Open = Open
        // Close = Close
        // Vol = Vol
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema3 = 11;

        #endregion

        #region Fields

        /// <summary>
        /// This closing value
        /// </summary>
        public double Close;

        /// <summary>
        /// This opening value
        /// </summary>
        public double Open;

        /// <summary>
        /// This daily trading volume
        /// </summary>
        public double Vol;

        /// <summary>
        /// This is a user value that can be anything.  It is used to provide special 
        /// property-based coloration to the graph elements.
        /// </summary>
        private double _colorValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StockPt"/> class. 
        /// Default Constructor
        /// </summary>
        public StockPt()
            : this(0, 0, 0, 0, 0, 0, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockPt"/> class. 
        /// Construct a new StockPt from the specified data values
        /// </summary>
        /// <param name="date">
        /// The trading date (<see cref="XDate"/>)
        /// </param>
        /// <param name="high">
        /// The daily high stock price
        /// </param>
        /// <param name="low">
        /// The daily low stock price
        /// </param>
        /// <param name="open">
        /// The opening stock price
        /// </param>
        /// <param name="close">
        /// The closing stock price
        /// </param>
        /// <param name="vol">
        /// The daily trading volume
        /// </param>
        public StockPt(double date, double high, double low, double open, double close, double vol)
            : this(date, high, low, open, close, vol, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockPt"/> class. 
        /// Construct a new StockPt from the specified data values including a Tag property
        /// </summary>
        /// <param name="date">
        /// The trading date (<see cref="XDate"/>)
        /// </param>
        /// <param name="high">
        /// The daily high stock price
        /// </param>
        /// <param name="low">
        /// The daily low stock price
        /// </param>
        /// <param name="open">
        /// The opening stock price
        /// </param>
        /// <param name="close">
        /// The closing stock price
        /// </param>
        /// <param name="vol">
        /// The daily trading volume
        /// </param>
        /// <param name="tag">
        /// The user-defined <see cref="PointPair.Tag"/> property.
        /// </param>
        public StockPt(double date, double high, double low, double open, double close, double vol, string tag)
            : base(date, high)
        {
            this.Low = low;
            this.Open = open;
            this.Close = close;
            this.Vol = vol;
            this.ColorValue = PointPair.Missing;
            this.Tag = tag;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockPt"/> class. 
        /// The StockPt copy constructor.
        /// </summary>
        /// <param name="rhs">
        /// The basis for the copy.
        /// </param>
        public StockPt(StockPt rhs)
            : base(rhs)
        {
            this.Low = rhs.Low;
            this.Open = rhs.Open;
            this.Close = rhs.Close;
            this.Vol = rhs.Vol;
            this.ColorValue = rhs.ColorValue;

            if (rhs.Tag is ICloneable)
            {
                this.Tag = ((ICloneable)rhs.Tag).Clone();
            }
            else
            {
                this.Tag = rhs.Tag;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockPt"/> class. 
        /// The StockPt copy constructor.
        /// </summary>
        /// <param name="rhs">
        /// The basis for the copy.
        /// </param>
        public StockPt(PointPair rhs)
            : base(rhs)
        {
            if (rhs is StockPt)
            {
                StockPt pt = rhs as StockPt;
                this.Open = pt.Open;
                this.Close = pt.Close;
                this.Vol = pt.Vol;
                this.ColorValue = rhs.ColorValue;
            }
            else
            {
                this.Open = PointPair.Missing;
                this.Close = PointPair.Missing;
                this.Vol = PointPair.Missing;
                this.ColorValue = PointPair.Missing;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockPt"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected StockPt(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema3");

            this.Open = info.GetDouble("Open");
            this.Close = info.GetDouble("Close");
            this.Vol = info.GetDouble("Vol");
            this.ColorValue = info.GetDouble("ColorValue");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The ColorValue property.  This is used with the
        /// <see cref="FillType.GradientByColorValue" /> option.
        /// </summary>
        public override double ColorValue
        {
            get
            {
                return this._colorValue;
            }

            set
            {
                this._colorValue = value;
            }
        }

        /// <summary>
        /// Map the Date property to the X value
        /// </summary>
        public double Date
        {
            get
            {
                return this.X;
            }

            set
            {
                this.X = value;
            }
        }

        /// <summary>
        /// Map the high property to the Y value
        /// </summary>
        public double High
        {
            get
            {
                return this.Y;
            }

            set
            {
                this.Y = value;
            }
        }

        /// <summary>
        /// Readonly value that determines if either the Date, Close, Open, High, or Low
        /// coordinate in this StockPt is an invalid (not plotable) value.
        /// It is considered invalid if it is missing (equal to System.Double.Max),
        /// Infinity, or NaN.
        /// </summary>
        /// <returns>true if any value is invalid</returns>
        public bool IsInvalid5D
        {
            get
            {
                return this.Date == PointPair.Missing || this.Close == PointPair.Missing
                       || this.Open == PointPair.Missing || this.High == PointPair.Missing
                       || this.Low == PointPair.Missing || double.IsInfinity(this.Date) || double.IsInfinity(this.Close)
                       || double.IsInfinity(this.Open) || double.IsInfinity(this.High) || double.IsInfinity(this.Low)
                       || double.IsNaN(this.Date) || double.IsNaN(this.Close) || double.IsNaN(this.Open)
                       || double.IsNaN(this.High) || double.IsNaN(this.Low);
            }
        }

        /// <summary>
        /// Map the low property to the Z value
        /// </summary>
        public double Low
        {
            get
            {
                return this.Z;
            }

            set
            {
                this.Z = value;
            }
        }

        #endregion

        #region Public Methods and Operators

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
            info.AddValue("schema3", schema2);
            info.AddValue("Open", this.Open);
            info.AddValue("Close", this.Close);
            info.AddValue("Vol", this.Vol);
            info.AddValue("ColorValue", this.ColorValue);
        }

        /// <summary>
        /// Format this StockPt value using the default format.  Example:  "( 12.345, -16.876 )".
        /// The two double values are formatted with the "g" format type.
        /// </summary>
        /// <param name="isShowAll">
        /// true to show all the value coordinates
        /// </param>
        /// <returns>
        /// A string representation of the <see cref="StockPt"/>.
        /// </returns>
        public override string ToString(bool isShowAll)
        {
            return this.ToString(PointPair.DefaultFormat, isShowAll);
        }

        /// <summary>
        /// Format this PointPair value using a general format string.
        /// Example:  a format string of "e2" would give "( 1.23e+001, -1.69e+001 )".
        /// If <see paramref="isShowAll"/>
        /// is true, then the third all coordinates are shown.
        /// </summary>
        /// <param name="format">
        /// A format string that will be used to format each of
        /// the two double type values (see <see cref="System.Double.ToString()"/>).
        /// </param>
        /// <param name="isShowAll">
        /// true to show all the value coordinates
        /// </param>
        /// <returns>
        /// A string representation of the PointPair
        /// </returns>
        public override string ToString(string format, bool isShowAll)
        {
            return "( " + XDate.ToString(this.Date, "g") + ", " + this.Close.ToString(format)
                   + (isShowAll
                          ? (", " + this.Low.ToString(format) + ", " + this.Open.ToString(format) + ", "
                             + this.Close.ToString(format))
                          : string.Empty) + " )";
        }

        #endregion
    }
}