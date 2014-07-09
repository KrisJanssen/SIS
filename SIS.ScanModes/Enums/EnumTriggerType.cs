// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumTriggerType.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The trigger type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.ScanModes.Enums
{
    /// <summary>
    /// The trigger type.
    /// </summary>
    public enum TriggerType : int
    {
        /// <summary>
        /// The pulse trigger.
        /// </summary>
        PulseTrigger = 1, 

        /// <summary>
        /// The level trigger.
        /// </summary>
        LevelTrigger = 2
    }
}