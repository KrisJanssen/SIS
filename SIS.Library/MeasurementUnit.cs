using System;

namespace SIS.Library
{
    /// <summary>
    /// Specifies the unit of measure for the given data.
    /// </summary>
    /// <remarks>
    /// These enumeration values correspond to the values used in the EXIF ResolutionUnit tag.
    /// </remarks>
    public enum MeasurementUnit
        : int
    {
        Pixel = 1,
        Inch = 2,
        Centimeter = 3,
        Nanometer = 4
    }
}
