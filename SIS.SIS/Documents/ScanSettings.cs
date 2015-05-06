// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanSettings.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The scan settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Documents
{
    /// <summary>
    /// The scan settings.
    /// </summary>
    public struct ScanSettings
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the annotation.
        /// </summary>
        public string Annotation { get; set; }

        /// <summary>
        /// Gets or sets the channels.
        /// </summary>
        public int Channels { get; set; }

        /// <summary>
        /// Gets or sets the image depth px.
        /// </summary>
        public ushort ImageDepthPx { get; set; }

        /// <summary>
        /// Gets or sets the image height px.
        /// </summary>
        public ushort ImageHeightPx { get; set; }

        /// <summary>
        /// Gets or sets the image width px.
        /// </summary>
        public ushort ImageWidthPx { get; set; }

        /// <summary>
        /// Gets or sets the init x nm.
        /// </summary>
        public double InitXNm { get; set; }

        /// <summary>
        /// Gets or sets the init y nm.
        /// </summary>
        public double InitYNm { get; set; }

        /// <summary>
        /// Gets or sets the init z nm.
        /// </summary>
        public double InitZNm { get; set; }

        /// <summary>
        /// Gets or sets the time p pixel.
        /// </summary>
        public double TimePPixel { get; set; }

        /// <summary>
        /// Gets or sets the x over scan px.
        /// </summary>
        public ushort XOverScanPx { get; set; }

        /// <summary>
        /// Gets or sets the x scan size nm.
        /// </summary>
        public double XScanSizeNm { get; set; }

        /// <summary>
        /// Gets or sets the y over scan px.
        /// </summary>
        public ushort YOverScanPx { get; set; }

        /// <summary>
        /// Gets or sets the y scan size nm.
        /// </summary>
        public double YScanSizeNm { get; set; }

        /// <summary>
        /// Gets or sets the z over scan px.
        /// </summary>
        public ushort ZOverScanPx { get; set; }

        /// <summary>
        /// Gets or sets the z scan size nm.
        /// </summary>
        public double ZScanSizeNm { get; set; }

        #endregion
    }
}