// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SnapObstacleController.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The snap obstacle controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate.Snapping
{
    using System.Drawing;

    using SIS.MDITemplate.Events;

    /// <summary>
    /// The snap obstacle controller.
    /// </summary>
    public sealed class SnapObstacleController : SnapObstacle
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapObstacleController"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        /// <param name="snapRegion">
        /// The snap region.
        /// </param>
        /// <param name="stickyEdges">
        /// The sticky edges.
        /// </param>
        public SnapObstacleController(string name, Rectangle bounds, SnapRegion snapRegion, bool stickyEdges)
            : base(name, bounds, snapRegion, stickyEdges)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapObstacleController"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        /// <param name="snapRegion">
        /// The snap region.
        /// </param>
        /// <param name="stickyEdges">
        /// The sticky edges.
        /// </param>
        /// <param name="snapProximity">
        /// The snap proximity.
        /// </param>
        /// <param name="snapDistance">
        /// The snap distance.
        /// </param>
        public SnapObstacleController(
            string name, 
            Rectangle bounds, 
            SnapRegion snapRegion, 
            bool stickyEdges, 
            int snapProximity, 
            int snapDistance)
            : base(name, bounds, snapRegion, stickyEdges, snapProximity, snapDistance)
        {
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Raised when the SnapManager is requesting that the obstacle move and/or resize itself.
        /// Usually this happens in response to another snap container with "sticky edges" changing
        /// its boundary.
        /// </summary>
        public event HandledEventHandler<Rectangle> BoundsChangeRequested;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Used for the obstacle to report changes in the obstacles size and/or location.
        /// </summary>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        public void SetBounds(Rectangle bounds)
        {
            if (this.bounds != bounds)
            {
                this.OnBoundsChanging();
                this.previousBounds = this.bounds;
                this.bounds = bounds;
                this.OnBoundsChanged();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on bounds change requested.
        /// </summary>
        /// <param name="newBounds">
        /// The new bounds.
        /// </param>
        /// <param name="handled">
        /// The handled.
        /// </param>
        protected override void OnBoundsChangeRequested(Rectangle newBounds, ref bool handled)
        {
            if (this.BoundsChangeRequested != null)
            {
                HandledEventArgs<Rectangle> e = new HandledEventArgs<Rectangle>(handled, newBounds);
                this.BoundsChangeRequested(this, e);
                handled = e.Handled;
            }

            base.OnBoundsChangeRequested(newBounds, ref handled);
        }

        #endregion
    }
}