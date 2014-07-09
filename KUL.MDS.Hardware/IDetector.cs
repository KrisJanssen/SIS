// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDetector.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The Detector interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    /// <summary>
    /// The Detector interface.
    /// </summary>
    internal interface IDetector
    {
        #region Public Properties

        /// <summary>
        /// Gets the total samples acquired.
        /// </summary>
        long TotalSamplesAcquired { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The read.
        /// </summary>
        void Read();

        /// <summary>
        /// The setup acquisition.
        /// </summary>
        /// <param name="__AcqSettings">
        /// The __ acq settings.
        /// </param>
        void SetupAcquisition(AcquisitionSettings __AcqSettings);

        /// <summary>
        /// The start acquisition.
        /// </summary>
        void StartAcquisition();

        /// <summary>
        /// The stop acquisition.
        /// </summary>
        void StopAcquisition();

        #endregion
    }
}