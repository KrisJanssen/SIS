// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointPair4.cs" company="">
//   
// </copyright>
// <summary>
//   The basic <see cref="PointPair" /> class holds three data values (X, Y, Z).  This
//   class extends the basic PointPair to contain four data values (X, Y, Z, T).
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// The basic <see cref="PointPair" /> class holds three data values (X, Y, Z).  This
    /// class extends the basic PointPair to contain four data values (X, Y, Z, T).
    /// </summary>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 3.3 $ $Date: 2007-03-17 18:43:44 $ </version>
    [Serializable]
    public class PointPair4 : PointPair, ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema3 = 11;

        #endregion

        #region Fields

        /// <summary>
        /// This PointPair4's T coordinate.
        /// </summary>
        public double T;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPair4"/> class. 
        /// Default Constructor
        /// </summary>
        public PointPair4()
            : base()
        {
            this.T = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPair4"/> class. 
        /// Creates a point pair with the specified X, Y, Z, and T value.
        /// </summary>
        /// <param name="x">
        /// This pair's x coordinate.
        /// </param>
        /// <param name="y">
        /// This pair's y coordinate.
        /// </param>
        /// <param name="z">
        /// This pair's z coordinate.
        /// </param>
        /// <param name="t">
        /// This pair's t coordinate.
        /// </param>
        public PointPair4(double x, double y, double z, double t)
            : base(x, y, z)
        {
            this.T = t;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPair4"/> class. 
        /// Creates a point pair with the specified X, Y, base value, and
        /// label (<see cref="PointPair.Tag"/>).
        /// </summary>
        /// <param name="x">
        /// This pair's x coordinate.
        /// </param>
        /// <param name="y">
        /// This pair's y coordinate.
        /// </param>
        /// <param name="z">
        /// This pair's z coordinate.
        /// </param>
        /// <param name="t">
        /// This pair's t coordinate.
        /// </param>
        /// <param name="label">
        /// This pair's string label (<see cref="PointPair.Tag"/>)
        /// </param>
        public PointPair4(double x, double y, double z, double t, string label)
            : base(x, y, z, label)
        {
            this.T = t;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPair4"/> class. 
        /// The PointPair4 copy constructor.
        /// </summary>
        /// <param name="rhs">
        /// The basis for the copy.
        /// </param>
        public PointPair4(PointPair4 rhs)
            : base(rhs)
        {
            this.T = rhs.T;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPair4"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected PointPair4(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema3");

            this.T = info.GetDouble("T");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Readonly value that determines if either the X, Y, Z, or T
        /// coordinate in this PointPair4 is an invalid (not plotable) value.
        /// It is considered invalid if it is missing (equal to System.Double.Max),
        /// Infinity, or NaN.
        /// </summary>
        /// <returns>true if any value is invalid</returns>
        public bool IsInvalid4D
        {
            get
            {
                return this.X == PointPair.Missing || this.Y == PointPair.Missing || this.Z == PointPair.Missing
                       || this.T == PointPair.Missing || double.IsInfinity(this.X) || double.IsInfinity(this.Y)
                       || double.IsInfinity(this.Z) || double.IsInfinity(this.T) || double.IsNaN(this.X)
                       || double.IsNaN(this.Y) || double.IsNaN(this.Z) || double.IsNaN(this.T);
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
            info.AddValue("schema2", schema3);
            info.AddValue("T", this.T);
        }

        /// <summary>
        /// Format this PointPair4 value using the default format.  Example:  "( 12.345, -16.876 )".
        /// The two double values are formatted with the "g" format type.
        /// </summary>
        /// <param name="isShowZT">
        /// true to show the third "Z" and fourth "T" value coordinates
        /// </param>
        /// <returns>
        /// A string representation of the PointPair4
        /// </returns>
        public new string ToString(bool isShowZT)
        {
            return this.ToString(PointPair.DefaultFormat, isShowZT);
        }

        /// <summary>
        /// Format this PointPair value using a general format string.
        /// Example:  a format string of "e2" would give "( 1.23e+001, -1.69e+001 )".
        /// If <see paramref="isShowZ"/>
        /// is true, then the third "Z" coordinate is also shown.
        /// </summary>
        /// <param name="format">
        /// A format string that will be used to format each of
        /// the two double type values (see <see cref="System.Double.ToString()"/>).
        /// </param>
        /// <param name="isShowZT">
        /// true to show the third "Z" or low dependent value coordinate
        /// </param>
        /// <returns>
        /// A string representation of the PointPair
        /// </returns>
        public new string ToString(string format, bool isShowZT)
        {
            return "( " + this.X.ToString(format) + ", " + this.Y.ToString(format)
                   + (isShowZT ? (", " + this.Z.ToString(format) + ", " + this.T.ToString(format)) : string.Empty) + " )";
        }

        /// <summary>
        /// Format this PointPair value using different general format strings for the X, Y, and Z values.
        /// Example:  a format string of "e2" would give "( 1.23e+001, -1.69e+001 )".
        /// </summary>
        /// <param name="formatX">
        /// A format string that will be used to format the X
        /// double type value (see <see cref="System.Double.ToString()"/>).
        /// </param>
        /// <param name="formatY">
        /// A format string that will be used to format the Y
        /// double type value (see <see cref="System.Double.ToString()"/>).
        /// </param>
        /// <param name="formatZ">
        /// A format string that will be used to format the Z
        /// double type value (see <see cref="System.Double.ToString()"/>).
        /// </param>
        /// <param name="formatT">
        /// A format string that will be used to format the T
        /// double type value (see <see cref="System.Double.ToString()"/>).
        /// </param>
        /// <returns>
        /// A string representation of the PointPair
        /// </returns>
        public string ToString(string formatX, string formatY, string formatZ, string formatT)
        {
            return "( " + this.X.ToString(formatX) + ", " + this.Y.ToString(formatY) + ", " + this.Z.ToString(formatZ)
                   + ", " + this.T.ToString(formatT) + " )";
        }

        #endregion
    }
}