// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="FontSpec.cs">
//   
// </copyright>
// <summary>
//   The <see cref="FontSpec" /> class is a generic font class that maintains the font family,
//   attributes, colors, border and fill modes, font size, and angle information.
//   This class can render text with a variety of alignment options using the
//   <see cref="AlignH" /> and <see cref="AlignV" /> parameters in the
//   <see cref="Draw(Graphics,PaneBase,string,float,float,AlignH,AlignV,float)" /> method.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// The <see cref="FontSpec"/> class is a generic font class that maintains the font family,
    /// attributes, colors, border and fill modes, font size, and angle information.
    /// This class can render text with a variety of alignment options using the
    /// <see cref="AlignH"/> and <see cref="AlignV"/> parameters in the
    /// <see cref="Draw(Graphics,PaneBase,string,float,float,AlignH,AlignV,float)"/> method.
    /// </summary>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 3.24 $ $Date: 2007-01-25 07:56:08 $ </version>
    [Serializable]
    public class FontSpec : ICloneable, ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        // Change to 2 with addition of isDropShadow, dropShadowColor, dropShadowAngle, dropShadowOffset
        // changed to 10 with the version 5 refactor -- not backwards compatible
        public const int schema = 10;

        #endregion

        #region Fields

        /// <summary>
        /// Private field that determines the angle at which this
        /// <see cref="FontSpec"/> object is drawn.  Use the public property
        /// <see cref="Angle"/> to access this value.
        /// </summary>
        /// <value>The angle of the font, measured in anti-clockwise degrees from
        /// horizontal.  Negative values are permitted.</value>
        private float _angle;

        /// <summary>
        /// Private field that determines the properties of the border around the text.
        /// Use the public property <see cref="Border"/> to access this value.
        /// </summary>
        private Border _border;

        /// <summary>
        /// Private field that determines the offset angle of the dropshadow for this
        /// <see cref="FontSpec" />.
        /// Use the public property <see cref="DropShadowAngle" /> to access this value.
        /// </summary>
        private float _dropShadowAngle;

        /// <summary>
        /// Private field that determines the color of the dropshadow for this
        /// <see cref="FontSpec" />.
        /// Use the public property <see cref="DropShadowColor" /> to access this value.
        /// </summary>
        private Color _dropShadowColor;

        /// <summary>
        /// Private field that determines the offset distance of the dropshadow for this
        /// <see cref="FontSpec" />.
        /// Use the public property <see cref="DropShadowOffset" /> to access this value.
        /// </summary>
        private float _dropShadowOffset;

        /// <summary>
        /// Private field that stores the font family name for this <see cref="FontSpec"/>.
        /// Use the public property <see cref="Family"/> to access this value.
        /// </summary>
        /// <value>A text string with the font family name, e.g., "Arial"</value>
        private string _family;

        /// <summary>
        /// Private field that stores the <see cref="ZedGraph.Fill"/> data for this
        /// <see cref="FontSpec"/>.  Use the public property <see cref="Fill"/> to
        /// access this value.
        /// </summary>
        private Fill _fill;

        /// <summary>
        /// Private field that stores a reference to the <see cref="Font"/>
        /// object for this <see cref="FontSpec"/>.  This font object will be at
        /// the actual drawn size <see cref="_scaledSize"/> according to the current
        /// size of the <see cref="GraphPane"/>.  Use the public method
        /// <see cref="GetFont"/> to access this font object.
        /// </summary>
        /// <value>A reference to a <see cref="Font"/> object</value>
        private Font _font;

        /// <summary>
        /// Private field that stores the color of the font characters for this
        /// <see cref="FontSpec"/>.  Use the public property <see cref="FontColor"/>
        /// to access this value.
        /// </summary>
        /// <value>A system <see cref="System.Drawing.Color"/> reference.</value>
        private Color _fontColor;

        /// <summary>
        /// Private field that determines if the <see cref="FontSpec" /> will be
        /// displayed using anti-aliasing logic.
        /// Use the public property <see cref="IsAntiAlias" /> to access this value.
        /// </summary>
        private bool _isAntiAlias;

        /// <summary>
        /// Private field that determines whether this <see cref="FontSpec"/> is
        /// drawn with bold typeface.
        /// Use the public property <see cref="IsBold"/> to access this value.
        /// </summary>
        /// <value>A boolean value, true for bold, false for normal</value>
        private bool _isBold;

        /// <summary>
        /// Private field that determines if the <see cref="FontSpec" /> will be
        /// displayed with a drop shadow.
        /// Use the public property <see cref="IsDropShadow" /> to access this value.
        /// </summary>
        private bool _isDropShadow;

        /// <summary>
        /// Private field that determines whether this <see cref="FontSpec"/> is
        /// drawn with italic typeface.
        /// Use the public property <see cref="IsItalic"/> to access this value.
        /// </summary>
        /// <value>A boolean value, true for italic, false for normal</value>
        private bool _isItalic;

        /// <summary>
        /// Private field that determines whether this <see cref="FontSpec"/> is
        /// drawn with underlined typeface.
        /// Use the public property <see cref="IsUnderline"/> to access this value.
        /// </summary>
        /// <value>A boolean value, true for underline, false for normal</value>
        private bool _isUnderline;

        /// <summary>
        /// Private field that temporarily stores the scaled size of the font for this
        /// <see cref="FontSpec"/> object.  This represents the actual on-screen
        /// size, rather than the <see cref="Size"/> that represents the reference
        /// size for a "full-sized" <see cref="GraphPane"/>.
        /// </summary>
        /// <value>The size of the font, measured in points (1/72 inch).</value>
        private float _scaledSize;

        /// <summary>
        /// Private field that determines the size of the font for this
        /// <see cref="FontSpec"/> object.  Use the public property
        /// <see cref="Size"/> to access this value.
        /// </summary>
        /// <value>The size of the font, measured in points (1/72 inch).</value>
        private float _size;

        /// <summary>
        /// Private field that determines the alignment with which this
        /// <see cref="FontSpec"/> object is drawn.  This alignment really only
        /// affects multi-line strings.  Use the public property
        /// <see cref="StringAlignment"/> to access this value.
        /// </summary>
        /// <value>A <see cref="StringAlignment"/> enumeration.</value>
        private StringAlignment _stringAlignment;

        /// <summary>
        /// Private field that stores a reference to the <see cref="Font"/>
        /// object that will be used for superscripts.  This font object will be a
        /// fraction of the <see cref="FontSpec"/> <see cref="_scaledSize"/>,
        /// based on the value of <see cref="Default.SuperSize"/>.  This
        /// property is internal, and has no public access.
        /// </summary>
        /// <value>A reference to a <see cref="Font"/> object</value>
        private Font _superScriptFont;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FontSpec"/> class. 
        /// Construct a <see cref="FontSpec"/> object with default properties.
        /// </summary>
        public FontSpec()
            : this("Arial", 12, Color.Black, false, false, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FontSpec"/> class. 
        /// Construct a <see cref="FontSpec"/> object with the given properties.  All other properties
        /// are defaulted according to the values specified in the <see cref="Default"/>
        /// default class.
        /// </summary>
        /// <param name="family">
        /// A text string representing the font family
        /// (default is "Arial")
        /// </param>
        /// <param name="size">
        /// A size of the font in points.  This size will be scaled
        /// based on the ratio of the <see cref="PaneBase.Rect"/> dimension to the
        /// <see cref="PaneBase.BaseDimension"/> of the <see cref="GraphPane"/> object. 
        /// </param>
        /// <param name="color">
        /// The color with which to render the font
        /// </param>
        /// <param name="isBold">
        /// true for a bold typeface, false otherwise
        /// </param>
        /// <param name="isItalic">
        /// true for an italic typeface, false otherwise
        /// </param>
        /// <param name="isUnderline">
        /// true for an underlined font, false otherwise
        /// </param>
        public FontSpec(string family, float size, Color color, bool isBold, bool isItalic, bool isUnderline)
        {
            this.Init(
                family, 
                size, 
                color, 
                isBold, 
                isItalic, 
                isUnderline, 
                Default.FillColor, 
                Default.FillBrush, 
                Default.FillType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FontSpec"/> class. 
        /// Construct a <see cref="FontSpec"/> object with the given properties.  All other properties
        /// are defaulted according to the values specified in the <see cref="Default"/>
        /// default class.
        /// </summary>
        /// <param name="family">
        /// A text string representing the font family
        /// (default is "Arial")
        /// </param>
        /// <param name="size">
        /// A size of the font in points.  This size will be scaled
        /// based on the ratio of the <see cref="PaneBase.Rect"/> dimension to the
        /// <see cref="PaneBase.BaseDimension"/> of the <see cref="GraphPane"/> object. 
        /// </param>
        /// <param name="color">
        /// The color with which to render the font
        /// </param>
        /// <param name="isBold">
        /// true for a bold typeface, false otherwise
        /// </param>
        /// <param name="isItalic">
        /// true for an italic typeface, false otherwise
        /// </param>
        /// <param name="isUnderline">
        /// true for an underlined font, false otherwise
        /// </param>
        /// <param name="fillColor">
        /// The <see cref="Color"/> to use for filling in the text background
        /// </param>
        /// <param name="fillBrush">
        /// The <see cref="Brush"/> to use for filling in the text background
        /// </param>
        /// <param name="fillType">
        /// The <see cref="ZedGraph.FillType"/> to use for the
        /// text background
        /// </param>
        public FontSpec(
            string family, 
            float size, 
            Color color, 
            bool isBold, 
            bool isItalic, 
            bool isUnderline, 
            Color fillColor, 
            Brush fillBrush, 
            FillType fillType)
        {
            this.Init(family, size, color, isBold, isItalic, isUnderline, fillColor, fillBrush, fillType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FontSpec"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The FontSpec object from which to copy
        /// </param>
        public FontSpec(FontSpec rhs)
        {
            this._fontColor = rhs.FontColor;
            this._family = rhs.Family;
            this._isBold = rhs.IsBold;
            this._isItalic = rhs.IsItalic;
            this._isUnderline = rhs.IsUnderline;
            this._fill = rhs.Fill.Clone();
            this._border = rhs.Border.Clone();
            this._isAntiAlias = rhs._isAntiAlias;

            this._stringAlignment = rhs.StringAlignment;
            this._angle = rhs.Angle;
            this._size = rhs.Size;

            this._isDropShadow = rhs._isDropShadow;
            this._dropShadowColor = rhs._dropShadowColor;
            this._dropShadowAngle = rhs._dropShadowAngle;
            this._dropShadowOffset = rhs._dropShadowOffset;

            this._scaledSize = rhs._scaledSize;
            this.Remake(1.0F, this._size, ref this._scaledSize, ref this._font);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FontSpec"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected FontSpec(SerializationInfo info, StreamingContext context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema");

            this._fontColor = (Color)info.GetValue("fontColor", typeof(Color));
            this._family = info.GetString("family");
            this._isBold = info.GetBoolean("isBold");
            this._isItalic = info.GetBoolean("isItalic");
            this._isUnderline = info.GetBoolean("isUnderline");
            this._isAntiAlias = info.GetBoolean("isAntiAlias");

            this._fill = (Fill)info.GetValue("fill", typeof(Fill));
            this._border = (Border)info.GetValue("border", typeof(Border));
            this._angle = info.GetSingle("angle");
            this._stringAlignment = (StringAlignment)info.GetValue("stringAlignment", typeof(StringAlignment));
            this._size = info.GetSingle("size");

            this._isDropShadow = info.GetBoolean("isDropShadow");
            this._dropShadowColor = (Color)info.GetValue("dropShadowColor", typeof(Color));
            this._dropShadowAngle = info.GetSingle("dropShadowAngle");
            this._dropShadowOffset = info.GetSingle("dropShadowOffset");

            this._scaledSize = -1;
            this.Remake(1.0F, this._size, ref this._scaledSize, ref this._font);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The angle at which this <see cref="FontSpec"/> object is drawn.
        /// </summary>
        /// <value>The angle of the font, measured in anti-clockwise degrees from
        /// horizontal.  Negative values are permitted.</value>
        public float Angle
        {
            get
            {
                return this._angle;
            }

            set
            {
                this._angle = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Border"/> class used to draw the border border
        /// around this text.
        /// </summary>
        public Border Border
        {
            get
            {
                return this._border;
            }

            set
            {
                this._border = value;
            }
        }

        /// <summary>
        /// Gets or sets the offset angle of the drop shadow for this <see cref="FontSpec" />.
        /// </summary>
        /// <remarks>
        /// This value only applies if <see cref="IsDropShadow" /> is true.
        /// </remarks>
        /// <value>The angle, measured in anti-clockwise degrees from
        /// horizontal.  Negative values are permitted.</value>
        /// <seealso cref="IsDropShadow" />
        /// <seealso cref="DropShadowColor" />
        /// <seealso cref="DropShadowOffset" />
        public float DropShadowAngle
        {
            get
            {
                return this._dropShadowAngle;
            }

            set
            {
                this._dropShadowAngle = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the drop shadow for this <see cref="FontSpec" />.
        /// </summary>
        /// <remarks>
        /// This value only applies if <see cref="IsDropShadow" /> is true.
        /// </remarks>
        /// <seealso cref="IsDropShadow" />
        /// <seealso cref="DropShadowAngle" />
        /// <seealso cref="DropShadowOffset" />
        public Color DropShadowColor
        {
            get
            {
                return this._dropShadowColor;
            }

            set
            {
                this._dropShadowColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the offset distance of the drop shadow for this <see cref="FontSpec" />.
        /// </summary>
        /// <remarks>
        /// This value only applies if <see cref="IsDropShadow" /> is true.
        /// </remarks>
        /// <value>The offset distance, measured as a fraction of the scaled font height.</value>
        /// <seealso cref="IsDropShadow" />
        /// <seealso cref="DropShadowColor" />
        /// <seealso cref="DropShadowAngle" />
        public float DropShadowOffset
        {
            get
            {
                return this._dropShadowOffset;
            }

            set
            {
                this._dropShadowOffset = value;
            }
        }

        /// <summary>
        /// The font family name for this <see cref="FontSpec"/>.
        /// </summary>
        /// <value>A text string with the font family name, e.g., "Arial"</value>
        public string Family
        {
            get
            {
                return this._family;
            }

            set
            {
                if (value != this._family)
                {
                    this._family = value;
                    this.Remake(this._scaledSize / this._size, this.Size, ref this._scaledSize, ref this._font);
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ZedGraph.Fill"/> data for this
        /// <see cref="FontSpec"/>, which controls how the background
        /// behind the text is filled.
        /// </summary>
        public Fill Fill
        {
            get
            {
                return this._fill;
            }

            set
            {
                this._fill = value;
            }
        }

        /// <summary>
        /// The color of the font characters for this <see cref="FontSpec"/>.
        /// Note that the border and background
        /// colors are set using the <see cref="ZedGraph.LineBase.Color"/> and
        /// <see cref="ZedGraph.Fill.Color"/> properties, respectively.
        /// </summary>
        /// <value>A system <see cref="System.Drawing.Color"/> reference.</value>
        public Color FontColor
        {
            get
            {
                return this._fontColor;
            }

            set
            {
                this._fontColor = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that determines if the <see cref="FontSpec" /> will be
        /// drawn using anti-aliasing logic within GDI+.
        /// </summary>
        /// <remarks>
        /// If this property is set to true, it will override the current setting of
        /// <see cref="Graphics.SmoothingMode" /> by setting the value temporarily to
        /// <see cref="SmoothingMode.HighQuality" />.  If this property is set to false,
        /// the the current setting of <see cref="Graphics.SmoothingMode" /> will be
        /// left as-is.
        /// </remarks>
        public bool IsAntiAlias
        {
            get
            {
                return this._isAntiAlias;
            }

            set
            {
                this._isAntiAlias = value;
            }
        }

        /// <summary>
        /// Determines whether this <see cref="FontSpec"/> is
        /// drawn with bold typeface.
        /// </summary>
        /// <value>A boolean value, true for bold, false for normal</value>
        public bool IsBold
        {
            get
            {
                return this._isBold;
            }

            set
            {
                if (value != this._isBold)
                {
                    this._isBold = value;
                    this.Remake(this._scaledSize / this._size, this.Size, ref this._scaledSize, ref this._font);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that determines if the <see cref="FontSpec" /> will be
        /// displayed with a drop shadow.
        /// </summary>
        /// <seealso cref="DropShadowColor" />
        /// <seealso cref="DropShadowAngle" />
        /// <seealso cref="DropShadowOffset" />
        public bool IsDropShadow
        {
            get
            {
                return this._isDropShadow;
            }

            set
            {
                this._isDropShadow = value;
            }
        }

        /// <summary>
        /// Determines whether this <see cref="FontSpec"/> is
        /// drawn with italic typeface.
        /// </summary>
        /// <value>A boolean value, true for italic, false for normal</value>
        public bool IsItalic
        {
            get
            {
                return this._isItalic;
            }

            set
            {
                if (value != this._isItalic)
                {
                    this._isItalic = value;
                    this.Remake(this._scaledSize / this._size, this.Size, ref this._scaledSize, ref this._font);
                }
            }
        }

        /// <summary>
        /// Determines whether this <see cref="FontSpec"/> is
        /// drawn with underlined typeface.
        /// </summary>
        /// <value>A boolean value, true for underline, false for normal</value>
        public bool IsUnderline
        {
            get
            {
                return this._isUnderline;
            }

            set
            {
                if (value != this._isUnderline)
                {
                    this._isUnderline = value;
                    this.Remake(this._scaledSize / this._size, this.Size, ref this._scaledSize, ref this._font);
                }
            }
        }

        /// <summary>
        /// The size of the font for this <see cref="FontSpec"/> object.
        /// </summary>
        /// <value>The size of the font, measured in points (1/72 inch).</value>
        public float Size
        {
            get
            {
                return this._size;
            }

            set
            {
                if (value != this._size)
                {
                    this.Remake(this._scaledSize / this._size * value, this._size, ref this._scaledSize, ref this._font);
                    this._size = value;
                }
            }
        }

        /// <summary>
        /// Determines the alignment with which this
        /// <see cref="FontSpec"/> object is drawn.  This alignment really only
        /// affects multi-line strings.
        /// </summary>
        /// <value>A <see cref="StringAlignment"/> enumeration.</value>
        public StringAlignment StringAlignment
        {
            get
            {
                return this._stringAlignment;
            }

            set
            {
                this._stringAlignment = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Get a <see cref="SizeF"/> struct representing the width and height
        /// of the bounding box for the specified text string, based on the scaled font size.
        /// </summary>
        /// <remarks>
        /// This routine differs from <see cref="MeasureString(Graphics,string,float)"/> in that it takes into
        /// account the rotation angle of the font, and gives the dimensions of the
        /// bounding box that encloses the text at the specified angle.
        /// </remarks>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="text">
        /// The text string for which the width is to be calculated
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <returns>
        /// The scaled text dimensions, in pixels, in the form of
        /// a <see cref="SizeF"/> struct
        /// </returns>
        public SizeF BoundingBox(Graphics g, string text, float scaleFactor)
        {
            return this.BoundingBox(g, text, scaleFactor, new SizeF());
        }

        /// <summary>
        /// Get a <see cref="SizeF"/> struct representing the width and height
        /// of the bounding box for the specified text string, based on the scaled font size.
        /// </summary>
        /// <remarks>
        /// This routine differs from <see cref="MeasureString(Graphics,string,float)"/> in that it takes into
        /// account the rotation angle of the font, and gives the dimensions of the
        /// bounding box that encloses the text at the specified angle.
        /// </remarks>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="text">
        /// The text string for which the width is to be calculated
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <param name="layoutArea">
        /// The limiting area (<see cref="SizeF"/>) into which the text
        /// must fit.  The actual rectangle may be smaller than this, but the text will be wrapped
        /// to accomodate the area.
        /// </param>
        /// <returns>
        /// The scaled text dimensions, in pixels, in the form of
        /// a <see cref="SizeF"/> struct
        /// </returns>
        public SizeF BoundingBox(Graphics g, string text, float scaleFactor, SizeF layoutArea)
        {
            // Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
            SizeF s;
            if (layoutArea.IsEmpty)
            {
                s = this.MeasureString(g, text, scaleFactor);
            }
            else
            {
                s = this.MeasureString(g, text, scaleFactor, layoutArea);
            }

            float cs = (float)Math.Abs(Math.Cos(this._angle * Math.PI / 180.0));
            float sn = (float)Math.Abs(Math.Sin(this._angle * Math.PI / 180.0));

            SizeF s2 = new SizeF(s.Width * cs + s.Height * sn, s.Width * sn + s.Height * cs);

            return s2;
        }

        /// <summary>
        /// Get a <see cref="SizeF"/> struct representing the width and height
        /// of the bounding box for the specified text string, based on the scaled font size.
        /// </summary>
        /// <remarks>
        /// This special case method will show the specified string as a power of 10,
        /// superscripted and downsized according to the
        /// <see cref="Default.SuperSize"/> and <see cref="Default.SuperShift"/>.
        /// This routine differs from <see cref="MeasureString(Graphics,string,float)"/> in that it takes into
        /// account the rotation angle of the font, and gives the dimensions of the
        /// bounding box that encloses the text at the specified angle.
        /// </remarks>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="text">
        /// The text string for which the width is to be calculated
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <returns>
        /// The scaled text dimensions, in pixels, in the form of
        /// a <see cref="SizeF"/> struct
        /// </returns>
        public SizeF BoundingBoxTenPower(Graphics g, string text, float scaleFactor)
        {
            // Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
            float scaledSuperSize = this._scaledSize * Default.SuperSize;
            this.Remake(scaleFactor, this.Size * Default.SuperSize, ref scaledSuperSize, ref this._superScriptFont);

            // Get the width and height of the text
            SizeF size10 = this.MeasureString(g, "10", scaleFactor);
            SizeF sizeText = g.MeasureString(text, this._superScriptFont);

            if (this._isDropShadow)
            {
                sizeText.Width +=
                    (float)(Math.Cos(this._dropShadowAngle) * this._dropShadowOffset * this._superScriptFont.Height);
                sizeText.Height +=
                    (float)(Math.Sin(this._dropShadowAngle) * this._dropShadowOffset * this._superScriptFont.Height);
            }

            SizeF totSize = new SizeF(
                size10.Width + sizeText.Width, 
                size10.Height + sizeText.Height * Default.SuperShift);

            float cs = (float)Math.Abs(Math.Cos(this._angle * Math.PI / 180.0));
            float sn = (float)Math.Abs(Math.Sin(this._angle * Math.PI / 180.0));

            SizeF s2 = new SizeF(totSize.Width * cs + totSize.Height * sn, totSize.Width * sn + totSize.Height * cs);

            return s2;
        }

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public FontSpec Clone()
        {
            return new FontSpec(this);
        }

        /// <summary>
        /// Render the specified <paramref name="text"/> to the specifed
        /// <see cref="Graphics"/> device.  The text, border, and fill options
        /// will be rendered as required.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="PaneBase"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="text">
        /// A string value containing the text to be
        /// displayed.  This can be multiple lines, separated by newline ('\n')
        /// characters
        /// </param>
        /// <param name="x">
        /// The X location to display the text, in screen
        /// coordinates, relative to the horizontal (<see cref="AlignH"/>)
        /// alignment parameter <paramref name="alignH"/>
        /// </param>
        /// <param name="y">
        /// The Y location to display the text, in screen
        /// coordinates, relative to the vertical (<see cref="AlignV"/>
        /// alignment parameter <paramref name="alignV"/>
        /// </param>
        /// <param name="alignH">
        /// A horizontal alignment parameter specified
        /// using the <see cref="AlignH"/> enum type
        /// </param>
        /// <param name="alignV">
        /// A vertical alignment parameter specified
        /// using the <see cref="AlignV"/> enum type
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        public void Draw(
            Graphics g, 
            PaneBase pane, 
            string text, 
            float x, 
            float y, 
            AlignH alignH, 
            AlignV alignV, 
            float scaleFactor)
        {
            this.Draw(g, pane, text, x, y, alignH, alignV, scaleFactor, new SizeF());
        }

        /// <summary>
        /// Render the specified <paramref name="text"/> to the specifed
        /// <see cref="Graphics"/> device.  The text, border, and fill options
        /// will be rendered as required.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="PaneBase"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="text">
        /// A string value containing the text to be
        /// displayed.  This can be multiple lines, separated by newline ('\n')
        /// characters
        /// </param>
        /// <param name="x">
        /// The X location to display the text, in screen
        /// coordinates, relative to the horizontal (<see cref="AlignH"/>)
        /// alignment parameter <paramref name="alignH"/>
        /// </param>
        /// <param name="y">
        /// The Y location to display the text, in screen
        /// coordinates, relative to the vertical (<see cref="AlignV"/>
        /// alignment parameter <paramref name="alignV"/>
        /// </param>
        /// <param name="alignH">
        /// A horizontal alignment parameter specified
        /// using the <see cref="AlignH"/> enum type
        /// </param>
        /// <param name="alignV">
        /// A vertical alignment parameter specified
        /// using the <see cref="AlignV"/> enum type
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <param name="layoutArea">
        /// The limiting area (<see cref="SizeF"/>) into which the text
        /// must fit.  The actual rectangle may be smaller than this, but the text will be wrapped
        /// to accomodate the area.
        /// </param>
        public void Draw(
            Graphics g, 
            PaneBase pane, 
            string text, 
            float x, 
            float y, 
            AlignH alignH, 
            AlignV alignV, 
            float scaleFactor, 
            SizeF layoutArea)
        {
            // make sure the font size is properly scaled
            // Remake( scaleFactor, this.Size, ref this.scaledSize, ref this.font );
            SmoothingMode sModeSave = g.SmoothingMode;
            TextRenderingHint sHintSave = g.TextRenderingHint;
            if (this._isAntiAlias)
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
            }

            SizeF sizeF;
            if (layoutArea.IsEmpty)
            {
                sizeF = this.MeasureString(g, text, scaleFactor);
            }
            else
            {
                sizeF = this.MeasureString(g, text, scaleFactor, layoutArea);
            }

            // Save the old transform matrix for later restoration
            Matrix saveMatrix = g.Transform;
            g.Transform = this.SetupMatrix(g.Transform, x, y, sizeF, alignH, alignV, this._angle);

            // Create a rectangle representing the border around the
            // text.  Note that, while the text is drawn based on the
            // TopCenter position, the rectangle is drawn based on
            // the TopLeft position.  Therefore, move the rectangle
            // width/2 to the left to align it properly
            RectangleF rectF = new RectangleF(-sizeF.Width / 2.0F, 0.0F, sizeF.Width, sizeF.Height);

            // If the background is to be filled, fill it
            this._fill.Draw(g, rectF);

            // Draw the border around the text if required
            this._border.Draw(g, pane, scaleFactor, rectF);

            // make a center justified StringFormat alignment
            // for drawing the text
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = this._stringAlignment;

            // 			if ( this.stringAlignment == StringAlignment.Far )
            // 				g.TranslateTransform( sizeF.Width / 2.0F, 0F, MatrixOrder.Prepend );
            // 			else if ( this.stringAlignment == StringAlignment.Near )
            // 				g.TranslateTransform( -sizeF.Width / 2.0F, 0F, MatrixOrder.Prepend );

            // Draw the drop shadow text.  Note that the coordinate system
            // is set up such that 0,0 is at the location where the
            // CenterTop of the text needs to be.
            if (this._isDropShadow)
            {
                float xShift = (float)(Math.Cos(this._dropShadowAngle) * this._dropShadowOffset * this._font.Height);
                float yShift = (float)(Math.Sin(this._dropShadowAngle) * this._dropShadowOffset * this._font.Height);
                RectangleF rectD = rectF;
                rectD.Offset(xShift, yShift);

                // make a solid brush for rendering the font itself
                using (SolidBrush brushD = new SolidBrush(this._dropShadowColor)) g.DrawString(text, this._font, brushD, rectD, strFormat);
            }

            // make a solid brush for rendering the font itself
            using (SolidBrush brush = new SolidBrush(this._fontColor))
            {
                // Draw the actual text.  Note that the coordinate system
                // is set up such that 0,0 is at the location where the
                // CenterTop of the text needs to be.
                // RectangleF layoutArea = new RectangleF( 0.0F, 0.0F, sizeF.Width, sizeF.Height );
                g.DrawString(text, this._font, brush, rectF, strFormat);
            }

            // Restore the transform matrix back to original
            g.Transform = saveMatrix;

            g.SmoothingMode = sModeSave;
            g.TextRenderingHint = sHintSave;
        }

        /// <summary>
        /// Render the specified <paramref name="text"/> to the specifed
        /// <see cref="Graphics"/> device.  The text, border, and fill options
        /// will be rendered as required.  This special case method will show the
        /// specified text as a power of 10, using the <see cref="Default.SuperSize"/>
        /// and <see cref="Default.SuperShift"/>.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="text">
        /// A string value containing the text to be
        /// displayed.  This can be multiple lines, separated by newline ('\n')
        /// characters
        /// </param>
        /// <param name="x">
        /// The X location to display the text, in screen
        /// coordinates, relative to the horizontal (<see cref="AlignH"/>)
        /// alignment parameter <paramref name="alignH"/>
        /// </param>
        /// <param name="y">
        /// The Y location to display the text, in screen
        /// coordinates, relative to the vertical (<see cref="AlignV"/>
        /// alignment parameter <paramref name="alignV"/>
        /// </param>
        /// <param name="alignH">
        /// A horizontal alignment parameter specified
        /// using the <see cref="AlignH"/> enum type
        /// </param>
        /// <param name="alignV">
        /// A vertical alignment parameter specified
        /// using the <see cref="AlignV"/> enum type
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        public void DrawTenPower(
            Graphics g, 
            GraphPane pane, 
            string text, 
            float x, 
            float y, 
            AlignH alignH, 
            AlignV alignV, 
            float scaleFactor)
        {
            SmoothingMode sModeSave = g.SmoothingMode;
            TextRenderingHint sHintSave = g.TextRenderingHint;
            if (this._isAntiAlias)
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
            }

            // make sure the font size is properly scaled
            this.Remake(scaleFactor, this.Size, ref this._scaledSize, ref this._font);
            float scaledSuperSize = this._scaledSize * Default.SuperSize;
            this.Remake(scaleFactor, this.Size * Default.SuperSize, ref scaledSuperSize, ref this._superScriptFont);

            // Get the width and height of the text
            SizeF size10 = g.MeasureString("10", this._font);
            SizeF sizeText = g.MeasureString(text, this._superScriptFont);
            SizeF totSize = new SizeF(
                size10.Width + sizeText.Width, 
                size10.Height + sizeText.Height * Default.SuperShift);
            float charWidth = g.MeasureString("x", this._superScriptFont).Width;

            // Save the old transform matrix for later restoration
            Matrix saveMatrix = g.Transform;

            g.Transform = this.SetupMatrix(g.Transform, x, y, totSize, alignH, alignV, this._angle);

            // make a center justified StringFormat alignment
            // for drawing the text
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = this._stringAlignment;

            // Create a rectangle representing the border around the
            // text.  Note that, while the text is drawn based on the
            // TopCenter position, the rectangle is drawn based on
            // the TopLeft position.  Therefore, move the rectangle
            // width/2 to the left to align it properly
            RectangleF rectF = new RectangleF(-totSize.Width / 2.0F, 0.0F, totSize.Width, totSize.Height);

            // If the background is to be filled, fill it
            this._fill.Draw(g, rectF);

            // Draw the border around the text if required
            this._border.Draw(g, pane, scaleFactor, rectF);

            // make a solid brush for rendering the font itself
            using (SolidBrush brush = new SolidBrush(this._fontColor))
            {
                // Draw the actual text.  Note that the coordinate system
                // is set up such that 0,0 is at the location where the
                // CenterTop of the text needs to be.
                g.DrawString(
                    "10", 
                    this._font, 
                    brush, 
                    (-totSize.Width + size10.Width) / 2.0F, 
                    sizeText.Height * Default.SuperShift, 
                    strFormat);
                g.DrawString(
                    text, 
                    this._superScriptFont, 
                    brush, 
                    (totSize.Width - sizeText.Width - charWidth) / 2.0F, 
                    0.0F, 
                    strFormat);
            }

            // Restore the transform matrix back to original
            g.Transform = saveMatrix;

            g.SmoothingMode = sModeSave;
            g.TextRenderingHint = sHintSave;
        }

        /// <summary>
        /// Returns a polygon that defines the bounding box of
        /// the text, taking into account alignment and rotation parameters.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="text">
        /// A string value containing the text to be
        /// displayed.  This can be multiple lines, separated by newline ('\n')
        /// characters
        /// </param>
        /// <param name="x">
        /// The X location to display the text, in screen
        /// coordinates, relative to the horizontal (<see cref="AlignH"/>)
        /// alignment parameter <paramref name="alignH"/>
        /// </param>
        /// <param name="y">
        /// The Y location to display the text, in screen
        /// coordinates, relative to the vertical (<see cref="AlignV"/>
        /// alignment parameter <paramref name="alignV"/>
        /// </param>
        /// <param name="alignH">
        /// A horizontal alignment parameter specified
        /// using the <see cref="AlignH"/> enum type
        /// </param>
        /// <param name="alignV">
        /// A vertical alignment parameter specified
        /// using the <see cref="AlignV"/> enum type
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <param name="layoutArea">
        /// The limiting area (<see cref="SizeF"/>) into which the text
        /// must fit.  The actual rectangle may be smaller than this, but the text will be wrapped
        /// to accomodate the area.
        /// </param>
        /// <returns>
        /// A polygon of 4 points defining the area of this text
        /// </returns>
        public PointF[] GetBox(
            Graphics g, 
            string text, 
            float x, 
            float y, 
            AlignH alignH, 
            AlignV alignV, 
            float scaleFactor, 
            SizeF layoutArea)
        {
            // make sure the font size is properly scaled
            this.Remake(scaleFactor, this.Size, ref this._scaledSize, ref this._font);

            // Get the width and height of the text
            SizeF sizeF;
            if (layoutArea.IsEmpty)
            {
                sizeF = g.MeasureString(text, this._font);
            }
            else
            {
                sizeF = g.MeasureString(text, this._font, layoutArea);
            }

            // Create a bounding box rectangle for the text
            RectangleF rect = new RectangleF(new PointF(-sizeF.Width / 2.0F, 0.0F), sizeF);

            Matrix matrix = new Matrix();
            this.SetupMatrix(matrix, x, y, sizeF, alignH, alignV, this._angle);

            PointF[] pts = new PointF[4];
            pts[0] = new PointF(rect.Left, rect.Top);
            pts[1] = new PointF(rect.Right, rect.Top);
            pts[2] = new PointF(rect.Right, rect.Bottom);
            pts[3] = new PointF(rect.Left, rect.Bottom);
            matrix.TransformPoints(pts);

            return pts;
        }

        /// <summary>
        /// Get the <see cref="Font"/> class for the current scaled font.
        /// </summary>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <returns>
        /// Returns a reference to a <see cref="Font"/> object
        /// with a size of <see cref="_scaledSize"/>, and font <see cref="Family"/>.
        /// </returns>
        public Font GetFont(float scaleFactor)
        {
            this.Remake(scaleFactor, this.Size, ref this._scaledSize, ref this._font);
            return this._font;
        }

        /// <summary>
        /// Get the height of the scaled font
        /// </summary>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <returns>
        /// The scaled font height, in pixels
        /// </returns>
        public float GetHeight(float scaleFactor)
        {
            this.Remake(scaleFactor, this.Size, ref this._scaledSize, ref this._font);
            float height = this._font.Height;
            if (this._isDropShadow)
            {
                height += (float)(Math.Sin(this._dropShadowAngle) * this._dropShadowOffset * this._font.Height);
            }

            return height;
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
            info.AddValue("fontColor", this._fontColor);
            info.AddValue("family", this._family);
            info.AddValue("isBold", this._isBold);
            info.AddValue("isItalic", this._isItalic);
            info.AddValue("isUnderline", this._isUnderline);
            info.AddValue("isAntiAlias", this._isAntiAlias);

            info.AddValue("fill", this._fill);
            info.AddValue("border", this._border);
            info.AddValue("angle", this._angle);
            info.AddValue("stringAlignment", this._stringAlignment);
            info.AddValue("size", this._size);

            info.AddValue("isDropShadow", this._isDropShadow);
            info.AddValue("dropShadowColor", this._dropShadowColor);
            info.AddValue("dropShadowAngle", this._dropShadowAngle);
            info.AddValue("dropShadowOffset", this._dropShadowOffset);
        }

        /// <summary>
        /// Get the average character width of the scaled font.  The average width is
        /// based on the character 'x'
        /// </summary>
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
        /// The scaled font width, in pixels
        /// </returns>
        public float GetWidth(Graphics g, float scaleFactor)
        {
            this.Remake(scaleFactor, this.Size, ref this._scaledSize, ref this._font);
            return g.MeasureString("x", this._font).Width;
        }

        /// <summary>
        /// Get the total width of the specified text string
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="text">
        /// The text string for which the width is to be calculated
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <returns>
        /// The scaled text width, in pixels
        /// </returns>
        public float GetWidth(Graphics g, string text, float scaleFactor)
        {
            this.Remake(scaleFactor, this.Size, ref this._scaledSize, ref this._font);
            float width = g.MeasureString(text, this._font).Width;
            if (this._isDropShadow)
            {
                width += (float)(Math.Cos(this._dropShadowAngle) * this._dropShadowOffset * this._font.Height);
            }

            return width;
        }

        /// <summary>
        /// Get a <see cref="SizeF"/> struct representing the width and height
        /// of the specified text string, based on the scaled font size
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="text">
        /// The text string for which the width is to be calculated
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <returns>
        /// The scaled text dimensions, in pixels, in the form of
        /// a <see cref="SizeF"/> struct
        /// </returns>
        public SizeF MeasureString(Graphics g, string text, float scaleFactor)
        {
            this.Remake(scaleFactor, this.Size, ref this._scaledSize, ref this._font);
            SizeF size = g.MeasureString(text, this._font);
            if (this._isDropShadow)
            {
                size.Width += (float)(Math.Cos(this._dropShadowAngle) * this._dropShadowOffset * this._font.Height);
                size.Height += (float)(Math.Sin(this._dropShadowAngle) * this._dropShadowOffset * this._font.Height);
            }

            return size;
        }

        /// <summary>
        /// Get a <see cref="SizeF"/> struct representing the width and height
        /// of the specified text string, based on the scaled font size, and using
        /// the specified <see cref="SizeF"/> as an outer limit.
        /// </summary>
        /// <remarks>
        /// This method will allow the text to wrap as necessary to fit the 
        /// <see paramref="layoutArea"/>.
        /// </remarks>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="text">
        /// The text string for which the width is to be calculated
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <param name="layoutArea">
        /// The limiting area (<see cref="SizeF"/>) into which the text
        /// must fit.  The actual rectangle may be smaller than this, but the text will be wrapped
        /// to accomodate the area.
        /// </param>
        /// <returns>
        /// The scaled text dimensions, in pixels, in the form of
        /// a <see cref="SizeF"/> struct
        /// </returns>
        public SizeF MeasureString(Graphics g, string text, float scaleFactor, SizeF layoutArea)
        {
            this.Remake(scaleFactor, this.Size, ref this._scaledSize, ref this._font);
            SizeF size = g.MeasureString(text, this._font, layoutArea);
            if (this._isDropShadow)
            {
                size.Width += (float)(Math.Cos(this._dropShadowAngle) * this._dropShadowOffset * this._font.Height);
                size.Height += (float)(Math.Sin(this._dropShadowAngle) * this._dropShadowOffset * this._font.Height);
            }

            return size;
        }

        /// <summary>
        /// Determines if the specified screen point lies within the bounding box of
        /// the text, taking into account alignment and rotation parameters.
        /// </summary>
        /// <param name="pt">
        /// The screen point, in pixel units
        /// </param>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="text">
        /// A string value containing the text to be
        /// displayed.  This can be multiple lines, separated by newline ('\n')
        /// characters
        /// </param>
        /// <param name="x">
        /// The X location to display the text, in screen
        /// coordinates, relative to the horizontal (<see cref="AlignH"/>)
        /// alignment parameter <paramref name="alignH"/>
        /// </param>
        /// <param name="y">
        /// The Y location to display the text, in screen
        /// coordinates, relative to the vertical (<see cref="AlignV"/>
        /// alignment parameter <paramref name="alignV"/>
        /// </param>
        /// <param name="alignH">
        /// A horizontal alignment parameter specified
        /// using the <see cref="AlignH"/> enum type
        /// </param>
        /// <param name="alignV">
        /// A vertical alignment parameter specified
        /// using the <see cref="AlignV"/> enum type
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <returns>
        /// true if the point lies within the bounding box, false otherwise
        /// </returns>
        public bool PointInBox(
            PointF pt, 
            Graphics g, 
            string text, 
            float x, 
            float y, 
            AlignH alignH, 
            AlignV alignV, 
            float scaleFactor)
        {
            return this.PointInBox(pt, g, text, x, y, alignH, alignV, scaleFactor, new SizeF());
        }

        /// <summary>
        /// Determines if the specified screen point lies within the bounding box of
        /// the text, taking into account alignment and rotation parameters.
        /// </summary>
        /// <param name="pt">
        /// The screen point, in pixel units
        /// </param>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="text">
        /// A string value containing the text to be
        /// displayed.  This can be multiple lines, separated by newline ('\n')
        /// characters
        /// </param>
        /// <param name="x">
        /// The X location to display the text, in screen
        /// coordinates, relative to the horizontal (<see cref="AlignH"/>)
        /// alignment parameter <paramref name="alignH"/>
        /// </param>
        /// <param name="y">
        /// The Y location to display the text, in screen
        /// coordinates, relative to the vertical (<see cref="AlignV"/>
        /// alignment parameter <paramref name="alignV"/>
        /// </param>
        /// <param name="alignH">
        /// A horizontal alignment parameter specified
        /// using the <see cref="AlignH"/> enum type
        /// </param>
        /// <param name="alignV">
        /// A vertical alignment parameter specified
        /// using the <see cref="AlignV"/> enum type
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <param name="layoutArea">
        /// The limiting area (<see cref="SizeF"/>) into which the text
        /// must fit.  The actual rectangle may be smaller than this, but the text will be wrapped
        /// to accomodate the area.
        /// </param>
        /// <returns>
        /// true if the point lies within the bounding box, false otherwise
        /// </returns>
        public bool PointInBox(
            PointF pt, 
            Graphics g, 
            string text, 
            float x, 
            float y, 
            AlignH alignH, 
            AlignV alignV, 
            float scaleFactor, 
            SizeF layoutArea)
        {
            // make sure the font size is properly scaled
            this.Remake(scaleFactor, this.Size, ref this._scaledSize, ref this._font);

            // Get the width and height of the text
            SizeF sizeF;
            if (layoutArea.IsEmpty)
            {
                sizeF = g.MeasureString(text, this._font);
            }
            else
            {
                sizeF = g.MeasureString(text, this._font, layoutArea);
            }

            // Create a bounding box rectangle for the text
            RectangleF rect = new RectangleF(new PointF(-sizeF.Width / 2.0F, 0.0F), sizeF);

            // Build a transform matrix that inverts that drawing transform
            // in this manner, the point is brought back to the box, rather
            // than vice-versa.  This allows the container check to be a simple
            // RectangleF.Contains, since the rectangle won't be rotated.
            Matrix matrix = this.GetMatrix(x, y, sizeF, alignH, alignV, this._angle);

            PointF[] pts = new PointF[1];
            pts[0] = pt;
            matrix.TransformPoints(pts);

            return rect.Contains(pts[0]);
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
        /// The get matrix.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <param name="sizeF">
        /// The size f.
        /// </param>
        /// <param name="alignH">
        /// The align h.
        /// </param>
        /// <param name="alignV">
        /// The align v.
        /// </param>
        /// <param name="angle">
        /// The angle.
        /// </param>
        /// <returns>
        /// The <see cref="Matrix"/>.
        /// </returns>
        private Matrix GetMatrix(float x, float y, SizeF sizeF, AlignH alignH, AlignV alignV, float angle)
        {
            // Build a transform matrix that inverts that drawing transform
            // in this manner, the point is brought back to the box, rather
            // than vice-versa.  This allows the container check to be a simple
            // RectangleF.Contains, since the rectangle won't be rotated.
            Matrix matrix = new Matrix();

            // In this case, the bounding box is anchored to the
            // top-left of the text box.  Handle the alignment
            // as needed.
            float xa, ya;
            if (alignH == AlignH.Left)
            {
                xa = sizeF.Width / 2.0F;
            }
            else if (alignH == AlignH.Right)
            {
                xa = -sizeF.Width / 2.0F;
            }
            else
            {
                xa = 0.0F;
            }

            if (alignV == AlignV.Center)
            {
                ya = -sizeF.Height / 2.0F;
            }
            else if (alignV == AlignV.Bottom)
            {
                ya = -sizeF.Height;
            }
            else
            {
                ya = 0.0F;
            }

            // Shift the coordinates to accomodate the alignment
            // parameters
            matrix.Translate(-xa, -ya, MatrixOrder.Prepend);

            // Rotate the coordinate system according to the 
            // specified angle of the FontSpec
            if (angle != 0.0F)
            {
                matrix.Rotate(angle, MatrixOrder.Prepend);
            }

            // Move the coordinate system to local coordinates
            // of this text object (that is, at the specified
            // x,y location)
            matrix.Translate(-x, -y, MatrixOrder.Prepend);

            return matrix;
        }

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="family">
        /// The family.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <param name="isBold">
        /// The is bold.
        /// </param>
        /// <param name="isItalic">
        /// The is italic.
        /// </param>
        /// <param name="isUnderline">
        /// The is underline.
        /// </param>
        /// <param name="fillColor">
        /// The fill color.
        /// </param>
        /// <param name="fillBrush">
        /// The fill brush.
        /// </param>
        /// <param name="fillType">
        /// The fill type.
        /// </param>
        private void Init(
            string family, 
            float size, 
            Color color, 
            bool isBold, 
            bool isItalic, 
            bool isUnderline, 
            Color fillColor, 
            Brush fillBrush, 
            FillType fillType)
        {
            this._fontColor = color;
            this._family = family;
            this._isBold = isBold;
            this._isItalic = isItalic;
            this._isUnderline = isUnderline;
            this._size = size;
            this._angle = 0F;

            this._isAntiAlias = Default.IsAntiAlias;
            this._stringAlignment = Default.StringAlignment;
            this._isDropShadow = Default.IsDropShadow;
            this._dropShadowColor = Default.DropShadowColor;
            this._dropShadowAngle = Default.DropShadowAngle;
            this._dropShadowOffset = Default.DropShadowOffset;

            this._fill = new Fill(fillColor, fillBrush, fillType);
            this._border = new Border(true, Color.Black, 1.0F);

            this._scaledSize = -1;
            this.Remake(1.0F, this._size, ref this._scaledSize, ref this._font);
        }

        /// <summary>
        /// Recreate the font based on a new scaled size.  The font
        /// will only be recreated if the scaled size has changed by
        /// at least 0.1 points.
        /// </summary>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <param name="size">
        /// The unscaled size of the font, in points
        /// </param>
        /// <param name="scaledSize">
        /// The scaled size of the font, in points
        /// </param>
        /// <param name="font">
        /// A reference to the <see cref="Font"/> object
        /// </param>
        private void Remake(float scaleFactor, float size, ref float scaledSize, ref Font font)
        {
            float newSize = size * scaleFactor;

            float oldSize = (font == null) ? 0.0f : font.Size;

            // Regenerate the font only if the size has changed significantly
            if (font == null || Math.Abs(newSize - oldSize) > 0.1 || font.Name != this.Family
                || font.Bold != this._isBold || font.Italic != this._isItalic || font.Underline != this._isUnderline)
            {
                FontStyle style = FontStyle.Regular;
                style = (this._isBold ? FontStyle.Bold : style) | (this._isItalic ? FontStyle.Italic : style)
                        | (this._isUnderline ? FontStyle.Underline : style);

                scaledSize = size * (float)scaleFactor;
                font = new Font(this._family, scaledSize, style, GraphicsUnit.World);
            }
        }

        /// <summary>
        /// The setup matrix.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <param name="sizeF">
        /// The size f.
        /// </param>
        /// <param name="alignH">
        /// The align h.
        /// </param>
        /// <param name="alignV">
        /// The align v.
        /// </param>
        /// <param name="angle">
        /// The angle.
        /// </param>
        /// <returns>
        /// The <see cref="Matrix"/>.
        /// </returns>
        private Matrix SetupMatrix(
            Matrix matrix, 
            float x, 
            float y, 
            SizeF sizeF, 
            AlignH alignH, 
            AlignV alignV, 
            float angle)
        {
            // Move the coordinate system to local coordinates
            // of this text object (that is, at the specified
            // x,y location)
            matrix.Translate(x, y, MatrixOrder.Prepend);

            // Rotate the coordinate system according to the 
            // specified angle of the FontSpec
            if (this._angle != 0.0F)
            {
                matrix.Rotate(-angle, MatrixOrder.Prepend);
            }

            // Since the text will be drawn by g.DrawString()
            // assuming the location is the TopCenter
            // (the Font is aligned using StringFormat to the
            // center so multi-line text is center justified),
            // shift the coordinate system so that we are
            // actually aligned per the caller specified position
            float xa, ya;
            if (alignH == AlignH.Left)
            {
                xa = sizeF.Width / 2.0F;
            }
            else if (alignH == AlignH.Right)
            {
                xa = -sizeF.Width / 2.0F;
            }
            else
            {
                xa = 0.0F;
            }

            if (alignV == AlignV.Center)
            {
                ya = -sizeF.Height / 2.0F;
            }
            else if (alignV == AlignV.Bottom)
            {
                ya = -sizeF.Height;
            }
            else
            {
                ya = 0.0F;
            }

            // Shift the coordinates to accomodate the alignment
            // parameters
            matrix.Translate(xa, ya, MatrixOrder.Prepend);

            return matrix;
        }

        #endregion

        /// <summary>
        /// A simple struct that defines the
        /// default property values for the <see cref="FontSpec"/> class.
        /// </summary>
        public struct Default
        {
            #region Static Fields

            /// <summary>
            /// Default value for <see cref="FontSpec.DropShadowAngle"/>, which determines
            /// the offset angle of the drop shadow for this <see cref="FontSpec" />.
            /// </summary>
            public static float DropShadowAngle = 45f;

            /// <summary>
            /// Default value for <see cref="FontSpec.DropShadowColor"/>, which determines
            /// the color of the drop shadow for this <see cref="FontSpec" />.
            /// </summary>
            public static Color DropShadowColor = Color.Black;

            /// <summary>
            /// Default value for <see cref="FontSpec.DropShadowOffset"/>, which determines
            /// the offset distance of the drop shadow for this <see cref="FontSpec" />.
            /// </summary>
            public static float DropShadowOffset = 0.05f;

            /// <summary>
            /// The default custom brush for filling in this <see cref="FontSpec"/>
            /// (<see cref="ZedGraph.Fill.Brush"/> property).
            /// </summary>
            public static Brush FillBrush = null;

            /// <summary>
            /// The default color for filling in the background of the text block
            /// (<see cref="ZedGraph.Fill.Color"/> property).
            /// </summary>
            public static Color FillColor = Color.White;

            /// <summary>
            /// The default fill mode for this <see cref="FontSpec"/>
            /// (<see cref="ZedGraph.Fill.Type"/> property).
            /// </summary>
            public static FillType FillType = FillType.Solid;

            /// <summary>
            /// Default value for <see cref="FontSpec.IsAntiAlias"/>, which determines
            /// if anti-aliasing logic is used for this <see cref="FontSpec" />.
            /// </summary>
            public static bool IsAntiAlias = false;

            /// <summary>
            /// Default value for <see cref="FontSpec.IsDropShadow"/>, which determines
            /// if the drop shadow is displayed for this <see cref="FontSpec" />.
            /// </summary>
            public static bool IsDropShadow = false;

            /// <summary>
            /// Default value for the alignment with which this
            /// <see cref="FontSpec"/> object is drawn.  This alignment really only
            /// affects multi-line strings.
            /// </summary>
            /// <value>A <see cref="StringAlignment"/> enumeration.</value>
            public static StringAlignment StringAlignment = StringAlignment.Center;

            /// <summary>
            /// The default shift fraction of the superscript, expressed as a
            /// fraction of the superscripted character height.  This is the height
            /// above the main font (a zero shift means the main font and the superscript
            /// font have the tops aligned).
            /// </summary>
            public static float SuperShift = 0.4F;

            /// <summary>
            /// The default size fraction of the superscript font, expressed as a fraction
            /// of the size of the main font.
            /// </summary>
            public static float SuperSize = 0.85F;

            #endregion
        }
    }
}