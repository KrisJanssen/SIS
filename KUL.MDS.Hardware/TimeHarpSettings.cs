// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeHarpSettings.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The time harp settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    /// <summary>
    /// The time harp settings.
    /// </summary>
    internal class TimeHarpSettings : AcquisitionSettings
    {
        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="TimeHarpSettings"/> class from being created.
        /// </summary>
        private TimeHarpSettings()
        {
            this.m_iDetectorType = (int)DetectorTypes.TimeHarp;
        }

        #endregion
    }
}