// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessorFeature.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The processor feature.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;

    /// <summary>
    /// The processor feature.
    /// </summary>
    [Flags]
    public enum ProcessorFeature
    {
        /// <summary>
        /// The dep.
        /// </summary>
        DEP = 1, 

        /// <summary>
        /// The sse.
        /// </summary>
        SSE = 2, 

        /// <summary>
        /// The ss e 2.
        /// </summary>
        SSE2 = 4, 

        /// <summary>
        /// The ss e 3.
        /// </summary>
        SSE3 = 8, 
    }
}