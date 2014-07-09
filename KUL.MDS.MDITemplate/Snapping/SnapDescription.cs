// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SnapDescription.cs" company="">
//   
// </copyright>
// <summary>
//   The snap description.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.MDITemplate.Snapping
{
    using System;

    /// <summary>
    /// The snap description.
    /// </summary>
    public sealed class SnapDescription
    {
        #region Fields

        /// <summary>
        /// The horizontal edge.
        /// </summary>
        private HorizontalSnapEdge horizontalEdge;

        /// <summary>
        /// The snapped to.
        /// </summary>
        private SnapObstacle snappedTo;

        /// <summary>
        /// The vertical edge.
        /// </summary>
        private VerticalSnapEdge verticalEdge;

        /// <summary>
        /// The x offset.
        /// </summary>
        private int xOffset;

        /// <summary>
        /// The y offset.
        /// </summary>
        private int yOffset;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapDescription"/> class.
        /// </summary>
        /// <param name="snappedTo">
        /// The snapped to.
        /// </param>
        /// <param name="horizontalEdge">
        /// The horizontal edge.
        /// </param>
        /// <param name="verticalEdge">
        /// The vertical edge.
        /// </param>
        /// <param name="xOffset">
        /// The x offset.
        /// </param>
        /// <param name="yOffset">
        /// The y offset.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public SnapDescription(
            SnapObstacle snappedTo, 
            HorizontalSnapEdge horizontalEdge, 
            VerticalSnapEdge verticalEdge, 
            int xOffset, 
            int yOffset)
        {
            if (snappedTo == null)
            {
                throw new ArgumentNullException("snappedTo");
            }

            this.snappedTo = snappedTo;
            this.horizontalEdge = horizontalEdge;
            this.verticalEdge = verticalEdge;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the horizontal edge.
        /// </summary>
        public HorizontalSnapEdge HorizontalEdge
        {
            get
            {
                return this.horizontalEdge;
            }

            set
            {
                this.horizontalEdge = value;
            }
        }

        /// <summary>
        /// Gets the snapped to.
        /// </summary>
        public SnapObstacle SnappedTo
        {
            get
            {
                return this.snappedTo;
            }
        }

        /// <summary>
        /// Gets or sets the vertical edge.
        /// </summary>
        public VerticalSnapEdge VerticalEdge
        {
            get
            {
                return this.verticalEdge;
            }

            set
            {
                this.verticalEdge = value;
            }
        }

        /// <summary>
        /// Gets or sets the x offset.
        /// </summary>
        public int XOffset
        {
            get
            {
                return this.xOffset;
            }

            set
            {
                this.xOffset = value;
            }
        }

        /// <summary>
        /// Gets or sets the y offset.
        /// </summary>
        public int YOffset
        {
            get
            {
                return this.yOffset;
            }

            set
            {
                this.yOffset = value;
            }
        }

        #endregion
    }
}