// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vertex.cs" company="">
//   
// </copyright>
// <summary>
//   The vertex.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Systemlayer.GpcWrapper
{
    /// <summary>
    /// The vertex.
    /// </summary>
    internal struct Vertex
    {
        #region Fields

        /// <summary>
        /// The x.
        /// </summary>
        public double X;

        /// <summary>
        /// The y.
        /// </summary>
        public double Y;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> struct.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        public Vertex(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion
    }
}