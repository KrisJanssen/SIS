// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanCommand.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The scan command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    /// <summary>
    /// The scan command.
    /// </summary>
    internal enum ScanCommand
    {
        /// <summary>
        /// Do nothing.
        /// Dummy command - e.g. to extend execution of commands beyond last "real" command.
        /// </summary>
        scan_cmd_do_nothing = (int)'0', 

        // Command to directly set the value of an output channel.
        // cycle: 10 μs cycle, in which value is set
        // channel: channel, which is set
        // value: value, to which the channel is set

        /// <summary>
        /// Set value.
        /// </summary>
        scan_cmd_set_value = 'V', 

        // Command to set value of an output channel relative to the current value.

        /// <summary>
        /// Set relative value.
        /// </summary>
        scan_cmd_set_value_relative = 'R', 

        // Command to set value by which a channel is incremented every cycle (^= 1st derivative).
        // cycle: 10 μs cycle, in which value is set
        // channel: channel, whose 1st increment is set
        // value: increment value

        /// <summary>
        /// Set increment.
        /// </summary>
        scan_cmd_set_increment_1 = 'I', 

        // Command to set value by which the increment of a channel is incremented every cycle (^= 2nd derivative).
        // cycle: 10 μs cycle, in which value is set
        // channel: channel, whose 2nd increment is set
        // value: increment value

        /// <summary>
        /// Set increment of increment.
        /// </summary>
        scan_cmd_set_increment_2 = 'J', 

        // Start of a loop; Up to 100 loops can be nested. 
        // channel: not used
        // value: number of iterations

        /// <summary>
        /// Loop start.
        /// </summary>
        scan_cmd_loop_start = 'S', 

        // End of a loop
        // channel: not used 
        // value: not used

        /// <summary>
        /// Loop end.
        /// </summary>
        scan_cmd_loop_end = 'E', 

        // Wait for a rising trigger.
        // cycle: execution of commands behind this one is halted at this cycle until a rising trigger is detected
        // channel: not used
        // value: not used
        // While the protocol is waiting for a trigger, increments are not applied.

        /// <summary>
        /// Wait trigger rising.
        /// </summary>
        scan_cmd_wait_trig_rising = 'U', 

        // Wait for falling trigger.
        // Parameters and usage same as for scan_cmd_wait_trig_rising

        /// <summary>
        /// Wait trigger falling.
        /// </summary>
        scan_cmd_wait_trig_falling = 'D'
    }
}