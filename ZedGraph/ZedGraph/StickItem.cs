// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="StickItem.cs">
//   
// </copyright>
// <summary>
//   Encapsulates a curve type that is displayed as a series of vertical "sticks",
//   one at each defined point.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Encapsulates a curve type that is displayed as a series of vertical "sticks",
    /// one at each defined point.
    /// </summary>
    /// <remarks>
    /// The sticks run from the zero value of the Y axis, to the Y point defined in each
    /// <see cref="PointPair"/> of the <see cref="IPointList" /> (see <see cref="CurveItem.Points"/>).
    /// The properties of the sticks are defined in the <see cref="Line"/> property.
    /// Normally, the <see cref="Symbol"/> is not visible.  However, if you manually enable the
    /// <see cref="Symbol"/> using the <see cref="ZedGraph.Symbol.IsVisible"/> property, the
    /// symbols will be drawn at the "Z" value from each <see cref="PointPair" /> (see
    /// <see cref="PointPair.Z" />).
    /// </remarks>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 1.7 $ $Date: 2007-01-25 07:56:09 $ </version>
    [Serializable]
    public class StickItem : LineItem, ICloneable, ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema3 = 10;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StickItem"/> class. 
        /// Create a new <see cref="StickItem"/>, specifying only the legend <see cref="CurveItem.Label"/>.
        /// </summary>
        /// <param name="label">
        /// The label that will appear in the legend.
        /// </param>
        public StickItem(string label)
            : base(label)
        {
            this._symbol.IsVisible = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StickItem"/> class. 
        /// Create a new <see cref="StickItem"/> using the specified properties.
        /// </summary>
        /// <param name="label">
        /// The label that will appear in the legend.
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
        /// <param name="lineWidth">
        /// The width (in points) to be used for the <see cref="Line"/>.  This
        /// width is scaled based on <see cref="PaneBase.CalcScaleFactor"/>.  Use a value of zero to
        /// hide the line (see <see cref="ZedGraph.LineBase.IsVisible"/>).
        /// </param>
        public StickItem(string label, double[] x, double[] y, Color color, float lineWidth)
            : this(label, new PointPairList(x, y), color, lineWidth)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StickItem"/> class. 
        /// Create a new <see cref="StickItem"/> using the specified properties.
        /// </summary>
        /// <param name="label">
        /// The label that will appear in the legend.
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
        public StickItem(string label, double[] x, double[] y, Color color)
            : this(label, new PointPairList(x, y), color)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StickItem"/> class. 
        /// Create a new <see cref="StickItem"/> using the specified properties.
        /// </summary>
        /// <param name="label">
        /// The label that will appear in the legend.
        /// </param>
        /// <param name="points">
        /// A <see cref="IPointList"/> of double precision value pairs that define
        /// the X and Y values for this curve
        /// </param>
        /// <param name="color">
        /// A <see cref="Color"/> value that will be applied to
        /// the <see cref="Line"/> and <see cref="Symbol"/> properties.
        /// </param>
        public StickItem(string label, IPointList points, Color color)
            : this(label, points, color, global::ZedGraph.ZedGraph.LineBase.Default.Width)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StickItem"/> class. 
        /// Create a new <see cref="StickItem"/> using the specified properties.
        /// </summary>
        /// <param name="label">
        /// The label that will appear in the legend.
        /// </param>
        /// <param name="points">
        /// A <see cref="IPointList"/> of double precision value pairs that define
        /// the X and Y values for this curve
        /// </param>
        /// <param name="color">
        /// A <see cref="Color"/> value that will be applied to
        /// the <see cref="Line"/> and <see cref="Symbol"/> properties.
        /// </param>
        /// <param name="lineWidth">
        /// The width (in points) to be used for the <see cref="Line"/>.  This
        /// width is scaled based on <see cref="PaneBase.CalcScaleFactor"/>.  Use a value of zero to
        /// hide the line (see <see cref="ZedGraph.LineBase.IsVisible"/>).
        /// </param>
        public StickItem(string label, IPointList points, Color color, float lineWidth)
            : base(label, points, color, Symbol.Default.Type, lineWidth)
        {
            this._symbol.IsVisible = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StickItem"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="StickItem"/> object from which to copy
        /// </param>
        public StickItem(StickItem rhs)
            : base(rhs)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StickItem"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected StickItem(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema3");
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public new StickItem Clone()
        {
            return new StickItem(this);
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
            info.AddValue("schema3", schema3);
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
            return this._symbol.IsVisible;
        }

        #endregion
    }
}