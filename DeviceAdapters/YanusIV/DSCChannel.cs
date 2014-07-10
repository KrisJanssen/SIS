// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DSCChannel.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The DSC channel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    /// <summary>
    /// The DSC channel.
    /// </summary>
    public enum DscChannel
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
        /// 3 bits pattern/marker
        /// </summary>
        DO = 7 // Channel 7 (digital outputs - 3 bits pattern/marker)
    }
}