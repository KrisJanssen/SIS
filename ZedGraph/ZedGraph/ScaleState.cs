// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ScaleState.cs">
//   
// </copyright>
// <summary>
//   A class that captures an  scale range.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;

    /// <summary>
    /// A class that captures an <see cref="Axis"/> scale range.
    /// </summary>
    /// <remarks>This structure is used by the <see cref="ZoomState"/> class to store
    /// <see cref="Axis"/> scale range settings in a collection for later retrieval.
    /// The class stores the <see cref="Scale.Min"/>, <see cref="Scale.Max"/>,
    /// <see cref="Scale.MinorStep"/>, and <see cref="Scale.MajorStep"/> properties, along with
    /// the corresponding auto-scale settings: <see cref="Scale.MinAuto"/>,
    /// <see cref="Scale.MaxAuto"/>, <see cref="Scale.MinorStepAuto"/>,
    /// and <see cref="Scale.MajorStepAuto"/>.</remarks>
    /// <author> John Champion </author>
    /// <version> $Revision: 3.2 $ $Date: 2007-02-19 08:05:24 $ </version>
    public class ScaleState : ICloneable
    {
        #region Fields

        /// <summary>
        /// The _format.
        /// </summary>
        private string _format;

        /// <summary>
        /// The status of <see cref="Scale.MinAuto"/>,
        /// <see cref="Scale.MaxAuto"/>, <see cref="Scale.MinorStepAuto"/>,
        /// and <see cref="Scale.MajorStepAuto"/>
        /// </summary>
        private bool _formatAuto;

        /// <summary>
        /// The _mag.
        /// </summary>
        private int _mag;

        /// <summary>
        /// The status of <see cref="Scale.MinAuto"/>,
        /// <see cref="Scale.MaxAuto"/>, <see cref="Scale.MinorStepAuto"/>,
        /// and <see cref="Scale.MajorStepAuto"/>
        /// </summary>
        private bool _magAuto;

        /// <summary>
        /// The axis range data for <see cref="Scale.Min"/>, <see cref="Scale.Max"/>,
        /// <see cref="Scale.MinorStep"/>, and <see cref="Scale.MajorStep"/>
        /// </summary>
        private double _majorStep;

        /// <summary>
        /// The status of <see cref="Scale.MinAuto"/>,
        /// <see cref="Scale.MaxAuto"/>, <see cref="Scale.MinorStepAuto"/>,
        /// and <see cref="Scale.MajorStepAuto"/>
        /// </summary>
        private bool _majorStepAuto;

        /// <summary>
        /// The status of <see cref="Scale.MajorUnit"/> and <see cref="Scale.MinorUnit"/>
        /// </summary>
        private DateUnit _majorUnit;

        /// <summary>
        /// The axis range data for <see cref="Scale.Min"/>, <see cref="Scale.Max"/>,
        /// <see cref="Scale.MinorStep"/>, and <see cref="Scale.MajorStep"/>
        /// </summary>
        private double _max;

        /// <summary>
        /// The status of <see cref="Scale.MinAuto"/>,
        /// <see cref="Scale.MaxAuto"/>, <see cref="Scale.MinorStepAuto"/>,
        /// and <see cref="Scale.MajorStepAuto"/>
        /// </summary>
        private bool _maxAuto;

        /// <summary>
        /// The axis range data for <see cref="Scale.Min"/>, <see cref="Scale.Max"/>,
        /// <see cref="Scale.MinorStep"/>, and <see cref="Scale.MajorStep"/>
        /// </summary>
        private double _min;

        /// <summary>
        /// The status of <see cref="Scale.MinAuto"/>,
        /// <see cref="Scale.MaxAuto"/>, <see cref="Scale.MinorStepAuto"/>,
        /// and <see cref="Scale.MajorStepAuto"/>
        /// </summary>
        private bool _minAuto;

        /// <summary>
        /// The axis range data for <see cref="Scale.Min"/>, <see cref="Scale.Max"/>,
        /// <see cref="Scale.MinorStep"/>, and <see cref="Scale.MajorStep"/>
        /// </summary>
        private double _minorStep;

        /// <summary>
        /// The status of <see cref="Scale.MinAuto"/>,
        /// <see cref="Scale.MaxAuto"/>, <see cref="Scale.MinorStepAuto"/>,
        /// and <see cref="Scale.MajorStepAuto"/>
        /// </summary>
        private bool _minorStepAuto;

        /// <summary>
        /// The status of <see cref="Scale.MajorUnit"/> and <see cref="Scale.MinorUnit"/>
        /// </summary>
        private DateUnit _minorUnit;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleState"/> class. 
        /// Construct a <see cref="ScaleState"/> from the specified <see cref="Axis"/>
        /// </summary>
        /// <param name="axis">
        /// The <see cref="Axis"/> from which to collect the scale
        /// range settings.
        /// </param>
        public ScaleState(Axis axis)
        {
            this._min = axis._scale._min;
            this._minorStep = axis._scale._minorStep;
            this._majorStep = axis._scale._majorStep;
            this._max = axis._scale._max;
            this._majorUnit = axis._scale._majorUnit;
            this._minorUnit = axis._scale._minorUnit;

            this._format = axis._scale._format;
            this._mag = axis._scale._mag;

            // this.numDec = axis.NumDec;
            this._minAuto = axis._scale._minAuto;
            this._majorStepAuto = axis._scale._majorStepAuto;
            this._minorStepAuto = axis._scale._minorStepAuto;
            this._maxAuto = axis._scale._maxAuto;

            this._formatAuto = axis._scale._formatAuto;
            this._magAuto = axis._scale._magAuto;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleState"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="ScaleState"/> object from which to copy
        /// </param>
        public ScaleState(ScaleState rhs)
        {
            this._min = rhs._min;
            this._majorStep = rhs._majorStep;
            this._minorStep = rhs._minorStep;
            this._max = rhs._max;
            this._majorUnit = rhs._majorUnit;
            this._minorUnit = rhs._minorUnit;

            this._format = rhs._format;
            this._mag = rhs._mag;

            this._minAuto = rhs._minAuto;
            this._majorStepAuto = rhs._majorStepAuto;
            this._minorStepAuto = rhs._minorStepAuto;
            this._maxAuto = rhs._maxAuto;

            this._formatAuto = rhs._formatAuto;
            this._magAuto = rhs._magAuto;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Copy the properties from this <see cref="ScaleState"/> out to the specified <see cref="Axis"/>.
        /// </summary>
        /// <param name="axis">
        /// The <see cref="Axis"/> reference to which the properties should be
        /// copied
        /// </param>
        public void ApplyScale(Axis axis)
        {
            axis._scale._min = this._min;
            axis._scale._majorStep = this._majorStep;
            axis._scale._minorStep = this._minorStep;
            axis._scale._max = this._max;
            axis._scale._majorUnit = this._majorUnit;
            axis._scale._minorUnit = this._minorUnit;

            axis._scale._format = this._format;
            axis._scale._mag = this._mag;

            // The auto settings must be made after the min/step/max settings, since setting those
            // properties actually affects the auto settings.
            axis._scale._minAuto = this._minAuto;
            axis._scale._minorStepAuto = this._minorStepAuto;
            axis._scale._majorStepAuto = this._majorStepAuto;
            axis._scale._maxAuto = this._maxAuto;

            axis._scale._formatAuto = this._formatAuto;
            axis._scale._magAuto = this._magAuto;
        }

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public ScaleState Clone()
        {
            return new ScaleState(this);
        }

        /// <summary>
        /// Determine if the state contained in this <see cref="ScaleState"/> object is different from
        /// the state of the specified <see cref="Axis"/>.
        /// </summary>
        /// <param name="axis">
        /// The <see cref="Axis"/> object with which to compare states.
        /// </param>
        /// <returns>
        /// true if the states are different, false otherwise
        /// </returns>
        public bool IsChanged(Axis axis)
        {
            return axis._scale._min != this._min || axis._scale._majorStep != this._majorStep
                   || axis._scale._minorStep != this._minorStep || axis._scale._max != this._max
                   || axis._scale._minorUnit != this._minorUnit || axis._scale._majorUnit != this._majorUnit
                   || axis._scale._minAuto != this._minAuto || axis._scale._minorStepAuto != this._minorStepAuto
                   || axis._scale._majorStepAuto != this._majorStepAuto || axis._scale._maxAuto != this._maxAuto;
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
    }
}