﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ZoomState.cs">
//   
// </copyright>
// <summary>
//   A class that captures all the scale range settings for a <see cref="GraphPane" />.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;

    /// <summary>
    /// A class that captures all the scale range settings for a <see cref="GraphPane"/>.
    /// </summary>
    /// <remarks>
    /// This class is used to store scale ranges in order to allow zooming out to
    /// prior scale range states.  <see cref="ZoomState"/> objects are maintained in the
    /// <see cref="ZoomStateStack"/> collection.  The <see cref="ZoomState"/> object holds
    /// a <see cref="ScaleState"/> object for each of the three axes; the <see cref="XAxis"/>,
    /// the <see cref="YAxis"/>, and the <see cref="Y2Axis"/>.
    /// </remarks>
    /// <author> John Champion </author>
    /// <version> $Revision: 3.15 $ $Date: 2007-04-16 00:03:07 $ </version>
    public class ZoomState : ICloneable
    {
        #region Fields

        /// <summary>
        /// An enum value indicating the type of adjustment being made to the
        /// scale range state.
        /// </summary>
        private StateType _type;

        /// <summary>
        /// <see cref="ScaleState"/> objects to store the state data from the axes.
        /// </summary>
        private ScaleState _x2Axis;

        /// <summary>
        /// <see cref="ScaleState"/> objects to store the state data from the axes.
        /// </summary>
        private ScaleState _xAxis;

        /// <summary>
        /// The _y 2 axis.
        /// </summary>
        private ScaleStateList _y2Axis;

        /// <summary>
        /// The _y axis.
        /// </summary>
        private ScaleStateList _yAxis;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomState"/> class. 
        /// Construct a <see cref="ZoomState"/> object from the scale ranges settings contained
        /// in the specified <see cref="GraphPane"/>.
        /// </summary>
        /// <param name="pane">
        /// The <see cref="GraphPane"/> from which to obtain the scale
        /// range values.
        /// </param>
        /// <param name="type">
        /// A <see cref="StateType"/> enumeration that indicates whether
        /// this saved state is from a pan or zoom.
        /// </param>
        public ZoomState(GraphPane pane, StateType type)
        {
            this._xAxis = new ScaleState(pane.XAxis);
            this._x2Axis = new ScaleState(pane.X2Axis);
            this._yAxis = new ScaleStateList(pane.YAxisList);
            this._y2Axis = new ScaleStateList(pane.Y2AxisList);
            this._type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomState"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="ZoomState"/> object from which to copy
        /// </param>
        public ZoomState(ZoomState rhs)
        {
            this._xAxis = new ScaleState(rhs._xAxis);
            this._x2Axis = new ScaleState(rhs._x2Axis);
            this._yAxis = new ScaleStateList(rhs._yAxis);
            this._y2Axis = new ScaleStateList(rhs._y2Axis);
        }

        #endregion

        #region Enums

        /// <summary>
        /// An enumeration that describes whether a given state is the result of a Pan or Zoom
        /// operation.
        /// </summary>
        public enum StateType
        {
            /// <summary>
            /// Indicates the <see cref="ZoomState"/> object is from a Zoom operation
            /// </summary>
            Zoom, 

            /// <summary>
            /// Indicates the <see cref="ZoomState"/> object is from a Wheel Zoom operation
            /// </summary>
            WheelZoom, 

            /// <summary>
            /// Indicates the <see cref="ZoomState"/> object is from a Pan operation
            /// </summary>
            Pan, 

            /// <summary>
            /// Indicates the <see cref="ZoomState"/> object is from a Scroll operation
            /// </summary>
            Scroll
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a <see cref="StateType" /> value indicating the type of action (zoom or pan)
        /// saved by this <see cref="ZoomState" />.
        /// </summary>
        public StateType Type
        {
            get
            {
                return this._type;
            }
        }

        /// <summary>
        /// Gets a string representing the type of adjustment that was made when this scale
        /// state was saved.
        /// </summary>
        /// <value>A string representation for the state change type; typically
        /// "Pan", "Zoom", or "Scroll".</value>
        public string TypeString
        {
            get
            {
                switch (this._type)
                {
                    case StateType.Pan:
                        return "Pan";
                    case StateType.WheelZoom:
                        return "WheelZoom";
                    case StateType.Zoom:
                    default:
                        return "Zoom";
                    case StateType.Scroll:
                        return "Scroll";
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Copy the properties from this <see cref="ZoomState"/> out to the specified <see cref="GraphPane"/>.
        /// </summary>
        /// <param name="pane">
        /// The <see cref="GraphPane"/> to which the scale range properties should be
        /// copied.
        /// </param>
        public void ApplyState(GraphPane pane)
        {
            this._xAxis.ApplyScale(pane.XAxis);
            this._x2Axis.ApplyScale(pane.X2Axis);
            this._yAxis.ApplyScale(pane.YAxisList);
            this._y2Axis.ApplyScale(pane.Y2AxisList);
        }

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public ZoomState Clone()
        {
            return new ZoomState(this);
        }

        /// <summary>
        /// Determine if the state contained in this <see cref="ZoomState"/> object is different from
        /// the state of the specified <see cref="GraphPane"/>.
        /// </summary>
        /// <param name="pane">
        /// The <see cref="GraphPane"/> object with which to compare states.
        /// </param>
        /// <returns>
        /// true if the states are different, false otherwise
        /// </returns>
        public bool IsChanged(GraphPane pane)
        {
            return this._xAxis.IsChanged(pane.XAxis) || this._x2Axis.IsChanged(pane.X2Axis)
                   || this._yAxis.IsChanged(pane.YAxisList) || this._y2Axis.IsChanged(pane.Y2AxisList);
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