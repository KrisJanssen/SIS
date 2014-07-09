// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ExponentScale.cs">
//   
// </copyright>
// <summary>
//   The ExponentScale class inherits from the <see cref="Scale" /> class, and implements
//   the features specific to <see cref="AxisType.Exponent" />.
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
    /// The ExponentScale class inherits from the <see cref="Scale" /> class, and implements
    /// the features specific to <see cref="AxisType.Exponent" />.
    /// </summary>
    /// <remarks>
    /// ExponentScale is a non-linear axis in which the values are scaled using an exponential function
    /// with the <see cref="Scale.Exponent" /> property.
    /// </remarks>
    /// 
    /// <author> John Champion with contributions by jackply </author>
    /// <version> $Revision: 1.8 $ $Date: 2007-04-16 00:03:01 $ </version>
    [Serializable]
    internal class ExponentScale : Scale, ISerializable
    {
        // , ICloneable
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema2 = 10;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentScale"/> class. 
        /// Default constructor that defines the owner <see cref="Axis"/>
        /// (containing object) for this new object.
        /// </summary>
        /// <param name="owner">
        /// The owner, or containing object, of this instance
        /// </param>
        public ExponentScale(Axis owner)
            : base(owner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentScale"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="ExponentScale"/> object from which to copy
        /// </param>
        /// <param name="owner">
        /// The <see cref="Axis"/> object that will own the
        /// new instance of <see cref="ExponentScale"/>
        /// </param>
        public ExponentScale(Scale rhs, Axis owner)
            : base(rhs, owner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentScale"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected ExponentScale(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema2");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the type.
        /// </summary>
        public override AxisType Type
        {
            get
            {
                return AxisType.Exponent;
            }
        }

        #endregion

        #region Public Methods and Operators

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
        public override Scale Clone(Axis owner)
        {
            return new ExponentScale(this, owner);
        }

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
        public override double DeLinearize(double val)
        {
            return Math.Pow(val, 1 / this._exponent);
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
        public override double Linearize(double val)
        {
            return SafeExp(val, this._exponent);
        }

        /// <summary>
        /// Select a reasonable exponential axis scale given a range of data values.
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
        /// <remarks>
        /// This method only applies to <see cref="AxisType.Exponent"/> type axes, and it
        /// is called by the general <see cref="Scale.PickScale"/> method.  The exponential scale
        /// relies on the <see cref="Scale.Exponent"/> property to set the scaling exponent.  This
        /// method honors the <see cref="Scale.MinAuto"/>, <see cref="Scale.MaxAuto"/>,
        /// and <see cref="Scale.MajorStepAuto"/> autorange settings.
        /// In the event that any of the autorange settings are false, the
        /// corresponding <see cref="Scale.Min"/>, <see cref="Scale.Max"/>, or <see cref="Scale.MajorStep"/>
        /// setting is explicitly honored, and the remaining autorange settings (if any) will
        /// be calculated to accomodate the non-autoranged values.  For log axes, the MinorStep
        /// value is not used.
        /// <para>
        /// On Exit:
        /// </para>
        /// <para>
        /// <see cref="Scale.Min"/> is set to scale minimum (if <see cref="Scale.MinAuto"/> = true)
        /// </para>
        /// <para>
        /// <see cref="Scale.Max"/> is set to scale maximum (if <see cref="Scale.MaxAuto"/> = true)
        /// </para>
        /// <para>
        /// <see cref="Scale.MajorStep"/> is set to scale step size (if <see cref="Scale.MajorStepAuto"/> = true)
        /// </para>
        /// <para>
        /// <see cref="Scale.Mag"/> is set to a magnitude multiplier according to the data
        /// </para>
        /// <para>
        /// <see cref="Scale.Format"/> is set to the display format for the values (this controls the
        /// number of decimal places, whether there are thousands separators, currency types, etc.)
        /// </para>
        /// </remarks>
        /// <seealso cref="Scale.PickScale"/>
        /// <seealso cref="AxisType.Exponent"/>
        public override void PickScale(GraphPane pane, Graphics g, float scaleFactor)
        {
            // call the base class first
            base.PickScale(pane, g, scaleFactor);

            // Test for trivial condition of range = 0 and pick a suitable default
            if (this._max - this._min < 1.0e-20)
            {
                if (this._maxAuto)
                {
                    this._max = this._max + 0.2 * (this._max == 0 ? 1.0 : Math.Abs(this._max));
                }

                if (this._minAuto)
                {
                    this._min = this._min - 0.2 * (this._min == 0 ? 1.0 : Math.Abs(this._min));
                }
            }

            // This is the zero-lever test.  If minVal is within the zero lever fraction
            // of the data range, then use zero.
            if (this._minAuto && this._min > 0 && this._min / (this._max - this._min) < Default.ZeroLever)
            {
                this._min = 0;
            }

            // Repeat the zero-lever test for cases where the maxVal is less than zero
            if (this._maxAuto && this._max < 0 && Math.Abs(this._max / (this._max - this._min)) < Default.ZeroLever)
            {
                this._max = 0;
            }

            // Calculate the new step size
            if (this._majorStepAuto)
            {
                double targetSteps = (this._ownerAxis is XAxis || this._ownerAxis is X2Axis)
                                         ? Default.TargetXSteps
                                         : Default.TargetYSteps;

                // Calculate the step size based on target steps
                this._majorStep = CalcStepSize(this._max - this._min, targetSteps);

                if (this._isPreventLabelOverlap)
                {
                    // Calculate the maximum number of labels
                    double maxLabels = (double)this.CalcMaxLabels(g, pane, scaleFactor);

                    if (maxLabels < (this._max - this._min) / this._majorStep)
                    {
                        this._majorStep = this.CalcBoundedStepSize(this._max - this._min, maxLabels);
                    }
                }
            }

            // Calculate the new step size
            if (this._minorStepAuto)
            {
                this._minorStep = CalcStepSize(
                    this._majorStep, 
                    (this._ownerAxis is XAxis || this._ownerAxis is X2Axis)
                        ? Default.TargetMinorXSteps
                        : Default.TargetMinorYSteps);
            }

            // Calculate the scale minimum
            if (this._minAuto)
            {
                this._min = this._min - this.MyMod(this._min, this._majorStep);
            }

            // Calculate the scale maximum
            if (this._maxAuto)
            {
                this._max = this.MyMod(this._max, this._majorStep) == 0.0
                                ? this._max
                                : this._max + this._majorStep - this.MyMod(this._max, this._majorStep);
            }

            // set the scale magnitude if required
            if (this._magAuto)
            {
                // Find the optimal scale display multiple
                double mag = 0;
                double mag2 = 0;

                if (Math.Abs(this._min) > 1.0e-10)
                {
                    mag = Math.Floor(Math.Log10(Math.Abs(this._min)));
                }

                if (Math.Abs(this._max) > 1.0e-10)
                {
                    mag2 = Math.Floor(Math.Log10(Math.Abs(this._max)));
                }

                if (Math.Abs(mag2) > Math.Abs(mag))
                {
                    mag = mag2;
                }

                // Do not use scale multiples for magnitudes below 4
                if (Math.Abs(mag) <= 3)
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
        /// Setup some temporary transform values in preparation for rendering the <see cref="Axis"/>.
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
        public override void SetupScaleData(GraphPane pane, Axis axis)
        {
            base.SetupScaleData(pane, axis);

            if (this._exponent > 0)
            {
                this._minLinTemp = this.Linearize(this._min);
                this._maxLinTemp = this.Linearize(this._max);
            }
            else if (this._exponent < 0)
            {
                this._minLinTemp = this.Linearize(this._max);
                this._maxLinTemp = this.Linearize(this._min);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determine the value for any major tic.
        /// </summary>
        /// <remarks>
        /// This method properly accounts for <see cref="Scale.IsLog"/>, <see cref="Scale.IsText"/>,
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
        internal override double CalcMajorTicValue(double baseVal, double tic)
        {
            if (this._exponent > 0.0)
            {
                // return baseVal + Math.Pow ( (double) this.majorStep * tic, exp );
                // baseVal is got from CalBase..., and it is exp..
                return Math.Pow(Math.Pow(baseVal, 1 / this._exponent) + this._majorStep * tic, this._exponent);
            }
            else if (this._exponent < 0.0)
            {
                // baseVal is got from CalBase..., and it is exp..
                return Math.Pow(Math.Pow(baseVal, 1 / this._exponent) + this._majorStep * tic, this._exponent);
            }

            return 1.0;
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
        internal override int CalcMinorStart(double baseVal)
        {
            return (int)((Math.Pow(this._min, this._exponent) - baseVal) / Math.Pow(this._minorStep, this._exponent));
        }

        /// <summary>
        /// Determine the value for any minor tic.
        /// </summary>
        /// <remarks>
        /// This method properly accounts for <see cref="Scale.IsLog"/>, <see cref="Scale.IsText"/>,
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
        internal override double CalcMinorTicValue(double baseVal, int iTic)
        {
            return baseVal + Math.Pow((double)this._majorStep * (double)iTic, this._exponent);
        }

        /// <summary>
        /// Make a value label for an <see cref="AxisType.Exponent"/> <see cref="Axis"/>.
        /// </summary>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="index">
        /// The zero-based, ordinal index of the label to be generated.  For example, a value of 2 would
        /// cause the third value label on the axis to be generated.
        /// </param>
        /// <param name="dVal">
        /// The numeric value associated with the label.  This value is ignored for log (<see cref="Scale.IsLog"/>)
        /// and text (<see cref="Scale.IsText"/>) type axes.
        /// </param>
        /// <returns>
        /// The resulting value label as a <see cref="string"/>
        /// </returns>
        internal override string MakeLabel(GraphPane pane, int index, double dVal)
        {
            if (this._format == null)
            {
                this._format = Scale.Default.Format;
            }

            double scaleMult = Math.Pow((double)10.0, this._mag);
            double val = Math.Pow(dVal, 1 / this._exponent) / scaleMult;
            return val.ToString(this._format);
        }

        #endregion
    }
}