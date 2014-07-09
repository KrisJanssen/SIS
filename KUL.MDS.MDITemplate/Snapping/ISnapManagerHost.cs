// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISnapManagerHost.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The SnapManagerHost interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate.Snapping
{
    /// <summary>
    /// The SnapManagerHost interface.
    /// </summary>
    public interface ISnapManagerHost
    {
        #region Public Properties

        /// <summary>
        /// Gets the snap manager.
        /// </summary>
        SnapManager SnapManager { get; }

        #endregion
    }
}