// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="PointD.cs">
//   
// </copyright>
// <summary>
//   Simple struct that stores X and Y coordinates as doubles.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;

    /// <summary>
    /// Simple struct that stores X and Y coordinates as doubles.
    /// </summary>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 3.1 $ $Date: 2006-06-24 20:26:44 $ </version>
    [Serializable]
    public struct PointD
    {
        #region Fields

        /// <summary>
        /// The X coordinate
        /// </summary>
        public double X;

        /// <summary>
        /// The Y coordinate
        /// </summary>
        public double Y;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PointD"/> struct. 
        /// Construct a <see cref="PointD"/> object from two double values.
        /// </summary>
        /// <param name="x">
        /// The X coordinate
        /// </param>
        /// <param name="y">
        /// The Y coordinate
        /// </param>
        public PointD(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion
    }
}