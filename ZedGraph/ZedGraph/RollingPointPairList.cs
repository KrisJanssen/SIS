// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="RollingPointPairList.cs">
//   
// </copyright>
// <summary>
//   A class that provides a rolling list of  objects.
//   This is essentially a
//   first-in-first-out (FIFO) queue with a fixed capacity which allows 'rolling'
//   (or oscilloscope like) graphs to be be animated without having the overhead of an
//   ever-growing ArrayList.
//   The queue is constructed with a fixed capacity and new points can be enqueued. When the
//   capacity is reached the oldest (first in) PointPair is overwritten. However, when
//   accessing via , the  objects are
//   seen in the order in which they were enqeued.
//   RollingPointPairList supports data editing through the
//   interface.
//   Colin Green with mods by John Champion
//   $Date: 2007-11-05 04:33:26 $
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// A class that provides a rolling list of <see cref="PointPair" /> objects.
    /// This is essentially a 
    /// first-in-first-out (FIFO) queue with a fixed capacity which allows 'rolling' 
    /// (or oscilloscope like) graphs to be be animated without having the overhead of an
    /// ever-growing ArrayList.
    /// 
    /// The queue is constructed with a fixed capacity and new points can be enqueued. When the 
    /// capacity is reached the oldest (first in) PointPair is overwritten. However, when 
    /// accessing via <see cref="IPointList" />, the <see cref="PointPair" /> objects are
    /// seen in the order in which they were enqeued.
    ///
    /// RollingPointPairList supports data editing through the <see cref="IPointListEdit" />
    /// interface.
    /// 
    /// <author>Colin Green with mods by John Champion</author>
    /// <version> $Date: 2007-11-05 04:33:26 $ </version>
    /// </summary>
    [Serializable]
    public class RollingPointPairList : IPointList, ISerializable, IPointListEdit
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema = 10;

        #endregion

        #region Fields

        /// <summary>
        /// The index of the previously enqueued item. -1 if buffer is empty.
        /// </summary>
        protected int _headIdx;

        /// <summary>
        /// An array of PointPair objects that acts as the underlying buffer.
        /// </summary>
        protected PointPair[] _mBuffer;

        /// <summary>
        /// The index of the next item to be dequeued. -1 if buffer is empty.
        /// </summary>
        protected int _tailIdx;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RollingPointPairList"/> class. 
        /// Constructs an empty buffer with the specified capacity.
        /// </summary>
        /// <param name="capacity">
        /// Number of elements in the rolling list.  This number
        /// cannot be changed once the RollingPointPairList is constructed.
        /// </param>
        public RollingPointPairList(int capacity)
            : this(capacity, false)
        {
            this._mBuffer = new PointPair[capacity];
            this._headIdx = this._tailIdx = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RollingPointPairList"/> class. 
        /// Constructs an empty buffer with the specified capacity.  Pre-allocates space
        /// for all PointPair's in the list if <paramref name="preLoad"/> is true.
        /// </summary>
        /// <param name="capacity">
        /// Number of elements in the rolling list.  This number
        /// cannot be changed once the RollingPointPairList is constructed.
        /// </param>
        /// <param name="preLoad">
        /// true to pre-allocate all PointPair instances in
        /// the list, false otherwise.  Note that in order to be memory efficient,
        /// the <see cref="Add(double,double,double)"/> method should be used to add
        /// data.  Avoid the <see cref="Add(PointPair)"/> method.
        /// </param>
        /// <seealso cref="Add(double,double,double)"/>
        public RollingPointPairList(int capacity, bool preLoad)
        {
            this._mBuffer = new PointPair[capacity];
            this._headIdx = this._tailIdx = -1;

            if (preLoad)
            {
                for (int i = 0; i < capacity; i++)
                {
                    this._mBuffer[i] = new PointPair();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RollingPointPairList"/> class. 
        /// Constructs a buffer with a copy of the items within the provided
        /// <see cref="IPointList"/>.
        /// The <see cref="Capacity"/> is set to the length of the provided list.
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="IPointList"/> to be copied.
        /// </param>
        public RollingPointPairList(IPointList rhs)
        {
            this._mBuffer = new PointPair[rhs.Count];

            for (int i = 0; i < rhs.Count; i++)
            {
                this._mBuffer[i] = new PointPair(rhs[i]);
            }

            this._headIdx = rhs.Count - 1;
            this._tailIdx = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RollingPointPairList"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected RollingPointPairList(SerializationInfo info, StreamingContext context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema");

            this._headIdx = info.GetInt32("headIdx");
            this._tailIdx = info.GetInt32("tailIdx");
            this._mBuffer = (PointPair[])info.GetValue("mBuffer", typeof(PointPair[]));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the capacity of the rolling buffer.
        /// </summary>
        public int Capacity
        {
            get
            {
                return this._mBuffer.Length;
            }
        }

        /// <summary>
        /// Gets the count of items within the rolling buffer. Note that this may be less than
        /// the capacity.
        /// </summary>
        public int Count
        {
            get
            {
                if (this._headIdx == -1)
                {
                    return 0;
                }

                if (this._headIdx > this._tailIdx)
                {
                    return (this._headIdx - this._tailIdx) + 1;
                }

                if (this._tailIdx > this._headIdx)
                {
                    return (this._mBuffer.Length - this._tailIdx) + this._headIdx + 1;
                }

                return 1;
            }
        }

        /// <summary>
        /// Gets a bolean that indicates if the buffer is empty.
        /// Alternatively you can test Count==0.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this._headIdx == -1;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Gets or sets the <see cref="PointPair"/> at the specified index in the buffer.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <remarks>
        /// Index must be within the current size of the buffer, e.g., the set
        /// method will not expand the buffer even if <see cref="Capacity"/> is available
        /// </remarks>
        /// <returns>
        /// The <see cref="PointPair"/>.
        /// </returns>
        public PointPair this[int index]
        {
            get
            {
                if (index >= this.Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                index += this._tailIdx;
                if (index >= this._mBuffer.Length)
                {
                    index -= this._mBuffer.Length;
                }

                return this._mBuffer[index];
            }

            set
            {
                if (index >= this.Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                index += this._tailIdx;
                if (index >= this._mBuffer.Length)
                {
                    index -= this._mBuffer.Length;
                }

                this._mBuffer[index] = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Add a <see cref="PointPair"/> onto the head of the queue,
        /// overwriting old values if the buffer is full.
        /// </summary>
        /// <param name="item">
        /// The <see cref="PointPair"/> to be added.
        /// </param>
        public void Add(PointPair item)
        {
            this._mBuffer[this.GetNextIndex()] = item;
        }

        /// <summary>
        /// Add an <see cref="IPointList"/> object to the head of the queue.
        /// </summary>
        /// <param name="pointList">
        /// A reference to the <see cref="IPointList"/> object to
        /// be added
        /// </param>
        public void Add(IPointList pointList)
        {
            // A slightly more efficient approach would be to determine where the new points should placed within
            // the buffer and to then copy them in directly - updating the head and tail indexes appropriately.
            for (int i = 0; i < pointList.Count; i++)
            {
                this.Add(pointList[i]);
            }
        }

        /// <summary>
        /// Add a set of values onto the head of the queue,
        /// overwriting old values if the buffer is full.
        /// </summary>
        /// <remarks>
        /// This method is much more efficient that the <see cref="Add(PointPair)">Add(PointPair)</see>
        /// method, since it does not require that a new PointPair instance be provided.
        /// If the buffer already contains a <see cref="PointPair"/> at the head position,
        /// then the x, y, z, and tag values will be copied into the existing PointPair.
        /// Otherwise, a new PointPair instance must be created.
        /// In this way, each PointPair position in the rolling list will only be allocated one time.
        /// To truly be memory efficient, the <see cref="Remove"/>, <see cref="RemoveAt"/>,
        /// and <see cref="Pop"/> methods should be avoided.  Also, the <paramref name="tag"/> property
        /// for this method should be null, since it is a reference type.
        /// </remarks>
        /// <param name="x">
        /// The X value
        /// </param>
        /// <param name="y">
        /// The Y value
        /// </param>
        /// <param name="z">
        /// The Z value
        /// </param>
        /// <param name="tag">
        /// The Tag value for the PointPair
        /// </param>
        public void Add(double x, double y, double z, object tag)
        {
            // advance the rolling list
            this.GetNextIndex();

            if (this._mBuffer[this._headIdx] == null)
            {
                this._mBuffer[this._headIdx] = new PointPair(x, y, z, tag);
            }
            else
            {
                this._mBuffer[this._headIdx].X = x;
                this._mBuffer[this._headIdx].Y = y;
                this._mBuffer[this._headIdx].Z = z;
                this._mBuffer[this._headIdx].Tag = tag;
            }
        }

        /// <summary>
        /// Add a set of values onto the head of the queue,
        /// overwriting old values if the buffer is full.
        /// </summary>
        /// <remarks>
        /// This method is much more efficient that the <see cref="Add(PointPair)">Add(PointPair)</see>
        /// method, since it does not require that a new PointPair instance be provided.
        /// If the buffer already contains a <see cref="PointPair"/> at the head position,
        /// then the x, y, z, and tag values will be copied into the existing PointPair.
        /// Otherwise, a new PointPair instance must be created.
        /// In this way, each PointPair position in the rolling list will only be allocated one time.
        /// To truly be memory efficient, the <see cref="Remove"/>, <see cref="RemoveAt"/>,
        /// and <see cref="Pop"/> methods should be avoided.
        /// </remarks>
        /// <param name="x">
        /// The X value
        /// </param>
        /// <param name="y">
        /// The Y value
        /// </param>
        public void Add(double x, double y)
        {
            this.Add(x, y, PointPair.Missing, null);
        }

        /// <summary>
        /// Add a set of values onto the head of the queue,
        /// overwriting old values if the buffer is full.
        /// </summary>
        /// <remarks>
        /// This method is much more efficient that the <see cref="Add(PointPair)">Add(PointPair)</see>
        /// method, since it does not require that a new PointPair instance be provided.
        /// If the buffer already contains a <see cref="PointPair"/> at the head position,
        /// then the x, y, z, and tag values will be copied into the existing PointPair.
        /// Otherwise, a new PointPair instance must be created.
        /// In this way, each PointPair position in the rolling list will only be allocated one time.
        /// To truly be memory efficient, the <see cref="Remove"/>, <see cref="RemoveAt"/>,
        /// and <see cref="Pop"/> methods should be avoided.  Also, the <paramref name="tag"/> property
        /// for this method should be null, since it is a reference type.
        /// </remarks>
        /// <param name="x">
        /// The X value
        /// </param>
        /// <param name="y">
        /// The Y value
        /// </param>
        /// <param name="tag">
        /// The Tag value for the PointPair
        /// </param>
        public void Add(double x, double y, object tag)
        {
            this.Add(x, y, PointPair.Missing, tag);
        }

        /// <summary>
        /// Add a set of values onto the head of the queue,
        /// overwriting old values if the buffer is full.
        /// </summary>
        /// <remarks>
        /// This method is much more efficient that the <see cref="Add(PointPair)">Add(PointPair)</see>
        /// method, since it does not require that a new PointPair instance be provided.
        /// If the buffer already contains a <see cref="PointPair"/> at the head position,
        /// then the x, y, z, and tag values will be copied into the existing PointPair.
        /// Otherwise, a new PointPair instance must be created.
        /// In this way, each PointPair position in the rolling list will only be allocated one time.
        /// To truly be memory efficient, the <see cref="Remove"/>, <see cref="RemoveAt"/>,
        /// and <see cref="Pop"/> methods should be avoided.
        /// </remarks>
        /// <param name="x">
        /// The X value
        /// </param>
        /// <param name="y">
        /// The Y value
        /// </param>
        /// <param name="z">
        /// The Z value
        /// </param>
        public void Add(double x, double y, double z)
        {
            this.Add(x, y, z, null);
        }

        /// <summary>
        /// Add a set of points to the <see cref="RollingPointPairList"/>
        /// from two arrays of type double.
        /// If either array is null, then a set of ordinal values is automatically
        /// generated in its place (see <see cref="AxisType.Ordinal"/>).
        /// If the arrays are of different size, then the larger array prevails and the
        /// smaller array is padded with <see cref="PointPairBase.Missing"/> values.
        /// </summary>
        /// <param name="x">
        /// A double[] array of X values
        /// </param>
        /// <param name="y">
        /// A double[] array of Y values
        /// </param>
        public void Add(double[] x, double[] y)
        {
            int len = 0;

            if (x != null)
            {
                len = x.Length;
            }

            if (y != null && y.Length > len)
            {
                len = y.Length;
            }

            for (int i = 0; i < len; i++)
            {
                PointPair point = new PointPair(0, 0, 0);
                if (x == null)
                {
                    point.X = (double)i + 1.0;
                }
                else if (i < x.Length)
                {
                    point.X = x[i];
                }
                else
                {
                    point.X = PointPair.Missing;
                }

                if (y == null)
                {
                    point.Y = (double)i + 1.0;
                }
                else if (i < y.Length)
                {
                    point.Y = y[i];
                }
                else
                {
                    point.Y = PointPair.Missing;
                }

                this.Add(point);
            }
        }

        /// <summary>
        /// Add a set of points to the <see cref="RollingPointPairList"/> from
        /// three arrays of type double.
        /// If the X or Y array is null, then a set of ordinal values is automatically
        /// generated in its place (see <see cref="AxisType.Ordinal"/>.
        /// If the <see paramref="z"/> value
        /// is null, then it is set to zero.
        /// If the arrays are of different size, then the larger array prevails and the
        /// smaller array is padded with <see cref="PointPairBase.Missing"/> values.
        /// </summary>
        /// <param name="x">
        /// A double[] array of X values
        /// </param>
        /// <param name="y">
        /// A double[] array of Y values
        /// </param>
        /// <param name="z">
        /// A double[] array of Z values
        /// </param>
        public void Add(double[] x, double[] y, double[] z)
        {
            int len = 0;

            if (x != null)
            {
                len = x.Length;
            }

            if (y != null && y.Length > len)
            {
                len = y.Length;
            }

            if (z != null && z.Length > len)
            {
                len = z.Length;
            }

            for (int i = 0; i < len; i++)
            {
                PointPair point = new PointPair();

                if (x == null)
                {
                    point.X = (double)i + 1.0;
                }
                else if (i < x.Length)
                {
                    point.X = x[i];
                }
                else
                {
                    point.X = PointPair.Missing;
                }

                if (y == null)
                {
                    point.Y = (double)i + 1.0;
                }
                else if (i < y.Length)
                {
                    point.Y = y[i];
                }
                else
                {
                    point.Y = PointPair.Missing;
                }

                if (z == null)
                {
                    point.Z = (double)i + 1.0;
                }
                else if (i < z.Length)
                {
                    point.Z = z[i];
                }
                else
                {
                    point.Z = PointPair.Missing;
                }

                this.Add(point);
            }
        }

        /// <summary>
        /// Clear the buffer of all <see cref="PointPair"/> objects.
        /// Note that the <see cref="Capacity" /> remains unchanged.
        /// </summary>
        public void Clear()
        {
            this._headIdx = this._tailIdx = -1;
        }

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public RollingPointPairList Clone()
        {
            return new RollingPointPairList(this);
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
            info.AddValue("headIdx", this._headIdx);
            info.AddValue("tailIdx", this._tailIdx);
            info.AddValue("mBuffer", this._mBuffer);
        }

        /// <summary>
        /// Peek at the <see cref="PointPair" /> item at the head of the queue.
        /// </summary>
        /// <returns>The <see cref="PointPair" /> item at the head of the queue.
        /// Throws an <see cref="InvalidOperationException" /> if the buffer was empty.
        /// </returns>
        public PointPair Peek()
        {
            if (this._headIdx == -1)
            {
                // buffer is currently empty.
                throw new InvalidOperationException("buffer is empty.");
            }

            return this._mBuffer[this._headIdx];
        }

        /// <summary>
        /// Pop an item off the head of the queue.
        /// </summary>
        /// <returns>The popped item. Throws an exception if the buffer was empty.</returns>
        public PointPair Pop()
        {
            if (this._tailIdx == -1)
            {
                // buffer is currently empty.
                throw new InvalidOperationException("buffer is empty.");
            }

            PointPair o = this._mBuffer[this._headIdx];

            if (this._tailIdx == this._headIdx)
            {
                // The buffer is now empty.
                this._headIdx = this._tailIdx = -1;
                return o;
            }

            if (--this._headIdx == -1)
            {
                // Wrap around.
                this._headIdx = this._mBuffer.Length - 1;
            }

            return o;
        }

        /// <summary>
        /// Remove an old item from the tail of the queue.
        /// </summary>
        /// <returns>The removed item. Throws an <see cref="InvalidOperationException" />
        /// if the buffer was empty. 
        /// Check the buffer's length (<see cref="Count" />) or the <see cref="IsEmpty" />
        /// property to avoid exceptions.</returns>
        public PointPair Remove()
        {
            if (this._tailIdx == -1)
            {
                // buffer is currently empty.
                throw new InvalidOperationException("buffer is empty.");
            }

            PointPair o = this._mBuffer[this._tailIdx];

            if (this._tailIdx == this._headIdx)
            {
                // The buffer is now empty.
                this._headIdx = this._tailIdx = -1;
                return o;
            }

            if (++this._tailIdx == this._mBuffer.Length)
            {
                // Wrap around.
                this._tailIdx = 0;
            }

            return o;
        }

        /// <summary>
        /// Remove the <see cref="PointPair"/> at the specified index
        /// </summary>
        /// <remarks>
        /// All items in the queue that lie after <paramref name="index"/> will
        /// be shifted back by one, and the queue will be one item shorter.
        /// </remarks>
        /// <param name="index">
        /// The ordinal position of the item to be removed.
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if index is less than
        /// zero or greater than or equal to <see cref="Count"/>
        /// </param>
        public void RemoveAt(int index)
        {
            int count = this.Count;

            if (index >= count || index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            // shift all the items that lie after index back by 1
            for (int i = index + this._tailIdx; i < this._tailIdx + count - 1; i++)
            {
                i = (i >= this._mBuffer.Length) ? 0 : i;
                int j = i + 1;
                j = (j >= this._mBuffer.Length) ? 0 : j;
                this._mBuffer[i] = this._mBuffer[j];
            }

            // Remove the item from the head (it's been duplicated already)
            this.Pop();
        }

        /// <summary>
        /// Remove a range of <see cref="PointPair"/> objects starting at the specified index
        /// </summary>
        /// <remarks>
        /// All items in the queue that lie after <paramref name="index"/> will
        /// be shifted back, and the queue will be <paramref name="count"/> items shorter.
        /// </remarks>
        /// <param name="index">
        /// The ordinal position of the item to be removed.
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if index is less than
        /// zero or greater than or equal to <see cref="Count"/>
        /// </param>
        /// <param name="count">
        /// The number of items to be removed.  Throws an
        /// <see cref="ArgumentOutOfRangeException"/> if <paramref name="count"/> is less than zero
        /// or greater than the total available items in the queue
        /// </param>
        public void RemoveRange(int index, int count)
        {
            int totalCount = this.Count;

            if (index >= totalCount || index < 0 || count < 0 || count > totalCount)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (int i = 0; i < count; i++)
            {
                this.RemoveAt(index);
            }
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
        /// Calculate that the next index in the buffer that should receive a new data point.
        /// Note that this method actually advances the buffer, so a datapoint should be
        /// added at _mBuffer[_headIdx].
        /// </summary>
        /// <returns>The index position of the new head element</returns>
        private int GetNextIndex()
        {
            if (this._headIdx == -1)
            {
                // buffer is currently empty.
                this._headIdx = this._tailIdx = 0;
            }
            else
            {
                // Determine the index to write to.
                if (++this._headIdx == this._mBuffer.Length)
                {
                    // Wrap around.
                    this._headIdx = 0;
                }

                if (this._headIdx == this._tailIdx)
                {
                    // Buffer overflow. Increment tailIdx.
                    if (++this._tailIdx == this._mBuffer.Length)
                    {
                        // Wrap around.
                        this._tailIdx = 0;
                    }
                }
            }

            return this._headIdx;
        }

        #endregion
    }
}