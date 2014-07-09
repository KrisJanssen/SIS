// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SamplePointList.cs" company="">
//   
// </copyright>
// <summary>
//   enumeration used to indicate which type of data will be plotted.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Collections;

    /// <summary>
    /// enumeration used to indicate which type of data will be plotted.
    /// </summary>
    public enum SampleType
    {
        /// <summary>
        /// Designates the "Time" property will be used
        /// </summary>
        Time, 

        /// <summary>
        /// Designates the "Position" property will be used
        /// </summary>
        Position, 

        /// <summary>
        /// Designates the Instantaneous Velocity property will be used
        /// </summary>
        VelocityInst, 

        /// <summary>
        /// Designates the "Time since start" property will be used
        /// </summary>
        TimeDiff, 

        /// <summary>
        /// Designates the Average Velocity property will be used
        /// </summary>
        VelocityAvg
    };

    /// <summary>
    /// A simple storage class to maintain an individual sampling of data
    /// </summary>
    public class Sample : System.Object
    {
        #region Fields

        /// <summary>
        /// The _position.
        /// </summary>
        private double _position;

        /// <summary>
        /// The _time.
        /// </summary>
        private DateTime _time;

        /// <summary>
        /// The _velocity.
        /// </summary>
        private double _velocity;

        #endregion

        #region Public Properties

        /// <summary>
        /// The position at sample time
        /// </summary>
        public double Position
        {
            get
            {
                return this._position;
            }

            set
            {
                this._position = value;
            }
        }

        /// <summary>
        /// The time of the sample
        /// </summary>
        public DateTime Time
        {
            get
            {
                return this._time;
            }

            set
            {
                this._time = value;
            }
        }

        /// <summary>
        /// The instantaneous velocity at sample time
        /// </summary>
        public double Velocity
        {
            get
            {
                return this._velocity;
            }

            set
            {
                this._velocity = value;
            }
        }

        #endregion
    }

    /// <summary>
    /// A collection class to maintain a set of samples
    /// </summary>
    [Serializable]
    public class SamplePointList : IPointList
    {
        #region Fields

        /// <summary>
        /// Determines what data type gets plotted for the X values
        /// </summary>
        public SampleType XType;

        /// <summary>
        /// Determines what data type gets plotted for the Y values
        /// </summary>
        public SampleType YType;

        // Stores the collection of samples
        /// <summary>
        /// The list.
        /// </summary>
        private ArrayList list;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePointList"/> class. 
        /// default constructor
        /// </summary>
        public SamplePointList()
        {
            this.XType = SampleType.Time;
            this.YType = SampleType.Position;
            this.list = new ArrayList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePointList"/> class. 
        /// copy constructor -- this returns a copy of the structure,
        /// but it does not duplicate the data (it just keeps a reference to the original)
        /// </summary>
        /// <param name="rhs">
        /// The SamplePointList to be copied
        /// </param>
        public SamplePointList(SamplePointList rhs)
        {
            this.XType = rhs.XType;
            this.YType = rhs.YType;

            // Don't duplicate the data values, just copy the reference to the ArrayList
            this.list = rhs.list;

            // foreach ( Sample sample in rhs )
            // 	list.Add( sample );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of samples in the collection
        /// </summary>
        public int Count
        {
            get
            {
                return this.list.Count;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Indexer: get the Sample instance at the specified ordinal position in the list
        /// </summary>
        /// <param name="index">
        /// The ordinal position in the list of samples
        /// </param>
        /// <returns>
        /// Returns a <see cref="PointPair"/> instance containing the
        /// data specified by <see cref="XType"/> and <see cref="YType"/>
        /// </returns>
        public PointPair this[int index]
        {
            get
            {
                PointPair pt = new PointPair();
                Sample sample = (Sample)this.list[index];
                pt.X = this.GetValue(sample, this.XType);
                pt.Y = this.GetValue(sample, this.YType);
                return pt;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Append a sample to the collection
        /// </summary>
        /// <param name="sample">
        /// The sample to append
        /// </param>
        /// <returns>
        /// The ordinal position at which the sample was added
        /// </returns>
        public int Add(Sample sample)
        {
            return this.list.Add(sample);
        }

        // generic Clone: just call the typesafe version

        /// <summary>
        /// typesafe clone method
        /// </summary>
        /// <returns>A new cloned SamplePointList.  This returns a copy of the structure,
        /// but it does not duplicate the data (it just keeps a reference to the original)
        /// </returns>
        public SamplePointList Clone()
        {
            return new SamplePointList(this);
        }

        /// <summary>
        /// Get the specified data type from the specified sample
        /// </summary>
        /// <param name="sample">
        /// The sample instance of interest
        /// </param>
        /// <param name="type">
        /// The data type to be extracted from the sample
        /// </param>
        /// <returns>
        /// A double value representing the requested data
        /// </returns>
        public double GetValue(Sample sample, SampleType type)
        {
            switch (type)
            {
                case SampleType.Position:
                    return sample.Position;
                case SampleType.Time:
                    return sample.Time.ToOADate();
                case SampleType.TimeDiff:
                    return sample.Time.ToOADate() - ((Sample)this.list[0]).Time.ToOADate();
                case SampleType.VelocityAvg:
                    double timeDiff = sample.Time.ToOADate() - ((Sample)this.list[0]).Time.ToOADate();
                    if (timeDiff <= 0)
                    {
                        return PointPair.Missing;
                    }
                    else
                    {
                        return (sample.Position - ((Sample)this.list[0]).Position) / timeDiff;
                    }

                case SampleType.VelocityInst:
                    return sample.Velocity;
                default:
                    return PointPair.Missing;
            }
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// The clone.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion
    }
}