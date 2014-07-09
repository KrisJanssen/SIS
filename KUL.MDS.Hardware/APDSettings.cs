// --------------------------------------------------------------------------------------------------------------------
// <copyright file="APDSettings.cs" company="">
//   
// </copyright>
// <summary>
//   The apd settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Hardware
{
    /// <summary>
    /// The apd settings.
    /// </summary>
    internal class APDSettings : AcquisitionSettings
    {
        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="APDSettings"/> class from being created.
        /// </summary>
        private APDSettings()
        {
            this.m_iDetectorType = (int)DetectorTypes.TimeHarp;
        }

        #endregion
    }
}