// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="IPointList.cs">
//   
// </copyright>
// <summary>
//   An interface to a collection class containing data
//   that define the set of points to be displayed on the curve.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;

    /// <summary>
    /// An interface to a collection class containing data
    /// that define the set of points to be displayed on the curve.
    /// </summary>
    /// <remarks>
    /// This interface is designed to allow customized data abstraction.  The default data
    /// collection class is <see cref="PointPairList" />, however, you can define your own
    /// data collection class using the <see cref="IPointList" /> interface.
    /// </remarks>
    /// <seealso cref="PointPairList" />
    /// <seealso cref="BasicArrayPointList" />
    /// 
    /// <author> John Champion</author>
    /// <version> $Revision: 1.6 $ $Date: 2007-11-11 07:29:43 $ </version>
    public interface IPointList : ICloneable
    {
        #region Public Properties

        /// <summary>
        /// Gets the number of points available in the list.
        /// </summary>
        int Count { get; }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Indexer to access a data point by its ordinal position in the collection.
        /// </summary>
        /// <remarks>
        /// This is the standard interface that ZedGraph uses to access the data.  Although
        /// you must pass a <see cref="PointPair"/> here, your internal data storage format
        /// can be anything.
        /// </remarks>
        /// <param name="index">
        /// The ordinal position (zero-based) of the
        /// data point to be accessed.
        /// </param>
        /// <value>
        /// A <see cref="PointPair"/> object instance.
        /// </value>
        /// <returns>
        /// The <see cref="PointPair"/>.
        /// </returns>
        PointPair this[int index] { get; }

        #endregion
    }
}