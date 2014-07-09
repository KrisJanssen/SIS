// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISnapObstacleHost.cs" company="">
//   
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