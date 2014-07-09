// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="SampleMultiPointList.cs">
//   
// </copyright>
// <summary>
//   An enum used to specify the X or Y data type of interest -- see
//   and .
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Collections;
    using System.Drawing;

    /// <summary>
    /// An enum used to specify the X or Y data type of interest -- see
    /// <see cref="SampleMultiPointList.XData" /> and <see cref="SampleMultiPointList.YData" />.
    /// </summary>
    public enum PerfDataType
    {
        /// <summary>
        /// The time (seconds) at which these data are measured
        /// </summary>
        Time, 

        /// <summary>
        /// The distance traveled, meters
        /// </summary>
        Distance, 

        /// <summary>
        /// The instantaneous velocity, meters per second
        /// </summary>
        Velocity, 

        /// <summary>
        /// The instantaneous acceleration, meters per second squared
        /// </summary>
        Acceleration
    };

    /// <summary>
    /// Sample data structure containing a variety of data values, in this case the values
    /// are related in that they correspond to the same time value.
    /// </summary>
    public class PerformanceData
    {
        #region Fields

        /// <summary>
        /// The instantaneous acceleration, meters per second squared
        /// </summary>
        public double acceleration;

        /// <summary>
        /// The distance traveled, meters
        /// </summary>
        public double distance;

        /// <summary>
        /// The time (seconds) at which these data are measured
        /// </summary>
        public double time;

        /// <summary>
        /// The instantaneous velocity, meters per second
        /// </summary>
        public double velocity;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceData"/> class. 
        /// Constructor that specifies each data value in the PerformanceData struct
        /// </summary>
        /// <param name="time">
        /// The time (seconds) at which these data are measured
        /// </param>
        /// <param name="distance">
        /// The distance traveled, meters
        /// </param>
        /// <param name="velocity">
        /// The instantaneous velocity, meters per second
        /// </param>
        /// <param name="acceleration">
        /// The instantaneous acceleration, meters per second squared
        /// </param>
        public PerformanceData(double time, double distance, double velocity, double acceleration)
        {
            this.time = time;
            this.distance = distance;
            this.velocity = velocity;
            this.acceleration = acceleration;
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Gets or sets the data value as specified by the <see cref="PerfDataType"/> enum
        /// </summary>
        /// <param name="type">
        /// The required data value type
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double this[PerfDataType type]
        {
            get
            {
                switch (type)
                {
                    default:
                    case PerfDataType.Time:
                        return this.time;
                    case PerfDataType.Distance:
                        return this.distance;
                    case PerfDataType.Velocity:
                        return this.velocity;
                    case PerfDataType.Acceleration:
                        return this.acceleration;
                }
            }

            set
            {
                switch (type)
                {
                    case PerfDataType.Time:
                        this.time = value;
                        break;
                    case PerfDataType.Distance:
                        this.distance = value;
                        break;
                    case PerfDataType.Velocity:
                        this.velocity = value;
                        break;
                    case PerfDataType.Acceleration:
                        this.acceleration = value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// A sample class that holds an internal collection, and implements the
    /// <see cref="IPointList" /> interface so that it can be used by ZedGraph as curve data.
    /// </summary>
    /// <remarks>
    /// This particular class efficiently implements the data storage so that the class
    /// can be cloned without duplicating the data points.  For example, you can create
    /// a <see cref="SampleMultiPointList" />, populate it with values, and set
    /// <see cref="XData" /> = <see cref="PerfDataType.Time" /> and
    /// <see cref="YData" /> = <see cref="PerfDataType.Distance" />.
    /// You can then clone this <see cref="SampleMultiPointList" /> to a new one, and set
    /// <see cref="YData" /> = <see cref="PerfDataType.Velocity" />.
    /// Each of these <see cref="SampleMultiPointList" />'s can then be used as an
    /// <see cref="ZedGraph.GraphPane.AddCurve(string,IPointList,Color)" /> argument,
    /// thereby plotting a distance vs time curve and a velocity vs time curve.  There
    /// will still be only one copy of the data in memory.
    /// </remarks>
    [Serializable]
    public class SampleMultiPointList : IPointList
    {
        #region Fields

        /// <summary>
        /// Determines what X data will be returned by the indexer of this list.
        /// </summary>
        public PerfDataType XData;

        /// <summary>
        /// Determines what Y data will be returned by the indexer of this list.
        /// </summary>
        public PerfDataType YData;

        /// <summary>
        /// This is where the data are stored.  Duplicating the <see cref="SampleMultiPointList" />
        /// copies the reference to this <see cref="ArrayList" />, but does not actually duplicate
        /// the data.
        /// </summary>
        private ArrayList DataCollection;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleMultiPointList"/> class. 
        /// Default constructor
        /// </summary>
        public SampleMultiPointList()
        {
            this.XData = PerfDataType.Time;
            this.YData = PerfDataType.Distance;
            this.DataCollection = new ArrayList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleMultiPointList"/> class. 
        /// The Copy Constructor.  This method does NOT duplicate the data, it merely makes
        /// another "Window" into the same collection.  You can make multiple copies and
        /// set the <see cref="XData"/> and/or <see cref="YData"/> properties to different
        /// values to plot different data, while maintaining only one copy of the original values.
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="SampleMultiPointList"/> from which to copy
        /// </param>
        public SampleMultiPointList(SampleMultiPointList rhs)
        {
            this.DataCollection = rhs.DataCollection;
            this.XData = rhs.XData;
            this.YData = rhs.YData;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of data points in the collection
        /// </summary>
        public int Count
        {
            get
            {
                return this.DataCollection.Count;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Indexer to access the data.  This gets the appropriate data and converts to
        /// the <see cref="PointPair"/> struct that is compatible with ZedGraph.  The
        /// actual data returned depends on the values of <see cref="XData"/> and
        /// <see cref="YData"/>.
        /// </summary>
        /// <param name="index">
        /// The ordinal position of the desired point in the list
        /// </param>
        /// <returns>
        /// A <see cref="PointPair"/> corresponding to the specified ordinal data position
        /// </returns>
        public PointPair this[int index]
        {
            get
            {
                double xVal, yVal;
                if (index >= 0 && index < this.Count)
                {
                    // grab the specified PerformanceData struct
                    PerformanceData perfData = (PerformanceData)this.DataCollection[index];

                    // extract the values from the struct according to the user-set
                    // enum values of XData and YData
                    xVal = perfData[this.XData];
                    yVal = perfData[this.YData];
                }
                else
                {
                    xVal = PointPair.Missing;
                    yVal = PointPair.Missing;
                }

                // insert the values into a pointpair and return
                return new PointPair(xVal, yVal, PointPair.Missing, null);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds the specified <see cref="PerformanceData"/> struct to the end of the collection.
        /// </summary>
        /// <param name="perfData">
        /// A <see cref="PerformanceData"/> struct to be added
        /// </param>
        /// <returns>
        /// The ordinal position in the collection where the values were added
        /// </returns>
        public int Add(PerformanceData perfData)
        {
            return this.DataCollection.Add(perfData);
        }

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public SampleMultiPointList Clone()
        {
            return new SampleMultiPointList(this);
        }

        /// <summary>
        /// Insert the specified <see cref="PerformanceData"/> struct into the list at
        /// the specified ordinal location.
        /// </summary>
        /// <param name="index">
        /// The ordinal location at which to insert
        /// </param>
        /// <param name="perfData">
        /// The <see cref="PerformanceData"/> struct to be inserted
        /// </param>
        public void Insert(int index, PerformanceData perfData)
        {
            this.DataCollection.Insert(index, perfData);
        }

        /// <summary>
        /// Remove the <see cref="PerformanceData"/> struct from the list at the specified
        /// ordinal location.
        /// </summary>
        /// <param name="index">
        /// The ordinal location of the <see cref="PerformanceData"/>
        /// struct to be removed
        /// </param>
        public void RemoveAt(int index)
        {
            this.DataCollection.RemoveAt(index);
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