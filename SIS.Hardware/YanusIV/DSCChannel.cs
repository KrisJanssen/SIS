// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DSCChannel.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The DSC channel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware.YanusIV
{
    /// <summary>
    /// The DSC channel.
    /// </summary>
    public enum DSCChannel
    {
        /// <summary>
        /// Dummy channel
        /// </summary>
        None = 0, // Channel 0 (dummy channel)

        /// <summary>
        /// The X channel
        /// </summary>
        X = 3, // Channel 3 (X axis)

        /// <summary>
        /// The Y channel
        /// </summary>
        Y = 4, // Channel 4 (Y axis)

        /// <summary>
        /// The polytrope.
        /// </summary>
        Polytrope = 5, // Channel 5 (Polytrope)

        /// <summary>
        /// The digital out (DO)
        /// </summary>
        DO = 7 // Channel 7 (digital outputs - 3 bits pattern/marker)
    }
}