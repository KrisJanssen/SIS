// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeasurementUnit.cs" company="">
//   
// </copyright>
// <summary>
//   Specifies the unit of measure for the given data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Library
{
    /// <summary>
    /// Specifies the unit of measure for the given data.
    /// </summary>
    /// <remarks>
    /// These enumeration values correspond to the values used in the EXIF ResolutionUnit tag.
    /// </remarks>
    public enum MeasurementUnit : int
    {
        /// <summary>
        /// The pixel.
        /// </summary>
        Pixel = 1, 

        /// <summary>
        /// The inch.
        /// </summary>
        Inch = 2, 

        /// <summary>
        /// The centimeter.
        /// </summary>
        Centimeter = 3, 

        /// <summary>
        /// The nanometer.
        /// </summary>
        Nanometer = 4
    }
}