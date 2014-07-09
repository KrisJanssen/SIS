// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointPairBase.cs" company="">
//   
// </copyright>
// <summary>
//   This is a base class that provides base-level functionality for a data point consisting
//   of an (X,Y) pair of double values.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// This is a base class that provides base-level functionality for a data point consisting
    /// of an (X,Y) pair of double values.
    /// </summary>
    /// <remarks>
    /// This class is typically a base class for actual <see cref="PointPair" /> type implementations.
    /// </remarks>
    /// 
    /// <author> Jerry Vos modified by John Champion </author>
    /// <version> $Revision: 1.4 $ $Date: 2007-04-16 00:03:02 $ </version>
    [Serializable]
    public class PointPairBase : ISerializable
    {
        #region Constants

        /// <summary>
        /// The default format to be used for displaying point values via the
        /// <see cref="ToString()"/> method.
        /// </summary>
        public const string DefaultFormat = "G";

        /// <summary>
        /// Missing values are represented internally using <see cref="System.Double.MaxValue"/>.
        /// </summary>
        public const double Missing = double.MaxValue;

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema = 11;

        #endregion

        #region Fields

        /// <summary>
        /// This PointPair's X coordinate
        /// </summary>
        public double X;

        /// <summary>
        /// This PointPair's Y coordinate
        /// </summary>
        public double Y;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPairBase"/> class. 
        /// Default Constructor
        /// </summary>
        public PointPairBase()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPairBase"/> class. 
        /// Creates a point pair with the specified X and Y.
        /// </summary>
        /// <param name="x">
        /// This pair's x coordinate.
        /// </param>
        /// <param name="y">
        /// This pair's y coordinate.
        /// </param>
        public PointPairBase(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPairBase"/> class. 
        /// Creates a point pair from the specified <see cref="PointF"/> struct.
        /// </summary>
        /// <param name="pt">
        /// The <see cref="PointF"/> struct from which to get the
        /// new <see cref="PointPair"/> values.
        /// </param>
        public PointPairBase(PointF pt)
            : this(pt.X, pt.Y)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPairBase"/> class. 
        /// The PointPairBase copy constructor.
        /// </summary>
        /// <param name="rhs">
        /// The basis for the copy.
        /// </param>
        public PointPairBase(PointPairBase rhs)
        {
            this.X = rhs.X;
            this.Y = rhs.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPairBase"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected PointPairBase(SerializationInfo info, StreamingContext context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema");

            this.X = info.GetDouble("X");
            this.Y = info.GetDouble("Y");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Readonly value that determines if either the X or the Y
        /// coordinate in this PointPair is an invalid (not plotable) value.
        /// It is considered invalid if it is missing (equal to System.Double.Max),
        /// Infinity, or NaN.
        /// </summary>
        /// <returns>true if either value is invalid</returns>
        public bool IsInvalid
        {
            get
            {
                return this.X == PointPairBase.Missing || this.Y == PointPairBase.Missing || double.IsInfinity(this.X)
                       || double.IsInfinity(this.Y) || double.IsNaN(this.X) || double.IsNaN(this.Y);
            }
        }

        /// <summary>
        /// Readonly value that determines if either the X or the Y
        /// coordinate in this PointPair is a missing value.
        /// </summary>
        /// <returns>true if either value is missing</returns>
        public bool IsMissing
        {
            get
            {
                return this.X == PointPairBase.Missing || this.Y == PointPairBase.Missing;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// static method to determine if the specified point value is invalid.
        /// </summary>
        /// <remarks>
        /// The value is considered invalid if it is <see cref="PointPairBase.Missing"/>,
        /// <see cref="Double.PositiveInfinity"/>, <see cref="Double.NegativeInfinity"/>
        /// or <see cref="Double.NaN"/>.
        /// </remarks>
        /// <param name="value">
        /// The value to be checked for validity.
        /// </param>
        /// <returns>
        /// true if the value is invalid, false otherwise
        /// </returns>
        public static bool IsValueInvalid(double value)
        {
            return value == PointPairBase.Missing || double.IsInfinity(value) || double.IsNaN(value);
        }

        /// <summary>
        /// Implicit conversion from PointPair to PointF.  Note that this conversion
        /// can result in data loss, since the data are being cast from a type
        /// double (64 bit) to a float (32 bit).
        /// </summary>
        /// <param name="pair">The PointPair struct on which to operate</param>
        /// <returns>A PointF struct equivalent to the PointPair</returns>
        public static implicit operator PointF(PointPairBase pair)
        {
            return new PointF((float)pair.X, (float)pair.Y);
        }

        /// <summary>
        /// Compare two <see cref="PointPairBase"/> objects for equality.  To be equal, X and Y
        /// must be exactly the same between the two objects.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="PointPairBase"/> object to be compared with.
        /// </param>
        /// <returns>
        /// true if the <see cref="PointPairBase"/> objects are equal, false otherwise
        /// </returns>
        public override bool Equals(object obj)
        {
            PointPairBase rhs = obj as PointPairBase;
            return this.X == rhs.X && this.Y == rhs.Y;
        }

        /// <summary>
        /// Return the HashCode from the base class.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
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
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("schema", schema);
            info.AddValue("X", this.X);
            info.AddValue("Y", this.Y);
        }

        /// <summary>
        /// Format this PointPair value using the default format.  Example:  "( 12.345, -16.876 )".
        /// The two double values are formatted with the "g" format type.
        /// </summary>
        /// <returns>A string representation of the PointPair</returns>
        public override string ToString()
        {
            return this.ToString(PointPairBase.DefaultFormat);
        }

        /// <summary>
        /// Format this PointPair value using a general format string.
        /// Example:  a format string of "e2" would give "( 1.23e+001, -1.69e+001 )".
        /// </summary>
        /// <param name="format">
        /// A format string that will be used to format each of
        /// the two double type values (see <see cref="System.Double.ToString()"/>).
        /// </param>
        /// <returns>
        /// A string representation of the PointPair
        /// </returns>
        public string ToString(string format)
        {
            return "( " + this.X.ToString(format) + ", " + this.Y.ToString(format) + " )";
        }

        /// <summary>
        /// Format this PointPair value using different general format strings for the X and Y values.
        /// Example:  a format string of "e2" would give "( 1.23e+001, -1.69e+001 )".
        /// The Z value is not displayed (see <see cref="PointPair.ToString( string, string, string )"/>).
        /// </summary>
        /// <param name="formatX">
        /// A format string that will be used to format the X
        /// double type value (see <see cref="System.Double.ToString()"/>).
        /// </param>
        /// <param name="formatY">
        /// A format string that will be used to format the Y
        /// double type value (see <see cref="System.Double.ToString()"/>).
        /// </param>
        /// <returns>
        /// A string representation of the PointPair
        /// </returns>
        public string ToString(string formatX, string formatY)
        {
            return "( " + this.X.ToString(formatX) + ", " + this.Y.ToString(formatY) + " )";
        }

        #endregion
    }
}