// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SnapManager.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The snap manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate.Snapping
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    using SIS.Base;
    using SIS.Systemlayer;

    /// <summary>
    /// The snap manager.
    /// </summary>
    public sealed class SnapManager
    {
        #region Constants

        /// <summary>
        /// The height value name.
        /// </summary>
        private const string heightValueName = "Height";

        /// <summary>
        /// The horizontal edge value name.
        /// </summary>
        private const string horizontalEdgeValueName = "HorizontalEdge";

        /// <summary>
        /// The is snapped value name.
        /// </summary>
        private const string isSnappedValueName = "IsSnapped";

        /// <summary>
        /// The left value name.
        /// </summary>
        private const string leftValueName = "Left";

        /// <summary>
        /// The null name.
        /// </summary>
        private const string nullName = "";

        /// <summary>
        /// The snapped to value name.
        /// </summary>
        private const string snappedToValueName = "SnappedTo";

        /// <summary>
        /// The top value name.
        /// </summary>
        private const string topValueName = "Top";

        /// <summary>
        /// The vertical edge value name.
        /// </summary>
        private const string verticalEdgeValueName = "VerticalEdge";

        /// <summary>
        /// The width value name.
        /// </summary>
        private const string widthValueName = "Width";

        /// <summary>
        /// The x offset value name.
        /// </summary>
        private const string xOffsetValueName = "XOffset";

        /// <summary>
        /// The y offset value name.
        /// </summary>
        private const string yOffsetValueName = "YOffset";

        #endregion

        #region Fields

        /// <summary>
        /// The obstacles.
        /// </summary>
        private Dictionary<SnapObstacle, SnapDescription> obstacles = new Dictionary<SnapObstacle, SnapDescription>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapManager"/> class.
        /// </summary>
        public SnapManager()
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The find my snap manager.
        /// </summary>
        /// <param name="me">
        /// The me.
        /// </param>
        /// <returns>
        /// The <see cref="SnapManager"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static SnapManager FindMySnapManager(Control me)
        {
            if (!(me is ISnapObstacleHost))
            {
                throw new ArgumentException("must be called with a Control that implements ISnapObstacleHost");
            }

            ISnapManagerHost ismh;

            ismh = me as ISnapManagerHost;

            if (ismh == null)
            {
                ismh = me.FindForm() as ISnapManagerHost;
            }

            SnapManager sm;
            if (ismh != null)
            {
                sm = ismh.SnapManager;
            }
            else
            {
                sm = null;
            }

            return sm;
        }

        /// <summary>
        /// The add snap obstacle.
        /// </summary>
        /// <param name="snapObstacleHost">
        /// The snap obstacle host.
        /// </param>
        public void AddSnapObstacle(ISnapObstacleHost snapObstacleHost)
        {
            this.AddSnapObstacle(snapObstacleHost.SnapObstacle);
        }

        /// <summary>
        /// The add snap obstacle.
        /// </summary>
        /// <param name="snapObstacle">
        /// The snap obstacle.
        /// </param>
        public void AddSnapObstacle(SnapObstacle snapObstacle)
        {
            if (!this.obstacles.ContainsKey(snapObstacle))
            {
                this.obstacles.Add(snapObstacle, null);

                if (snapObstacle.StickyEdges)
                {
                    snapObstacle.BoundsChanging += this.SnapObstacle_BoundsChanging;
                    snapObstacle.BoundsChanged += this.SnapObstacle_BoundsChanged;
                }
            }
        }

        /// <summary>
        /// Given an obstacle and its attempted destination, determines the correct landing
        /// spot for an obstacle.
        /// </summary>
        /// <param name="movingObstacle">
        /// The obstacle that is moving.
        /// </param>
        /// <param name="newLocation">
        /// The upper-left coordinate of the obstacle's original intended destination.
        /// </param>
        /// <returns>
        /// A Point that determines where the obstacle should be placed instead. If there are no adjustments
        /// required to the obstacle's desintation, then the return value will be equal to newLocation.
        /// </returns>
        /// <remarks>
        /// movingObstacle's SnapDescription will also be updated. The caller of this method is required
        /// to update the SnapObstacle with the new, adjusted location.
        /// </remarks>
        public Point AdjustObstacleDestination(SnapObstacle movingObstacle, Point newLocation)
        {
            Point adjusted1 = this.AdjustObstacleDestination(movingObstacle, newLocation, false);
            Point adjusted2 = this.AdjustObstacleDestination(movingObstacle, adjusted1, true);
            return adjusted2;
        }

        /// <summary>
        /// The adjust obstacle destination.
        /// </summary>
        /// <param name="movingObstacle">
        /// The moving obstacle.
        /// </param>
        /// <param name="newLocation">
        /// The new location.
        /// </param>
        /// <param name="considerStickies">
        /// The consider stickies.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        public Point AdjustObstacleDestination(SnapObstacle movingObstacle, Point newLocation, bool considerStickies)
        {
            Point adjustedLocation = newLocation;
            SnapDescription sd = this.obstacles[movingObstacle];
            SnapDescription newSD = null;

            foreach (SnapObstacle avoidee in this.obstacles.Keys)
            {
                if (avoidee.StickyEdges != considerStickies)
                {
                    continue;
                }

                if (avoidee.Enabled && !object.ReferenceEquals(avoidee, movingObstacle))
                {
                    SnapDescription newSD2 = this.DetermineNewSnapDescription(
                        movingObstacle, 
                        adjustedLocation, 
                        avoidee, 
                        newSD);

                    if (newSD2 != null)
                    {
                        Point adjustedLocation2 = AdjustNewLocation(movingObstacle, adjustedLocation, newSD2);
                        newSD = newSD2;
                        adjustedLocation = adjustedLocation2;
                        Rectangle newBounds = new Rectangle(adjustedLocation, movingObstacle.Bounds.Size);
                    }
                }
            }

            if (sd == null || !sd.SnappedTo.StickyEdges || newSD == null || newSD.SnappedTo.StickyEdges)
            {
                this.obstacles[movingObstacle] = newSD;
            }

            return adjustedLocation;
        }

        /// <summary>
        /// The contains snap obstacle.
        /// </summary>
        /// <param name="snapObstacleHost">
        /// The snap obstacle host.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ContainsSnapObstacle(ISnapObstacleHost snapObstacleHost)
        {
            return this.ContainsSnapObstacle(snapObstacleHost.SnapObstacle);
        }

        /// <summary>
        /// The contains snap obstacle.
        /// </summary>
        /// <param name="snapObstacle">
        /// The snap obstacle.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ContainsSnapObstacle(SnapObstacle snapObstacle)
        {
            return this.obstacles.ContainsKey(snapObstacle);
        }

        /// <summary>
        /// The find obstacle.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="SnapObstacle"/>.
        /// </returns>
        public SnapObstacle FindObstacle(string name)
        {
            foreach (SnapObstacle so in this.obstacles.Keys)
            {
                if (string.Compare(so.Name, name, true) == 0)
                {
                    return so;
                }
            }

            return null;
        }

        /// <summary>
        /// The load.
        /// </summary>
        /// <param name="loadFrom">
        /// The load from.
        /// </param>
        public void Load(ISimpleCollection<string, string> loadFrom)
        {
            SnapObstacle[] newObstacles = new SnapObstacle[this.obstacles.Count];
            this.obstacles.Keys.CopyTo(newObstacles, 0);

            foreach (SnapObstacle obstacle in newObstacles)
            {
                if (obstacle.EnableSave)
                {
                    this.LoadSnapObstacleData(loadFrom, obstacle);
                }
            }
        }

        /// <summary>
        /// The park obstacle.
        /// </summary>
        /// <param name="obstacle">
        /// The obstacle.
        /// </param>
        /// <param name="snappedTo">
        /// The snapped to.
        /// </param>
        /// <param name="hEdge">
        /// The h edge.
        /// </param>
        /// <param name="vEdge">
        /// The v edge.
        /// </param>
        public void ParkObstacle(
            ISnapObstacleHost obstacle, 
            ISnapObstacleHost snappedTo, 
            HorizontalSnapEdge hEdge, 
            VerticalSnapEdge vEdge)
        {
            this.ParkObstacle(obstacle.SnapObstacle, snappedTo.SnapObstacle, hEdge, vEdge);
        }

        /// <summary>
        /// The park obstacle.
        /// </summary>
        /// <param name="obstacle">
        /// The obstacle.
        /// </param>
        /// <param name="snappedTo">
        /// The snapped to.
        /// </param>
        /// <param name="hEdge">
        /// The h edge.
        /// </param>
        /// <param name="vEdge">
        /// The v edge.
        /// </param>
        public void ParkObstacle(
            SnapObstacle obstacle, 
            SnapObstacle snappedTo, 
            HorizontalSnapEdge hEdge, 
            VerticalSnapEdge vEdge)
        {
            SnapDescription sd = new SnapDescription(
                snappedTo, 
                hEdge, 
                vEdge, 
                obstacle.SnapDistance, 
                obstacle.SnapDistance);
            this.obstacles[obstacle] = sd;
            ParkObstacle(obstacle, sd);
        }

        /// <summary>
        /// The remove snap obstacle.
        /// </summary>
        /// <param name="snapObstacleHost">
        /// The snap obstacle host.
        /// </param>
        public void RemoveSnapObstacle(ISnapObstacleHost snapObstacleHost)
        {
            this.RemoveSnapObstacle(snapObstacleHost.SnapObstacle);
        }

        /// <summary>
        /// The remove snap obstacle.
        /// </summary>
        /// <param name="snapObstacle">
        /// The snap obstacle.
        /// </param>
        public void RemoveSnapObstacle(SnapObstacle snapObstacle)
        {
            if (this.obstacles.ContainsKey(snapObstacle))
            {
                this.obstacles.Remove(snapObstacle);

                if (snapObstacle.StickyEdges)
                {
                    snapObstacle.BoundsChanging -= this.SnapObstacle_BoundsChanging;
                    snapObstacle.BoundsChanged -= this.SnapObstacle_BoundsChanged;
                }
            }
        }

        /// <summary>
        /// The repark obstacle.
        /// </summary>
        /// <param name="obstacle">
        /// The obstacle.
        /// </param>
        public void ReparkObstacle(ISnapObstacleHost obstacle)
        {
            this.ReparkObstacle(obstacle.SnapObstacle);
        }

        /// <summary>
        /// The repark obstacle.
        /// </summary>
        /// <param name="obstacle">
        /// The obstacle.
        /// </param>
        public void ReparkObstacle(SnapObstacle obstacle)
        {
            if (this.obstacles.ContainsKey(obstacle))
            {
                SnapDescription sd = this.obstacles[obstacle];

                if (sd != null)
                {
                    ParkObstacle(obstacle, sd);
                }
            }
        }

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="saveTo">
        /// The save to.
        /// </param>
        public void Save(ISimpleCollection<string, string> saveTo)
        {
            foreach (SnapObstacle obstacle in this.obstacles.Keys)
            {
                // TODO: how do we 'erase' something that has this property set to false, for full generality?
                if (obstacle.EnableSave)
                {
                    this.SaveSnapObstacleData(saveTo, obstacle);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The adjust new location.
        /// </summary>
        /// <param name="obstacle">
        /// The obstacle.
        /// </param>
        /// <param name="newLocation">
        /// The new location.
        /// </param>
        /// <param name="snapDescription">
        /// The snap description.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        private static Point AdjustNewLocation(
            SnapObstacle obstacle, 
            Point newLocation, 
            SnapDescription snapDescription)
        {
            if (snapDescription == null
                || (snapDescription.HorizontalEdge == HorizontalSnapEdge.Neither
                    && snapDescription.VerticalEdge == VerticalSnapEdge.Neither))
            {
                return obstacle.Bounds.Location;
            }

            Rectangle obstacleRect = new Rectangle(newLocation, obstacle.Bounds.Size);
            Rectangle snappedToRect = snapDescription.SnappedTo.Bounds;
            HorizontalSnapEdge hEdge = snapDescription.HorizontalEdge;
            VerticalSnapEdge vEdge = snapDescription.VerticalEdge;
            SnapRegion region = snapDescription.SnappedTo.SnapRegion;

            int deltaY = 0;

            if (hEdge == HorizontalSnapEdge.Top && region == SnapRegion.Exterior)
            {
                int newBottomEdge = snappedToRect.Top - snapDescription.YOffset;
                deltaY = obstacleRect.Bottom - newBottomEdge;
            }
            else if (hEdge == HorizontalSnapEdge.Bottom && region == SnapRegion.Exterior)
            {
                int newTopEdge = snappedToRect.Bottom + snapDescription.YOffset;
                deltaY = obstacleRect.Top - newTopEdge;
            }
            else if (hEdge == HorizontalSnapEdge.Top && region == SnapRegion.Interior)
            {
                int newTopEdge = Math.Min(snappedToRect.Bottom, snappedToRect.Top + snapDescription.YOffset);
                deltaY = obstacleRect.Top - newTopEdge;
            }
            else if (hEdge == HorizontalSnapEdge.Bottom && region == SnapRegion.Interior)
            {
                int newBottomEdge = Math.Max(snappedToRect.Top, snappedToRect.Bottom - snapDescription.YOffset);
                deltaY = obstacleRect.Bottom - newBottomEdge;
            }

            int deltaX = 0;

            if (vEdge == VerticalSnapEdge.Left && region == SnapRegion.Exterior)
            {
                int newRightEdge = snappedToRect.Left - snapDescription.XOffset;
                deltaX = obstacleRect.Right - newRightEdge;
            }
            else if (vEdge == VerticalSnapEdge.Right && region == SnapRegion.Exterior)
            {
                int newLeftEdge = snappedToRect.Right + snapDescription.XOffset;
                deltaX = obstacleRect.Left - newLeftEdge;
            }
            else if (vEdge == VerticalSnapEdge.Left && region == SnapRegion.Interior)
            {
                int newLeftEdge = Math.Min(snappedToRect.Right, snappedToRect.Left + snapDescription.XOffset);
                deltaX = obstacleRect.Left - newLeftEdge;
            }
            else if (vEdge == VerticalSnapEdge.Right && region == SnapRegion.Interior)
            {
                int newRightEdge = Math.Max(snappedToRect.Left, snappedToRect.Right - snapDescription.XOffset);
                deltaX = obstacleRect.Right - newRightEdge;
            }

            Point adjustedLocation = new Point(obstacleRect.Left - deltaX, obstacleRect.Top - deltaY);
            return adjustedLocation;
        }

        /// <summary>
        /// The are edges close.
        /// </summary>
        /// <param name="l1">
        /// The l 1.
        /// </param>
        /// <param name="r1">
        /// The r 1.
        /// </param>
        /// <param name="l2">
        /// The l 2.
        /// </param>
        /// <param name="r2">
        /// The r 2.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        private static bool AreEdgesClose(int l1, int r1, int l2, int r2)
        {
            if (r1 < l2)
            {
                return false;
            }
            else if (r2 < l1)
            {
                return false;
            }
            else if (l1 <= l2 && l2 <= r1 && r1 <= r2)
            {
                return true;
            }
            else if (l2 <= l1 && l1 <= r2 && r2 <= r1)
            {
                return true;
            }
            else if (l1 <= l2 && r2 <= r1)
            {
                return true;
            }
            else if (l2 <= l1 && l1 <= r2)
            {
                return true;
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// The park obstacle.
        /// </summary>
        /// <param name="avoider">
        /// The avoider.
        /// </param>
        /// <param name="snapDescription">
        /// The snap description.
        /// </param>
        private static void ParkObstacle(SnapObstacle avoider, SnapDescription snapDescription)
        {
            Point newLocation = avoider.Bounds.Location;
            Point adjustedLocation = AdjustNewLocation(avoider, newLocation, snapDescription);
            Rectangle newBounds = new Rectangle(adjustedLocation, avoider.Bounds.Size);
            avoider.RequestBoundsChange(newBounds);
        }

        /// <summary>
        /// The determine new snap description.
        /// </summary>
        /// <param name="avoider">
        /// The avoider.
        /// </param>
        /// <param name="newLocation">
        /// The new location.
        /// </param>
        /// <param name="avoidee">
        /// The avoidee.
        /// </param>
        /// <param name="currentSnapDescription">
        /// The current snap description.
        /// </param>
        /// <returns>
        /// The <see cref="SnapDescription"/>.
        /// </returns>
        /// <exception cref="InvalidEnumArgumentException">
        /// </exception>
        private SnapDescription DetermineNewSnapDescription(
            SnapObstacle avoider, 
            Point newLocation, 
            SnapObstacle avoidee, 
            SnapDescription currentSnapDescription)
        {
            int ourSnapProximity;

            if (currentSnapDescription != null
                && (currentSnapDescription.HorizontalEdge != HorizontalSnapEdge.Neither
                    || currentSnapDescription.VerticalEdge != VerticalSnapEdge.Neither))
            {
                // the avoider is already snapped to the avoidee -- make it more difficult to un-snap
                ourSnapProximity = avoidee.SnapProximity * 2;
            }
            else
            {
                ourSnapProximity = avoidee.SnapProximity;
            }

            Rectangle avoiderRect = avoider.Bounds;
            avoiderRect.Location = newLocation;
            Rectangle avoideeRect = avoidee.Bounds;

            // Are the vertical edges close enough for snapping?
            bool vertProximity = AreEdgesClose(avoiderRect.Top, avoiderRect.Bottom, avoideeRect.Top, avoideeRect.Bottom);

            // Are the horizontal edges close enough for snapping?
            bool horizProximity = AreEdgesClose(
                avoiderRect.Left, 
                avoiderRect.Right, 
                avoideeRect.Left, 
                avoideeRect.Right);

            // Compute distances from pertinent edges
            // (e.g. if SnapRegion.Interior, figure out distance from avoider's right edge to avoidee's right edge,
            // if SnapRegion.Exterior, figure out distance from avoider's right edge to avoidee's left edge)
            int leftDistance;
            int rightDistance;
            int topDistance;
            int bottomDistance;

            switch (avoidee.SnapRegion)
            {
                case SnapRegion.Interior:
                    leftDistance = Math.Abs(avoiderRect.Left - avoideeRect.Left);
                    rightDistance = Math.Abs(avoiderRect.Right - avoideeRect.Right);
                    topDistance = Math.Abs(avoiderRect.Top - avoideeRect.Top);
                    bottomDistance = Math.Abs(avoiderRect.Bottom - avoideeRect.Bottom);
                    break;

                case SnapRegion.Exterior:
                    leftDistance = Math.Abs(avoiderRect.Left - avoideeRect.Right);
                    rightDistance = Math.Abs(avoiderRect.Right - avoideeRect.Left);
                    topDistance = Math.Abs(avoiderRect.Top - avoideeRect.Bottom);
                    bottomDistance = Math.Abs(avoiderRect.Bottom - avoideeRect.Top);
                    break;

                default:
                    throw new InvalidEnumArgumentException("avoidee.SnapRegion");
            }

            bool leftClose = leftDistance < ourSnapProximity;
            bool rightClose = rightDistance < ourSnapProximity;
            bool topClose = topDistance < ourSnapProximity;
            bool bottomClose = bottomDistance < ourSnapProximity;

            VerticalSnapEdge vEdge = VerticalSnapEdge.Neither;

            if (vertProximity)
            {
                if ((leftClose && avoidee.SnapRegion == SnapRegion.Exterior)
                    || (rightClose && avoidee.SnapRegion == SnapRegion.Interior))
                {
                    vEdge = VerticalSnapEdge.Right;
                }
                else if ((rightClose && avoidee.SnapRegion == SnapRegion.Exterior)
                         || (leftClose && avoidee.SnapRegion == SnapRegion.Interior))
                {
                    vEdge = VerticalSnapEdge.Left;
                }
            }

            HorizontalSnapEdge hEdge = HorizontalSnapEdge.Neither;

            if (horizProximity)
            {
                if ((topClose && avoidee.SnapRegion == SnapRegion.Exterior)
                    || (bottomClose && avoidee.SnapRegion == SnapRegion.Interior))
                {
                    hEdge = HorizontalSnapEdge.Bottom;
                }
                else if ((bottomClose && avoidee.SnapRegion == SnapRegion.Exterior)
                         || (topClose && avoidee.SnapRegion == SnapRegion.Interior))
                {
                    hEdge = HorizontalSnapEdge.Top;
                }
            }

            SnapDescription sd;

            if (hEdge != HorizontalSnapEdge.Neither || vEdge != VerticalSnapEdge.Neither)
            {
                int xOffset = avoider.SnapDistance;
                int yOffset = avoider.SnapDistance;

                if (hEdge == HorizontalSnapEdge.Neither)
                {
                    if (avoidee.SnapRegion == SnapRegion.Interior)
                    {
                        yOffset = avoiderRect.Top - avoideeRect.Top;
                        hEdge = HorizontalSnapEdge.Top;
                    }
                }

                if (vEdge == VerticalSnapEdge.Neither)
                {
                    if (avoidee.SnapRegion == SnapRegion.Interior)
                    {
                        xOffset = avoiderRect.Left - avoideeRect.Left;
                        vEdge = VerticalSnapEdge.Left;
                    }
                }

                sd = new SnapDescription(avoidee, hEdge, vEdge, xOffset, yOffset);
            }
            else
            {
                sd = null;
            }

            return sd;
        }

        /// <summary>
        /// The load snap obstacle data.
        /// </summary>
        /// <param name="loadFrom">
        /// The load from.
        /// </param>
        /// <param name="so">
        /// The so.
        /// </param>
        private void LoadSnapObstacleData(ISimpleCollection<string, string> loadFrom, SnapObstacle so)
        {
            string prefix = so.Name + ".";
            SnapDescription sd;

            string isSnappedString = loadFrom.Get(prefix + isSnappedValueName);
            bool isSnapped = bool.Parse(isSnappedString);

            if (isSnapped)
            {
                string snappedToString = loadFrom.Get(prefix + snappedToValueName);
                SnapObstacle snappedTo = this.FindObstacle(snappedToString);

                string horizontalEdgeString = loadFrom.Get(prefix + horizontalEdgeValueName);
                HorizontalSnapEdge horizontalEdge =
                    (HorizontalSnapEdge)Enum.Parse(typeof(HorizontalSnapEdge), horizontalEdgeString, true);

                string verticalEdgeString = loadFrom.Get(prefix + verticalEdgeValueName);
                VerticalSnapEdge verticalEdge =
                    (VerticalSnapEdge)Enum.Parse(typeof(VerticalSnapEdge), verticalEdgeString, true);

                string xOffsetString = loadFrom.Get(prefix + xOffsetValueName);
                int xOffset = int.Parse(xOffsetString, CultureInfo.InvariantCulture);

                string yOffsetString = loadFrom.Get(prefix + yOffsetValueName);
                int yOffset = int.Parse(yOffsetString, CultureInfo.InvariantCulture);

                sd = new SnapDescription(snappedTo, horizontalEdge, verticalEdge, xOffset, yOffset);
            }
            else
            {
                sd = null;
            }

            this.obstacles[so] = sd;

            string leftString = loadFrom.Get(prefix + leftValueName);
            int left = int.Parse(leftString, CultureInfo.InvariantCulture);

            string topString = loadFrom.Get(prefix + topValueName);
            int top = int.Parse(topString, CultureInfo.InvariantCulture);

            string widthString = loadFrom.Get(prefix + widthValueName);
            int width = int.Parse(widthString, CultureInfo.InvariantCulture);

            string heightString = loadFrom.Get(prefix + heightValueName);
            int height = int.Parse(heightString, CultureInfo.InvariantCulture);

            Rectangle newBounds = new Rectangle(left, top, width, height);
            so.RequestBoundsChange(newBounds);

            if (sd != null)
            {
                ParkObstacle(so, sd);
            }
        }

        /// <summary>
        /// The save snap obstacle data.
        /// </summary>
        /// <param name="saveTo">
        /// The save to.
        /// </param>
        /// <param name="so">
        /// The so.
        /// </param>
        private void SaveSnapObstacleData(ISimpleCollection<string, string> saveTo, SnapObstacle so)
        {
            string prefix = so.Name + ".";
            SnapDescription sd = this.obstacles[so];

            bool isSnappedValue = sd != null;
            saveTo.Set(prefix + isSnappedValueName, isSnappedValue.ToString(CultureInfo.InvariantCulture));

            if (isSnappedValue)
            {
                saveTo.Set(prefix + snappedToValueName, sd.SnappedTo.Name);
                saveTo.Set(prefix + horizontalEdgeValueName, sd.HorizontalEdge.ToString());
                saveTo.Set(prefix + verticalEdgeValueName, sd.VerticalEdge.ToString());
                saveTo.Set(prefix + xOffsetValueName, sd.XOffset.ToString(CultureInfo.InvariantCulture));
                saveTo.Set(prefix + yOffsetValueName, sd.YOffset.ToString(CultureInfo.InvariantCulture));
            }

            saveTo.Set(prefix + leftValueName, so.Bounds.Left.ToString(CultureInfo.InvariantCulture));
            saveTo.Set(prefix + topValueName, so.Bounds.Top.ToString(CultureInfo.InvariantCulture));
            saveTo.Set(prefix + widthValueName, so.Bounds.Width.ToString(CultureInfo.InvariantCulture));
            saveTo.Set(prefix + heightValueName, so.Bounds.Height.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// The snap obstacle_ bounds changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SnapObstacle_BoundsChanged(object sender, EventArgs<Rectangle> e)
        {
            SnapObstacle senderSO = (SnapObstacle)sender;
            Rectangle fromRect = e.Data;
            Rectangle toRect = senderSO.Bounds;
            this.UpdateDependentObstacles(senderSO, fromRect, toRect);
        }

        /// <summary>
        /// The snap obstacle_ bounds changing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SnapObstacle_BoundsChanging(object sender, EventArgs<Rectangle> e)
        {
        }

        /// <summary>
        /// The update dependent obstacles.
        /// </summary>
        /// <param name="senderSO">
        /// The sender so.
        /// </param>
        /// <param name="fromRect">
        /// The from rect.
        /// </param>
        /// <param name="toRect">
        /// The to rect.
        /// </param>
        private void UpdateDependentObstacles(SnapObstacle senderSO, Rectangle fromRect, Rectangle toRect)
        {
            int leftDelta = toRect.Left - fromRect.Left;
            int topDelta = toRect.Top - fromRect.Top;
            int rightDelta = toRect.Right - fromRect.Right;
            int bottomDelta = toRect.Bottom - fromRect.Bottom;

            foreach (SnapObstacle obstacle in this.obstacles.Keys)
            {
                if (!object.ReferenceEquals(senderSO, obstacle))
                {
                    SnapDescription sd = this.obstacles[obstacle];

                    if (sd != null && object.ReferenceEquals(sd.SnappedTo, senderSO))
                    {
                        int deltaX;

                        if (sd.VerticalEdge == VerticalSnapEdge.Right)
                        {
                            deltaX = rightDelta;
                        }
                        else
                        {
                            deltaX = leftDelta;
                        }

                        int deltaY;

                        if (sd.HorizontalEdge == HorizontalSnapEdge.Bottom)
                        {
                            deltaY = bottomDelta;
                        }
                        else
                        {
                            deltaY = topDelta;
                        }

                        Rectangle oldBounds = obstacle.Bounds;
                        Point newLocation1 = new Point(oldBounds.Left + deltaX, oldBounds.Top + deltaY);
                        Point newLocation2 = AdjustNewLocation(obstacle, newLocation1, sd);
                        Rectangle newBounds = new Rectangle(newLocation2, oldBounds.Size);

                        obstacle.RequestBoundsChange(newBounds);

                        // Recursively update anything snapped to this obstacle
                        this.UpdateDependentObstacles(obstacle, oldBounds, newBounds);
                    }
                }
            }
        }

        #endregion
    }
}