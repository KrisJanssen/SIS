// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SnapObstacle.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The snap obstacle.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate.Snapping
{
    using System;
    using System.Drawing;

    using SIS.Base;

    /// <summary>
    /// The snap obstacle.
    /// </summary>
    public abstract class SnapObstacle
    {
        #region Constants

        /// <summary>
        /// The default snap distance.
        /// </summary>
        public const int DefaultSnapDistance = 3;

        /// <summary>
        /// The default snap proximity.
        /// </summary>
        public const int DefaultSnapProximity = 15;

        #endregion

        #region Fields

        /// <summary>
        /// The bounds.
        /// </summary>
        protected Rectangle bounds;

        /// <summary>
        /// The previous bounds.
        /// </summary>
        protected Rectangle previousBounds; // for BoundsChanged event

        /// <summary>
        /// The enable save.
        /// </summary>
        private bool enableSave;

        /// <summary>
        /// The enabled.
        /// </summary>
        private bool enabled;

        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The snap distance.
        /// </summary>
        private int snapDistance;

        /// <summary>
        /// The snap proximity.
        /// </summary>
        private int snapProximity;

        /// <summary>
        /// The snap region.
        /// </summary>
        private SnapRegion snapRegion;

        /// <summary>
        /// The sticky edges.
        /// </summary>
        private bool stickyEdges;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapObstacle"/> class.
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
        internal SnapObstacle(string name, Rectangle bounds, SnapRegion snapRegion, bool stickyEdges)
            : this(name, bounds, snapRegion, stickyEdges, DefaultSnapProximity, DefaultSnapDistance)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapObstacle"/> class.
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
        internal SnapObstacle(
            string name, 
            Rectangle bounds, 
            SnapRegion snapRegion, 
            bool stickyEdges, 
            int snapProximity, 
            int snapDistance)
        {
            this.name = name;
            this.bounds = bounds;
            this.previousBounds = bounds;
            this.snapRegion = snapRegion;
            this.stickyEdges = stickyEdges;
            this.snapProximity = snapProximity;
            this.snapDistance = snapDistance;
            this.enabled = true;
            this.enableSave = true;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Raised after the Bounds is changed.
        /// </summary>
        /// <remarks>
        /// The Data property of the event args is the value that Bounds was just changed from.
        /// </remarks>
        public event EventHandler<EventArgs<Rectangle>> BoundsChanged;

        /// <summary>
        /// Raised before the Bounds is changed.
        /// </summary>
        /// <remarks>
        /// The Data property of the event args is the value that Bounds is being set to.
        /// </remarks>
        public event EventHandler<EventArgs<Rectangle>> BoundsChanging;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the bounds of this snap obstacle, defined in coordinates relative to its container.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return this.bounds;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether enable save.
        /// </summary>
        public bool EnableSave
        {
            get
            {
                return this.enableSave;
            }

            set
            {
                this.enableSave = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }

            set
            {
                this.enabled = value;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets how close another obstacle will be parked when it snaps to this one, in pixels.
        /// </summary>
        public int SnapDistance
        {
            get
            {
                return this.snapDistance;
            }
        }

        /// <summary>
        /// Gets how close another obstacle must be to snap to this one, in pixels
        /// </summary>
        public int SnapProximity
        {
            get
            {
                return this.snapProximity;
            }
        }

        /// <summary>
        /// Gets the snap region.
        /// </summary>
        public SnapRegion SnapRegion
        {
            get
            {
                return this.snapRegion;
            }
        }

        /// <summary>
        /// Gets whether or not this obstacle has "sticky" edges.
        /// </summary>
        /// <remarks>
        /// If an obstacle has sticky edges, than any obstacle that is snapped on 
        /// to it will move with this obstacle.
        /// </remarks>
        public bool StickyEdges
        {
            get
            {
                return this.stickyEdges;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The request bounds change.
        /// </summary>
        /// <param name="newBounds">
        /// The new bounds.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool RequestBoundsChange(Rectangle newBounds)
        {
            bool handled = false;
            this.OnBoundsChangeRequested(newBounds, ref handled);
            return handled;
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
        protected virtual void OnBoundsChangeRequested(Rectangle newBounds, ref bool handled)
        {
        }

        /// <summary>
        /// The on bounds changed.
        /// </summary>
        protected virtual void OnBoundsChanged()
        {
            if (this.BoundsChanged != null)
            {
                this.BoundsChanged(this, new EventArgs<Rectangle>(this.previousBounds));
            }
        }

        /// <summary>
        /// The on bounds changing.
        /// </summary>
        protected virtual void OnBoundsChanging()
        {
            if (this.BoundsChanging != null)
            {
                this.BoundsChanging(this, new EventArgs<Rectangle>(this.Bounds));
            }
        }

        #endregion
    }
}