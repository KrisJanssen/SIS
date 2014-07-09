// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Border.cs">
//   
// </copyright>
// <summary>
//   A class that encapsulates Border (frame) properties for an object.  The  class
//   is used in a variety of ZedGraph objects to handle the drawing of the Border around the object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// A class that encapsulates Border (frame) properties for an object.  The <see cref="Border"/> class
    /// is used in a variety of ZedGraph objects to handle the drawing of the Border around the object.
    /// </summary>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 3.18 $ $Date: 2007-03-17 18:43:44 $ </version>
    [Serializable]
    public class Border : LineBase, ISerializable, ICloneable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema = 11;

        #endregion

        #region Fields

        /// <summary>
        /// Private field that stores the amount of inflation to be done on the rectangle
        /// before rendering.  This allows the border to be inset or outset relative to
        /// the actual rectangle area.  Use the public property <see cref="InflateFactor"/>
        /// to access this value.
        /// </summary>
        private float _inflateFactor;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Border"/> class. 
        /// The default constructor.  Initialized to default values.
        /// </summary>
        public Border()
            : base()
        {
            this._inflateFactor = Default.InflateFactor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Border"/> class. 
        /// Constructor that specifies the visibility, color and penWidth of the Border.
        /// </summary>
        /// <param name="isVisible">
        /// Determines whether or not the Border will be drawn.
        /// </param>
        /// <param name="color">
        /// The color of the Border
        /// </param>
        /// <param name="width">
        /// The width, in points (1/72 inch), for the Border.
        /// </param>
        public Border(bool isVisible, Color color, float width)
            : base(color)
        {
            this._width = width;
            this._isVisible = isVisible;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Border"/> class. 
        /// Constructor that specifies the color and penWidth of the Border.
        /// </summary>
        /// <param name="color">
        /// The color of the Border
        /// </param>
        /// <param name="width">
        /// The width, in points (1/72 inch), for the Border.
        /// </param>
        public Border(Color color, float width)
            : this(!color.IsEmpty, color, width)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Border"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The Border object from which to copy
        /// </param>
        public Border(Border rhs)
            : base(rhs)
        {
            this._inflateFactor = rhs._inflateFactor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Border"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected Border(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema");

            this._inflateFactor = info.GetSingle("inflateFactor");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the amount of inflation to be done on the rectangle
        /// before rendering.
        /// </summary>
        /// <remarks>This allows the border to be inset or outset relative to
        /// the actual rectangle area.
        /// </remarks>
        public float InflateFactor
        {
            get
            {
                return this._inflateFactor;
            }

            set
            {
                this._inflateFactor = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public Border Clone()
        {
            return new Border(this);
        }

        /*
		/// <summary>
		/// Create a new <see cref="Pen"/> object from the properties of this
		/// <see cref="Border"/> object.
		/// </summary>
		/// <param name="isPenWidthScaled">
		/// Set to true to have the <see cref="Border"/> pen width scaled with the
		/// scaleFactor.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor for the features of the graph based on the <see cref="PaneBase.BaseDimension"/>.  This
		/// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
		/// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
		/// </param>
		/// <returns>A <see cref="Pen"/> object with the proper color and pen width.</returns>
		public Pen MakePen( bool isPenWidthScaled, float scaleFactor )
		{
			float scaledPenWidth = _width;
			if ( isPenWidthScaled )
				scaledPenWidth = (float)(_width * scaleFactor);
			
			return new Pen( _color, scaledPenWidth );
		}
		*/

        /// <summary>
        /// Draw the specified Border (<see cref="RectangleF"/>) using the properties of
        /// this <see cref="Border"/> object.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="PaneBase"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor for the features of the graph based on the <see cref="PaneBase.BaseDimension"/>.  This
        /// scaling factor is calculated by the <see cref="PaneBase.CalcScaleFactor"/> method.  The scale factor
        /// represents a linear multiple to be applied to font sizes, symbol sizes, etc.
        /// </param>
        /// <param name="rect">
        /// A <see cref="RectangleF"/> struct to be drawn.
        /// </param>
        public void Draw(Graphics g, PaneBase pane, float scaleFactor, RectangleF rect)
        {
            // Need to use the RectangleF props since rounding it can cause the axisFrame to
            // not line up properly with the last tic mark
            if (this._isVisible)
            {
                RectangleF tRect = rect;

                float scaledInflate = (float)(this._inflateFactor * scaleFactor);
                tRect.Inflate(scaledInflate, scaledInflate);

                using (Pen pen = this.GetPen(pane, scaleFactor)) g.DrawRectangle(pen, tRect.X, tRect.Y, tRect.Width, tRect.Height);
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

            info.AddValue("schema", schema);
            info.AddValue("inflateFactor", this._inflateFactor);
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
        /// default property values for the <see cref="Fill"/> class.
        /// </summary>
        public new struct Default
        {
            #region Static Fields

            /// <summary>
            /// The default value for <see cref="Border.InflateFactor"/>, in units of points (1/72 inch).
            /// </summary>
            /// <seealso cref="Border.InflateFactor"/>
            public static float InflateFactor = 0.0F;

            #endregion
        }
    }
}