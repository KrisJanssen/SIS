// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISnapObstacleHost.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The SnapObstacleHost interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate.Snapping
{
    /// <summary>
    /// The SnapObstacleHost interface.
    /// </summary>
    public interface ISnapObstacleHost
    {
        #region Public Properties

        /// <summary>
        /// Gets the snap obstacle.
        /// </summary>
        SnapObstacle SnapObstacle { get; }

        #endregion
    }
}