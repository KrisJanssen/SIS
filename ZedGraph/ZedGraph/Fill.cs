//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright � 2004  John Champion
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//=============================================================================

namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
	/// A class that encapsulates color-fill properties for an object.  The <see cref="Fill"/> class
	/// is used in <see cref="PaneBase.Rect"/>, <see cref="Chart.Rect"/>, <see cref="Legend"/>,
	/// <see cref="Bar"/>, and <see cref="Line"/> objects.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.22 $ $Date: 2007-01-26 09:01:49 $ </version>
	[Serializable]
	public class Fill : ISerializable, ICloneable
	{
	#region Fields

		/// <summary>
		/// Private field that stores the fill color.  Use the public
		/// property <see cref="Color"/> to access this value.  This property is
		/// only applicable if the <see cref="Type"/> is not <see cref="ZedGraph.FillType.None"/>.
		/// </summary>
		private Color _color;
		/// <summary>
		/// Private field that stores the secondary color for gradientByValue fills.  Use the public
		/// property <see cref="SecondaryValueGradientColor"/> to access this value.  This property is
		/// only applicable if the <see cref="Type"/> is <see cref="ZedGraph.FillType.GradientByX"/>,
		/// <see cref="ZedGraph.FillType.GradientByY"/>, or <see cref="ZedGraph.FillType.GradientByZ"/>.
		/// </summary>
		private Color _secondaryValueGradientColor;
		/// <summary>
		/// Private field that stores the custom fill brush.  Use the public
		/// property <see cref="Brush"/> to access this value.  This property is
		/// only applicable if the 
		/// <see cref="Type"/> property is set to <see cref="ZedGraph.FillType.Brush"/>.
		/// </summary>
		protected Brush	_brush;
		/// <summary>
		/// Private field that determines the type of color fill.  Use the public
		/// property <see cref="Type"/> to access this value.  The fill color
		/// is determined by the property <see cref="Color"/> or
		/// <see cref="Brush"/>.
		/// </summary>
		private FillType	_type;
		/// <summary>
		/// Private field that determines if the brush will be scaled to the bounding box
		/// of the filled object.  If this value is false, then the brush will only be aligned
		/// with the filled object based on the <see cref="AlignH"/> and <see cref="AlignV"/>
		/// properties.
		/// </summary>
		private bool		_isScaled;
		/// <summary>
		/// Private field that determines how the brush will be aligned with the filled object
		/// in the horizontal direction.  This value is a <see cref="ZedGraph.AlignH"/> enumeration.
		/// This field only applies if <see cref="IsScaled"/> is false.
		/// properties.
		/// </summary>
		/// <seealso cref="AlignH"/>
		/// <seealso cref="AlignV"/>
		private AlignH		_alignH;
		/// <summary>
		/// Private field that determines how the brush will be aligned with the filled object
		/// in the vertical direction.  This value is a <see cref="ZedGraph.AlignV"/> enumeration.
		/// This field only applies if <see cref="IsScaled"/> is false.
		/// properties.
		/// </summary>
		/// <seealso cref="AlignH"/>
		/// <seealso cref="AlignV"/>
		private AlignV		_alignV;

		private double	_rangeMin;
		private double	_rangeMax;
		private double _rangeDefault;
		private Bitmap	_gradientBM;

		/// <summary>
		/// Private field that saves the image passed to the constructor.
		/// This is used strictly for serialization.
		/// </summary>
		private Image	_image;
		/// <summary>
		/// Private field that saves the image wrapmode passed to the constructor.
		/// This is used strictly for serialization.
		/// </summary>
		private WrapMode _wrapMode;
		/// <summary>
		/// Private field that saves the list of colors used to create the
		/// <see cref="LinearGradientBrush"/> in the constructor.  This is used strictly
		/// for serialization.
		/// </summary>
		private Color[] _colorList;
		/// <summary>
		/// Private field that saves the list of positions used to create the
		/// <see cref="LinearGradientBrush"/> in the constructor.  This is used strictly
		/// for serialization.
		/// </summary>
		private float[] _positionList;
		/// <summary>
		/// Private field the saves the angle of the fill.  This is used strictly for serialization.
		/// </summary>
		private float _angle;


	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="Fill"/> class.
		/// </summary>
		public struct Default
		{
			// Default Fill properties
			/// <summary>
			/// The default scaling mode for <see cref="Brush"/> fills.
			/// This is the default value for the <see cref="Fill.IsScaled"/> property.
			/// </summary>
			public static bool IsScaled = true;
			/// <summary>
			/// The default horizontal alignment for <see cref="Brush"/> fills.
			/// This is the default value for the <see cref="Fill.AlignH"/> property.
			/// </summary>
			public static AlignH AlignH = AlignH.Center;
			/// <summary>
			/// The default vertical alignment for <see cref="Brush"/> fills.
			/// This is the default value for the <see cref="Fill.AlignV"/> property.
			/// </summary>
			public static AlignV AlignV = AlignV.Center;
		}
	#endregion
	
	#region Constructors
		/// <summary>
		/// Generic initializer to default values
		/// </summary>
		private void Init()
		{
			this._color = Color.White;
			this._secondaryValueGradientColor = Color.White;
			this._brush = null;
			this._type = FillType.None;
			this._isScaled = Default.IsScaled;
			this._alignH = Default.AlignH;
			this._alignV = Default.AlignV;
			this._rangeMin = 0.0;
			this._rangeMax = 1.0;
			this._rangeDefault = double.MaxValue;
			this._gradientBM = null;

			this._colorList = null;
			this._positionList = null;
			this._angle = 0;
			this._image = null;
			this._wrapMode = WrapMode.Tile;

		}

		/// <summary>
		/// The default constructor.  Initialized to no fill.
		/// </summary>
		public Fill()
		{
			this.Init();
		}
		
		/// <summary>
		/// Constructor that specifies the color, brush, and type for this fill.
		/// </summary>
		/// <param name="color">The color of the fill for solid fills</param>
		/// <param name="brush">A custom brush for fills.  Can be a <see cref="SolidBrush"/>,
		/// <see cref="LinearGradientBrush"/>, or <see cref="TextureBrush"/>.</param>
		/// <param name="type">The <see cref="FillType"/> for this fill.</param>
		public Fill( Color color, Brush brush, FillType type )
		{
			this.Init();
			this._color = color;
			this._brush = brush;
			this._type = type;
		}
		
		/// <summary>
		/// Constructor that creates a solid color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Solid"/>, and setting <see cref="Color"/> to the
		/// specified color value.
		/// </summary>
		/// <param name="color">The color of the solid fill</param>
		public Fill( Color color )
		{
			this.Init();
			this._color = color;
			if ( color != Color.Empty )
				this._type = FillType.Solid;
		}
		
		/// <summary>
		/// Constructor that creates a linear gradient color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors and angle.
		/// </summary>
		/// <param name="color1">The first color for the gradient fill</param>
		/// <param name="color2">The second color for the gradient fill</param>
		/// <param name="angle">The angle (degrees) of the gradient fill</param>
		public Fill( Color color1, Color color2, float angle )
		{
			this.Init();
			this._color = color2;

			ColorBlend blend = new ColorBlend( 2 );
			blend.Colors[0] = color1;
			blend.Colors[1] = color2;
			blend.Positions[0] = 0.0f;
			blend.Positions[1] = 1.0f;
			this._type = FillType.Brush;

			this.CreateBrushFromBlend( blend, angle );
		}
		
		/// <summary>
		/// Constructor that creates a linear gradient color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.
		/// </summary>
		/// <param name="color1">The first color for the gradient fill</param>
		/// <param name="color2">The second color for the gradient fill</param>
		public Fill( Color color1, Color color2 ) : this( color1, color2, 0.0F )
		{
		}
		
		/// <summary>
		/// Constructor that creates a linear gradient color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of three colors.
		/// </summary>
		/// <param name="color1">The first color for the gradient fill</param>
		/// <param name="color2">The second color for the gradient fill</param>
		/// <param name="color3">The third color for the gradient fill</param>
		public Fill( Color color1, Color color2, Color color3 ) :
			this( color1, color2, color3, 0.0f )
		{
		}

		/// <summary>
		/// Constructor that creates a linear gradient color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of three colors
		/// </summary>
		/// <param name="color1">The first color for the gradient fill</param>
		/// <param name="color2">The second color for the gradient fill</param>
		/// <param name="color3">The third color for the gradient fill</param>
		/// <param name="angle">The angle (degrees) of the gradient fill</param>
		public Fill( Color color1, Color color2, Color color3, float angle )
		{
			this.Init();
			this._color = color3;

			ColorBlend blend = new ColorBlend( 3 );
			blend.Colors[0] = color1;
			blend.Colors[1] = color2;
			blend.Colors[2] = color3;
			blend.Positions[0] = 0.0f;
			blend.Positions[1] = 0.5f;
			blend.Positions[2] = 1.0f;
			this._type = FillType.Brush;
			
			this.CreateBrushFromBlend( blend, angle );
		}
		
		/// <summary>
		/// Constructor that creates a linear gradient multi-color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of many colors based on a <see cref="ColorBlend"/> object.  The gradient
		/// angle is defaulted to zero.
		/// </summary>
		/// <param name="blend">The <see cref="ColorBlend"/> object that defines the colors
		/// and positions along the gradient.</param>
		public Fill( ColorBlend blend ) :
			this( blend, 0.0F )
		{
		}

		/// <summary>
		/// Constructor that creates a linear gradient multi-color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of many colors based on a <see cref="ColorBlend"/> object, drawn at the
		/// specified angle (degrees).
		/// </summary>
		/// <param name="blend">The <see cref="ColorBlend"/> object that defines the colors
		/// and positions along the gradient.</param>
		/// <param name="angle">The angle (degrees) of the gradient fill</param>
		public Fill( ColorBlend blend, float angle )
		{
			this.Init();
			this._type = FillType.Brush;
			this.CreateBrushFromBlend( blend, angle );
		}

		/// <summary>
		/// Constructor that creates a linear gradient multi-color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of many colors based on an array of <see cref="Color"/> objects, drawn at an
		/// angle of zero (degrees).  The <see paramref="colors"/> array is used to create
		/// a <see cref="ColorBlend"/> object assuming a even linear distribution of the colors
		/// across the gradient.
		/// </summary>
		/// <param name="colors">The array of <see cref="Color"/> objects that defines the colors
		/// along the gradient.</param>
		public Fill( Color[] colors ) :
			this( colors, 0.0F )
		{
		}

		/// <summary>
		/// Constructor that creates a linear gradient multi-color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of many colors based on an array of <see cref="Color"/> objects, drawn at the
		/// specified angle (degrees).  The <see paramref="colors"/> array is used to create
		/// a <see cref="ColorBlend"/> object assuming a even linear distribution of the colors
		/// across the gradient.
		/// </summary>
		/// <param name="colors">The array of <see cref="Color"/> objects that defines the colors
		/// along the gradient.</param>
		/// <param name="angle">The angle (degrees) of the gradient fill</param>
		public Fill( Color[] colors, float angle )
		{
			this.Init();
			this._color = colors[ colors.Length - 1 ];

			ColorBlend blend = new ColorBlend();
			blend.Colors = colors;
			blend.Positions = new float[colors.Length];
			blend.Positions[0] = 0.0F;
			for ( int i=1; i<colors.Length; i++ )
				blend.Positions[i] = (float) i / (float)( colors.Length - 1 );
			this._type = FillType.Brush;

			this.CreateBrushFromBlend( blend, angle );
		}

		/// <summary>
		/// Constructor that creates a linear gradient multi-color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of many colors based on an array of <see cref="Color"/> objects, drawn at the
		/// an angle of zero (degrees).  The <see paramref="colors"/> array is used to create
		/// a <see cref="ColorBlend"/> object assuming a even linear distribution of the colors
		/// across the gradient.
		/// </summary>
		/// <param name="colors">The array of <see cref="Color"/> objects that defines the colors
		/// along the gradient.</param>
		/// <param name="positions">The array of floating point values that defines the color
		/// positions along the gradient.  Values should range from 0 to 1.</param>
		public Fill( Color[] colors, float[] positions ) :
			this( colors, positions, 0.0F )
		{
		}

		/// <summary>
		/// Constructor that creates a linear gradient multi-color-fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> using the specified colors.  This gradient fill
		/// consists of many colors based on an array of <see cref="Color"/> objects, drawn at the
		/// specified angle (degrees).  The <see paramref="colors"/> array is used to create
		/// a <see cref="ColorBlend"/> object assuming a even linear distribution of the colors
		/// across the gradient.
		/// </summary>
		/// <param name="colors">The array of <see cref="Color"/> objects that defines the colors
		/// along the gradient.</param>
		/// <param name="positions">The array of floating point values that defines the color
		/// positions along the gradient.  Values should range from 0 to 1.</param>
		/// <param name="angle">The angle (degrees) of the gradient fill</param>
		public Fill( Color[] colors, float[] positions, float angle )
		{
			this.Init();
			this._color = colors[ colors.Length - 1 ];

			ColorBlend blend = new ColorBlend();
			blend.Colors = colors;
			blend.Positions = positions;
			this._type = FillType.Brush;

			this.CreateBrushFromBlend( blend, angle );
		}

		/// <summary>
		/// Constructor that creates a texture fill, setting <see cref="Type"/> to
		/// <see cref="FillType.Brush"/> and using the specified image.
		/// </summary>
		/// <param name="image">The <see cref="Image"/> to use for filling</param>
		/// <param name="wrapMode">The <see cref="WrapMode"/> class that controls the image wrapping properties</param>
		public Fill( Image image, WrapMode wrapMode )
		{
			this.Init();
			this._color = Color.White;
			this._brush = new TextureBrush( image, wrapMode );
			this._type = FillType.Brush;
			this._image = image;
			this._wrapMode = wrapMode;
		}
		
		/// <summary>
		/// Constructor that creates a <see cref="Brush"/> fill, using a user-supplied, custom
		/// <see cref="Brush"/>.  The brush will be scaled to fit the destination screen object
		/// unless you manually change <see cref="IsScaled"/> to false;
		/// </summary>
		/// <param name="brush">The <see cref="Brush"/> to use for fancy fills.  Typically, this would
		/// be a <see cref="LinearGradientBrush"/> or a <see cref="TextureBrush"/> class</param>
		public Fill( Brush brush ) : this( brush, Default.IsScaled )
		{
		}
		
		/// <summary>
		/// Constructor that creates a <see cref="Brush"/> fill, using a user-supplied, custom
		/// <see cref="Brush"/>.  The brush will be scaled to fit the destination screen object
		/// according to the <see paramref="isScaled"/> parameter.
		/// </summary>
		/// <param name="brush">The <see cref="Brush"/> to use for fancy fills.  Typically, this would
		/// be a <see cref="LinearGradientBrush"/> or a <see cref="TextureBrush"/> class</param>
		/// <param name="isScaled">Determines if the brush will be scaled to fit the bounding box
		/// of the destination object.  true to scale it, false to leave it unscaled</param>
		public Fill( Brush brush, bool isScaled )
		{
			this.Init();
			this._isScaled = isScaled;
			this._color = Color.White;
			this._brush = (Brush) brush.Clone();
			this._type = FillType.Brush;
		}
		
		/// <summary>
		/// Constructor that creates a <see cref="Brush"/> fill, using a user-supplied, custom
		/// <see cref="Brush"/>.  This constructor will make the brush unscaled (see <see cref="IsScaled"/>),
		/// but it provides <see paramref="alignH"/> and <see paramref="alignV"/> parameters to control
		/// alignment of the brush with respect to the filled object.
		/// </summary>
		/// <param name="brush">The <see cref="Brush"/> to use for fancy fills.  Typically, this would
		/// be a <see cref="LinearGradientBrush"/> or a <see cref="TextureBrush"/> class</param>
		/// <param name="alignH">Controls the horizontal alignment of the brush within the filled object
		/// (see <see cref="AlignH"/></param>
		/// <param name="alignV">Controls the vertical alignment of the brush within the filled object
		/// (see <see cref="AlignV"/></param>
		public Fill( Brush brush, AlignH alignH, AlignV alignV )
		{
			this.Init();
			this._alignH = alignH;
			this._alignV = alignV;
			this._isScaled = false;
			this._color = Color.White;
			this._brush = (Brush) brush.Clone();
			this._type = FillType.Brush;
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The Fill object from which to copy</param>
		public Fill( Fill rhs )
		{
			this._color = rhs._color;
			this._secondaryValueGradientColor = rhs._color;

			if ( rhs._brush != null )
				this._brush = (Brush) rhs._brush.Clone();
			else
				this._brush = null;
			this._type = rhs._type;
			this._alignH = rhs.AlignH;
			this._alignV = rhs.AlignV;
            this._isScaled = rhs.IsScaled;
			this._rangeMin = rhs._rangeMin;
			this._rangeMax = rhs._rangeMax;
			this._rangeDefault = rhs._rangeDefault;
			this._gradientBM = null;

			if ( rhs._colorList != null )
				this._colorList = (Color[]) rhs._colorList.Clone();
			else
				this._colorList = null;

			if ( rhs._positionList != null )
			{
				this._positionList = (float[]) rhs._positionList.Clone();
			}
			else
				this._positionList = null;

			if ( rhs._image != null )
				this._image = (Image) rhs._image.Clone();
			else
				this._image = null;

			this._angle = rhs._angle;
			this._wrapMode = rhs._wrapMode;
		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of <see cref="Clone" />
		/// </summary>
		/// <returns>A deep copy of this object</returns>
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Typesafe, deep-copy clone method.
		/// </summary>
		/// <returns>A new, independent copy of this class</returns>
		public Fill Clone()
		{
			return new Fill( this );
		}

		private void CreateBrushFromBlend( ColorBlend blend, float angle )
		{
			this._angle = angle;

			this._colorList = (Color[]) blend.Colors.Clone();
			this._positionList = (float[]) blend.Positions.Clone();

			this._brush = new LinearGradientBrush( new Rectangle( 0, 0, 100, 100 ),
				Color.Red, Color.White, angle );
			((LinearGradientBrush)this._brush).InterpolationColors = blend;
		}

	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int schema = 10;
		// schema changed to 2 with addition of rangeDefault
		// schema changed to 10 with version 5 refactor -- not backwards compatible

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected Fill( SerializationInfo info, StreamingContext context )
		{
			this.Init();

			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema" );

			this._color = (Color) info.GetValue( "color", typeof(Color) );
			this._secondaryValueGradientColor = (Color) info.GetValue( "secondaryValueGradientColor", typeof( Color ) );
			//brush = (Brush) info.GetValue( "brush", typeof(Brush) );
			//brushHolder = (BrushHolder) info.GetValue( "brushHolder", typeof(BrushHolder) );
			this._type = (FillType) info.GetValue( "type", typeof(FillType) );
			this._isScaled = info.GetBoolean( "isScaled" );
			this._alignH = (AlignH) info.GetValue( "alignH", typeof(AlignH) );
			this._alignV = (AlignV) info.GetValue( "alignV", typeof(AlignV) );
			this._rangeMin = info.GetDouble( "rangeMin" );
			this._rangeMax = info.GetDouble( "rangeMax" );

			//BrushHolder brushHolder = (BrushHolder) info.GetValue( "brushHolder", typeof( BrushHolder ) );
			//brush = brush;

			this._colorList = (Color[]) info.GetValue( "colorList", typeof(Color[]) );
			this._positionList = (float[]) info.GetValue( "positionList", typeof(float[]) );
			this._angle = info.GetSingle( "angle" );
			this._image = (Image) info.GetValue( "image", typeof(Image) );
			this._wrapMode = (WrapMode) info.GetValue( "wrapMode", typeof(WrapMode) );

			if ( this._colorList != null && this._positionList != null )
			{
				ColorBlend blend = new ColorBlend();
				blend.Colors = this._colorList;
				blend.Positions = this._positionList;
				this.CreateBrushFromBlend( blend, this._angle );
			}
			else if ( this._image != null )
			{
				this._brush = new TextureBrush( this._image, this._wrapMode );
			}

			this._rangeDefault = info.GetDouble( "rangeDefault" );
		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermission(SecurityAction.Demand,SerializationFormatter=true)]
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{

			info.AddValue( "schema", schema );
			info.AddValue( "color", this._color );
			info.AddValue( "secondaryValueGradientColor", this._secondaryValueGradientColor );
			//info.AddValue( "brush", brush );
			//info.AddValue( "brushHolder", brushHolder );
			info.AddValue( "type", this._type );
			info.AddValue( "isScaled", this._isScaled );
			info.AddValue( "alignH", this._alignH );
			info.AddValue( "alignV", this._alignV );
			info.AddValue( "rangeMin", this._rangeMin );
			info.AddValue( "rangeMax", this._rangeMax );

			//BrushHolder brushHolder = new BrushHolder();
			//brush = brush;
			//info.AddValue( "brushHolder", brushHolder );

			info.AddValue( "colorList", this._colorList );
			info.AddValue( "positionList", this._positionList );
			info.AddValue( "angle", this._angle );
			info.AddValue( "image", this._image );
			info.AddValue( "wrapMode", this._wrapMode );

			info.AddValue( "rangeDefault", this._rangeDefault );
		}
	#endregion

	#region Properties

		/// <summary>
		/// The fill color.  This property is used as a single color to make a solid fill
		/// (<see cref="Type"/> is <see cref="FillType.Solid"/>), or it can be used in 
		/// combination with <see cref="System.Drawing.Color.White"/> to make a
		/// <see cref="LinearGradientBrush"/>
		/// when <see cref="Type"/> is <see cref="FillType.Brush"/> and <see cref="Brush"/>
		/// is null.
		/// </summary>
		/// <seealso cref="Type"/>
		public Color Color
		{
			get { return this._color; }
			set { this._color = value; }
		}

		/// <summary>
		/// Gets or sets the secondary color for gradientByValue fills.
		/// </summary>
		/// <remarks>
		/// This property is only applicable if the <see cref="Type"/> is
		/// <see cref="ZedGraph.FillType.GradientByX"/>,
		/// <see cref="ZedGraph.FillType.GradientByY"/>, or
		/// <see cref="ZedGraph.FillType.GradientByZ"/>.  Once the gradient-by-value logic picks
		/// a color, a new gradient will be created using the SecondaryValueGradientColor, the
		/// resulting gradient-by-value color, and the angle setting for this
		/// <see cref="Fill" />. Use a value of <see cref="System.Drawing.Color.Empty">Color.Empty</see> to have
		/// a solid-color <see cref="Fill" /> resulting from a gradient-by-value
		/// <see cref="FillType" />.
		/// </remarks>
		public Color SecondaryValueGradientColor
		{
			get { return this._secondaryValueGradientColor; }
			set { this._secondaryValueGradientColor = value; }
		}

		/// <summary>
		/// The custom fill brush.  This can be a <see cref="SolidBrush"/>, a
		/// <see cref="LinearGradientBrush"/>, or a <see cref="TextureBrush"/>.  This property is
		/// only applicable if the <see cref="Type"/> property is set
		/// to <see cref="FillType.Brush"/>.
		/// </summary>
		public Brush Brush
		{
			get { return this._brush; }
			set { this._brush = value; }
		}
		/// <summary>
		/// Determines the type of fill, which can be either solid
		/// color (<see cref="ZedGraph.FillType.Solid"/>) or a custom brush
		/// (<see cref="ZedGraph.FillType.Brush"/>).  See <see cref="Type"/> for
		/// more information.
		/// </summary>
		/// <seealso cref="ZedGraph.Fill.Color"/>
		public FillType Type
		{
			get { return this._type; }
			set { this._type = value; }
		}
		/// <summary>
		/// This property determines the type of color fill. 
		/// Returns true if the <see cref="Type"/> property is either
		/// <see cref="FillType.Solid"/> or
		/// <see cref="FillType.Brush"/>.  If set to true, this property
		/// will automatically set the <see cref="Type"/> to
		/// <see cref="FillType.Brush"/>.  If set to false, this property
		/// will automatically set the <see cref="Type"/> to
		/// <see cref="FillType.None"/>.  In order to get a regular
		/// solid-color fill, you have to manually set <see cref="Type"/>
		/// to <see cref="FillType.Solid"/>.
		/// </summary>
		/// <seealso cref="Color"/>
		/// <seealso cref="Brush"/>
		/// <seealso cref="Type"/>
		public bool IsVisible
		{
			get { return this._type != FillType.None; }
			set { this._type = value ? ( this._type == FillType.None ? FillType.Brush : this._type ) : FillType.None; }
		}

		/// <summary>
		/// Determines if the brush will be scaled to the bounding box
		/// of the filled object.  If this value is false, then the brush will only be aligned
		/// with the filled object based on the <see cref="AlignH"/> and <see cref="AlignV"/>
		/// properties.
		/// </summary>
		public bool IsScaled
		{
			get { return this._isScaled; }
			set { this._isScaled = value; }
		}
		
		/// <summary>
		/// Determines how the brush will be aligned with the filled object
		/// in the horizontal direction.  This value is a <see cref="ZedGraph.AlignH"/> enumeration.
		/// This field only applies if <see cref="IsScaled"/> is false.
		/// </summary>
		/// <seealso cref="AlignV"/>
		public AlignH AlignH
		{
			get { return this._alignH; }
			set { this._alignH = value; }
		}
		
		/// <summary>
		/// Determines how the brush will be aligned with the filled object
		/// in the vertical direction.  This value is a <see cref="ZedGraph.AlignV"/> enumeration.
		/// This field only applies if <see cref="IsScaled"/> is false.
		/// </summary>
		/// <seealso cref="AlignH"/>
		public AlignV AlignV
		{
			get { return this._alignV; }
			set { this._alignV = value; }
		}

		/// <summary>
		/// Returns a boolean value indicating whether or not this fill is a "Gradient-By-Value"
		/// type.  This is true for <see cref="FillType.GradientByX"/>, <see cref="FillType.GradientByY"/>,
		/// or <see cref="FillType.GradientByZ"/>.
		/// </summary>
		/// <remarks>
		/// The gradient by value fill method allows the fill color for each point or bar to
		/// be based on a value for that point (either X, Y, or Z in the <see cref="IPointList"/>.
		/// For example, assume a <see cref="Fill"/> class is defined with a linear gradient ranging from
		/// <see cref="System.Drawing.Color.Blue"/> to <see cref="System.Drawing.Color.Red"/> and the <see cref="Fill.Type"/>
		/// is set to <see cref="FillType.GradientByY"/>.  If <see cref="RangeMin"/> is set to 
		/// 100.0 and <see cref="RangeMax"/> is set to 200.0, then a point that has a Y value of
		/// 100 or less will be colored blue, a point with a Y value of 200 or more will be
		/// colored red, and a point between 100 and 200 will have a color based on a linear scale
		/// between blue and red.  Note that the fill color is always solid for any given point.
		/// You can use the Z value from <see cref="IPointList"/> along with
		/// <see cref="FillType.GradientByZ"/> to color individual points according to some
		/// property that is independent of the X,Y point pair.
		/// </remarks>
		/// <value>true if this is a Gradient-by-value type, false otherwise</value>
		/// <seealso cref="FillType.GradientByX"/>
		/// <seealso cref="FillType.GradientByY"/>
		/// <seealso cref="FillType.GradientByZ"/>
		public bool IsGradientValueType
		{
			get { return this._type == FillType.GradientByX || this._type == FillType.GradientByY ||
					this._type == FillType.GradientByZ || this._type == FillType.GradientByColorValue; }
		}

		/// <summary>
		/// The minimum user-scale value for the gradient-by-value determination.  This defines
		/// the user-scale value for the start of the gradient.
		/// </summary>
		/// <seealso cref="FillType.GradientByX"/>
		/// <seealso cref="FillType.GradientByY"/>
		/// <seealso cref="FillType.GradientByZ"/>
		/// <seealso cref="IsGradientValueType"/>
		/// <seealso cref="RangeMax"/>
		/// <seealso cref="RangeDefault"/>
		/// <value>A double value, in user scale unit</value>
		public double RangeMin
		{
			get { return this._rangeMin; }
			set { this._rangeMin = value; }
		}
		/// <summary>
		/// The maximum user-scale value for the gradient-by-value determination.  This defines
		/// the user-scale value for the end of the gradient.
		/// </summary>
		/// <seealso cref="FillType.GradientByX"/>
		/// <seealso cref="FillType.GradientByY"/>
		/// <seealso cref="FillType.GradientByZ"/>
		/// <seealso cref="IsGradientValueType"/>
		/// <seealso cref="RangeMin"/>
		/// <seealso cref="RangeDefault"/>
		/// <value>A double value, in user scale unit</value>
		public double RangeMax
		{
			get { return this._rangeMax; }
			set { this._rangeMax = value; }
		}

		/// <summary>
		/// The default user-scale value for the gradient-by-value determination.  This defines the
		/// value that will be used when there is no point value available, or the actual point value
		/// is invalid.
		/// </summary>
		/// <remarks>
		/// Note that this value, when defined, will determine the color that is used in the legend.
		/// If this value is set to double.MaxValue, then it remains "undefined."  In this case, the
		/// legend symbols will actually be filled with a color gradient representing the range of
		/// colors.
		/// </remarks>
		/// <seealso cref="FillType.GradientByX"/>
		/// <seealso cref="FillType.GradientByY"/>
		/// <seealso cref="FillType.GradientByZ"/>
		/// <seealso cref="IsGradientValueType"/>
		/// <seealso cref="RangeMin"/>
		/// <seealso cref="RangeMax"/>
		/// <value>A double value, in user scale unit</value>
		public double RangeDefault
		{
			get { return this._rangeDefault; }
			set { this._rangeDefault = value; }
		}

	#endregion

	#region Methods

		/// <summary>
		/// Create a fill brush using current properties.  This method will construct a brush based on the
		/// settings of <see cref="ZedGraph.Fill.Type"/>, <see cref="ZedGraph.Fill.Color"/>
		/// and <see cref="ZedGraph.Fill.Brush"/>.  If
		/// <see cref="ZedGraph.Fill.Type"/> is set to <see cref="ZedGraph.FillType.Brush"/> and
		/// <see cref="ZedGraph.Fill.Brush"/>
		/// is null, then a <see cref="LinearGradientBrush"/> will be created between the colors of
		/// <see cref="System.Drawing.Color.White"/> and <see cref="ZedGraph.Fill.Color"/>.
		/// </summary>
		/// <param name="rect">A rectangle that bounds the object to be filled.  This determines
		/// the start and end of the gradient fill.</param>
		/// <returns>A <see cref="System.Drawing.Brush"/> class representing the fill brush</returns>
		public Brush MakeBrush( RectangleF rect )
		{
			// just provide a default value for the valueFraction
			// return MakeBrush( rect, new PointPair( 0.5, 0.5, 0.5 ) );
			return this.MakeBrush( rect, null );
		}

		/// <summary>
		/// Create a fill brush using current properties.  This method will construct a brush based on the
		/// settings of <see cref="ZedGraph.Fill.Type"/>, <see cref="ZedGraph.Fill.Color"/>
		/// and <see cref="ZedGraph.Fill.Brush"/>.  If
		/// <see cref="ZedGraph.Fill.Type"/> is set to <see cref="ZedGraph.FillType.Brush"/> and
		/// <see cref="ZedGraph.Fill.Brush"/>
		/// is null, then a <see cref="LinearGradientBrush"/> will be created between the colors of
		/// <see cref="System.Drawing.Color.White"/> and <see cref="ZedGraph.Fill.Color"/>.
		/// </summary>
		/// <param name="rect">A rectangle that bounds the object to be filled.  This determines
		/// the start and end of the gradient fill.</param>
		/// <param name="dataValue">The data value to be used for a value-based
		/// color gradient.  This is only applicable for <see cref="FillType.GradientByX"/>,
		/// <see cref="FillType.GradientByY"/> or <see cref="FillType.GradientByZ"/>.</param>
		/// <returns>A <see cref="System.Drawing.Brush"/> class representing the fill brush</returns>
		public Brush MakeBrush( RectangleF rect, PointPair dataValue )
		{
			// get a brush
			if ( this.IsVisible && ( !this._color.IsEmpty || this._brush != null ) )
			{
				if ( rect.Height < 1.0F )
					rect.Height = 1.0F;
				if ( rect.Width < 1.0F )
					rect.Width = 1.0F;
					
				//Brush	brush;
				if ( this._type == FillType.Brush )
				{
					return this.ScaleBrush( rect, this._brush, this._isScaled );
				}
				else if ( this.IsGradientValueType )
				{
					if ( dataValue != null )
					{
						if ( !this._secondaryValueGradientColor.IsEmpty )
						{
							// Go ahead and create a new Fill so we can do all the scaling, etc.,
							// that is associated with a gradient
							Fill tmpFill = new Fill( this._secondaryValueGradientColor,
									this.GetGradientColor( dataValue ), this._angle );
							return tmpFill.MakeBrush( rect );
						}
						else
							return new SolidBrush( this.GetGradientColor( dataValue ) );
					}
					else if ( this._rangeDefault != double.MaxValue )
					{
						if ( !this._secondaryValueGradientColor.IsEmpty )
						{
							// Go ahead and create a new Fill so we can do all the scaling, etc.,
							// that is associated with a gradient
							Fill tmpFill = new Fill( this._secondaryValueGradientColor,
									this.GetGradientColor( this._rangeDefault ), this._angle );
							return tmpFill.MakeBrush( rect );
						}
						else
							return new SolidBrush( this.GetGradientColor( this._rangeDefault ) );
					}
					else
						return this.ScaleBrush( rect, this._brush, true );
				}
				else
					return new SolidBrush( this._color );
			}

			// Always return a suitable default
			return new SolidBrush( Color.White );
		}

		internal Color GetGradientColor( PointPair dataValue )
		{
			double val;

			if ( dataValue == null )
				val = this._rangeDefault;
			else if ( this._type == FillType.GradientByColorValue )
				val = dataValue.ColorValue;
			else if ( this._type == FillType.GradientByZ )
				val = dataValue.Z;
			else if ( this._type == FillType.GradientByY )
				val = dataValue.Y;
			else
				val = dataValue.X;

			return this.GetGradientColor( val );
		}

		internal Color GetGradientColor( double val )
		{
			double valueFraction;

			if ( Double.IsInfinity( val ) || double.IsNaN( val ) || val == PointPair.Missing )
				val = this._rangeDefault;

			if ( this._rangeMax - this._rangeMin < 1e-20 || val == double.MaxValue )
				valueFraction = 0.5;
			else			
				valueFraction = ( val - this._rangeMin ) / ( this._rangeMax - this._rangeMin );

			if ( valueFraction < 0.0 )
				valueFraction = 0.0;
			else if ( valueFraction > 1.0 )
				valueFraction = 1.0;

			if ( this._gradientBM == null )
			{
				RectangleF rect = new RectangleF( 0, 0, 100, 1 );
				this._gradientBM = new Bitmap( 100, 1 );
				Graphics gBM = Graphics.FromImage( this._gradientBM );

				Brush tmpBrush = this.ScaleBrush( rect, this._brush, true );
				gBM.FillRectangle( tmpBrush, rect );
			}

			return this._gradientBM.GetPixel( (int) (99.9 * valueFraction), 0 );
		}

		private Brush ScaleBrush( RectangleF rect, Brush brush, bool isScaled )
		{
			if ( brush != null )
			{
				if ( brush is SolidBrush )
				{
					return (Brush) brush.Clone();
				}
				else if ( brush is LinearGradientBrush )
				{
					LinearGradientBrush linBrush = (LinearGradientBrush) brush.Clone();
					
					if ( isScaled )
					{
						linBrush.ScaleTransform( rect.Width / linBrush.Rectangle.Width,
							rect.Height / linBrush.Rectangle.Height, MatrixOrder.Append );
						linBrush.TranslateTransform( rect.Left - linBrush.Rectangle.Left,
							rect.Top - linBrush.Rectangle.Top, MatrixOrder.Append );
					}
					else
					{
						float	dx = 0,
								dy = 0;
						switch ( this._alignH )
						{
						case AlignH.Left:
							dx = rect.Left - linBrush.Rectangle.Left;
							break;
						case AlignH.Center:
							dx = ( rect.Left + rect.Width / 2.0F ) - linBrush.Rectangle.Left;
							break;
						case AlignH.Right:
							dx = ( rect.Left + rect.Width ) - linBrush.Rectangle.Left;
							break;
						}
						
						switch ( this._alignV )
						{
						case AlignV.Top:
							dy = rect.Top - linBrush.Rectangle.Top;
							break;
						case AlignV.Center:
							dy = ( rect.Top + rect.Height / 2.0F ) - linBrush.Rectangle.Top;
							break;
						case AlignV.Bottom:
							dy = ( rect.Top + rect.Height) - linBrush.Rectangle.Top;
							break;
						}

						linBrush.TranslateTransform( dx, dy, MatrixOrder.Append );
					}
					
					return linBrush;
					
				} // LinearGradientBrush
				else if ( brush is TextureBrush )
				{
					TextureBrush texBrush = (TextureBrush) brush.Clone();
					
					if ( isScaled )
					{
						texBrush.ScaleTransform( rect.Width / texBrush.Image.Width,
							rect.Height / texBrush.Image.Height, MatrixOrder.Append );
						texBrush.TranslateTransform( rect.Left, rect.Top, MatrixOrder.Append );
					}
					else
					{
						float	dx = 0,
								dy = 0;
						switch ( this._alignH )
						{
						case AlignH.Left:
							dx = rect.Left;
							break;
						case AlignH.Center:
							dx = ( rect.Left + rect.Width / 2.0F );
							break;
						case AlignH.Right:
							dx = ( rect.Left + rect.Width );
							break;
						}
						
						switch ( this._alignV )
						{
						case AlignV.Top:
							dy = rect.Top;
							break;
						case AlignV.Center:
							dy = ( rect.Top + rect.Height / 2.0F );
							break;
						case AlignV.Bottom:
							dy = ( rect.Top + rect.Height);
							break;
						}

						texBrush.TranslateTransform( dx, dy, MatrixOrder.Append );
					}
					
					return texBrush;
				}
				else // other brush type
				{
					return (Brush) brush.Clone();
				}
			}
			else
				// If they didn't provide a brush, make one using the fillcolor gradient to white
				return new LinearGradientBrush( rect, Color.White, this._color, 0F );
		}

		/// <summary>
		/// Fill the background of the <see cref="RectangleF"/> area, using the
		/// fill type from this <see cref="Fill"/>.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="rect">The <see cref="RectangleF"/> struct specifying the area
		/// to be filled</param>
		public void Draw( Graphics g, RectangleF rect )
		{
			this.Draw( g, rect, null );
			/*
			if ( this.IsVisible )
			{
				using( Brush brush = this.MakeBrush( rect ) )
				{
					g.FillRectangle( brush, rect );
					//brush.Dispose();
				}
			}
			*/
		}

		/// <summary>
		/// Fill the background of the <see cref="RectangleF"/> area, using the
		/// fill type from this <see cref="Fill"/>.
		/// </summary>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="rect">The <see cref="RectangleF"/> struct specifying the area
		/// to be filled</param>
		/// <param name="pt">The data value to be used in case it's a
		/// <see cref="FillType.GradientByX" />, <see cref="FillType.GradientByY" />, or
		/// <see cref="FillType.GradientByZ" /> <see cref="FillType" />.</param>
		public void Draw( Graphics g, RectangleF rect, PointPair pt )
		{
			if ( this.IsVisible )
			{
				using ( Brush brush = this.MakeBrush( rect, pt ) )
				{
					g.FillRectangle( brush, rect );
				}
			}
		}


	#endregion
	}
}
