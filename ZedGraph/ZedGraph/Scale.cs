// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Scale.cs">
//   
// </copyright>
// <summary>
//   The Scale class is an abstract base class that encompasses the properties
//   and methods associated with a scale of data.
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
    /// The Scale class is an abstract base class that encompasses the properties
    /// and methods associated with a scale of data.
    /// </summary>
    /// <remarks>This class is inherited by the
    /// <see cref="LinearScale"/>, <see cref="LogScale"/>, <see cref="OrdinalScale"/>,
    /// <see cref="TextScale"/>, <see cref="DateScale"/>, <see cref="ExponentScale"/>,
    /// <see cref="DateAsOrdinalScale"/>, and <see cref="LinearAsOrdinalScale"/>
    /// classes to define specific characteristics for those types.
    /// </remarks>
    /// 
    /// <author> John Champion  </author>
    /// <version> $Revision: 1.33 $ $Date: 2007-09-19 06:41:56 $ </version>
    [Serializable]
    public abstract class Scale : ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        // schema changed to 2 with isScaleVisible
        public const int schema = 11;

        #endregion

        #region Fields

        /// <summary> Private field for the alignment of the <see cref="Axis"/> tic labels.
        /// This fields controls whether the inside, center, or outside edges of the text labels are aligned.
        /// Use the public property <see cref="Scale.Align"/>
        /// for access to this value. </summary>
        /// <seealso cref="FormatAuto"/>
        internal AlignP _align;

        /// <summary> Private field for the alignment of the <see cref="Axis"/> tic labels.
        /// This fields controls whether the left, center, or right edges of the text labels are aligned.
        /// Use the public property <see cref="Scale.AlignH"/>
        /// for access to this value. </summary>
        /// <seealso cref="FormatAuto"/>
        internal AlignH _alignH;

        /// <summary> Private fields for the <see cref="Axis"/> scale definitions.
        /// Use the public properties <see cref="Min"/>, <see cref="Max"/>,
        /// <see cref="MajorStep"/>, <see cref="MinorStep"/>, and <see cref="Exponent" />
        /// for access to these values.
        /// </summary>
        internal double _baseTic;

        /// <summary> Private fields for the <see cref="Axis"/> scale definitions.
        /// Use the public properties <see cref="Min"/>, <see cref="Max"/>,
        /// <see cref="MajorStep"/>, <see cref="MinorStep"/>, and <see cref="Exponent" />
        /// for access to these values.
        /// </summary>
        internal double _exponent;

        /// <summary> Private fields for the <see cref="Axis"/> font specificatios.
        /// Use the public properties <see cref="FontSpec"/> and
        /// <see cref="Scale.FontSpec"/> for access to these values. </summary>
        internal FontSpec _fontSpec;

        /// <summary> Private field for the format of the <see cref="Axis"/> tic labels.
        /// Use the public property <see cref="Format"/> for access to this value. </summary>
        /// <seealso cref="FormatAuto"/>
        internal string _format;

        /// <summary> Private fields for the <see cref="Axis"/> automatic scaling modes.
        /// Use the public properties <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
        /// <see cref="MajorStepAuto"/>, <see cref="MinorStepAuto"/>, 
        /// <see cref="MagAuto"/> and <see cref="FormatAuto"/>
        /// for access to these values.
        /// </summary>
        internal bool _formatAuto;

        /// <summary> Private fields for the <see cref="Scale"/> attributes.
        /// Use the public properties <see cref="Scale.IsReverse"/> and <see cref="Scale.IsUseTenPower"/>
        /// for access to these values.
        /// </summary>
        internal bool _isLabelsInside;

        /// <summary> Private fields for the <see cref="Scale"/> attributes.
        /// Use the public properties <see cref="Scale.IsReverse"/> and <see cref="Scale.IsUseTenPower"/>
        /// for access to these values.
        /// </summary>
        internal bool _isPreventLabelOverlap;

        /// <summary> Private fields for the <see cref="Scale"/> attributes.
        /// Use the public properties <see cref="Scale.IsReverse"/> and <see cref="Scale.IsUseTenPower"/>
        /// for access to these values.
        /// </summary>
        internal bool _isReverse;

        /// <summary> Private fields for the <see cref="Scale"/> attributes.
        /// Use the public properties <see cref="Scale.IsReverse"/> and <see cref="Scale.IsUseTenPower"/>
        /// for access to these values.
        /// </summary>
        internal bool _isSkipCrossLabel;

        /// <summary> Private fields for the <see cref="Scale"/> attributes.
        /// Use the public properties <see cref="Scale.IsReverse"/> and <see cref="Scale.IsUseTenPower"/>
        /// for access to these values.
        /// </summary>
        internal bool _isSkipFirstLabel;

        /// <summary> Private fields for the <see cref="Scale"/> attributes.
        /// Use the public properties <see cref="Scale.IsReverse"/> and <see cref="Scale.IsUseTenPower"/>
        /// for access to these values.
        /// </summary>
        internal bool _isSkipLastLabel;

        /// <summary> Private fields for the <see cref="Scale"/> attributes.
        /// Use the public properties <see cref="Scale.IsReverse"/> and <see cref="Scale.IsUseTenPower"/>
        /// for access to these values.
        /// </summary>
        internal bool _isUseTenPower;

        /// <summary> Private fields for the <see cref="Scale"/> attributes.
        /// Use the public properties <see cref="Scale.IsReverse"/> and <see cref="Scale.IsUseTenPower"/>
        /// for access to these values.
        /// </summary>
        internal bool _isVisible;

        /// <summary>
        /// Data range temporary values, used by GetRange().
        /// </summary>
        internal double _lBound;

        /// <summary>
        /// Internal field that stores the amount of space between the scale labels and the
        /// major tics.  Use the public property <see cref="LabelGap" /> to access this
        /// value.
        /// </summary>
        internal float _labelGap;

        /// <summary> Private field for the <see cref="Axis"/> scale value display.
        /// Use the public property <see cref="Mag"/> for access to this value.
        /// </summary>
        internal int _mag;

        /// <summary> Private fields for the <see cref="Axis"/> automatic scaling modes.
        /// Use the public properties <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
        /// <see cref="MajorStepAuto"/>, <see cref="MinorStepAuto"/>, 
        /// <see cref="MagAuto"/> and <see cref="FormatAuto"/>
        /// for access to these values.
        /// </summary>
        internal bool _magAuto;

        /// <summary> Private fields for the <see cref="Axis"/> scale definitions.
        /// Use the public properties <see cref="Min"/>, <see cref="Max"/>,
        /// <see cref="MajorStep"/>, <see cref="MinorStep"/>, and <see cref="Exponent" />
        /// for access to these values.
        /// </summary>
        internal double _majorStep;

        /// <summary> Private fields for the <see cref="Axis"/> automatic scaling modes.
        /// Use the public properties <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
        /// <see cref="MajorStepAuto"/>, <see cref="MinorStepAuto"/>, 
        /// <see cref="MagAuto"/> and <see cref="FormatAuto"/>
        /// for access to these values.
        /// </summary>
        internal bool _majorStepAuto;

        /// <summary>
        /// Private fields for Unit types to be used for the major and minor tics.
        /// See <see cref="MajorUnit"/> and <see cref="MinorUnit"/> for the corresponding
        /// public properties.
        /// These types only apply for date-time scales (<see cref="IsDate"/>).
        /// </summary>
        /// <value>The value of these types is of enumeration type <see cref="DateUnit"/>
        /// </value>
        internal DateUnit _majorUnit;

        /// <summary> Private fields for the <see cref="Axis"/> scale definitions.
        /// Use the public properties <see cref="Min"/>, <see cref="Max"/>,
        /// <see cref="MajorStep"/>, <see cref="MinorStep"/>, and <see cref="Exponent" />
        /// for access to these values.
        /// </summary>
        internal double _max;

        /// <summary> Private fields for the <see cref="Axis"/> automatic scaling modes.
        /// Use the public properties <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
        /// <see cref="MajorStepAuto"/>, <see cref="MinorStepAuto"/>, 
        /// <see cref="MagAuto"/> and <see cref="FormatAuto"/>
        /// for access to these values.
        /// </summary>
        internal bool _maxAuto;

        /// <summary> Private fields for the <see cref="Axis"/> "grace" settings.
        /// These values determine how much extra space is left before the first data value
        /// and after the last data value.
        /// Use the public properties <see cref="MinGrace"/> and <see cref="MaxGrace"/>
        /// for access to these values.
        /// </summary>
        internal double _maxGrace;

        /// <summary>
        /// Scale values for calculating transforms.  These are temporary values
        /// used ONLY during the Draw process.
        /// </summary>
        /// <remarks>
        /// These values are just <see cref="Scale.Min" /> and <see cref="Scale.Max" />
        /// for normal linear scales, but for log or exponent scales they will be a
        /// linear representation.  For <see cref="LogScale" />, it is the
        /// <see cref="Math.Log(double)" /> of the value, and for <see cref="ExponentScale" />,
        /// it is the <see cref="Math.Exp(double)" />
        /// of the value.
        /// </remarks>
        internal double _maxLinTemp;

        /// <summary>
        /// Pixel positions at the minimum and maximum value for this scale.
        /// These are temporary values used/valid only during the Draw process.
        /// </summary>
        internal float _maxPix;

        /// <summary> Private fields for the <see cref="Axis"/> scale definitions.
        /// Use the public properties <see cref="Min"/>, <see cref="Max"/>,
        /// <see cref="MajorStep"/>, <see cref="MinorStep"/>, and <see cref="Exponent" />
        /// for access to these values.
        /// </summary>
        internal double _min;

        /// <summary> Private fields for the <see cref="Axis"/> automatic scaling modes.
        /// Use the public properties <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
        /// <see cref="MajorStepAuto"/>, <see cref="MinorStepAuto"/>, 
        /// <see cref="MagAuto"/> and <see cref="FormatAuto"/>
        /// for access to these values.
        /// </summary>
        internal bool _minAuto;

        /// <summary> Private fields for the <see cref="Axis"/> "grace" settings.
        /// These values determine how much extra space is left before the first data value
        /// and after the last data value.
        /// Use the public properties <see cref="MinGrace"/> and <see cref="MaxGrace"/>
        /// for access to these values.
        /// </summary>
        internal double _minGrace;

        /// <summary>
        /// Scale values for calculating transforms.  These are temporary values
        /// used ONLY during the Draw process.
        /// </summary>
        /// <remarks>
        /// These values are just <see cref="Scale.Min" /> and <see cref="Scale.Max" />
        /// for normal linear scales, but for log or exponent scales they will be a
        /// linear representation.  For <see cref="LogScale" />, it is the
        /// <see cref="Math.Log(double)" /> of the value, and for <see cref="ExponentScale" />,
        /// it is the <see cref="Math.Exp(double)" />
        /// of the value.
        /// </remarks>
        internal double _minLinTemp;

        /// <summary>
        /// Pixel positions at the minimum and maximum value for this scale.
        /// These are temporary values used/valid only during the Draw process.
        /// </summary>
        internal float _minPix;

        /// <summary> Private fields for the <see cref="Axis"/> scale definitions.
        /// Use the public properties <see cref="Min"/>, <see cref="Max"/>,
        /// <see cref="MajorStep"/>, <see cref="MinorStep"/>, and <see cref="Exponent" />
        /// for access to these values.
        /// </summary>
        internal double _minorStep;

        /// <summary> Private fields for the <see cref="Axis"/> automatic scaling modes.
        /// Use the public properties <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
        /// <see cref="MajorStepAuto"/>, <see cref="MinorStepAuto"/>, 
        /// <see cref="MagAuto"/> and <see cref="FormatAuto"/>
        /// for access to these values.
        /// </summary>
        internal bool _minorStepAuto;

        /// <summary>
        /// Private fields for Unit types to be used for the major and minor tics.
        /// See <see cref="MajorUnit"/> and <see cref="MinorUnit"/> for the corresponding
        /// public properties.
        /// These types only apply for date-time scales (<see cref="IsDate"/>).
        /// </summary>
        /// <value>The value of these types is of enumeration type <see cref="DateUnit"/>
        /// </value>
        internal DateUnit _minorUnit;

        /// <summary>
        /// private field that stores the owner Axis that contains this Scale instance.
        /// </summary>
        internal Axis _ownerAxis;

        /// <summary>
        /// Data range temporary values, used by GetRange().
        /// </summary>
        internal double _rangeMax;

        /// <summary>
        /// Data range temporary values, used by GetRange().
        /// </summary>
        internal double _rangeMin;

        /// <summary> Private <see cref="System.Collections.ArrayList"/> field for the <see cref="Axis"/> array of text labels.
        /// This property is only used if <see cref="Type"/> is set to
        /// <see cref="AxisType.Text"/> </summary>
        internal string[] _textLabels = null;

        /// <summary>
        /// Data range temporary values, used by GetRange().
        /// </summary>
        internal double _uBound;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Scale"/> class. 
        /// Basic constructor -- requires that the <see cref="Scale"/> object be intialized with
        /// a pre-existing owner <see cref="Axis"/>.
        /// </summary>
        /// <param name="ownerAxis">
        /// The <see cref="Axis"/> object that is the owner of this
        /// <see cref="Scale"/> instance.
        /// </param>
        public Scale(Axis ownerAxis)
        {
            this._ownerAxis = ownerAxis;

            this._min = 0.0;
            this._max = 1.0;
            this._majorStep = 0.1;
            this._minorStep = 0.1;
            this._exponent = 1.0;
            this._mag = 0;
            this._baseTic = PointPair.Missing;

            this._minGrace = Default.MinGrace;
            this._maxGrace = Default.MaxGrace;

            this._minAuto = true;
            this._maxAuto = true;
            this._majorStepAuto = true;
            this._minorStepAuto = true;
            this._magAuto = true;
            this._formatAuto = true;

            this._isReverse = Default.IsReverse;
            this._isUseTenPower = true;
            this._isPreventLabelOverlap = true;
            this._isVisible = true;
            this._isSkipFirstLabel = false;
            this._isSkipLastLabel = false;
            this._isSkipCrossLabel = false;

            this._majorUnit = DateUnit.Day;
            this._minorUnit = DateUnit.Day;

            this._format = null;
            this._textLabels = null;

            this._isLabelsInside = Default.IsLabelsInside;
            this._align = Default.Align;
            this._alignH = Default.AlignH;

            this._fontSpec = new FontSpec(
                Default.FontFamily, 
                Default.FontSize, 
                Default.FontColor, 
                Default.FontBold, 
                Default.FontUnderline, 
                Default.FontItalic, 
                Default.FillColor, 
                Default.FillBrush, 
                Default.FillType);

            this._fontSpec.Border.IsVisible = false;
            this._labelGap = Default.LabelGap;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scale"/> class. 
        /// Copy Constructor.  Create a new <see cref="Scale"/> object based on the specified
        /// existing one.
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="Scale"/> object to be copied.
        /// </param>
        /// <param name="owner">
        /// The <see cref="Axis"/> object that will own the
        /// new instance of <see cref="Scale"/>
        /// </param>
        public Scale(Scale rhs, Axis owner)
        {
            this._ownerAxis = owner;

            this._min = rhs._min;
            this._max = rhs._max;
            this._majorStep = rhs._majorStep;
            this._minorStep = rhs._minorStep;
            this._exponent = rhs._exponent;
            this._baseTic = rhs._baseTic;

            this._minAuto = rhs._minAuto;
            this._maxAuto = rhs._maxAuto;
            this._majorStepAuto = rhs._majorStepAuto;
            this._minorStepAuto = rhs._minorStepAuto;
            this._magAuto = rhs._magAuto;
            this._formatAuto = rhs._formatAuto;

            this._minGrace = rhs._minGrace;
            this._maxGrace = rhs._maxGrace;

            this._mag = rhs._mag;

            this._isUseTenPower = rhs._isUseTenPower;
            this._isReverse = rhs._isReverse;
            this._isPreventLabelOverlap = rhs._isPreventLabelOverlap;
            this._isVisible = rhs._isVisible;
            this._isSkipFirstLabel = rhs._isSkipFirstLabel;
            this._isSkipLastLabel = rhs._isSkipLastLabel;
            this._isSkipCrossLabel = rhs._isSkipCrossLabel;

            this._majorUnit = rhs._majorUnit;
            this._minorUnit = rhs._minorUnit;

            this._format = rhs._format;

            this._isLabelsInside = rhs._isLabelsInside;
            this._align = rhs._align;
            this._alignH = rhs._alignH;

            this._fontSpec = (FontSpec)rhs._fontSpec.Clone();

            this._labelGap = rhs._labelGap;

            if (rhs._textLabels != null)
            {
                this._textLabels = (string[])rhs._textLabels.Clone();
            }
            else
            {
                this._textLabels = null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scale"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected Scale(SerializationInfo info, StreamingContext context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema");

            this._min = info.GetDouble("min");
            this._max = info.GetDouble("max");
            this._majorStep = info.GetDouble("majorStep");
            this._minorStep = info.GetDouble("minorStep");
            this._exponent = info.GetDouble("exponent");
            this._baseTic = info.GetDouble("baseTic");

            this._minAuto = info.GetBoolean("minAuto");
            this._maxAuto = info.GetBoolean("maxAuto");
            this._majorStepAuto = info.GetBoolean("majorStepAuto");
            this._minorStepAuto = info.GetBoolean("minorStepAuto");
            this._magAuto = info.GetBoolean("magAuto");
            this._formatAuto = info.GetBoolean("formatAuto");

            this._minGrace = info.GetDouble("minGrace");
            this._maxGrace = info.GetDouble("maxGrace");

            this._mag = info.GetInt32("mag");

            this._isReverse = info.GetBoolean("isReverse");
            this._isPreventLabelOverlap = info.GetBoolean("isPreventLabelOverlap");
            this._isUseTenPower = info.GetBoolean("isUseTenPower");

            this._isVisible = true;
            this._isVisible = info.GetBoolean("isVisible");

            this._isSkipFirstLabel = info.GetBoolean("isSkipFirstLabel");
            this._isSkipLastLabel = info.GetBoolean("isSkipLastLabel");
            this._isSkipCrossLabel = info.GetBoolean("isSkipCrossLabel");

            this._textLabels = (string[])info.GetValue("textLabels", typeof(string[]));
            this._format = info.GetString("format");

            this._majorUnit = (DateUnit)info.GetValue("majorUnit", typeof(DateUnit));
            this._minorUnit = (DateUnit)info.GetValue("minorUnit", typeof(DateUnit));

            this._isLabelsInside = info.GetBoolean("isLabelsInside");
            this._align = (AlignP)info.GetValue("align", typeof(AlignP));
            if (schema >= 11)
            {
                this._alignH = (AlignH)info.GetValue("alignH", typeof(AlignH));
            }

            this._fontSpec = (FontSpec)info.GetValue("fontSpec", typeof(FontSpec));
            this._labelGap = info.GetSingle("labelGap");
        }

        #endregion

        #region Public Properties

        /// <summary> Controls the alignment of the <see cref="Axis"/> tic labels.
        /// </summary>
        /// <remarks>
        /// This property controls whether the inside, center, or outside edges of the
        /// text labels are aligned.
        /// </remarks>
        public AlignP Align
        {
            get
            {
                return this._align;
            }

            set
            {
                this._align = value;
            }
        }

        /// <summary> Controls the alignment of the <see cref="Axis"/> tic labels.
        /// </summary>
        /// <remarks>
        /// This property controls whether the left, center, or right edges of the
        /// text labels are aligned.
        /// </remarks>
        public AlignH AlignH
        {
            get
            {
                return this._alignH;
            }

            set
            {
                this._alignH = value;
            }
        }

        /// <summary>
        /// Gets or sets the scale value at which the first major tic label will appear.
        /// </summary>
        /// <remarks>This property allows the scale labels to start at an irregular value.
        /// For example, on a scale range with <see cref="Min"/> = 0, <see cref="Max"/> = 1000,
        /// and <see cref="MajorStep"/> = 200, a <see cref="BaseTic"/> value of 50 would cause
        /// the scale labels to appear at values 50, 250, 450, 650, and 850.  Note that the
        /// default value for this property is <see cref="PointPairBase.Missing"/>, which means the
        /// value is not used.  Setting this property to any value other than
        /// <see cref="PointPairBase.Missing"/> will activate the effect.  The value specified must
        /// coincide with the first major tic.  That is, if <see cref="BaseTic"/> were set to
        /// 650 in the example above, then the major tics would only occur at 650 and 850.  This
        /// setting may affect the minor tics, since the minor tics are always referenced to the
        /// <see cref="BaseTic"/>.  That is, in the example above, if the <see cref="MinorStep"/>
        /// were set to 30 (making it a non-multiple of the major step), then the minor tics would
        /// occur at 20, 50 (so it lines up with the BaseTic), 80, 110, 140, etc.
        /// </remarks>
        /// <value> The value is defined in user scale units </value>
        /// <seealso cref="Min"/>
        /// <seealso cref="Max"/>
        /// <seealso cref="MajorStep"/>
        /// <seealso cref="MinorStep"/>
        /// <seealso cref="Axis.Cross"/>
        public double BaseTic
        {
            get
            {
                return this._baseTic;
            }

            set
            {
                this._baseTic = value;
            }
        }

        /// <summary>
        /// Gets or sets the scale exponent value.  This only applies to <see cref="AxisType.Exponent" />. 
        /// </summary>
        /// <seealso cref="Min"/>
        /// <seealso cref="Max"/>
        /// <seealso cref="MinorStep"/>
        /// <seealso cref="MajorStepAuto"/>
        /// <seealso cref="ZedGraph.Scale.Default.TargetXSteps"/>
        /// <seealso cref="ZedGraph.Scale.Default.TargetYSteps"/>
        /// <seealso cref="ZedGraph.Scale.Default.ZeroLever"/>
        /// <seealso cref="ZedGraph.Scale.Default.MaxTextLabels"/>
        public double Exponent
        {
            get
            {
                return this._exponent;
            }

            set
            {
                this._exponent = value;
            }
        }

        /// <summary>
        /// Gets a reference to the <see cref="ZedGraph.FontSpec"/> class used to render
        /// the scale values
        /// </summary>
        /// <seealso cref="Default.FontFamily"/>
        /// <seealso cref="Default.FontSize"/>
        /// <seealso cref="Default.FontColor"/>
        /// <seealso cref="Default.FontBold"/>
        /// <seealso cref="Default.FontUnderline"/>
        /// <seealso cref="Default.FontItalic"/>
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
                    throw new ArgumentNullException("Uninitialized FontSpec in Scale");
                }

                this._fontSpec = value;
            }
        }

        /// <summary>
        /// The format of the <see cref="Axis"/> tic labels.
        /// </summary>
        /// <remarks>
        /// This property may be a date format or a numeric format, depending on the setting of
        /// <see cref="Type">Scale.Type</see>.
        /// This property may be set automatically by ZedGraph, depending on the state of
        /// <see cref="FormatAuto"/>.
        /// </remarks>
        /// <value>The format string conforms to the
        /// <see cref="System.Globalization.DateTimeFormatInfo" /> for date formats, and
        /// <see cref="System.Globalization.NumberFormatInfo" /> for numeric formats.
        /// </value>
        /// <seealso cref="Mag"/>
        /// <seealso cref="FormatAuto"/>
        /// <seealso cref="FontSpec"/>
        // /// <seealso cref="NumDec"/>
        public string Format
        {
            get
            {
                return this._format;
            }

            set
            {
                this._format = value;
                this._formatAuto = false;
            }
        }

        /// <summary>
        /// Determines whether or not the scale label format <see cref="Format"/>
        /// is determined automatically based on the range of data values.
        /// </summary>
        /// <remarks>
        /// This value will be set to false if
        /// <see cref="Format"/> is manually changed.
        /// </remarks>
        /// <value>true if <see cref="Format"/> will be set automatically, false
        /// if it is to be set manually by the user</value>
        /// <seealso cref="Mag"/>
        /// <seealso cref="Format"/>
        /// <seealso cref="FontSpec"/>
        public bool FormatAuto
        {
            get
            {
                return this._formatAuto;
            }

            set
            {
                this._formatAuto = value;
            }
        }

        /// <summary>
        /// Gets a value that indicates if this <see cref="Scale" /> is of any of the
        /// ordinal types in the <see cref="AxisType" /> enumeration.
        /// </summary>
        /// <seealso cref="Type" />
        public bool IsAnyOrdinal
        {
            get
            {
                AxisType type = this.Type;

                return type == AxisType.Ordinal || type == AxisType.Text || type == AxisType.LinearAsOrdinal
                       || type == AxisType.DateAsOrdinal;
            }
        }

        /// <summary>
        /// True if this scale is <see cref="AxisType.Date" />, false otherwise.
        /// </summary>
        public bool IsDate
        {
            get
            {
                return this is DateScale;
            }
        }

        /// <summary>
        /// True if this scale is <see cref="AxisType.Exponent" />, false otherwise.
        /// </summary>
        public bool IsExponent
        {
            get
            {
                return this is ExponentScale;
            }
        }

        /// <summary>
        /// Gets or sets a value that causes the axis scale labels and title to appear on the
        /// opposite side of the axis.
        /// </summary>
        /// <remarks>
        /// For example, setting this flag to true for the <see cref="YAxis"/> will shift the
        /// axis labels and title to the right side of the <see cref="YAxis"/> instead of the
        /// normal left-side location.  Set this property to true for the <see cref="XAxis" />,
        /// and set the <see cref="Axis.Cross"/> property for the <see cref="XAxis"/> to an arbitrarily
        /// large value (assuming <see cref="IsReverse"/> is false for the <see cref="YAxis" />) in
        /// order to have the <see cref="XAxis"/> appear at the top of the <see cref="Chart.Rect" />.
        /// </remarks>
        /// <seealso cref="IsReverse"/>
        /// <seealso cref="Axis.Cross"/>
        public bool IsLabelsInside
        {
            get
            {
                return this._isLabelsInside;
            }

            set
            {
                this._isLabelsInside = value;
            }
        }

        /// <summary>
        /// True if this scale is <see cref="AxisType.Log" />, false otherwise.
        /// </summary>
        public bool IsLog
        {
            get
            {
                return this is LogScale;
            }
        }

        /// <summary>
        /// True if this scale is <see cref="AxisType.Ordinal" />, false otherwise.
        /// </summary>
        /// <remarks>
        /// Note that this is only true for an actual <see cref="OrdinalScale" /> class.
        /// This property will be false for other ordinal types such as
        /// <see cref="AxisType.Text" />, <see cref="AxisType.LinearAsOrdinal" />,
        /// or <see cref="AxisType.DateAsOrdinal" />.  Use the <see cref="IsAnyOrdinal" />
        /// as a "catchall" for all ordinal type axes.
        /// </remarks>
        public bool IsOrdinal
        {
            get
            {
                return this is OrdinalScale;
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="bool"/> value that determines if ZedGraph will check to
        /// see if the <see cref="Axis"/> scale labels are close enough to overlap.  If so,
        /// ZedGraph will adjust the step size to prevent overlap.
        /// </summary>
        /// <remarks>
        /// The process of checking for overlap is done during the <see cref="GraphPane.AxisChange()"/>
        /// method call, and affects the selection of the major step size (<see cref="MajorStep"/>).
        /// </remarks>
        /// <value> boolean value; true to check for overlap, false otherwise</value>
        public bool IsPreventLabelOverlap
        {
            get
            {
                return this._isPreventLabelOverlap;
            }

            set
            {
                this._isPreventLabelOverlap = value;
            }
        }

        /// <summary>
        /// Determines if the scale values are reversed for this <see cref="Axis"/>
        /// </summary>
        /// <value>true for the X values to decrease to the right or the Y values to
        /// decrease upwards, false otherwise</value>
        /// <seealso cref="ZedGraph.Scale.Default.IsReverse"/>.
        public bool IsReverse
        {
            get
            {
                return this._isReverse;
            }

            set
            {
                this._isReverse = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that causes the scale label that is located at the <see cref="Axis.Cross" />
        /// value for this <see cref="Axis"/> to be hidden.
        /// </summary>
        /// <remarks>
        /// For axes that have an active <see cref="Axis.Cross"/> setting (e.g., <see cref="Axis.CrossAuto"/>
        /// is false), the scale label at the <see cref="Axis.Cross" /> value is overlapped by opposing axes.
        /// Use this property to hide the scale label to avoid the overlap.
        /// </remarks>
        public bool IsSkipCrossLabel
        {
            get
            {
                return this._isSkipCrossLabel;
            }

            set
            {
                this._isSkipCrossLabel = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that causes the first scale label for this <see cref="Axis"/> to be
        /// hidden.
        /// </summary>
        /// <remarks>
        /// Often, for axis that have an active <see cref="Axis.Cross"/> setting (e.g., <see cref="Axis.CrossAuto"/>
        /// is false), the first and/or last scale label are overlapped by opposing axes.  Use this
        /// property to hide the first scale label to avoid the overlap.  Note that setting this value
        /// to true will hide any scale label that appears within <see cref="Scale.Default.EdgeTolerance"/> of the
        /// beginning of the <see cref="Axis"/>.
        /// </remarks>
        public bool IsSkipFirstLabel
        {
            get
            {
                return this._isSkipFirstLabel;
            }

            set
            {
                this._isSkipFirstLabel = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that causes the last scale label for this <see cref="Axis"/> to be
        /// hidden.
        /// </summary>
        /// <remarks>
        /// Often, for axis that have an active <see cref="Axis.Cross"/> setting (e.g., <see cref="Axis.CrossAuto"/>
        /// is false), the first and/or last scale label are overlapped by opposing axes.  Use this
        /// property to hide the last scale label to avoid the overlap.  Note that setting this value
        /// to true will hide any scale label that appears within <see cref="Scale.Default.EdgeTolerance"/> of the
        /// end of the <see cref="Axis"/>.
        /// </remarks>
        public bool IsSkipLastLabel
        {
            get
            {
                return this._isSkipLastLabel;
            }

            set
            {
                this._isSkipLastLabel = value;
            }
        }

        /// <summary>
        /// True if this scale is <see cref="AxisType.Text" />, false otherwise.
        /// </summary>
        public bool IsText
        {
            get
            {
                return this is TextScale;
            }
        }

        /// <summary>
        /// Determines if powers-of-ten notation will be used for the numeric value labels.
        /// </summary>
        /// <remarks>
        /// The powers-of-ten notation is just the text "10" followed by a superscripted value
        /// indicating the magnitude.  This mode is only valid for log scales (see
        /// <see cref="IsLog"/> and <see cref="Type"/>).
        /// </remarks>
        /// <value> boolean value; true to show the title as a power of ten, false to
        /// show a regular numeric value (e.g., "0.01", "10", "1000")</value>
        public bool IsUseTenPower
        {
            get
            {
                return this._isUseTenPower;
            }

            set
            {
                this._isUseTenPower = value;
            }
        }

        /// <summary>
        /// Gets or sets a property that determines whether or not the scale values will be shown.
        /// </summary>
        /// <value>true to show the scale values, false otherwise</value>
        /// <seealso cref="Axis.IsVisible"/>.
        public bool IsVisible
        {
            get
            {
                return this._isVisible;
            }

            set
            {
                this._isVisible = value;
            }
        }

        /// <summary>
        /// The gap between the scale labels and the tics.
        /// </summary>
        public float LabelGap
        {
            get
            {
                return this._labelGap;
            }

            set
            {
                this._labelGap = value;
            }
        }

        /// <summary>
        /// The magnitude multiplier for scale values.
        /// </summary>
        /// <remarks>
        /// This is used to limit
        /// the size of the displayed value labels.  For example, if the value
        /// is really 2000000, then the graph will display 2000 with a 10^3
        /// magnitude multiplier.  This value can be determined automatically
        /// depending on the state of <see cref="MagAuto"/>.
        /// If this value is set manually by the user,
        /// then <see cref="MagAuto"/> will also be set to false.
        /// </remarks>
        /// <value>The magnitude multiplier (power of 10) for the scale
        /// value labels</value>
        /// <seealso cref="AxisLabel.IsOmitMag"/>
        /// <seealso cref="Axis.Title"/>
        /// <seealso cref="Format"/>
        /// <seealso cref="FontSpec"/>
        // /// <seealso cref="NumDec"/>
        public int Mag
        {
            get
            {
                return this._mag;
            }

            set
            {
                this._mag = value;
                this._magAuto = false;
            }
        }

        /// <summary>
        /// Determines whether the <see cref="Mag"/> value will be set
        /// automatically based on the data, or manually by the user.
        /// </summary>
        /// <remarks>
        /// If the user manually sets the <see cref="Mag"/> value, then this
        /// flag will be set to false.
        /// </remarks>
        /// <value>true to have <see cref="Mag"/> set automatically,
        /// false otherwise</value>
        /// <seealso cref="AxisLabel.IsOmitMag"/>
        /// <seealso cref="Axis.Title"/>
        /// <seealso cref="Mag"/>
        public bool MagAuto
        {
            get
            {
                return this._magAuto;
            }

            set
            {
                this._magAuto = value;
            }
        }

        /// <summary>
        /// Gets or sets the scale step size for this <see cref="Scale" /> (the increment between
        /// labeled axis values).
        /// </summary>
        /// <remarks>
        /// This value can be set
        /// automatically based on the state of <see cref="MajorStepAuto"/>.  If
        /// this value is set manually, then <see cref="MajorStepAuto"/> will
        /// also be set to false.  This value is ignored for <see cref="AxisType.Log"/>
        /// axes.  For <see cref="AxisType.Date"/> axes, this
        /// value is defined in units of <see cref="MajorUnit"/>.
        /// </remarks>
        /// <value> The value is defined in user scale units </value>
        /// <seealso cref="Min"/>
        /// <seealso cref="Max"/>
        /// <seealso cref="MinorStep"/>
        /// <seealso cref="MajorStepAuto"/>
        /// <seealso cref="ZedGraph.Scale.Default.TargetXSteps"/>
        /// <seealso cref="ZedGraph.Scale.Default.TargetYSteps"/>
        /// <seealso cref="ZedGraph.Scale.Default.ZeroLever"/>
        /// <seealso cref="ZedGraph.Scale.Default.MaxTextLabels"/>
        public double MajorStep
        {
            get
            {
                return this._majorStep;
            }

            set
            {
                if (value < 1e-300)
                {
                    this._majorStepAuto = true;
                }
                else
                {
                    this._majorStep = value;
                    this._majorStepAuto = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that determines whether or not the scale step size <see cref="MajorStep"/>
        /// is set automatically.
        /// </summary>
        /// <remarks>
        /// This value will be set to false if
        /// <see cref="MajorStep"/> is manually changed.
        /// </remarks>
        /// <value>true for automatic mode, false for manual mode</value>
        /// <seealso cref="MajorStep"/>
        public bool MajorStepAuto
        {
            get
            {
                return this._majorStepAuto;
            }

            set
            {
                this._majorStepAuto = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of units used for the major step size (<see cref="MajorStep"/>).
        /// </summary>
        /// <remarks>
        /// This unit type only applies to Date-Time axes (<see cref="AxisType.Date"/> = true).
        /// The axis is set to date type with the <see cref="Type"/> property.
        /// The unit types are defined as <see cref="DateUnit"/>.
        /// </remarks>
        /// <value> The value is a <see cref="DateUnit"/> enum type </value>
        /// <seealso cref="Min"/>
        /// <seealso cref="Max"/>
        /// <seealso cref="MajorStep"/>
        /// <seealso cref="MinorStep"/>
        /// <seealso cref="MajorStepAuto"/>
        public DateUnit MajorUnit
        {
            get
            {
                return this._majorUnit;
            }

            set
            {
                this._majorUnit = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum scale value for this <see cref="Scale" />.
        /// </summary>
        /// <remarks>
        /// This value can be set
        /// automatically based on the state of <see cref="MaxAuto"/>.  If
        /// this value is set manually, then <see cref="MaxAuto"/> will
        /// also be set to false.
        /// </remarks>
        /// <value> The value is defined in user scale units for <see cref="AxisType.Log"/>
        /// and <see cref="AxisType.Linear"/> axes. For <see cref="AxisType.Text"/>
        /// and <see cref="AxisType.Ordinal"/> axes,
        /// this value is an ordinal starting with 1.0.  For <see cref="AxisType.Date"/>
        /// axes, this value is in XL Date format (see <see cref="XDate"/>, which is the
        /// number of days since the reference date of January 1, 1900.</value>
        /// <seealso cref="Min"/>
        /// <seealso cref="MajorStep"/>
        /// <seealso cref="MinorStep"/>
        /// <seealso cref="MaxAuto"/>
        public virtual double Max
        {
            get
            {
                return this._max;
            }

            set
            {
                this._max = value;
                this._maxAuto = false;
            }
        }

        /// <summary>
        /// Gets or sets a value that determines whether or not the maximum scale value <see cref="Max"/>
        /// is set automatically.
        /// </summary>
        /// <remarks>
        /// This value will be set to false if
        /// <see cref="Max"/> is manually changed.
        /// </remarks>
        /// <value>true for automatic mode, false for manual mode</value>
        /// <seealso cref="Max"/>
        public bool MaxAuto
        {
            get
            {
                return this._maxAuto;
            }

            set
            {
                this._maxAuto = value;
            }
        }

        /// <summary> Gets or sets the "grace" value applied to the maximum data range.
        /// </summary>
        /// <remarks>
        /// This values determines how much extra space is left after the last data value.
        /// This value is
        /// expressed as a fraction of the total data range.  For example, assume the data
        /// range is from 4.0 to 16.0, leaving a range of 12.0.  If MaxGrace is set to
        /// 0.1, then 10% of the range, or 1.2 will be added to the maximum data value.
        /// The scale will then be ranged to cover at least 4.0 to 17.2.
        /// </remarks>
        /// <seealso cref="Max"/>
        /// <seealso cref="ZedGraph.Scale.Default.MaxGrace"/>
        /// <seealso cref="MinGrace"/>
        public double MaxGrace
        {
            get
            {
                return this._maxGrace;
            }

            set
            {
                this._maxGrace = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum scale value for this <see cref="Scale" />.
        /// </summary>
        /// <remarks>This value can be set
        /// automatically based on the state of <see cref="MinAuto"/>.  If
        /// this value is set manually, then <see cref="MinAuto"/> will
        /// also be set to false.
        /// </remarks>
        /// <value> The value is defined in user scale units for <see cref="AxisType.Log"/>
        /// and <see cref="AxisType.Linear"/> axes. For <see cref="AxisType.Text"/>
        /// and <see cref="AxisType.Ordinal"/> axes,
        /// this value is an ordinal starting with 1.0.  For <see cref="AxisType.Date"/>
        /// axes, this value is in XL Date format (see <see cref="XDate"/>, which is the
        /// number of days since the reference date of January 1, 1900.</value>
        /// <seealso cref="Max"/>
        /// <seealso cref="MajorStep"/>
        /// <seealso cref="MinorStep"/>
        /// <seealso cref="MinAuto"/>
        public virtual double Min
        {
            get
            {
                return this._min;
            }

            set
            {
                this._min = value;
                this._minAuto = false;
            }
        }

        /// <summary>
        /// Gets or sets a value that determines whether or not the minimum scale value <see cref="Min"/>
        /// is set automatically.
        /// </summary>
        /// <remarks>
        /// This value will be set to false if
        /// <see cref="Min"/> is manually changed.
        /// </remarks>
        /// <value>true for automatic mode, false for manual mode</value>
        /// <seealso cref="Min"/>
        public bool MinAuto
        {
            get
            {
                return this._minAuto;
            }

            set
            {
                this._minAuto = value;
            }
        }

        /// <summary> Gets or sets the "grace" value applied to the minimum data range.
        /// </summary>
        /// <remarks>
        /// This value is
        /// expressed as a fraction of the total data range.  For example, assume the data
        /// range is from 4.0 to 16.0, leaving a range of 12.0.  If MinGrace is set to
        /// 0.1, then 10% of the range, or 1.2 will be subtracted from the minimum data value.
        /// The scale will then be ranged to cover at least 2.8 to 16.0.
        /// </remarks>
        /// <seealso cref="Min"/>
        /// <seealso cref="ZedGraph.Scale.Default.MinGrace"/>
        /// <seealso cref="MaxGrace"/>
        public double MinGrace
        {
            get
            {
                return this._minGrace;
            }

            set
            {
                this._minGrace = value;
            }
        }

        /// <summary>
        /// Gets or sets the scale minor step size for this <see cref="Scale" /> (the spacing between
        /// minor tics).
        /// </summary>
        /// <remarks>This value can be set
        /// automatically based on the state of <see cref="MinorStepAuto"/>.  If
        /// this value is set manually, then <see cref="MinorStepAuto"/> will
        /// also be set to false.  This value is ignored for <see cref="AxisType.Log"/> and
        /// <see cref="AxisType.Text"/> axes.  For <see cref="AxisType.Date"/> axes, this
        /// value is defined in units of <see cref="MinorUnit"/>.
        /// </remarks>
        /// <value> The value is defined in user scale units </value>
        /// <seealso cref="Min"/>
        /// <seealso cref="Max"/>
        /// <seealso cref="MajorStep"/>
        /// <seealso cref="MinorStepAuto"/>
        public double MinorStep
        {
            get
            {
                return this._minorStep;
            }

            set
            {
                if (value < 1e-300)
                {
                    this._minorStepAuto = true;
                }
                else
                {
                    this._minorStep = value;
                    this._minorStepAuto = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that determines whether or not the minor scale step size <see cref="MinorStep"/>
        /// is set automatically.
        /// </summary>
        /// <remarks>
        /// This value will be set to false if
        /// <see cref="MinorStep"/> is manually changed.
        /// </remarks>
        /// <value>true for automatic mode, false for manual mode</value>
        /// <seealso cref="MinorStep"/>
        public bool MinorStepAuto
        {
            get
            {
                return this._minorStepAuto;
            }

            set
            {
                this._minorStepAuto = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of units used for the minor step size (<see cref="MinorStep"/>).
        /// </summary>
        /// <remarks>
        /// This unit type only applies to Date-Time axes (<see cref="AxisType.Date"/> = true).
        /// The axis is set to date type with the <see cref="Type"/> property.
        /// The unit types are defined as <see cref="DateUnit"/>.
        /// </remarks>
        /// <value> The value is a <see cref="DateUnit"/> enum type </value>
        /// <seealso cref="Min"/>
        /// <seealso cref="Max"/>
        /// <seealso cref="MajorStep"/>
        /// <seealso cref="MinorStep"/>
        /// <seealso cref="MinorStepAuto"/>
        public DateUnit MinorUnit
        {
            get
            {
                return this._minorUnit;
            }

            set
            {
                this._minorUnit = value;
            }
        }

        /// <summary>
        /// The text labels for this <see cref="Axis"/>.
        /// </summary>
        /// <remarks>
        /// This property is only
        /// applicable if <see cref="Type"/> is set to <see cref="AxisType.Text"/>.
        /// </remarks>
        public string[] TextLabels
        {
            get
            {
                return this._textLabels;
            }

            set
            {
                this._textLabels = value;
            }
        }

        /// <summary>
        /// Get an <see cref="AxisType" /> enumeration that indicates the type of this scale.
        /// </summary>
        public abstract AxisType Type { get; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the major unit multiplier for this scale type, if any.
        /// </summary>
        /// <remarks>The major unit multiplier will correct the units of
        /// <see cref="MajorStep" /> to match the units of <see cref="Min" />
        /// and <see cref="Max" />.  This reflects the setting of
        /// <see cref="MajorUnit" />.
        /// </remarks>
        internal virtual double MajorUnitMultiplier
        {
            get
            {
                return 1.0;
            }
        }

        /// <summary>
        /// Gets the minor unit multiplier for this scale type, if any.
        /// </summary>
        /// <remarks>The minor unit multiplier will correct the units of
        /// <see cref="MinorStep" /> to match the units of <see cref="Min" />
        /// and <see cref="Max" />.  This reflects the setting of
        /// <see cref="MinorUnit" />.
        /// </remarks>
        internal virtual double MinorUnitMultiplier
        {
            get
            {
                return 1.0;
            }
        }

        /// <summary>
        /// Gets or sets the linearized version of the <see cref="Max" /> scale range.
        /// </summary>
        /// <remarks>
        /// This value is valid at any time, whereas <see cref="_maxLinTemp" /> is an optimization
        /// pre-set that is only valid during draw operations.
        /// </remarks>
        internal double _maxLinearized
        {
            get
            {
                return this.Linearize(this._max);
            }

            set
            {
                this._max = this.DeLinearize(value);
            }
        }

        /// <summary>
        /// Gets or sets the linearized version of the <see cref="Min" /> scale range.
        /// </summary>
        /// <remarks>
        /// This value is valid at any time, whereas <see cref="_minLinTemp" /> is an optimization
        /// pre-set that is only valid during draw operations.
        /// </remarks>
        internal double _minLinearized
        {
            get
            {
                return this.Linearize(this._min);
            }

            set
            {
                this._min = this.DeLinearize(value);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Calculate an exponential in a safe manner to avoid math exceptions
        /// </summary>
        /// <param name="x">
        /// The value for which the exponential is to be calculated
        /// </param>
        /// <param name="exponent">
        /// The exponent value to use for calculating the exponential.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public static double SafeExp(double x, double exponent)
        {
            if (x > 1.0e-20)
            {
                return Math.Pow(x, exponent);
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Calculate a base 10 logarithm in a safe manner to avoid math exceptions
        /// </summary>
        /// <param name="x">
        /// The value for which the logarithm is to be calculated
        /// </param>
        /// <returns>
        /// The value of the logarithm, or 0 if the <paramref name="x"/>
        /// argument was negative or zero
        /// </returns>
        public static double SafeLog(double x)
        {
            if (x > 1.0e-20)
            {
                return Math.Log10(x);
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Calculate the maximum number of labels that will fit on this axis.
        /// </summary>
        /// <remarks>
        /// This method works for
        /// both X and Y direction axes, and it works for angled text (assuming that a bounding box
        /// is an appropriate measure).  Technically, labels at 45 degree angles could fit better than
        /// the return value of this method since the bounding boxes can overlap without the labels actually
        /// overlapping.
        /// </remarks>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object
        /// associated with this <see cref="Axis"/>
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CalcMaxLabels(Graphics g, GraphPane pane, float scaleFactor)
        {
            SizeF size = this.GetScaleMaxSpace(g, pane, scaleFactor, false);

            // The font angles are already set such that the Width is parallel to the appropriate (X or Y)
            // axis.  Therefore, we always use size.Width.
            // use the minimum of 1/4 the max Width or 1 character space
            // 			double allowance = this.Scale.FontSpec.GetWidth( g, scaleFactor );
            // 			if ( allowance > size.Width / 4 )
            // 				allowance = size.Width / 4;
            float maxWidth = 1000;
            float temp = 1000;
            float costh = (float)Math.Abs(Math.Cos(this._fontSpec.Angle * Math.PI / 180.0));
            float sinth = (float)Math.Abs(Math.Sin(this._fontSpec.Angle * Math.PI / 180.0));

            if (costh > 0.001)
            {
                maxWidth = size.Width / costh;
            }

            if (sinth > 0.001)
            {
                temp = size.Height / sinth;
            }

            if (temp < maxWidth)
            {
                maxWidth = temp;
            }

            // maxWidth = size.Width;
            /*
						if ( this is XAxis )
							// Add an extra character width to leave a minimum of 1 character space between labels
							maxWidth = size.Width + this.Scale.FontSpec.GetWidth( g, scaleFactor );
						else
							// For vertical spacing, we only need 1/2 character
							maxWidth = size.Width + this.Scale.FontSpec.GetWidth( g, scaleFactor ) / 2.0;
			*/
            if (maxWidth <= 0)
            {
                maxWidth = 1;
            }

            // Calculate the maximum number of labels
            double width;
            RectangleF chartRect = pane.Chart._rect;
            if (this._ownerAxis is XAxis || this._ownerAxis is X2Axis)
            {
                width = (chartRect.Width == 0) ? pane.Rect.Width * 0.75 : chartRect.Width;
            }
            else
            {
                width = (chartRect.Height == 0) ? pane.Rect.Height * 0.75 : chartRect.Height;
            }

            int maxLabels = (int)(width / maxWidth);
            if (maxLabels <= 0)
            {
                maxLabels = 1;
            }

            return maxLabels;
        }

        /// <summary>
        /// Create a new clone of the current item, with a new owner assignment
        /// </summary>
        /// <param name="owner">
        /// The new <see cref="Axis"/> instance that will be
        /// the owner of the new Scale
        /// </param>
        /// <returns>
        /// A new <see cref="Scale"/> clone.
        /// </returns>
        public abstract Scale Clone(Axis owner);

        /*
	#region events

		/// <summary>
		/// A delegate that allows full custom formatting of the Axis labels
		/// </summary>
		/// <param name="pane">The <see cref="GraphPane" /> for which the label is to be
		/// formatted</param>
		/// <param name="axis">The <see cref="Axis" /> for which the label is to be formatted</param>
		/// <param name="val">The value to be formatted</param>
		/// <param name="index">The zero-based index of the label to be formatted</param>
		/// <returns>
		/// A string value representing the label, or null if the ZedGraph should go ahead
		/// and generate the label according to the current settings</returns>
		/// <seealso cref="ScaleFormatEvent" />
		public delegate string ScaleFormatHandler( GraphPane pane, Axis axis, double val, int index );

		/// <summary>
		/// Subscribe to this event to handle custom formatting of the scale labels.
		/// </summary>
		public event ScaleFormatHandler ScaleFormatEvent;

	#endregion
*/

        /// <summary>
        /// Convert a value from its linear equivalent to its actual scale value
        /// for this type of scale.
        /// </summary>
        /// <remarks>
        /// The default behavior is to just return the value unchanged.  However,
        /// for <see cref="AxisType.Log"/> and <see cref="AxisType.Exponent"/>,
        /// it returns the anti-log or inverse-power equivalent.
        /// </remarks>
        /// <param name="val">
        /// The value to be converted
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public virtual double DeLinearize(double val)
        {
            return val;
        }

        /// <summary>
        /// Determine the width, in pixel units, of each bar cluster including
        /// the cluster gaps and bar gaps.
        /// </summary>
        /// <remarks>
        /// This method uses the <see cref="BarSettings.ClusterScaleWidth"/> for
        /// non-ordinal axes, or a cluster width of 1.0 for ordinal axes.
        /// </remarks>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object
        /// associated with this <see cref="Axis"/>
        /// </param>
        /// <returns>
        /// The width of each bar cluster, in pixel units
        /// </returns>
        public float GetClusterWidth(GraphPane pane)
        {
            double basisVal = this._min;
            return
                Math.Abs(
                    this.Transform(basisVal + (this.IsAnyOrdinal ? 1.0 : pane._barSettings._clusterScaleWidth))
                    - this.Transform(basisVal));
        }

        /// <summary>
        /// Calculates the cluster width, in pixels, by transforming the specified
        /// clusterScaleWidth.
        /// </summary>
        /// <param name="clusterScaleWidth">
        /// The width in user scale units of each
        /// bar cluster
        /// </param>
        /// <returns>
        /// The equivalent pixel size of the bar cluster
        /// </returns>
        public float GetClusterWidth(double clusterScaleWidth)
        {
            double basisVal = this._min;
            return Math.Abs(this.Transform(basisVal + clusterScaleWidth) - this.Transform(basisVal));
        }

        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> instance with the data needed to
        /// serialize the target object
        /// </summary>
        /// <remarks>
        /// You MUST set the _ownerAxis property after deserializing a BarSettings object.
        /// </remarks>
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
            info.AddValue("min", this._min);
            info.AddValue("max", this._max);
            info.AddValue("majorStep", this._majorStep);
            info.AddValue("minorStep", this._minorStep);
            info.AddValue("exponent", this._exponent);
            info.AddValue("baseTic", this._baseTic);

            info.AddValue("minAuto", this._minAuto);
            info.AddValue("maxAuto", this._maxAuto);
            info.AddValue("majorStepAuto", this._majorStepAuto);
            info.AddValue("minorStepAuto", this._minorStepAuto);
            info.AddValue("magAuto", this._magAuto);
            info.AddValue("formatAuto", this._formatAuto);

            info.AddValue("minGrace", this._minGrace);
            info.AddValue("maxGrace", this._maxGrace);

            info.AddValue("mag", this._mag);
            info.AddValue("isReverse", this._isReverse);
            info.AddValue("isPreventLabelOverlap", this._isPreventLabelOverlap);
            info.AddValue("isUseTenPower", this._isUseTenPower);
            info.AddValue("isVisible", this._isVisible);
            info.AddValue("isSkipFirstLabel", this._isSkipFirstLabel);
            info.AddValue("isSkipLastLabel", this._isSkipLastLabel);
            info.AddValue("isSkipCrossLabel", this._isSkipCrossLabel);

            info.AddValue("textLabels", this._textLabels);
            info.AddValue("format", this._format);

            info.AddValue("majorUnit", this._majorUnit);
            info.AddValue("minorUnit", this._minorUnit);

            info.AddValue("isLabelsInside", this._isLabelsInside);
            info.AddValue("align", this._align);
            info.AddValue("alignH", this._alignH);
            info.AddValue("fontSpec", this._fontSpec);
            info.AddValue("labelGap", this._labelGap);
        }

        /// <summary>
        /// Convert a value to its linear equivalent for this type of scale.
        /// </summary>
        /// <remarks>
        /// The default behavior is to just return the value unchanged.  However,
        /// for <see cref="AxisType.Log"/> and <see cref="AxisType.Exponent"/>,
        /// it returns the log or power equivalent.
        /// </remarks>
        /// <param name="val">
        /// The value to be converted
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public virtual double Linearize(double val)
        {
            return val;
        }

        /// <summary>
        /// Transform the coordinate value from user coordinates (scale value)
        /// to graphics device coordinates (pixels).
        /// </summary>
        /// <remarks>
        /// Assumes that the origin
        /// has been set to the "left" of this axis, facing from the label side.
        /// Note that the left side corresponds to the scale minimum for the X and
        /// Y2 axes, but it is the scale maximum for the Y axis.
        /// This method takes into
        /// account the scale range (<see cref="Min"/> and <see cref="Max"/>),
        /// logarithmic state (<see cref="IsLog"/>), scale reverse state
        /// (<see cref="IsReverse"/>) and axis type (<see cref="XAxis"/>,
        /// <see cref="YAxis"/>, or <see cref="Y2Axis"/>).  Note that
        /// the <see cref="Chart.Rect"/> must be valid, and
        /// <see cref="SetupScaleData"/> must be called for the
        /// current configuration before using this method.
        /// </remarks>
        /// <param name="x">
        /// The coordinate value, in linearized user scale units, to
        /// be transformed
        /// </param>
        /// <returns>
        /// the coordinate value transformed to screen coordinates
        /// for use in calling the <see cref="Draw"/> method
        /// </returns>
        public float LocalTransform(double x)
        {
            // Must take into account Log, and Reverse Axes
            double ratio;
            float rv;

            // Coordinate values for log scales are already in exponent form, so no need
            // to take the log here
            ratio = (x - this._minLinTemp) / (this._maxLinTemp - this._minLinTemp);

            if (this._isReverse == (this._ownerAxis is YAxis || this._ownerAxis is X2Axis))
            {
                rv = (float)((this._maxPix - this._minPix) * ratio);
            }
            else
            {
                rv = (float)((this._maxPix - this._minPix) * (1.0F - ratio));
            }

            return rv;
        }

        /// <summary>
        /// A construction method that creates a new <see cref="Scale"/> object using the
        /// properties of an existing <see cref="Scale"/> object, but specifying a new
        /// <see cref="AxisType"/>.
        /// </summary>
        /// <remarks>
        /// This constructor is used to change the type of an existing <see cref="Axis"/>.
        /// By specifying the old <see cref="Scale"/> object, you are giving a set of properties
        /// (which encompasses all fields associated with the scale, since the derived types
        /// have no fields) to be used in creating a new <see cref="Scale"/> object, only this
        /// time having the newly specified object type.
        /// </remarks>
        /// <param name="oldScale">
        /// The existing <see cref="Scale"/> object from which to
        /// copy the field data.
        /// </param>
        /// <param name="type">
        /// An <see cref="AxisType"/> representing the type of derived type
        /// of new <see cref="Scale"/> object to create.
        /// </param>
        /// <returns>
        /// The new <see cref="Scale"/> object.
        /// </returns>
        public Scale MakeNewScale(Scale oldScale, AxisType type)
        {
            switch (type)
            {
                case AxisType.Linear:
                    return new LinearScale(oldScale, this._ownerAxis);
                case AxisType.Date:
                    return new DateScale(oldScale, this._ownerAxis);
                case AxisType.Log:
                    return new LogScale(oldScale, this._ownerAxis);
                case AxisType.Exponent:
                    return new ExponentScale(oldScale, this._ownerAxis);
                case AxisType.Ordinal:
                    return new OrdinalScale(oldScale, this._ownerAxis);
                case AxisType.Text:
                    return new TextScale(oldScale, this._ownerAxis);
                case AxisType.DateAsOrdinal:
                    return new DateAsOrdinalScale(oldScale, this._ownerAxis);
                case AxisType.LinearAsOrdinal:
                    return new LinearAsOrdinalScale(oldScale, this._ownerAxis);
                default:
                    throw new Exception("Implementation Error: Invalid AxisType");
            }
        }

        /// <summary>
        /// Select a reasonable scale given a range of data values.
        /// </summary>
        /// <remarks>
        /// The scale range is chosen
        /// based on increments of 1, 2, or 5 (because they are even divisors of 10).  This
        /// routine honors the <see cref="MinAuto"/>, <see cref="MaxAuto"/>,
        /// and <see cref="MajorStepAuto"/> autorange settings as well as the <see cref="IsLog"/>
        /// setting.  In the event that any of the autorange settings are false, the
        /// corresponding <see cref="Min"/>, <see cref="Max"/>, or <see cref="MajorStep"/>
        /// setting is explicitly honored, and the remaining autorange settings (if any) will
        /// be calculated to accomodate the non-autoranged values.  The basic defaults for
        /// scale selection are defined using <see cref="Default.ZeroLever"/>,
        /// <see cref="Default.TargetXSteps"/>, and <see cref="Default.TargetYSteps"/>
        /// from the <see cref="Default"/> default class.
        /// <para>
        /// On Exit:
        /// </para>
        /// <para>
        /// <see cref="Min"/> is set to scale minimum (if <see cref="MinAuto"/> = true)
        /// </para>
        /// <para>
        /// <see cref="Max"/> is set to scale maximum (if <see cref="MaxAuto"/> = true)
        /// </para>
        /// <para>
        /// <see cref="MajorStep"/> is set to scale step size (if <see cref="MajorStepAuto"/> = true)
        /// </para>
        /// <para>
        /// <see cref="MinorStep"/> is set to scale minor step size (if <see cref="MinorStepAuto"/> = true)
        /// </para>
        /// <para>
        /// <see cref="Mag"/> is set to a magnitude multiplier according to the data
        /// </para>
        /// <para>
        /// <see cref="Format"/> is set to the display format for the values (this controls the
        /// number of decimal places, whether there are thousands separators, currency types, etc.)
        /// </para>
        /// </remarks>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object
        /// associated with this <see cref="Axis"/>
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
        public virtual void PickScale(GraphPane pane, Graphics g, float scaleFactor)
        {
            double minVal = this._rangeMin;
            double maxVal = this._rangeMax;

            // Make sure that minVal and maxVal are legitimate values
            if (double.IsInfinity(minVal) || double.IsNaN(minVal) || minVal == double.MaxValue)
            {
                minVal = 0.0;
            }

            if (double.IsInfinity(maxVal) || double.IsNaN(maxVal) || maxVal == double.MaxValue)
            {
                maxVal = 0.0;
            }

            // if the scales are autoranged, use the actual data values for the range
            double range = maxVal - minVal;

            // "Grace" is applied to the numeric axis types only
            bool numType = !this.IsAnyOrdinal;

            // For autoranged values, assign the value.  If appropriate, adjust the value by the
            // "Grace" value.
            if (this._minAuto)
            {
                this._min = minVal;

                // Do not let the grace value extend the axis below zero when all the values were positive
                if (numType && (this._min < 0 || minVal - this._minGrace * range >= 0.0))
                {
                    this._min = minVal - this._minGrace * range;
                }
            }

            if (this._maxAuto)
            {
                this._max = maxVal;

                // Do not let the grace value extend the axis above zero when all the values were negative
                if (numType && (this._max > 0 || maxVal + this._maxGrace * range <= 0.0))
                {
                    this._max = maxVal + this._maxGrace * range;
                }
            }

            if (this._max == this._min && this._maxAuto && this._minAuto)
            {
                if (Math.Abs(this._max) > 1e-100)
                {
                    this._max *= this._min < 0 ? 0.95 : 1.05;
                    this._min *= this._min < 0 ? 1.05 : 0.95;
                }
                else
                {
                    this._max = 1.0;
                    this._min = -1.0;
                }
            }

            if (this._max <= this._min)
            {
                if (this._maxAuto)
                {
                    this._max = this._min + 1.0;
                }
                else if (this._minAuto)
                {
                    this._min = this._max - 1.0;
                }
            }
        }

        /// <summary>
        /// Reverse transform the user coordinates (scale value)
        /// given a graphics device coordinate (pixels).
        /// </summary>
        /// <remarks>
        /// This method takes into
        /// account the scale range (<see cref="Min"/> and <see cref="Max"/>),
        /// logarithmic state (<see cref="IsLog"/>), scale reverse state
        /// (<see cref="IsReverse"/>) and axis type (<see cref="XAxis"/>,
        /// <see cref="YAxis"/>, or <see cref="Y2Axis"/>).
        /// Note that the <see cref="Chart.Rect"/> must be valid, and
        /// <see cref="SetupScaleData"/> must be called for the
        /// current configuration before using this method (this is called everytime
        /// the graph is drawn (i.e., <see cref="GraphPane.Draw"/> is called).
        /// </remarks>
        /// <param name="pixVal">
        /// The screen pixel value, in graphics device coordinates to
        /// be transformed
        /// </param>
        /// <returns>
        /// The user scale value that corresponds to the screen pixel location
        /// </returns>
        public double ReverseTransform(float pixVal)
        {
            double val;

            // see if the sign of the equation needs to be reversed
            if (this._isReverse == (this._ownerAxis is XAxis || this._ownerAxis is X2Axis))
            {
                val = (double)(pixVal - this._maxPix) / (double)(this._minPix - this._maxPix)
                      * (this._maxLinTemp - this._minLinTemp) + this._minLinTemp;
            }
            else
            {
                val = (double)(pixVal - this._minPix) / (double)(this._maxPix - this._minPix)
                      * (this._maxLinTemp - this._minLinTemp) + this._minLinTemp;
            }

            return this.DeLinearize(val);
        }

        /// <summary>
        /// Setup some temporary transform values in preparation for rendering the
        /// <see cref="Axis"/>.
        /// </summary>
        /// <remarks>
        /// This method is typically called by the parent <see cref="GraphPane"/>
        /// object as part of the <see cref="GraphPane.Draw"/> method.  It is also
        /// called by <see cref="GraphPane.GeneralTransform(double,double,CoordType)"/> and
        /// <see cref="GraphPane.ReverseTransform( PointF, out double, out double )"/>
        /// methods to setup for coordinate transformations.
        /// </remarks>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="axis">
        /// The parent <see cref="Axis"/> for this <see cref="Scale"/>
        /// </param>
        public virtual void SetupScaleData(GraphPane pane, Axis axis)
        {
            // save the ChartRect data for transforming scale values to pixels
            if (axis is XAxis || axis is X2Axis)
            {
                this._minPix = pane.Chart._rect.Left;
                this._maxPix = pane.Chart._rect.Right;
            }
            else
            {
                this._minPix = pane.Chart._rect.Top;
                this._maxPix = pane.Chart._rect.Bottom;
            }

            this._minLinTemp = this.Linearize(this._min);
            this._maxLinTemp = this.Linearize(this._max);
        }

        /// <summary>
        /// Transform the coordinate value from user coordinates (scale value)
        /// to graphics device coordinates (pixels).
        /// </summary>
        /// <remarks>
        /// This method takes into
        /// account the scale range (<see cref="Min"/> and <see cref="Max"/>),
        /// logarithmic state (<see cref="IsLog"/>), scale reverse state
        /// (<see cref="IsReverse"/>) and axis type (<see cref="XAxis"/>,
        /// <see cref="YAxis"/>, or <see cref="Y2Axis"/>).
        /// Note that the <see cref="Chart.Rect"/> must be valid, and
        /// <see cref="SetupScaleData"/> must be called for the
        /// current configuration before using this method (this is called everytime
        /// the graph is drawn (i.e., <see cref="GraphPane.Draw"/> is called).
        /// </remarks>
        /// <param name="x">
        /// The coordinate value, in user scale units, to
        /// be transformed
        /// </param>
        /// <returns>
        /// the coordinate value transformed to screen coordinates
        /// for use in calling the <see cref="Graphics"/> draw routines
        /// </returns>
        public float Transform(double x)
        {
            // Must take into account Log, and Reverse Axes
            double denom = this._maxLinTemp - this._minLinTemp;
            double ratio;
            if (denom > 1e-100)
            {
                ratio = (this.Linearize(x) - this._minLinTemp) / denom;
            }
            else
            {
                ratio = 0;
            }

            // _isReverse   axisType    Eqn
            // T          XAxis     _maxPix - ...
            // F          YAxis     _maxPix - ...
            // F          Y2Axis    _maxPix - ...

            // T          YAxis     _minPix + ...
            // T          Y2Axis    _minPix + ...
            // F          XAxis     _minPix + ...
            if (this._isReverse == (this._ownerAxis is XAxis || this._ownerAxis is X2Axis))
            {
                return (float)(this._maxPix - (this._maxPix - this._minPix) * ratio);
            }
            else
            {
                return (float)(this._minPix + (this._maxPix - this._minPix) * ratio);
            }
        }

        /// <summary>
        /// Transform the coordinate value from user coordinates (scale value)
        /// to graphics device coordinates (pixels).
        /// </summary>
        /// <remarks>
        /// This method takes into
        /// account the scale range (<see cref="Min"/> and <see cref="Max"/>),
        /// logarithmic state (<see cref="IsLog"/>), scale reverse state
        /// (<see cref="IsReverse"/>) and axis type (<see cref="XAxis"/>,
        /// <see cref="YAxis"/>, or <see cref="Y2Axis"/>).
        /// Note that the <see cref="Chart.Rect"/> must be valid, and
        /// <see cref="SetupScaleData"/> must be called for the
        /// current configuration before using this method (this is called everytime
        /// the graph is drawn (i.e., <see cref="GraphPane.Draw"/> is called).
        /// </remarks>
        /// <param name="isOverrideOrdinal">
        /// true to force the axis to honor the data
        /// value, rather than replacing it with the ordinal value
        /// </param>
        /// <param name="i">
        /// The ordinal value of this point, just in case
        /// this is an <see cref="AxisType.Ordinal"/> axis
        /// </param>
        /// <param name="x">
        /// The coordinate value, in user scale units, to
        /// be transformed
        /// </param>
        /// <returns>
        /// the coordinate value transformed to screen coordinates
        /// for use in calling the <see cref="Graphics"/> draw routines
        /// </returns>
        public float Transform(bool isOverrideOrdinal, int i, double x)
        {
            // ordinal types ignore the X value, and just use the ordinal position
            if (this.IsAnyOrdinal && i >= 0 && !isOverrideOrdinal)
            {
                x = (double)i + 1.0;
            }

            return this.Transform(x);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determine the value for the first major tic.
        /// </summary>
        /// <remarks>
        /// This is done by finding the first possible value that is an integral multiple of
        /// the step size, taking into account the date/time units if appropriate.
        /// This method properly accounts for <see cref="IsLog"/>, <see cref="IsText"/>,
        /// and other axis format settings.
        /// </remarks>
        /// <returns>
        /// First major tic value (floating point double).
        /// </returns>
        internal virtual double CalcBaseTic()
        {
            if (this._baseTic != PointPair.Missing)
            {
                return this._baseTic;
            }
            else if (this.IsAnyOrdinal)
            {
                // basetic is always 1 for ordinal types
                return 1;
            }
            else
            {
                // default behavior is linear or ordinal type
                // go to the nearest even multiple of the step size
                return Math.Ceiling((double)this._min / (double)this._majorStep - 0.00000001) * (double)this._majorStep;
            }
        }

        /*
		/// <summary>
		/// Make a value label for the axis at the specified ordinal position.
		/// </summary>
		/// <remarks>
		/// This method properly accounts for <see cref="IsLog"/>, <see cref="IsText"/>,
		/// and other axis format settings.
		/// </remarks>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="index">
		/// The zero-based, ordinal index of the label to be generated.  For example, a value of 2 would
		/// cause the third value label on the axis to be generated.
		/// </param>
		/// <param name="dVal">
		/// The numeric value associated with the label.  This value is ignored for log (<see cref="IsLog"/>)
		/// and text (<see cref="IsText"/>) type axes.
		/// </param>
		/// <returns>The resulting value label as a <see cref="string" /></returns>
		virtual internal string MakeLabel( GraphPane pane, int index, double dVal )
		{
			if ( this.ScaleFormatEvent != null )
			{
				string label;

				label = this.ScaleFormatEvent( pane, _ownerAxis, dVal, index );
				if ( label != null )
					return label;
			}

			if ( _format == null )
				_format = Scale.Default.Format;

			// linear or ordinal is the default behavior
			// this method is overridden for other Scale types

			double scaleMult = Math.Pow( (double) 10.0, _mag );

			return ( dVal / scaleMult ).ToString( _format );
		}
*/

        /// <summary>
        /// Determine the value for any major tic.
        /// </summary>
        /// <remarks>
        /// This method properly accounts for <see cref="IsLog"/>, <see cref="IsText"/>,
        /// and other axis format settings.
        /// </remarks>
        /// <param name="baseVal">
        /// The value of the first major tic (floating point double)
        /// </param>
        /// <param name="tic">
        /// The major tic number (0 = first major tic).  For log scales, this is the actual power of 10.
        /// </param>
        /// <returns>
        /// The specified major tic value (floating point double).
        /// </returns>
        internal virtual double CalcMajorTicValue(double baseVal, double tic)
        {
            // Default behavior is a normal linear scale (also works for ordinal types)
            return baseVal + (double)this._majorStep * tic;
        }

        /// <summary>
        /// Internal routine to determine the ordinals of the first minor tic mark
        /// </summary>
        /// <param name="baseVal">
        /// The value of the first major tic for the axis.
        /// </param>
        /// <returns>
        /// The ordinal position of the first minor tic, relative to the first major tic.
        /// This value can be negative (e.g., -3 means the first minor tic is 3 minor step
        /// increments before the first major tic.
        /// </returns>
        internal virtual int CalcMinorStart(double baseVal)
        {
            // Default behavior is for a linear scale (works for ordinal as well
            return (int)((this._min - baseVal) / this._minorStep);
        }

        /// <summary>
        /// Determine the value for any minor tic.
        /// </summary>
        /// <remarks>
        /// This method properly accounts for <see cref="IsLog"/>, <see cref="IsText"/>,
        /// and other axis format settings.
        /// </remarks>
        /// <param name="baseVal">
        /// The value of the first major tic (floating point double).  This tic value is the base
        /// reference for all tics (including minor ones).
        /// </param>
        /// <param name="iTic">
        /// The major tic number (0 = first major tic).  For log scales, this is the actual power of 10.
        /// </param>
        /// <returns>
        /// The specified minor tic value (floating point double).
        /// </returns>
        internal virtual double CalcMinorTicValue(double baseVal, int iTic)
        {
            // default behavior is a linear axis (works for ordinal types too
            return baseVal + (double)this._minorStep * (double)iTic;
        }

        /// <summary>
        /// Internal routine to determine the ordinals of the first and last major axis label.
        /// </summary>
        /// <returns>
        /// This is the total number of major tics for this axis.
        /// </returns>
        internal virtual int CalcNumTics()
        {
            int nTics = 1;

            // default behavior is for a linear or ordinal scale
            nTics = (int)((this._max - this._min) / this._majorStep + 0.01) + 1;

            if (nTics < 1)
            {
                nTics = 1;
            }
            else if (nTics > 1000)
            {
                nTics = 1000;
            }

            return nTics;
        }

        /// <summary>
        /// Draw the scale, including the tic marks, value labels, and grid lines as
        /// required for this <see cref="Axis"/>.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <param name="shiftPos">
        /// The number of pixels to shift to account for non-primary axis position (e.g.,
        /// the second, third, fourth, etc. <see cref="YAxis"/> or <see cref="Y2Axis"/>.
        /// </param>
        internal void Draw(Graphics g, GraphPane pane, float scaleFactor, float shiftPos)
        {
            MajorGrid majorGrid = this._ownerAxis._majorGrid;
            MajorTic majorTic = this._ownerAxis._majorTic;
            MinorTic minorTic = this._ownerAxis._minorTic;

            float rightPix, topPix;

            this.GetTopRightPix(pane, out topPix, out rightPix);

            // calculate the total number of major tics required
            int nTics = this.CalcNumTics();

            // get the first major tic value
            double baseVal = this.CalcBaseTic();

            using (Pen pen = new Pen(this._ownerAxis.Color, pane.ScaledPenWidth(majorTic._penWidth, scaleFactor)))
            {
                // redraw the axis border
                if (this._ownerAxis.IsAxisSegmentVisible)
                {
                    g.DrawLine(pen, 0.0F, shiftPos, rightPix, shiftPos);
                }

                // Draw a zero-value line if needed
                if (majorGrid._isZeroLine && this._min < 0.0 && this._max > 0.0)
                {
                    float zeroPix = this.LocalTransform(0.0);
                    g.DrawLine(pen, zeroPix, 0.0F, zeroPix, topPix);
                }
            }

            // draw the major tics and labels
            this.DrawLabels(g, pane, baseVal, nTics, topPix, shiftPos, scaleFactor);

            // 			_ownerAxis.DrawMinorTics( g, pane, baseVal, shiftPos, scaleFactor, topPix );
            this._ownerAxis.DrawTitle(g, pane, shiftPos, scaleFactor);
        }

        /// <summary>
        /// The draw grid.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="pane">
        /// The pane.
        /// </param>
        /// <param name="baseVal">
        /// The base val.
        /// </param>
        /// <param name="topPix">
        /// The top pix.
        /// </param>
        /// <param name="scaleFactor">
        /// The scale factor.
        /// </param>
        internal void DrawGrid(Graphics g, GraphPane pane, double baseVal, float topPix, float scaleFactor)
        {
            MajorTic tic = this._ownerAxis._majorTic;
            MajorGrid grid = this._ownerAxis._majorGrid;

            int nTics = this.CalcNumTics();

            double dVal, dVal2;
            float pixVal, pixVal2;

            using (Pen gridPen = grid.GetPen(pane, scaleFactor))
            {
                // get the Y position of the center of the axis labels
                // (the axis itself is referenced at zero)
                // 				SizeF maxLabelSize = GetScaleMaxSpace( g, pane, scaleFactor, true );
                // 				float charHeight = _fontSpec.GetHeight( scaleFactor );
                // 				float maxSpace = maxLabelSize.Height;

                // 				float edgeTolerance = Default.EdgeTolerance * scaleFactor;
                double rangeTol = (this._maxLinTemp - this._minLinTemp) * 0.001;

                int firstTic = (int)((this._minLinTemp - baseVal) / this._majorStep + 0.99);
                if (firstTic < 0)
                {
                    firstTic = 0;
                }

                // save the position of the previous tic
                // 				float lastPixVal = -10000;

                // loop for each major tic
                for (int i = firstTic; i < nTics + firstTic; i++)
                {
                    dVal = this.CalcMajorTicValue(baseVal, i);

                    // If we're before the start of the scale, just go to the next tic
                    if (dVal < this._minLinTemp)
                    {
                        continue;
                    }

                    // if we've already past the end of the scale, then we're done
                    if (dVal > this._maxLinTemp + rangeTol)
                    {
                        break;
                    }

                    // convert the value to a pixel position
                    pixVal = this.LocalTransform(dVal);

                    // see if the tic marks will be drawn between the labels instead of at the labels
                    // (this applies only to AxisType.Text
                    if (tic._isBetweenLabels && this.IsText)
                    {
                        // We need one extra tic in order to draw the tics between labels
                        // so provide an exception here
                        if (i == 0)
                        {
                            dVal2 = this.CalcMajorTicValue(baseVal, -0.5);
                            if (dVal2 >= this._minLinTemp)
                            {
                                pixVal2 = this.LocalTransform(dVal2);
                                grid.Draw(g, gridPen, pixVal2, topPix);
                            }
                        }

                        dVal2 = this.CalcMajorTicValue(baseVal, (double)i + 0.5);
                        if (dVal2 > this._maxLinTemp)
                        {
                            break;
                        }

                        pixVal2 = this.LocalTransform(dVal2);
                    }
                    else
                    {
                        pixVal2 = pixVal;
                    }

                    // draw the grid
                    grid.Draw(g, gridPen, pixVal2, topPix);
                }
            }
        }

        /// <summary>
        /// The draw label.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="pane">
        /// The pane.
        /// </param>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <param name="dVal">
        /// The d val.
        /// </param>
        /// <param name="pixVal">
        /// The pix val.
        /// </param>
        /// <param name="shift">
        /// The shift.
        /// </param>
        /// <param name="maxSpace">
        /// The max space.
        /// </param>
        /// <param name="scaledTic">
        /// The scaled tic.
        /// </param>
        /// <param name="charHeight">
        /// The char height.
        /// </param>
        /// <param name="scaleFactor">
        /// The scale factor.
        /// </param>
        internal void DrawLabel(
            Graphics g, 
            GraphPane pane, 
            int i, 
            double dVal, 
            float pixVal, 
            float shift, 
            float maxSpace, 
            float scaledTic, 
            float charHeight, 
            float scaleFactor)
        {
            float textTop, textCenter;
            if (this._ownerAxis.MajorTic.IsOutside)
            {
                textTop = scaledTic + charHeight * this._labelGap;
            }
            else
            {
                textTop = charHeight * this._labelGap;
            }

            // draw the label
            // string tmpStr = MakeLabel( pane, i, dVal );
            string tmpStr = this._ownerAxis.MakeLabelEventWorks(pane, i, dVal);

            float height;
            if (this.IsLog && this._isUseTenPower)
            {
                height = this._fontSpec.BoundingBoxTenPower(g, tmpStr, scaleFactor).Height;
            }
            else
            {
                height = this._fontSpec.BoundingBox(g, tmpStr, scaleFactor).Height;
            }

            if (this._align == AlignP.Center)
            {
                textCenter = textTop + maxSpace / 2.0F;
            }
            else if (this._align == AlignP.Outside)
            {
                textCenter = textTop + maxSpace - height / 2.0F;
            }
            else
            {
                // inside
                textCenter = textTop + height / 2.0F;
            }

            if (this._isLabelsInside)
            {
                textCenter = shift - textCenter;
            }
            else
            {
                textCenter = shift + textCenter;
            }

            AlignV av = AlignV.Center;
            AlignH ah = AlignH.Center;

            if (this._ownerAxis is XAxis || this._ownerAxis is X2Axis)
            {
                ah = this._alignH;
            }
            else
            {
                av = this._alignH == AlignH.Left
                         ? AlignV.Top
                         : (this._alignH == AlignH.Right ? AlignV.Bottom : AlignV.Center);
            }

            if (this.IsLog && this._isUseTenPower)
            {
                this._fontSpec.DrawTenPower(g, pane, tmpStr, pixVal, textCenter, ah, av, scaleFactor);
            }
            else
            {
                this._fontSpec.Draw(g, pane, tmpStr, pixVal, textCenter, ah, av, scaleFactor);
            }
        }

        /// <summary>
        /// Draw the value labels, tic marks, and grid lines as
        /// required for this <see cref="Axis"/>.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="baseVal">
        /// The first major tic value for the axis
        /// </param>
        /// <param name="nTics">
        /// The total number of major tics for the axis
        /// </param>
        /// <param name="topPix">
        /// The pixel location of the far side of the ChartRect from this axis.
        /// This value is the ChartRect.Height for the XAxis, or the ChartRect.Width
        /// for the YAxis and Y2Axis.
        /// </param>
        /// <param name="shift">
        /// The number of pixels to shift this axis, based on the
        /// value of <see cref="Axis.Cross"/>.  A positive value is into the ChartRect relative to
        /// the default axis position.
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        internal void DrawLabels(
            Graphics g, 
            GraphPane pane, 
            double baseVal, 
            int nTics, 
            float topPix, 
            float shift, 
            float scaleFactor)
        {
            MajorTic tic = this._ownerAxis._majorTic;

            // 			MajorGrid grid = _ownerAxis._majorGrid;
            double dVal, dVal2;
            float pixVal, pixVal2;
            float scaledTic = tic.ScaledTic(scaleFactor);

            double scaleMult = Math.Pow((double)10.0, this._mag);

            using (Pen ticPen = tic.GetPen(pane, scaleFactor))
            {
                // 			using ( Pen gridPen = grid.GetPen( pane, scaleFactor ) )
                // get the Y position of the center of the axis labels
                // (the axis itself is referenced at zero)
                SizeF maxLabelSize = this.GetScaleMaxSpace(g, pane, scaleFactor, true);
                float charHeight = this._fontSpec.GetHeight(scaleFactor);
                float maxSpace = maxLabelSize.Height;

                float edgeTolerance = Default.EdgeTolerance * scaleFactor;
                double rangeTol = (this._maxLinTemp - this._minLinTemp) * 0.001;

                int firstTic = (int)((this._minLinTemp - baseVal) / this._majorStep + 0.99);
                if (firstTic < 0)
                {
                    firstTic = 0;
                }

                // save the position of the previous tic
                float lastPixVal = -10000;

                // loop for each major tic
                for (int i = firstTic; i < nTics + firstTic; i++)
                {
                    dVal = this.CalcMajorTicValue(baseVal, i);

                    // If we're before the start of the scale, just go to the next tic
                    if (dVal < this._minLinTemp)
                    {
                        continue;
                    }

                    // if we've already past the end of the scale, then we're done
                    if (dVal > this._maxLinTemp + rangeTol)
                    {
                        break;
                    }

                    // convert the value to a pixel position
                    pixVal = this.LocalTransform(dVal);

                    // see if the tic marks will be drawn between the labels instead of at the labels
                    // (this applies only to AxisType.Text
                    if (tic._isBetweenLabels && this.IsText)
                    {
                        // We need one extra tic in order to draw the tics between labels
                        // so provide an exception here
                        if (i == 0)
                        {
                            dVal2 = this.CalcMajorTicValue(baseVal, -0.5);
                            if (dVal2 >= this._minLinTemp)
                            {
                                pixVal2 = this.LocalTransform(dVal2);
                                tic.Draw(g, pane, ticPen, pixVal2, topPix, shift, scaledTic);

                                // 								grid.Draw( g, gridPen, pixVal2, topPix );
                            }
                        }

                        dVal2 = this.CalcMajorTicValue(baseVal, (double)i + 0.5);
                        if (dVal2 > this._maxLinTemp)
                        {
                            break;
                        }

                        pixVal2 = this.LocalTransform(dVal2);
                    }
                    else
                    {
                        pixVal2 = pixVal;
                    }

                    tic.Draw(g, pane, ticPen, pixVal2, topPix, shift, scaledTic);

                    // draw the grid
                    // 					grid.Draw( g, gridPen, pixVal2, topPix );
                    bool isMaxValueAtMaxPix = ((this._ownerAxis is XAxis || this._ownerAxis is Y2Axis)
                                               && !this.IsReverse) || (this._ownerAxis is Y2Axis && this.IsReverse);

                    bool isSkipZone = (((this._isSkipFirstLabel && isMaxValueAtMaxPix)
                                        || (this._isSkipLastLabel && !isMaxValueAtMaxPix)) && pixVal < edgeTolerance)
                                      || (((this._isSkipLastLabel && isMaxValueAtMaxPix)
                                           || (this._isSkipFirstLabel && !isMaxValueAtMaxPix))
                                          && pixVal > this._maxPix - this._minPix - edgeTolerance);

                    bool isSkipCross = this._isSkipCrossLabel && !this._ownerAxis._crossAuto
                                       && Math.Abs(this._ownerAxis._cross - dVal) < rangeTol * 10.0;

                    isSkipZone = isSkipZone || isSkipCross;

                    if (this._isVisible && !isSkipZone)
                    {
                        // For exponential scales, just skip any label that would overlap with the previous one
                        // This is because exponential scales have varying label spacing
                        if (this.IsPreventLabelOverlap && Math.Abs(pixVal - lastPixVal) < maxLabelSize.Width)
                        {
                            continue;
                        }

                        this.DrawLabel(g, pane, i, dVal, pixVal, shift, maxSpace, scaledTic, charHeight, scaleFactor);

                        lastPixVal = pixVal;
                    }
                }
            }
        }

        /// <summary>
        /// Get the maximum width of the scale value text that is required to label this
        /// <see cref="Axis"/>.
        /// The results of this method are used to determine how much space is required for
        /// the axis labels.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        /// <param name="applyAngle">
        /// true to get the bounding box of the text using the <see cref="ZedGraph.FontSpec.Angle"/>,
        /// false to just get the bounding box without rotation
        /// </param>
        /// <returns>
        /// the maximum width of the text in pixel units
        /// </returns>
        internal SizeF GetScaleMaxSpace(Graphics g, GraphPane pane, float scaleFactor, bool applyAngle)
        {
            if (this._isVisible)
            {
                double dVal, scaleMult = Math.Pow((double)10.0, this._mag);
                int i;

                float saveAngle = this._fontSpec.Angle;
                if (!applyAngle)
                {
                    this._fontSpec.Angle = 0;
                }

                int nTics = this.CalcNumTics();

                double startVal = this.CalcBaseTic();

                SizeF maxSpace = new SizeF(0, 0);

                // Repeat for each tic
                for (i = 0; i < nTics; i++)
                {
                    dVal = this.CalcMajorTicValue(startVal, i);

                    // draw the label
                    // string tmpStr = MakeLabel( pane, i, dVal );
                    string tmpStr = this._ownerAxis.MakeLabelEventWorks(pane, i, dVal);

                    SizeF sizeF;
                    if (this.IsLog && this._isUseTenPower)
                    {
                        sizeF = this._fontSpec.BoundingBoxTenPower(g, tmpStr, scaleFactor);
                    }
                    else
                    {
                        sizeF = this._fontSpec.BoundingBox(g, tmpStr, scaleFactor);
                    }

                    if (sizeF.Height > maxSpace.Height)
                    {
                        maxSpace.Height = sizeF.Height;
                    }

                    if (sizeF.Width > maxSpace.Width)
                    {
                        maxSpace.Width = sizeF.Width;
                    }
                }

                this._fontSpec.Angle = saveAngle;

                return maxSpace;
            }
            else
            {
                return new SizeF(0, 0);
            }
        }

        /// <summary>
        /// The get top right pix.
        /// </summary>
        /// <param name="pane">
        /// The pane.
        /// </param>
        /// <param name="topPix">
        /// The top pix.
        /// </param>
        /// <param name="rightPix">
        /// The right pix.
        /// </param>
        internal void GetTopRightPix(GraphPane pane, out float topPix, out float rightPix)
        {
            if (this._ownerAxis is XAxis || this._ownerAxis is X2Axis)
            {
                rightPix = pane.Chart._rect.Width;
                topPix = -pane.Chart._rect.Height;
            }
            else
            {
                rightPix = pane.Chart._rect.Height;
                topPix = -pane.Chart._rect.Width;
            }

            // sanity check
            if (this._min >= this._max)
            {
                return;
            }

            // if the step size is outrageous, then quit
            // (step size not used for log scales)
            if (!this.IsLog)
            {
                if (this._majorStep <= 0 || this._minorStep <= 0)
                {
                    return;
                }

                double tMajor = (this._max - this._min) / (this._majorStep * this.MajorUnitMultiplier);
                double tMinor = (this._max - this._min) / (this._minorStep * this.MinorUnitMultiplier);

                MinorTic minorTic = this._ownerAxis._minorTic;

                if (tMajor > 1000 || ((minorTic.IsOutside || minorTic.IsInside || minorTic.IsOpposite) && tMinor > 5000))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Make a value label for the axis at the specified ordinal position.
        /// </summary>
        /// <remarks>
        /// This method properly accounts for <see cref="IsLog"/>, <see cref="IsText"/>,
        /// and other axis format settings.
        /// </remarks>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="index">
        /// The zero-based, ordinal index of the label to be generated.  For example, a value of 2 would
        /// cause the third value label on the axis to be generated.
        /// </param>
        /// <param name="dVal">
        /// The numeric value associated with the label.  This value is ignored for log (<see cref="IsLog"/>)
        /// and text (<see cref="IsText"/>) type axes.
        /// </param>
        /// <returns>
        /// The resulting value label as a <see cref="string"/>
        /// </returns>
        internal virtual string MakeLabel(GraphPane pane, int index, double dVal)
        {
            if (this._format == null)
            {
                this._format = Scale.Default.Format;
            }

            // linear or ordinal is the default behavior
            // this method is overridden for other Scale types
            double scaleMult = Math.Pow((double)10.0, this._mag);

            return (dVal / scaleMult).ToString(this._format);
        }

        /// <summary>
        /// Define suitable default ranges for an axis in the event that
        /// no data were available
        /// </summary>
        /// <param name="pane">
        /// The <see cref="GraphPane"/> of interest
        /// </param>
        /// <param name="axis">
        /// The <see cref="Axis"/> for which to set the range
        /// </param>
        internal void SetRange(GraphPane pane, Axis axis)
        {
            if (this._rangeMin >= double.MaxValue || this._rangeMax <= double.MinValue)
            {
                // If this is a Y axis, and the main Y axis is valid, use it for defaults
                if (axis != pane.XAxis && axis != pane.X2Axis && pane.YAxis.Scale._rangeMin < double.MaxValue
                    && pane.YAxis.Scale._rangeMax > double.MinValue)
                {
                    this._rangeMin = pane.YAxis.Scale._rangeMin;
                    this._rangeMax = pane.YAxis.Scale._rangeMax;
                }
                    
                    // Otherwise, if this is a Y axis, and the main Y2 axis is valid, use it for defaults
                else if (axis != pane.XAxis && axis != pane.X2Axis && pane.Y2Axis.Scale._rangeMin < double.MaxValue
                         && pane.Y2Axis.Scale._rangeMax > double.MinValue)
                {
                    this._rangeMin = pane.Y2Axis.Scale._rangeMin;
                    this._rangeMax = pane.Y2Axis.Scale._rangeMax;
                }
                    
                    // Otherwise, just use 0 and 1
                else
                {
                    this._rangeMin = 0;
                    this._rangeMax = 1;
                }
            }

            /*
				if ( yMinVal >= Double.MaxValue || yMaxVal <= Double.MinValue )
				{
					if ( y2MinVal < Double.MaxValue && y2MaxVal > Double.MinValue )
					{
						yMinVal = y2MinVal;
						yMaxVal = y2MaxVal;
					}
					else
					{
						yMinVal = 0;
						yMaxVal = 0.01;
					}
				}
			
				if ( y2MinVal >= Double.MaxValue || y2MaxVal <= Double.MinValue )
				{
					if ( yMinVal < Double.MaxValue && yMaxVal > Double.MinValue )
					{
						y2MinVal = yMinVal;
						y2MaxVal = yMaxVal;
					}
					else
					{
						y2MinVal = 0;
						y2MaxVal = 1;
					}
				}
				*/
        }

        /// <summary>
        /// The set scale mag.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <param name="step">
        /// The step.
        /// </param>
        internal void SetScaleMag(double min, double max, double step)
        {
            // set the scale magnitude if required
            if (this._magAuto)
            {
                // Find the optimal scale display multiple
                double mag = -100;
                double mag2 = -100;

                if (Math.Abs(this._min) > 1.0e-30)
                {
                    mag = Math.Floor(Math.Log10(Math.Abs(this._min)));
                }

                if (Math.Abs(this._max) > 1.0e-30)
                {
                    mag2 = Math.Floor(Math.Log10(Math.Abs(this._max)));
                }

                mag = Math.Max(mag2, mag);

                // Do not use scale multiples for magnitudes below 4
                if (mag == -100 || Math.Abs(mag) <= 3)
                {
                    mag = 0;
                }

                // Use a power of 10 that is a multiple of 3 (engineering scale)
                this._mag = (int)(Math.Floor(mag / 3.0) * 3.0);
            }

            // Calculate the appropriate number of dec places to display if required
            if (this._formatAuto)
            {
                int numDec = 0 - (int)(Math.Floor(Math.Log10(this._majorStep)) - this._mag);
                if (numDec < 0)
                {
                    numDec = 0;
                }

                this._format = "f" + numDec.ToString();
            }
        }

        /// <summary>
        /// Calculate a step size based on a data range.
        /// </summary>
        /// <remarks>
        /// This utility method
        /// will try to honor the <see cref="Default.TargetXSteps"/> and
        /// <see cref="Default.TargetYSteps"/> number of
        /// steps while using a rational increment (1, 2, or 5 -- which are
        /// even divisors of 10).  This method is used by <see cref="PickScale"/>.
        /// </remarks>
        /// <param name="range">
        /// The range of data in user scale units.  This can
        /// be a full range of the data for the major step size, or just the
        /// value of the major step size to calculate the minor step size
        /// </param>
        /// <param name="targetSteps">
        /// The desired "typical" number of steps
        /// to divide the range into
        /// </param>
        /// <returns>
        /// The calculated step size for the specified data range.
        /// </returns>
        protected static double CalcStepSize(double range, double targetSteps)
        {
            // Calculate an initial guess at step size
            double tempStep = range / targetSteps;

            // Get the magnitude of the step size
            double mag = Math.Floor(Math.Log10(tempStep));
            double magPow = Math.Pow((double)10.0, mag);

            // Calculate most significant digit of the new step size
            double magMsd = (int)(tempStep / magPow + .5);

            // promote the MSD to either 1, 2, or 5
            if (magMsd > 5.0)
            {
                magMsd = 10.0;
            }
            else if (magMsd > 2.0)
            {
                magMsd = 5.0;
            }
            else if (magMsd > 1.0)
            {
                magMsd = 2.0;
            }

            return magMsd * magPow;
        }

        /// <summary>
        /// Calculate a step size based on a data range, limited to a maximum number of steps.
        /// </summary>
        /// <remarks>
        /// This utility method
        /// will calculate a step size, of no more than maxSteps,
        /// using a rational increment (1, 2, or 5 -- which are
        /// even divisors of 10).  This method is used by <see cref="PickScale"/>.
        /// </remarks>
        /// <param name="range">
        /// The range of data in user scale units.  This can
        /// be a full range of the data for the major step size, or just the
        /// value of the major step size to calculate the minor step size
        /// </param>
        /// <param name="maxSteps">
        /// The maximum allowable number of steps
        /// to divide the range into
        /// </param>
        /// <returns>
        /// The calculated step size for the specified data range.
        /// </returns>
        protected double CalcBoundedStepSize(double range, double maxSteps)
        {
            // Calculate an initial guess at step size
            double tempStep = range / maxSteps;

            // Get the magnitude of the step size
            double mag = Math.Floor(Math.Log10(tempStep));
            double magPow = Math.Pow((double)10.0, mag);

            // Calculate most significant digit of the new step size
            double magMsd = Math.Ceiling(tempStep / magPow);

            // promote the MSD to either 1, 2, or 5
            if (magMsd > 5.0)
            {
                magMsd = 10.0;
            }
            else if (magMsd > 2.0)
            {
                magMsd = 5.0;
            }
            else if (magMsd > 1.0)
            {
                magMsd = 2.0;
            }

            return magMsd * magPow;
        }

        /// <summary>
        /// Calculate the modulus (remainder) in a safe manner so that divide
        /// by zero errors are avoided
        /// </summary>
        /// <param name="x">
        /// The divisor
        /// </param>
        /// <param name="y">
        /// The dividend
        /// </param>
        /// <returns>
        /// the value of the modulus, or zero for the divide-by-zero
        /// case
        /// </returns>
        protected double MyMod(double x, double y)
        {
            double temp;

            if (y == 0)
            {
                return 0;
            }

            temp = x / y;
            return y * (temp - Math.Floor(temp));
        }

        #endregion

        /// <summary>
        /// A simple struct that defines the
        /// default property values for the <see cref="Scale"/> class.
        /// </summary>
        public struct Default
        {
            #region Static Fields

            /// <summary> The default alignment of the <see cref="Axis"/> tic labels.
            /// This value controls whether the inside, center, or outside edges of the text labels are aligned.
            /// </summary>
            /// <seealso cref="AlignP"/>
            public static AlignP Align = AlignP.Center;

            /// <summary> The default alignment of the <see cref="Axis"/> tic labels.
            /// This value controls whether the left, center, or right edges of the text labels are aligned.
            /// </summary>
            /// <seealso cref="AlignH"/>
            public static AlignH AlignH = AlignH.Center;

            /// <summary>
            /// Determines the size of the band at the beginning and end of the axis that will have labels
            /// omitted if the axis is shifted due to a non-default location using the <see cref="Axis.Cross"/>
            /// property.
            /// </summary>
            /// <remarks>
            /// This parameter applies only when <see cref="Axis.CrossAuto"/> is false.  It is scaled according
            /// to the size of the graph based on <see cref="PaneBase.BaseDimension"/>.  When a non-default
            /// axis location is selected, the first and last labels on that axis will overlap the opposing
            /// axis frame.  This parameter allows those labels to be omitted to avoid the overlap.  Set this
            /// parameter to zero to turn off the effect.
            /// </remarks>
            public static float EdgeTolerance = 6;

            /// <summary>
            /// The default custom brush for filling in the scale text background
            /// (see <see cref="ZedGraph.Fill.Brush"/> property).
            /// </summary>
            public static Brush FillBrush = null;

            /// <summary>
            /// The default color for filling in the scale text background
            /// (see <see cref="ZedGraph.Fill.Color"/> property).
            /// </summary>
            public static Color FillColor = Color.White;

            /// <summary>
            /// The default fill mode for filling in the scale text background
            /// (see <see cref="ZedGraph.Fill.Type"/> property).
            /// </summary>
            public static FillType FillType = FillType.None;

            /// <summary>
            /// The default font bold mode for the <see cref="Axis"/> scale values
            /// font specification <see cref="FontSpec"/>
            /// (<see cref="ZedGraph.FontSpec.IsBold"/> property). true
            /// for a bold typeface, false otherwise.
            /// </summary>
            public static bool FontBold = false;

            /// <summary>
            /// The default font color for the <see cref="Axis"/> scale values
            /// font specification <see cref="FontSpec"/>
            /// (<see cref="ZedGraph.FontSpec.FontColor"/> property).
            /// </summary>
            public static Color FontColor = Color.Black;

            /// <summary>
            /// The default font family for the <see cref="Axis"/> scale values
            /// font specification <see cref="FontSpec"/>
            /// (<see cref="ZedGraph.FontSpec.Family"/> property).
            /// </summary>
            public static string FontFamily = "Arial";

            /// <summary>
            /// The default font italic mode for the <see cref="Axis"/> scale values
            /// font specification <see cref="FontSpec"/>
            /// (<see cref="ZedGraph.FontSpec.IsItalic"/> property). true
            /// for an italic typeface, false otherwise.
            /// </summary>
            public static bool FontItalic = false;

            /// <summary>
            /// The default font size for the <see cref="Axis"/> scale values
            /// font specification <see cref="FontSpec"/>
            /// (<see cref="ZedGraph.FontSpec.Size"/> property).  Units are
            /// in points (1/72 inch).
            /// </summary>
            public static float FontSize = 14;

            /// <summary>
            /// The default font underline mode for the <see cref="Axis"/> scale values
            /// font specification <see cref="FontSpec"/>
            /// (<see cref="ZedGraph.FontSpec.IsUnderline"/> property). true
            /// for an underlined typeface, false otherwise.
            /// </summary>
            public static bool FontUnderline = false;

            /// <summary>
            /// The default setting for the <see cref="Axis"/> scale format string
            /// (<see cref="Format"/> property).  For numeric values, this value is
            /// setting according to the <see cref="String.Format(string,object)"/> format strings.  For date
            /// type values, this value is set as per the <see cref="XDate.ToString()"/> function.
            /// </summary>
            // public static string ScaleFormat = "&dd-&mmm-&yy &hh:&nn";
            public static string Format = "g";

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// This is the format used for the scale values when auto-ranging code
            /// selects a <see cref="Format"/> of <see cref="DateUnit.Day"/>
            /// for <see cref="MajorUnit"/> and <see cref="DateUnit.Day"/> for 
            /// for <see cref="MinorUnit"/>.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            /// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
            public static string FormatDayDay = "d-MMM";

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// This is the format used for the scale values when auto-ranging code
            /// selects a <see cref="Format"/> of <see cref="DateUnit.Day"/>
            /// for <see cref="MajorUnit"/> and <see cref="DateUnit.Hour"/> for 
            /// for <see cref="MinorUnit"/>.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            /// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
            public static string FormatDayHour = "d-MMM HH:mm";

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// This is the format used for the scale values when auto-ranging code
            /// selects a <see cref="Format"/> of <see cref="DateUnit.Hour"/>
            /// for <see cref="MajorUnit"/> and <see cref="DateUnit.Hour"/> for 
            /// for <see cref="MinorUnit"/>.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            /// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
            public static string FormatHourHour = "HH:mm";

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// This is the format used for the scale values when auto-ranging code
            /// selects a <see cref="Format"/> of <see cref="DateUnit.Hour"/>
            /// for <see cref="MajorUnit"/> and <see cref="DateUnit.Minute"/> for 
            /// for <see cref="MinorUnit"/>.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            /// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
            public static string FormatHourMinute = "HH:mm";

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// This is the format used for the scale values when auto-ranging code
            /// selects a <see cref="Format"/> of <see cref="DateUnit.Millisecond"/>
            /// for <see cref="MajorUnit"/> and <see cref="DateUnit.Millisecond"/> for 
            /// for <see cref="MinorUnit"/>.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            /// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
            public static string FormatMillisecond = "ss.fff";

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// This is the format used for the scale values when auto-ranging code
            /// selects a <see cref="Format"/> of <see cref="DateUnit.Minute"/>
            /// for <see cref="MajorUnit"/> and <see cref="DateUnit.Minute"/> for 
            /// for <see cref="MinorUnit"/>.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            /// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
            public static string FormatMinuteMinute = "HH:mm";

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// This is the format used for the scale values when auto-ranging code
            /// selects a <see cref="Format"/> of <see cref="DateUnit.Minute"/>
            /// for <see cref="MajorUnit"/> and <see cref="DateUnit.Second"/> for 
            /// for <see cref="MinorUnit"/>.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            /// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
            public static string FormatMinuteSecond = "mm:ss";

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// This is the format used for the scale values when auto-ranging code
            /// selects a <see cref="Format"/> of <see cref="DateUnit.Month"/>
            /// for <see cref="MajorUnit"/> and <see cref="DateUnit.Month"/> for 
            /// for <see cref="MinorUnit"/>.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            /// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
            public static string FormatMonthMonth = "MMM-yyyy";

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// This is the format used for the scale values when auto-ranging code
            /// selects a <see cref="Format"/> of <see cref="DateUnit.Second"/>
            /// for <see cref="MajorUnit"/> and <see cref="DateUnit.Second"/> for 
            /// for <see cref="MinorUnit"/>.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            /// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
            public static string FormatSecondSecond = "mm:ss";

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// This is the format used for the scale values when auto-ranging code
            /// selects a <see cref="Format"/> of <see cref="DateUnit.Year"/>
            /// for <see cref="MajorUnit"/> and <see cref="DateUnit.Month"/> for 
            /// for <see cref="MinorUnit"/>.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            /// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
            public static string FormatYearMonth = "MMM-yyyy";

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// This is the format used for the scale values when auto-ranging code
            /// selects a <see cref="Format"/> of <see cref="DateUnit.Year"/>
            /// for <see cref="MajorUnit"/> and <see cref="DateUnit.Year"/> for 
            /// for <see cref="MinorUnit"/>.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            /// <seealso cref="System.Globalization.DateTimeFormatInfo"/>
            public static string FormatYearYear = "yyyy";

            /// <summary>
            /// The default value for <see cref="IsLabelsInside"/>, which determines
            /// whether or not the scale labels and title for the <see cref="Axis"/> will appear
            /// on the opposite side of the <see cref="Axis"/> that it normally appears.
            /// </summary>
            public static bool IsLabelsInside = false;

            /// <summary>
            /// The default reverse mode for the <see cref="Axis"/> scale
            /// (<see cref="IsReverse"/> property). true for a reversed scale
            /// (X decreasing to the left, Y/Y2 decreasing upwards), false otherwise.
            /// </summary>
            public static bool IsReverse = false;

            /// <summary>
            /// The default value for <see cref="IsVisible"/>, which determines
            /// whether or not the scale values are displayed.
            /// </summary>
            public static bool IsVisible = true;

            /// <summary>
            /// The default setting for the gap between the outside tics (or the axis edge
            /// if there are no outside tics) and the scale labels, expressed as a fraction of
            /// the major tic size.
            /// </summary>
            public static float LabelGap = 0.3f;

            /// <summary> The default "grace" value applied to the maximum data range.
            /// This value is
            /// expressed as a fraction of the total data range.  For example, assume the data
            /// range is from 4.0 to 16.0, leaving a range of 12.0.  If MaxGrace is set to
            /// 0.1, then 10% of the range, or 1.2 will be added to the maximum data value.
            /// The scale will then be ranged to cover at least 4.0 to 17.2.
            /// </summary>
            /// <seealso cref="MinGrace"/>
            /// <seealso cref="MaxGrace"/>
            public static double MaxGrace = 0.1;

            /// <summary>
            /// The maximum number of text labels (major tics) that will be allowed on the plot by
            /// the automatic scaling logic.  This value applies only to <see cref="AxisType.Text"/>
            /// axes.  If there are more than MaxTextLabels on the plot, then
            /// <see cref="MajorStep"/> will be increased to reduce the number of labels.  That is,
            /// the step size might be increased to 2.0 to show only every other label.
            /// </summary>
            public static double MaxTextLabels = 12.0;

            /// <summary> The default "grace" value applied to the minimum data range.
            /// This value is
            /// expressed as a fraction of the total data range.  For example, assume the data
            /// range is from 4.0 to 16.0, leaving a range of 12.0.  If MinGrace is set to
            /// 0.1, then 10% of the range, or 1.2 will be subtracted from the minimum data value.
            /// The scale will then be ranged to cover at least 2.8 to 16.0.
            /// </summary>
            /// <seealso cref="MinGrace"/>
            public static double MinGrace = 0.1;

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// If the total span of data exceeds this number (in days), then the auto-range
            /// code will select <see cref="MajorUnit"/> = <see cref="DateUnit.Day"/>
            /// and <see cref="MinorUnit"/> = <see cref="DateUnit.Day"/>.
            /// This value normally defaults to 10 days.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            public static double RangeDayDay = 10; // 10 days

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// If the total span of data exceeds this number (in days), then the auto-range
            /// code will select <see cref="MajorUnit"/> = <see cref="DateUnit.Day"/>
            /// and <see cref="MinorUnit"/> = <see cref="DateUnit.Hour"/>.
            /// This value normally defaults to 3 days.
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            public static double RangeDayHour = 3; // 3 days

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// If the total span of data exceeds this number (in days), then the auto-range
            /// code will select <see cref="MajorUnit"/> = <see cref="DateUnit.Hour"/>
            /// and <see cref="MinorUnit"/> = <see cref="DateUnit.Hour"/>.
            /// This value normally defaults to 0.4167 days (10 hours).
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            public static double RangeHourHour = 0.4167; // 10 hours

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// If the total span of data exceeds this number (in days), then the auto-range
            /// code will select <see cref="MajorUnit"/> = <see cref="DateUnit.Hour"/>
            /// and <see cref="MinorUnit"/> = <see cref="DateUnit.Minute"/>.
            /// This value normally defaults to 0.125 days (3 hours).
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            public static double RangeHourMinute = 0.125; // 3 hours

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// If the total span of data exceeds this number (in days), then the auto-range
            /// code will select <see cref="MajorUnit"/> = <see cref="DateUnit.Minute"/>
            /// and <see cref="MinorUnit"/> = <see cref="DateUnit.Minute"/>.
            /// This value normally defaults to 6.94e-3 days (10 minutes).
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            public static double RangeMinuteMinute = 6.94e-3; // 10 Minutes

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// If the total span of data exceeds this number (in days), then the auto-range
            /// code will select <see cref="MajorUnit"/> = <see cref="DateUnit.Minute"/>
            /// and <see cref="MinorUnit"/> = <see cref="DateUnit.Second"/>.
            /// This value normally defaults to 2.083e-3 days (3 minutes).
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            public static double RangeMinuteSecond = 2.083e-3; // 3 Minutes

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// If the total span of data exceeds this number (in days), then the auto-range
            /// code will select <see cref="MajorUnit"/> = <see cref="DateUnit.Month"/>
            /// and <see cref="MinorUnit"/> = <see cref="DateUnit.Month"/>.
            /// This value normally defaults to 300 days (10 months).
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            public static double RangeMonthMonth = 300; // 10 months

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// If the total span of data exceeds this number (in days), then the auto-range
            /// code will select <see cref="MajorUnit"/> = <see cref="DateUnit.Second"/>
            /// and <see cref="MinorUnit"/> = <see cref="DateUnit.Second"/>.
            /// This value normally defaults to 3.472e-5 days (3 seconds).
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            public static double RangeSecondSecond = 3.472e-5; // 3 Seconds

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// If the total span of data exceeds this number (in days), then the auto-range
            /// code will select <see cref="MajorUnit"/> = <see cref="DateUnit.Year"/>
            /// and <see cref="MinorUnit"/> = <see cref="DateUnit.Month"/>.
            /// This value normally defaults to 730 days (2 years).
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            public static double RangeYearMonth = 730; // 2 years

            /// <summary>
            /// A default setting for the <see cref="AxisType.Date"/> auto-ranging code.
            /// This values applies only to Date-Time type axes.
            /// If the total span of data exceeds this number (in days), then the auto-range
            /// code will select <see cref="MajorUnit"/> = <see cref="DateUnit.Year"/>
            /// and <see cref="MinorUnit"/> = <see cref="DateUnit.Year"/>.
            /// This value normally defaults to 1825 days (5 years).
            /// This value is used by the <see cref="DateScale.CalcDateStepSize"/> method.
            /// </summary>
            public static double RangeYearYear = 1825; // 5 years

            /// <summary>
            /// The default target number of minor steps for automatically selecting the X axis
            /// scale minor step size (see <see cref="PickScale"/>).
            /// This number is an initial target value for the number of minor steps
            /// on an axis.  This value is maintained only in the
            /// <see cref="Default"/> class, and cannot be changed after compilation.
            /// </summary>
            public static double TargetMinorXSteps = 5.0;

            /// <summary>
            /// The default target number of minor steps for automatically selecting the Y or Y2 axis
            /// scale minor step size (see <see cref="PickScale"/>).
            /// This number is an initial target value for the number of minor steps
            /// on an axis.  This value is maintained only in the
            /// <see cref="Default"/> class, and cannot be changed after compilation.
            /// </summary>
            public static double TargetMinorYSteps = 5.0;

            /// <summary>
            /// The default target number of steps for automatically selecting the X axis
            /// scale step size (see <see cref="PickScale"/>).
            /// This number is an initial target value for the number of major steps
            /// on an axis.  This value is maintained only in the
            /// <see cref="Default"/> class, and cannot be changed after compilation.
            /// </summary>
            public static double TargetXSteps = 7.0;

            /// <summary>
            /// The default target number of steps for automatically selecting the Y or Y2 axis
            /// scale step size (see <see cref="PickScale"/>).
            /// This number is an initial target value for the number of major steps
            /// on an axis.  This value is maintained only in the
            /// <see cref="Default"/> class, and cannot be changed after compilation.
            /// </summary>
            public static double TargetYSteps = 7.0;

            /// <summary>
            /// The default "zero lever" for automatically selecting the axis
            /// scale range (see <see cref="PickScale"/>). This number is
            /// used to determine when an axis scale range should be extended to
            /// include the zero value.  This value is maintained only in the
            /// <see cref="Default"/> class, and cannot be changed after compilation.
            /// </summary>
            public static double ZeroLever = 0.25;

            #endregion
        }
    }
}