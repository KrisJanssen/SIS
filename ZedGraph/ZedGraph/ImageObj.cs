// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ImageObj.cs">
//   
// </copyright>
// <summary>
//   A class that represents an image object on the graph.  A list of
//   objects is maintained by the
//   collection class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// A class that represents an image object on the graph.  A list of
    /// <see cref="GraphObj"/> objects is maintained by the <see cref="GraphObjList"/>
    /// collection class.
    /// </summary>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 3.2 $ $Date: 2006-09-09 17:32:01 $ </version>
    [Serializable]
    public class ImageObj : GraphObj, ICloneable, ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema2 = 10;

        #endregion

        #region Fields

        /// <summary>
        /// Private field that stores the image.  Use the public property <see cref="Image"/>
        /// to access this value.
        /// </summary>
        private Image _image;

        /// <summary>
        /// Private field that determines if the image will be scaled to the output rectangle.
        /// </summary>
        /// <value>true to scale the image, false to draw the image unscaled, but clipped
        /// to the destination rectangle</value>
        private bool _isScaled;

        #endregion

        #region Constructors and Destructors

        /// <overloads>
        /// Constructors for the <see cref="ImageObj"/> object
        /// </overloads>
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObj"/> class. 
        /// Initializes a new instance of the <see cref="ImageObj"/> class. 
        /// A default constructor that places a null <see cref="System.Drawing.Image"/> at a
        /// default <see cref="RectangleF"/> of (0,0,1,1)
        /// </summary>
        public ImageObj()
            : this(null, 0, 0, 1, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObj"/> class. 
        /// A constructor that allows the <see cref="System.Drawing.Image"/> and
        /// <see cref="RectangleF"/> location for the
        /// <see cref="ImageObj"/> to be pre-specified.
        /// </summary>
        /// <param name="image">
        /// A <see cref="System.Drawing.Image"/> class that defines
        /// the image
        /// </param>
        /// <param name="rect">
        /// A <see cref="RectangleF"/> struct that defines the
        /// image location, specifed in units based on the
        /// <see cref="Location.CoordinateFrame"/> property.
        /// </param>
        public ImageObj(Image image, RectangleF rect)
            : this(image, rect.X, rect.Y, rect.Width, rect.Height)
        {
        }

        /// <overloads>
        /// Constructors for the <see cref="ImageObj"/> object
        /// </overloads>
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObj"/> class. 
        /// Initializes a new instance of the <see cref="ImageObj"/> class. 
        /// A constructor that allows the <see cref="System.Drawing.Image"/> and
        /// <see cref="RectangleF"/> location for the
        /// <see cref="ImageObj"/> to be pre-specified.
        /// </summary>
        /// <param name="image">
        /// A <see cref="System.Drawing.Image"/> class that defines
        /// the image
        /// </param>
        /// <param name="rect">
        /// A <see cref="RectangleF"/> struct that defines the
        /// image location, specifed in units based on the
        /// <see cref="Location.CoordinateFrame"/> property.
        /// </param>
        /// <param name="coordType">
        /// The <see cref="CoordType"/> enum value that
        /// indicates what type of coordinate system the x and y parameters are
        /// referenced to.
        /// </param>
        /// <param name="alignH">
        /// The <see cref="ZedGraph.AlignH"/> enum that specifies
        /// the horizontal alignment of the object with respect to the (x,y) location
        /// </param>
        /// <param name="alignV">
        /// The <see cref="ZedGraph.AlignV"/> enum that specifies
        /// the vertical alignment of the object with respect to the (x,y) location
        /// </param>
        public ImageObj(Image image, RectangleF rect, CoordType coordType, AlignH alignH, AlignV alignV)
            : base(rect.X, rect.Y, rect.Width, rect.Height, coordType, alignH, alignV)
        {
            this._image = image;
            this._isScaled = Default.IsScaled;
        }

        /// <overloads>
        /// Constructors for the <see cref="ImageObj"/> object
        /// </overloads>
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObj"/> class. 
        /// Initializes a new instance of the <see cref="ImageObj"/> class. 
        /// A constructor that allows the <see cref="System.Drawing.Image"/> and
        /// individual <see cref="System.Single"/> coordinate locations for the
        /// <see cref="ImageObj"/> to be pre-specified.
        /// </summary>
        /// <param name="image">
        /// A <see cref="System.Drawing.Image"/> class that defines
        /// the image
        /// </param>
        /// <param name="left">
        /// The position of the left side of the rectangle that defines the
        /// <see cref="ImageObj"/> location.  The units of this position are specified by the
        /// <see cref="Location.CoordinateFrame"/> property.
        /// </param>
        /// <param name="top">
        /// The position of the top side of the rectangle that defines the
        /// <see cref="ImageObj"/> location.  The units of this position are specified by the
        /// <see cref="Location.CoordinateFrame"/> property.
        /// </param>
        /// <param name="width">
        /// The width of the rectangle that defines the
        /// <see cref="ImageObj"/> location.  The units of this position are specified by the
        /// <see cref="Location.CoordinateFrame"/> property.
        /// </param>
        /// <param name="height">
        /// The height of the rectangle that defines the
        /// <see cref="ImageObj"/> location.  The units of this position are specified by the
        /// <see cref="Location.CoordinateFrame"/> property.
        /// </param>
        public ImageObj(Image image, double left, double top, double width, double height)
            : base(left, top, width, height)
        {
            this._image = image;
            this._isScaled = Default.IsScaled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObj"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="ImageObj"/> object from which to copy
        /// </param>
        public ImageObj(ImageObj rhs)
            : base(rhs)
        {
            this._image = rhs._image;
            this._isScaled = rhs.IsScaled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObj"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected ImageObj(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema2");

            this._image = (Image)info.GetValue("image", typeof(Image));
            this._isScaled = info.GetBoolean("isScaled");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The <see cref="System.Drawing.Image"/> object.
        /// </summary>
        /// <value> A <see cref="System.Drawing.Image"/> class reference. </value>
        public Image Image
        {
            get
            {
                return this._image;
            }

            set
            {
                this._image = value;
            }
        }

        /// <summary>
        /// Gets or sets a property that determines if the image will be scaled to the
        /// output rectangle (see <see cref="Location"/>).
        /// </summary>
        /// <value>true to scale the image, false to draw the image unscaled, but clipped
        /// to the destination rectangle</value>
        public bool IsScaled
        {
            get
            {
                return this._isScaled;
            }

            set
            {
                this._isScaled = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public ImageObj Clone()
        {
            return new ImageObj(this);
        }

        /// <summary>
        /// Render this object to the specified <see cref="Graphics"/> device
        /// This method is normally only called by the Draw method
        /// of the parent <see cref="GraphObjList"/> collection object.
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
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        public override void Draw(Graphics g, PaneBase pane, float scaleFactor)
        {
            if (this._image != null)
            {
                // Convert the rectangle coordinates from the user coordinate system
                // to the screen coordinate system
                RectangleF tmpRect = this._location.TransformRect(pane);

                if (this._isScaled)
                {
                    g.DrawImage(this._image, tmpRect);
                }
                else
                {
                    Region clip = g.Clip;
                    g.SetClip(tmpRect);
                    g.DrawImageUnscaled(this._image, Rectangle.Round(tmpRect));
                    g.SetClip(clip, CombineMode.Replace);

                    // g.DrawImageUnscaledAndClipped( image, Rectangle.Round( tmpRect ) );
                }
            }
        }

        /// <summary>
        /// Determines the shape type and Coords values for this GraphObj
        /// </summary>
        /// <param name="pane">
        /// The pane.
        /// </param>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="scaleFactor">
        /// The scale Factor.
        /// </param>
        /// <param name="shape">
        /// The shape.
        /// </param>
        /// <param name="coords">
        /// The coords.
        /// </param>
        public override void GetCoords(
            PaneBase pane, 
            Graphics g, 
            float scaleFactor, 
            out string shape, 
            out string coords)
        {
            // transform the x,y location from the user-defined
            // coordinate frame to the screen pixel location
            RectangleF pixRect = this._location.TransformRect(pane);

            shape = "rect";
            coords = string.Format(
                "{0:f0},{1:f0},{2:f0},{3:f0}", 
                pixRect.Left, 
                pixRect.Top, 
                pixRect.Right, 
                pixRect.Bottom);
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
            info.AddValue("image", this._image);
            info.AddValue("isScaled", this._isScaled);
        }

        /// <summary>
        /// Determine if the specified screen point lies inside the bounding box of this
        /// <see cref="ArrowObj"/>.  The bounding box is calculated assuming a distance
        /// of <see cref="GraphPane.Default.NearestTol"/> pixels around the arrow segment.
        /// </summary>
        /// <param name="pt">
        /// The screen point, in pixels
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="PaneBase"/> object that is the parent or
        /// owner of this object.
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
        /// <returns>
        /// true if the point lies in the bounding box, false otherwise
        /// </returns>
        public override bool PointInBox(PointF pt, PaneBase pane, Graphics g, float scaleFactor)
        {
            if (this._image != null)
            {
                if (! base.PointInBox(pt, pane, g, scaleFactor))
                {
                    return false;
                }

                // transform the x,y location from the user-defined
                // coordinate frame to the screen pixel location
                RectangleF tmpRect = this._location.TransformRect(pane);

                return tmpRect.Contains(pt);
            }
            else
            {
                return false;
            }
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
        /// default property values for the <see cref="ImageObj"/> class.
        /// </summary>
        public new struct Default
        {
            // Default text item properties
            #region Static Fields

            /// <summary>
            /// Default value for the <see cref="ImageObj"/>
            /// <see cref="ImageObj.IsScaled"/> property.
            /// </summary>
            public static bool IsScaled = true;

            #endregion
        }
    }
}