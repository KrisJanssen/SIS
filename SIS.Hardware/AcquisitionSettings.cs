// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcquisitionSettings.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The detector types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    /// <summary>
    /// The detector types.
    /// </summary>
    public enum DetectorTypes
    {
        /// <summary>
        /// The apd.
        /// </summary>
        APD, 

        /// <summary>
        /// The time harp.
        /// </summary>
        TimeHarp
    };

    /// <summary>
    /// The acquisition settings.
    /// </summary>
    public abstract class AcquisitionSettings
    {
        #region Fields

        /// <summary>
        /// The m_i detector type.
        /// </summary>
        protected int m_iDetectorType;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the detector type.
        /// </summary>
        public int DetectorType
        {
            get
            {
                return this.m_iDetectorType;
            }
        }

        #endregion
    }
}