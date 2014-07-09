// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="BasicArrayPointList.cs">
//   
// </copyright>
// <summary>
//   A data collection class for ZedGraph, provided as an alternative to <see cref="PointPairList" />.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;

    /// <summary>
    /// A data collection class for ZedGraph, provided as an alternative to <see cref="PointPairList" />.
    /// </summary>
    /// <remarks>
    /// The data storage class for ZedGraph can be any type, so long as it uses the <see cref="IPointList" />
    /// interface.  This class, albeit simple, is a demonstration of implementing the <see cref="IPointList" />
    /// interface to provide a simple data collection using only two arrays.  The <see cref="IPointList" />
    /// interface can also be used as a layer between ZedGraph and a database, for example.
    /// </remarks>
    /// <seealso cref="PointPairList" />
    /// <seealso cref="IPointList" />
    /// 
    /// <author> John Champion</author>
    /// <version> $Revision: 3.4 $ $Date: 2007-02-18 05:51:53 $ </version>
    [Serializable]
    public class BasicArrayPointList : IPointList
    {
        #region Fields

        /// <summary>
        /// Instance of an array of x values
        /// </summary>
        public double[] x;

        /// <summary>
        /// Instance of an array of x values
        /// </summary>
        public double[] y;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicArrayPointList"/> class. 
        /// Constructor to initialize the PointPairList from two arrays of
        /// type double.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        public BasicArrayPointList(double[] x, double[] y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicArrayPointList"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The PointPairList from which to copy
        /// </param>
        public BasicArrayPointList(BasicArrayPointList rhs)
        {
            this.x = (double[])rhs.x.Clone();
            this.y = (double[])rhs.y.Clone();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the number of points available in the arrays.  Count will be the greater
        /// of the lengths of the X and Y arrays.
        /// </summary>
        public int Count
        {
            get
            {
                return this.x.Length > this.y.Length ? this.x.Length : this.y.Length;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Indexer to access the specified <see cref="PointPair"/> object by
        /// its ordinal position in the list.
        /// </summary>
        /// <remarks>
        /// Returns <see cref="PointPairBase.Missing"/> for any value of <see paramref="index"/>
        /// that is outside of its corresponding array bounds.
        /// </remarks>
        /// <param name="index">
        /// The ordinal position (zero-based) of the
        /// <see cref="PointPair"/> object to be accessed.
        /// </param>
        /// <value>
        /// A <see cref="PointPair"/> object reference.
        /// </value>
        /// <returns>
        /// The <see cref="PointPair"/>.
        /// </returns>
        public PointPair this[int index]
        {
            get
            {
                double xVal, yVal;
                if (index >= 0 && index < this.x.Length)
                {
                    xVal = this.x[index];
                }
                else
                {
                    xVal = PointPair.Missing;
                }

                if (index >= 0 && index < this.y.Length)
                {
                    yVal = this.y[index];
                }
                else
                {
                    yVal = PointPair.Missing;
                }

                return new PointPair(xVal, yVal, PointPair.Missing, null);
            }

            set
            {
                if (index >= 0 && index < this.x.Length)
                {
                    this.x[index] = value.X;
                }

                if (index >= 0 && index < this.y.Length)
                {
                    this.y[index] = value.Y;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public BasicArrayPointList Clone()
        {
            return new BasicArrayPointList(this);
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