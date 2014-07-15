// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanMarkers.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Defines the ScanMarkers type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    /// <summary>
    /// The markers that will be used to synchronize the scan with data recording.
    /// </summary>
    internal enum ScanMarkers
    {
        /// <summary>
        /// The line marker.
        /// </summary>
        LineMarker = 4, 

        /// <summary>
        /// The frame marker.
        /// </summary>
        FrameMarker = 2
    }
}