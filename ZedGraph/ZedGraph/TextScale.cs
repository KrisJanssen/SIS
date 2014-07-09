// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="TextScale.cs">
//   
// </copyright>
// <summary>
//   The TextScale class inherits from the  class, and implements
//   the features specific to .
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// The TextScale class inherits from the <see cref="Scale" /> class, and implements
    /// the features specific to <see cref="AxisType.Text" />.
    /// </summary>
    /// <remarks>
    /// TextScale is an ordinal axis with user-defined text labels.  An ordinal axis means that
    /// all data points are evenly spaced at integral values, and the actual coordinate values
    /// for points corresponding to that axis are ignored.  That is, if the X axis is an
    /// ordinal type, then all X values associated with the curves are ignored.
    /// </remarks>
    /// 
    /// <author> John Champion  </author>
    /// <version> $Revision: 1.8 $ $Date: 2006-08-25 05:19:09 $ </version>
    [Serializable]
    internal class TextScale : Scale, ISerializable
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
        /// Initializes a new instance of the <see cref="TextScale"/> class.
        /// </summary>
        /// <param name="owner">
        /// The owner.
        /// </param>
        public TextScale(Axis owner)
            : base(owner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextScale"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="TextScale"/> object from which to copy
        /// </param>
        /// <param name="owner">
        /// The <see cref="Axis"/> object that will own the
        /// new instance of <see cref="TextScale"/>
        /// </param>
        public TextScale(Scale rhs, Axis owner)
            : base(rhs, owner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextScale"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected TextScale(SerializationInfo info, StreamingContext context)
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
                return AxisType.Text;
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
            return new TextScale(this, owner);
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
        /// Select a reasonable text axis scale given a range of data values.
        /// </summary>
        /// <remarks>
        /// This method only applies to <see cref="AxisType.Text"/> type axes, and it
        /// is called by the general <see cref="PickScale"/> method.  This is an ordinal
        /// type, such that the labeled values start at 1.0 and increment by 1.0 for
        /// each successive label.  The maximum number of labels on the graph is
        /// determined by <see cref="Scale.Default.MaxTextLabels"/>.  If necessary, this method will
        /// set the <see cref="Scale.MajorStep"/> value to greater than 1.0 in order to keep the total
        /// labels displayed below <see cref="Scale.Default.MaxTextLabels"/>.  For example, a
        /// <see cref="Scale.MajorStep"/> size of 2.0 would only display every other label on the
        /// axis.  The <see cref="Scale.MajorStep"/> value calculated by this routine is always
        /// an integral value.  This
        /// method honors the <see cref="Scale.MinAuto"/>, <see cref="Scale.MaxAuto"/>,
        /// and <see cref="Scale.MajorStepAuto"/> autorange settings.
        /// In the event that any of the autorange settings are false, the
        /// corresponding <see cref="Scale.Min"/>, <see cref="Scale.Max"/>, or <see cref="Scale.MajorStep"/>
        /// setting is explicitly honored, and the remaining autorange settings (if any) will
        /// be calculated to accomodate the non-autoranged values.
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
        /// <see cref="Scale.MinorStep"/> is set to scale minor step size (if <see cref="Scale.MinorStepAuto"/> = true)
        /// </para>
        /// <para>
        /// <see cref="Scale.Mag"/> is set to a magnitude multiplier according to the data
        /// </para>
        /// <para>
        /// <see cref="Scale.Format"/> is set to the display format for the values (this controls the
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
        /// <seealso cref="PickScale"/>
        /// <seealso cref="AxisType.Text"/>
        public override void PickScale(GraphPane pane, Graphics g, float scaleFactor)
        {
            // call the base class first
            base.PickScale(pane, g, scaleFactor);

            // if text labels are provided, then autorange to the number of labels
            if (this._textLabels != null)
            {
                if (this._minAuto)
                {
                    this._min = 0.5;
                }

                if (this._maxAuto)
                {
                    this._max = this._textLabels.Length + 0.5;
                }
            }
            else
            {
                if (this._minAuto)
                {
                    this._min -= 0.5;
                }

                if (this._maxAuto)
                {
                    this._max += 0.5;
                }
            }

            // Test for trivial condition of range = 0 and pick a suitable default
            if (this._max - this._min < .1)
            {
                if (this._maxAuto)
                {
                    this._max = this._min + 10.0;
                }
                else
                {
                    this._min = this._max - 10.0;
                }
            }

            if (this._majorStepAuto)
            {
                if (!this._isPreventLabelOverlap)
                {
                    this._majorStep = 1;
                }
                else if (this._textLabels != null)
                {
                    // Calculate the maximum number of labels
                    double maxLabels = (double)this.CalcMaxLabels(g, pane, scaleFactor);

                    // Calculate a step size based on the width of the labels
                    double tmpStep = Math.Ceiling((this._max - this._min) / maxLabels);

                    // Use the lesser of the two step sizes
                    // if ( tmpStep < this.majorStep )
                    this._majorStep = tmpStep;
                }
                else
                {
                    this._majorStep = (int)((this._max - this._min - 1.0) / Default.MaxTextLabels) + 1.0;
                }
            }
            else
            {
                this._majorStep = (int)this._majorStep;
                if (this._majorStep <= 0)
                {
                    this._majorStep = 1.0;
                }
            }

            if (this._minorStepAuto)
            {
                this._minorStep = this._majorStep / 10;

                // _minorStep = CalcStepSize( _majorStep, 10 );
                if (this._minorStep < 1)
                {
                    this._minorStep = 1;
                }
            }

            this._mag = 0;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determine the value for the first major tic.
        /// </summary>
        /// <remarks>
        /// This is done by finding the first possible value that is an integral multiple of
        /// the step size, taking into account the date/time units if appropriate.
        /// This method properly accounts for <see cref="Scale.IsLog"/>, <see cref="Scale.IsText"/>,
        /// and other axis format settings.
        /// </remarks>
        /// <returns>
        /// First major tic value (floating point double).
        /// </returns>
        internal override double CalcBaseTic()
        {
            if (this._baseTic != PointPair.Missing)
            {
                return this._baseTic;
            }
            else
            {
                return 1.0;
            }
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
            // This should never happen (no minor tics for text labels)
            return 0;
        }

        /// <summary>
        /// Internal routine to determine the ordinals of the first and last major axis label.
        /// </summary>
        /// <returns>
        /// This is the total number of major tics for this axis.
        /// </returns>
        internal override int CalcNumTics()
        {
            int nTics = 1;

            // If no array of labels is available, just assume 10 labels so we don't blow up.
            if (this._textLabels == null)
            {
                nTics = 10;
            }
            else
            {
                nTics = this._textLabels.Length;
            }

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
        /// Make a value label for an <see cref="AxisType.Text"/> <see cref="Axis"/>.
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

            index *= (int)this._majorStep;
            if (this._textLabels == null || index < 0 || index >= this._textLabels.Length)
            {
                return string.Empty;
            }
            else
            {
                return this._textLabels[index];
            }
        }

        #endregion
    }
}