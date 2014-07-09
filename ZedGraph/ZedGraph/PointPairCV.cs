// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="PointPairCV.cs">
//   
// </copyright>
// <summary>
//   A simple instance that stores a data point (X, Y, Z).  This differs from a regular
//   in that it maps the  property
//   to an independent value.  That is,  and
//   are not related (as they are in the
//   ).
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if ( !DOTNET1 ) // Is this a .Net 2 compilation?

#endif

namespace ZedGraph.ZedGraph
{
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// A simple instance that stores a data point (X, Y, Z).  This differs from a regular
    /// <see cref="PointPair" /> in that it maps the <see cref="ColorValue" /> property
    /// to an independent value.  That is, <see cref="ColorValue" /> and
    /// <see cref="PointPair.Z" /> are not related (as they are in the
    /// <see cref="PointPair" />).
    /// </summary>
    public class PointPairCV : PointPair
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema3 = 11;

        #endregion

        #region Fields

        /// <summary>
        /// This is a user value that can be anything.  It is used to provide special 
        /// property-based coloration to the graph elements.
        /// </summary>
        private double _colorValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPairCV"/> class. 
        /// Creates a point pair with the specified X, Y, and base value.
        /// </summary>
        /// <param name="x">
        /// This pair's x coordinate.
        /// </param>
        /// <param name="y">
        /// This pair's y coordinate.
        /// </param>
        /// <param name="z">
        /// This pair's z or lower dependent coordinate.
        /// </param>
        public PointPairCV(double x, double y, double z)
            : base(x, y, z, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointPairCV"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected PointPairCV(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema3");

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
            info.AddValue("ColorValue", this.ColorValue);
        }

        #endregion
    }
}