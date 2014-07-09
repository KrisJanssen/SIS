// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="TextObj.cs">
//   
// </copyright>
// <summary>
//   A class that represents a text object on the graph.  A list of
//   <see cref="GraphObj" /> objects is maintained by the
//   <see cref="GraphObjList" /> collection class.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// A class that represents a text object on the graph.  A list of
    /// <see cref="GraphObj"/> objects is maintained by the
    /// <see cref="GraphObjList"/> collection class.
    /// </summary>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 3.4 $ $Date: 2007-01-25 07:56:09 $ </version>
    [Serializable]
    public class TextObj : GraphObj, ICloneable, ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema2 = 10;

        #endregion

        #region Fields

        /// <summary>
        /// Private field to store the <see cref="FontSpec"/> class used to render
        /// this <see cref="TextObj"/>.  Use the public property <see cref="FontSpec"/>
        /// to access this class.
        /// </summary>
        private FontSpec _fontSpec;

        /*
		/// <summary>
		/// Private field to indicate whether this <see cref="TextObj"/> is to be
		/// wrapped when rendered.  Wrapping is to be done within <see cref="TextObj.wrappedRect"/>.
		/// Use the public property <see cref="TextObj.IsWrapped"/>
		/// to access this value.
		/// </summary>
		private bool isWrapped;
		*/

        /// <summary>
        /// Private field holding the SizeF into which this <see cref="TextObj"/>
        /// should be rendered. Use the public property <see cref="TextObj.LayoutArea"/>
        /// to access this value.
        /// </summary>
        private SizeF _layoutArea;

        /// <summary> Private field to store the actual text string for this
        /// <see cref="TextObj"/>.  Use the public property <see cref="TextObj.Text"/>
        /// to access this value.
        /// </summary>
        private string _text;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextObj"/> class. 
        /// Constructor that sets all <see cref="TextObj"/> properties to default
        /// values as defined in the <see cref="Default"/> class.
        /// </summary>
        /// <param name="text">
        /// The text to be displayed.
        /// </param>
        /// <param name="x">
        /// The x position of the text.  The units
        /// of this position are specified by the
        /// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
        /// aligned to this position based on the <see cref="AlignH"/>
        /// property.
        /// </param>
        /// <param name="y">
        /// The y position of the text.  The units
        /// of this position are specified by the
        /// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
        /// aligned to this position based on the
        /// <see cref="AlignV"/> property.
        /// </param>
        public TextObj(string text, double x, double y)
            : base(x, y)
        {
            this.Init(text);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextObj"/> class. 
        /// Constructor that sets all <see cref="TextObj"/> properties to default
        /// values as defined in the <see cref="Default"/> class.
        /// </summary>
        /// <param name="text">
        /// The text to be displayed.
        /// </param>
        /// <param name="x">
        /// The x position of the text.  The units
        /// of this position are specified by the
        /// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
        /// aligned to this position based on the <see cref="AlignH"/>
        /// property.
        /// </param>
        /// <param name="y">
        /// The y position of the text.  The units
        /// of this position are specified by the
        /// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
        /// aligned to this position based on the
        /// <see cref="AlignV"/> property.
        /// </param>
        /// <param name="coordType">
        /// The <see cref="CoordType"/> enum value that
        /// indicates what type of coordinate system the x and y parameters are
        /// referenced to.
        /// </param>
        public TextObj(string text, double x, double y, CoordType coordType)
            : base(x, y, coordType)
        {
            this.Init(text);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextObj"/> class. 
        /// Constructor that sets all <see cref="TextObj"/> properties to default
        /// values as defined in the <see cref="Default"/> class.
        /// </summary>
        /// <param name="text">
        /// The text to be displayed.
        /// </param>
        /// <param name="x">
        /// The x position of the text.  The units
        /// of this position are specified by the
        /// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
        /// aligned to this position based on the <see cref="AlignH"/>
        /// property.
        /// </param>
        /// <param name="y">
        /// The y position of the text.  The units
        /// of this position are specified by the
        /// <see cref="ZedGraph.Location.CoordinateFrame"/> property.  The text will be
        /// aligned to this position based on the
        /// <see cref="AlignV"/> property.
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
        public TextObj(string text, double x, double y, CoordType coordType, AlignH alignH, AlignV alignV)
            : base(x, y, coordType, alignH, alignV)
        {
            this.Init(text);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextObj"/> class. 
        /// Parameterless constructor that initializes a new <see cref="TextObj"/>.
        /// </summary>
        public TextObj()
            : base(0, 0)
        {
            this.Init(string.Empty);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextObj"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="TextObj"/> object from which to copy
        /// </param>
        public TextObj(TextObj rhs)
            : base(rhs)
        {
            this._text = rhs.Text;
            this._fontSpec = new FontSpec(rhs.FontSpec);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextObj"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected TextObj(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema2");

            this._text = info.GetString("text");
            this._fontSpec = (FontSpec)info.GetValue("fontSpec", typeof(FontSpec));

            // isWrapped = info.GetBoolean ("isWrapped") ;
            this._layoutArea = (SizeF)info.GetValue("layoutArea", typeof(SizeF));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a reference to the <see cref="FontSpec"/> class used to render
        /// this <see cref="TextObj"/>
        /// </summary>
        /// <seealso cref="Default.FontColor"/>
        /// <seealso cref="Default.FontBold"/>
        /// <seealso cref="Default.FontItalic"/>
        /// <seealso cref="Default.FontUnderline"/>
        /// <seealso cref="Default.FontFamily"/>
        /// <seealso cref="Default.FontSize"/>
        public FontSpec FontSpec
        {
            get
            {
                return this._fontSpec;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Uninitialized FontSpec in TextObj");
                }

                this._fontSpec = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SizeF LayoutArea
        {
            get
            {
                return this._layoutArea;
            }

            set
            {
                this._layoutArea = value;
            }
        }

        /// <summary>
        /// The <see cref="TextObj"/> to be displayed.  This text can be multi-line by
        /// including newline ('\n') characters between the lines.
        /// </summary>
        public string Text
        {
            get
            {
                return this._text;
            }

            set
            {
                this._text = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public TextObj Clone()
        {
            return new TextObj(this);
        }

        /// <summary>
        /// Render this <see cref="TextObj"/> object to the specified <see cref="Graphics"/> device
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
            // transform the x,y location from the user-defined
            // coordinate frame to the screen pixel location
            PointF pix = this._location.Transform(pane);

            // Draw the text on the screen, including any frame and background
            // fill elements
            if (pix.X > -100000 && pix.X < 100000 && pix.Y > -100000 && pix.Y < 100000)
            {
                // if ( this.layoutSize.IsEmpty )
                // 	this.FontSpec.Draw( g, pane.IsPenWidthScaled, this.text, pix.X, pix.Y,
                // 		this.location.AlignH, this.location.AlignV, scaleFactor );
                // else
                this.FontSpec.Draw(
                    g, 
                    pane, 
                    this._text, 
                    pix.X, 
                    pix.Y, 
                    this._location.AlignH, 
                    this._location.AlignV, 
                    scaleFactor, 
                    this._layoutArea);
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
            PointF pix = this._location.Transform(pane);

            PointF[] pts = this._fontSpec.GetBox(
                g, 
                this._text, 
                pix.X, 
                pix.Y, 
                this._location.AlignH, 
                this._location.AlignV, 
                scaleFactor, 
                new SizeF());

            shape = "poly";
            coords = string.Format(
                "{0:f0},{1:f0},{2:f0},{3:f0},{4:f0},{5:f0},{6:f0},{7:f0},", 
                pts[0].X, 
                pts[0].Y, 
                pts[1].X, 
                pts[1].Y, 
                pts[2].X, 
                pts[2].Y, 
                pts[3].X, 
                pts[3].Y);
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
            info.AddValue("text", this._text);
            info.AddValue("fontSpec", this._fontSpec);

            // info.AddValue( "isWrapped", isWrapped );
            info.AddValue("layoutArea", this._layoutArea);
        }

        /// <summary>
        /// Determine if the specified screen point lies inside the bounding box of this
        /// <see cref="TextObj"/>.  This method takes into account rotation and alignment
        /// parameters of the text, as specified in the <see cref="FontSpec"/>.
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
            if (! base.PointInBox(pt, pane, g, scaleFactor))
            {
                return false;
            }

            // transform the x,y location from the user-defined
            // coordinate frame to the screen pixel location
            PointF pix = this._location.Transform(pane);

            return this._fontSpec.PointInBox(
                pt, 
                g, 
                this._text, 
                pix.X, 
                pix.Y, 
                this._location.AlignH, 
                this._location.AlignV, 
                scaleFactor, 
                this.LayoutArea);
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
        /// The init.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        private void Init(string text)
        {
            if (text != null)
            {
                this._text = text;
            }
            else
            {
                text = "Text";
            }

            this._fontSpec = new FontSpec(
                Default.FontFamily, 
                Default.FontSize, 
                Default.FontColor, 
                Default.FontBold, 
                Default.FontItalic, 
                Default.FontUnderline);

            // this.isWrapped = Default.IsWrapped ;
            this._layoutArea = new SizeF(0, 0);
        }

        #endregion

        /// <summary>
        /// A simple struct that defines the
        /// default property values for the <see cref="TextObj"/> class.
        /// </summary>
        public new struct Default
        {
            /*
			/// <summary>
			/// The default wrapped flag for rendering this <see cref="TextObj,Text"/>. 
			/// </summary>
			public static bool IsWrapped = false ;
			/// <summary>
			/// The default RectangleF for rendering this <see cref="TextObj.Text"/> 
			/// </summary>
			public static SizeF WrappedSize = new SizeF( 0,0 );
			*/
            #region Static Fields

            /// <summary>
            /// The default font bold mode for the <see cref="TextObj"/> text
            /// (<see cref="ZedGraph.FontSpec.IsBold"/> property). true
            /// for a bold typeface, false otherwise.
            /// </summary>
            public static bool FontBold = false;

            /// <summary>
            /// The default font color for the <see cref="TextObj"/> text
            /// (<see cref="ZedGraph.FontSpec.FontColor"/> property).
            /// </summary>
            public static Color FontColor = Color.Black;

            /// <summary>
            /// The default font family for the <see cref="TextObj"/> text
            /// (<see cref="ZedGraph.FontSpec.Family"/> property).
            /// </summary>
            public static string FontFamily = "Arial";

            /// <summary>
            /// The default font italic mode for the <see cref="TextObj"/> text
            /// (<see cref="ZedGraph.FontSpec.IsItalic"/> property). true
            /// for an italic typeface, false otherwise.
            /// </summary>
            public static bool FontItalic = false;

            /// <summary>
            /// The default font size for the <see cref="TextObj"/> text
            /// (<see cref="ZedGraph.FontSpec.Size"/> property).  Units are
            /// in points (1/72 inch).
            /// </summary>
            public static float FontSize = 12.0F;

            /// <summary>
            /// The default font underline mode for the <see cref="TextObj"/> text
            /// (<see cref="ZedGraph.FontSpec.IsUnderline"/> property). true
            /// for an underlined typeface, false otherwise.
            /// </summary>
            public static bool FontUnderline = false;

            #endregion
        }
    }
}