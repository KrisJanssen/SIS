// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Kris Janssen" file="CCDHelper.cs">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Class that helps finding the pins for a specific filter
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.WPFControls.CCDControl
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Class that helps finding the pins for a specific filter
    /// </summary>
    internal static class CCDHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets a specific pin of a specific filter
        /// </summary>
        /// <param name="filter">
        /// Filter to retrieve the pin for (defines which object should make this method available)
        /// </param>
        /// <param name="dir">
        /// Direction
        /// </param>
        /// <param name="num">
        /// Number
        /// </param>
        /// <returns>
        /// IPin object or null
        /// </returns>
        public static IPin GetPin(this IBaseFilter filter, PinDirection dir, int num)
        {
            // Declare variables
            IPin[] pin = new IPin[1];
            IEnumPins pinsEnum = null;

            // Enumerator the pins
            if (filter.EnumPins(out pinsEnum) == 0)
            {
                // Get the pin direction
                PinDirection pinDir;
                int n = 0;

                // Loop the pins
                while (pinsEnum.Next(1, pin, out n) == 0)
                {
                    // Query the direction
                    pin[0].QueryDirection(out pinDir);

                    // Is the pin direction correct?
                    if (pinDir == dir)
                    {
                        // Yes, check the pins
                        if (num == 0)
                        {
                            return pin[0];
                        }

                        num--;
                    }

                    // Release the pin, this is not the one we are looking for
                    Marshal.ReleaseComObject(pin[0]);
                    pin[0] = null;
                }
            }

            // Not found
            return null;
        }

        #endregion
    }
}