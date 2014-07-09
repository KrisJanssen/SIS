// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MajorGrid.cs">
//   
// </copyright>
// <summary>
//   Class that handles the data associated with the major grid lines on the chart.
//   Inherits from .
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Class that handles the data associated with the major grid lines on the chart.
    /// Inherits from <see cref="MinorGrid" />.
    /// </summary>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 3.1 $ $Date: 2006-06-24 20:26:44 $ </version>
    [Serializable]
    public class MajorGrid : MinorGrid, ICloneable, ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema2 = 10;

        #endregion

        #region Fields

        /// <summary>
        /// The _is zero line.
        /// </summary>
        internal bool _isZeroLine;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MajorGrid"/> class. 
        /// Default constructor
        /// </summary>
        public MajorGrid()
        {
            this._dashOn = Default.DashOn;
            this._dashOff = Default.DashOff;
            this._penWidth = Default.PenWidth;
            this._isVisible = Default.IsVisible;
            this._color = Default.Color;
            this._isZeroLine = Default.IsZeroLine;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MajorGrid"/> class. 
        /// Copy constructor
        /// </summary>
        /// <param name="rhs">
        /// The source <see cref="MajorGrid"/> to be copied.
        /// </param>
        public MajorGrid(MajorGrid rhs)
            : base(rhs)
        {
            this._isZeroLine = rhs._isZeroLine;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MajorGrid"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected MajorGrid(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema2");

            this._isZeroLine = info.GetBoolean("isZeroLine");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a boolean value that determines if a line will be drawn at the
        /// zero value for the axis.
        /// </summary>
        /// <remarks>
        /// The zero line is a line that divides the negative values from the positive values.
        /// The default is set according to
        /// <see cref="XAxis.Default.IsZeroLine"/>, <see cref="YAxis.Default.IsZeroLine"/>,
        /// <see cref="Y2Axis.Default.IsZeroLine"/>,
        /// </remarks>
        /// <value>true to show the zero line, false otherwise</value>
        public bool IsZeroLine
        {
            get
            {
                return this._isZeroLine;
            }

            set
            {
                this._isZeroLine = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public new MajorGrid Clone()
        {
            return new MajorGrid(this);
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

            info.AddValue("isZeroLine", this._isZeroLine);
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
        /// default property values for the <see cref="MajorGrid"/> class.
        /// </summary>
        public new struct Default
        {
            #region Static Fields

            /// <summary>
            /// The default color for the <see cref="Axis"/> grid lines
            /// (<see cref="MinorGrid.Color"/> property).  This color only affects the
            /// grid lines.
            /// </summary>
            public static Color Color = Color.Black;

            /// <summary>
            /// The default "dash off" size for drawing the <see cref="Axis"/> grid
            /// (<see cref="MinorGrid.DashOff"/> property). Units are in points (1/72 inch).
            /// </summary>
            public static float DashOff = 5.0F;

            /// <summary>
            /// The default "dash on" size for drawing the <see cref="Axis"/> grid
            /// (<see cref="MinorGrid.DashOn"/> property). Units are in points (1/72 inch).
            /// </summary>
            public static float DashOn = 1.0F;

            /// <summary>
            /// The default display mode for the <see cref="Axis"/> grid lines
            /// (<see cref="MinorGrid.IsVisible"/> property). true
            /// to show the grid lines, false to hide them.
            /// </summary>
            public static bool IsVisible = false;

            /// <summary>
            /// The default boolean value that determines if a line will be drawn at the
            /// zero value for the axis.
            /// </summary>
            /// <remarks>
            /// The zero line is a line that divides the negative values from the positive values.
            /// The default is set according to
            /// <see cref="XAxis.Default.IsZeroLine"/>, <see cref="YAxis.Default.IsZeroLine"/>,
            /// <see cref="Y2Axis.Default.IsZeroLine"/>,
            /// </remarks>
            /// <value>true to show the zero line, false otherwise</value>
            public static bool IsZeroLine = false;

            /// <summary>
            /// The default pen width for drawing the <see cref="Axis"/> grid
            /// (<see cref="MinorGrid.PenWidth"/> property). Units are in points (1/72 inch).
            /// </summary>
            public static float PenWidth = 1.0F;

            #endregion
        }
    }
}