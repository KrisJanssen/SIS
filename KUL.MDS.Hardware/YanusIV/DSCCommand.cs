// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DSCCommand.cs" company="">
//   
// </copyright>
// <summary>
//   Structure that represents a DSC command line
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Hardware.YanusIV
{
    using System;

    /// <summary>
    /// Structure that represents a DSC command line
    /// </summary>
    public struct DSCCommand
    {
        #region Fields

        /// <summary>
        /// The channel.
        /// </summary>
        public DSCChannel Channel; // the channel number - the command will act on this channel

        /// <summary>
        /// The cycle.
        /// </summary>
        public ulong Cycle; // the cycle in which the command will be executed        

        /// <summary>
        /// The scan cmd.
        /// </summary>
        public ScanCommand ScanCmd; // the scan command - defines the type of action to be performed

        /// <summary>
        /// The value.
        /// </summary>
        public long Value; // channel value - the value to set for the channel value

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DSCCommand"/> struct.
        /// </summary>
        /// <param name="__scCommand">
        /// The __sc command.
        /// </param>
        /// <param name="__ui64Cycle">
        /// The __ui 64 cycle.
        /// </param>
        /// <param name="__dsccChannel">
        /// The __dscc channel.
        /// </param>
        /// <param name="__int64Value">
        /// The __int 64 value.
        /// </param>
        public DSCCommand(ScanCommand __scCommand, ulong __ui64Cycle, DSCChannel __dsccChannel, long __int64Value)
        {
            this.ScanCmd = __scCommand;
            this.Cycle = __ui64Cycle;
            this.Channel = __dsccChannel;
            this.Value = __int64Value;
        }

        #endregion
    }

    /// <summary>
    /// The dsc channel.
    /// </summary>
    public enum DSCChannel
    {
        /// <summary>
        /// The none.
        /// </summary>
        None = 0, // Channel 0 (dummy channel)

        /// <summary>
        /// The x.
        /// </summary>
        X = 3, // Channel 3 (X axis)

        /// <summary>
        /// The y.
        /// </summary>
        Y = 4, // Channel 4 (Y axis)

        /// <summary>
        /// The polytrope.
        /// </summary>
        Polytrope = 5, // Channel 5 (Polytrope)

        /// <summary>
        /// The do.
        /// </summary>
        DO = 7 // Channel 7 (digital outputs - 3 bits pattern/marker)
    }

    /// <summary>
    /// The dsp command.
    /// </summary>
    public enum DSPCommand
    {
        // Does nothing, but keeps the interface busy and signals. Break of protocol execution; can also serve as comment/marker.
        // No arguments.
        /// <summary>
        /// The ds p_ cm d_ d o_ nothing.
        /// </summary>
        DSP_CMD_DO_NOTHING = '#', 

        // Tells DSP to return null terminated information string (about YanusIV).
        // No arguments.
        /// <summary>
        /// The ds p_ cm d_ retur n_ info.
        /// </summary>
        DSP_CMD_RETURN_INFO = 'R', 

        // Clears the scan control protocol.
        // No arguments.
        // Returns 0 on success.
        /// <summary>
        /// The ds p_ cm d_ clea r_ prot.
        /// </summary>
        DSP_CMD_CLEAR_PROT = 'C', 

        // Adds a command to the scan command list.
        // Arguments:
        // char <scanner control command (see below the ScanCommand enum struct)>
        // cycles_t <cycle (current type long)>
        // int <channel>
        // int <value>
        // All arguments must be present. Even if they don't apply to the scan command.
        // Returns 0 on success or an error.
        /// <summary>
        /// The ds p_ cm d_ ad d_ pro t_ cmd.
        /// </summary>
        DSP_CMD_ADD_PROT_CMD = 'A', 

        // Initialize protocol: sort commands within blocks ...(?) // currently not functional.
        // No arguments.
        /// <summary>
        /// The ds p_ cm d_ ini t_ prot.
        /// </summary>
        DSP_CMD_INIT_PROT = 'I', 

        // Executes the current protocol.
        // No arguments.
        // Returns 0 on success or a numerical error code.
        /// <summary>
        /// The ds p_ cm d_ execut e_ prot.
        /// </summary>
        DSP_CMD_EXECUTE_PROT = 'X', 

        // List protocol loaded in YanusIV.
        // No arguments.
        /// <summary>
        /// The ds p_ cm d_ lis t_ prot.
        /// </summary>
        DSP_CMD_LIST_PROT = 'L', 

        // Shows the contents of the buffer for scan channel "channel" 
        // Argument:
        // int <channel>
        /// <summary>
        /// The ds p_ cm d_ sho w_ buffer.
        /// </summary>
        DSP_CMD_SHOW_BUFFER = 'B', 

        // Directly sets the value of a scan channel. Like the 'V' command in 
        // scan ScanCommand protocols, but the current protocol remains unchanged and the 
        // value is set immediately after the line is parsed.
        // Arguments:
        // int <channel>
        // long <value>
        // Returns 0 on success or a numerical error code.
        /// <summary>
        /// The ds p_ cm d_ se t_ value.
        /// </summary>
        DSP_CMD_SET_VALUE = 'V'
    }

    /// <summary>
    /// The scan command.
    /// </summary>
    public enum ScanCommand
    {
        // Dummy command - e.g. to extend execution of commands beyond last "real" command.
        /// <summary>
        /// The scan_cmd_do_nothing.
        /// </summary>
        scan_cmd_do_nothing = (int)'0', 

        // Command to directly set the value of an output channel.
        // cycle: 10 μs cycle, in which value is set
        // channel: channel, which is set
        // value: value, to which the channel is set
        /// <summary>
        /// The scan_cmd_set_value.
        /// </summary>
        scan_cmd_set_value = 'V', 

        // Command to set value of an output channel relative to the current value.
        /// <summary>
        /// The scan_cmd_set_value_relative.
        /// </summary>
        scan_cmd_set_value_relative = 'R', 

        // Command to set value by which a channel is incremented every cycle (^= 1st derivative).
        // cycle: 10 μs cycle, in which value is set
        // channel: channel, whose 1st increment is set
        // value: increment value
        /// <summary>
        /// The scan_cmd_set_increment_1.
        /// </summary>
        scan_cmd_set_increment_1 = 'I', 

        // Command to set value by which the increment of a channel is incremented every cycle (^= 2nd derivative).
        // cycle: 10 μs cycle, in which value is set
        // channel: channel, whose 2nd increment is set
        // value: increment value
        /// <summary>
        /// The scan_cmd_set_increment_2.
        /// </summary>
        scan_cmd_set_increment_2 = 'J', 

        // Start of a loop; Up to 100 loops can be nested. 
        // channel: not used
        // value: number of iterations
        /// <summary>
        /// The scan_cmd_loop_start.
        /// </summary>
        scan_cmd_loop_start = 'S', 

        // End of a loop
        // channel: not used 
        // value: not used
        /// <summary>
        /// The scan_cmd_loop_end.
        /// </summary>
        scan_cmd_loop_end = 'E', 

        // Wait for a rising trigger.
        // cycle: execution of commands behind this one is halted at this cycle until a rising trigger is detected
        // channel: not used
        // value: not used
        // While the protocol is waiting for a trigger, increments are not applied.
        /// <summary>
        /// The scan_cmd_wait_trig_rising.
        /// </summary>
        scan_cmd_wait_trig_rising = 'U', 

        // Wait for falling trigger.
        // Parameters and usage same as for scan_cmd_wait_trig_rising
        /// <summary>
        /// The scan_cmd_wait_trig_falling.
        /// </summary>
        scan_cmd_wait_trig_falling = 'D'
    }

    /// <summary>
    /// An enumeration of all error codes that can be returned by the YanusIV DSC.
    /// Codes are returned by the DSC via RS232 as decimal numbers in ASCII representation.
    /// </summary>
    public enum DSPError
    {
        // Nothing wrong here!
        /// <summary>
        /// The sca n_ cm d_ n o_ error.
        /// </summary>
        SCAN_CMD_NO_ERROR = 0, 

        // Calculation took too long for 10 us frame
        /// <summary>
        /// The sca n_ cm d_ ru n_ timeout.
        /// </summary>
        SCAN_CMD_RUN_TIMEOUT = 1, 

        // Run aborted by sending character on RS232
        /// <summary>
        /// The sca n_ cm d_ ru n_ aborted.
        /// </summary>
        SCAN_CMD_RUN_ABORTED = 2, 

        // Trying to run an empty command list
        /// <summary>
        /// The sca n_ cm d_ lis t_ empty.
        /// </summary>
        SCAN_CMD_LIST_EMPTY = 3, 

        // Trying to run a command list with unclosed loops.
        /// <summary>
        /// The sca n_ cm d_ lis t_ no t_ closed.
        /// </summary>
        SCAN_CMD_LIST_NOT_CLOSED = 4, 

        // Tried to add command to a command-list that has already maximum length.
        /// <summary>
        /// The sca n_ cm d_ lis t_ overflow.
        /// </summary>
        SCAN_CMD_LIST_OVERFLOW = 10, 

        // Cycle time of newly added command is impossible.
        /// <summary>
        /// The sca n_ cm d_ lis t_ disorder.
        /// </summary>
        SCAN_CMD_LIST_DISORDER = 11, 

        // Command is specified with non-existing channel number.
        /// <summary>
        /// The sca n_ cm d_ invali d_ channel.
        /// </summary>
        SCAN_CMD_INVALID_CHANNEL = 12, 

        // Too many nested loops.
        /// <summary>
        /// The sca n_ cm d_ loo p_ overflow.
        /// </summary>
        SCAN_CMD_LOOP_OVERFLOW = 13, 

        // Invalid number of loop, iterations (<0)
        /// <summary>
        /// The sca n_ cm d_ invali d_ iterations.
        /// </summary>
        SCAN_CMD_INVALID_ITERATIONS = 14, 

        // Trying to close a loop, which is already closed.
        /// <summary>
        /// The sca n_ cm d_ loo p_ i s_ closed.
        /// </summary>
        SCAN_CMD_LOOP_IS_CLOSED = 15, 

        // Unknown command
        /// <summary>
        /// The sca n_ cm d_ unkonw n_ command.
        /// </summary>
        SCAN_CMD_UNKONWN_COMMAND = 16, 

        // No debug buffer available for the selected channel
        /// <summary>
        /// The sca n_ cm d_ n o_ buffer.
        /// </summary>
        SCAN_CMD_NO_BUFFER = 17, 

        // Invalid syntax when adding a scan command:
        // not enough parameters or too many parameters.
        /// <summary>
        /// The sca n_ cm d_ invali d_ syntax.
        /// </summary>
        SCAN_CMD_INVALID_SYNTAX = 18, 

        // COM port is closed and DSP commands cannot be sent.
        /// <summary>
        /// The sca n_ cm d_ co m_ por t_ closed.
        /// </summary>
        SCAN_CMD_COM_PORT_CLOSED = 19, 

        // Galvo is OFF and DSP commands cannot be sent.
        /// <summary>
        /// The sca n_ cm d_ co m_ galv o_ i s_ off.
        /// </summary>
        SCAN_CMD_COM_GALVO_IS_OFF = 20
    }
}