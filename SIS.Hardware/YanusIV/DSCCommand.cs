// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DSCCommand.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Structure that represents a DSC command line
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware.YanusIV
{
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
        /// The scan command.
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
        /// <param name="scanCommand">
        /// The scan command.
        /// </param>
        /// <param name="cycleNumber">
        /// The DSC cycle when the command should run.
        /// </param>
        /// <param name="channel">
        /// The DSC channel.
        /// </param>
        /// <param name="channelValue">
        /// The value that needs to be applied to the channel.
        /// </param>
        public DSCCommand(ScanCommand scanCommand, ulong cycleNumber, DSCChannel channel, long channelValue)
        {
            this.ScanCmd = scanCommand;
            this.Cycle = cycleNumber;
            this.Channel = channel;
            this.Value = channelValue;
        }

        #endregion
    }

    /// <summary>
    /// The DSP command.
    /// </summary>
    public enum DSPCommand
    {

        // Does nothing, but keeps the interface busy and signals. Break of protocol execution; can also serve as comment/marker.
        // No arguments.
        /// <summary>
        /// Do nothing.
        /// </summary>
        DSP_CMD_DO_NOTHING = '#', 

        // Tells DSP to return null terminated information string (about YanusIV).
        // No arguments.
        /// <summary>
        /// Return info.
        /// </summary>
        DSP_CMD_RETURN_INFO = 'R', 

        // Clears the scan control protocol.
        // No arguments.
        // Returns 0 on success.
        /// <summary>
        /// Clear protocol.
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
        /// Add protocol.
        /// </summary>
        DSP_CMD_ADD_PROT_CMD = 'A', 

        // Initialize protocol: sort commands within blocks ...(?) // currently not functional.
        // No arguments.
        /// <summary>
        /// Initialize protocol.
        /// </summary>
        DSP_CMD_INIT_PROT = 'I', 

        // Executes the current protocol.
        // No arguments.
        // Returns 0 on success or a numerical error code.
        /// <summary>
        /// Execute protocol.
        /// </summary>
        DSP_CMD_EXECUTE_PROT = 'X', 

        // List protocol loaded in YanusIV.
        // No arguments.
        /// <summary>
        /// List loaded protocol.
        /// </summary>
        DSP_CMD_LIST_PROT = 'L', 

        // Shows the contents of the buffer for scan channel "channel" 
        // Argument:
        // int <channel>
        /// <summary>
        /// Show the buffer.
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
        /// Set value.
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
        /// Do nothing.
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

    /// <summary>
    /// An enumeration of all error codes that can be returned by the YanusIV DSC.
    /// Codes are returned by the DSC via RS232 as decimal numbers in ASCII representation.
    /// </summary>
    public enum DSPError
    {
        // Nothing wrong here!

        /// <summary>
        /// No error.
        /// </summary>
        SCAN_CMD_NO_ERROR = 0, 

        // Calculation took too long for 10 us frame

        /// <summary>
        /// Calculation timeout.
        /// </summary>
        SCAN_CMD_RUN_TIMEOUT = 1, 

        // Run aborted by sending character on RS232

        /// <summary>
        /// Scan aborted.
        /// </summary>
        SCAN_CMD_RUN_ABORTED = 2, 

        // Trying to run an empty command list

        /// <summary>
        /// Command list empty.
        /// </summary>
        SCAN_CMD_LIST_EMPTY = 3, 

        // Trying to run a command list with unclosed loops.

        /// <summary>
        /// Command list with unclosed loops.
        /// </summary>
        SCAN_CMD_LIST_NOT_CLOSED = 4, 

        // Tried to add command to a command-list that has already maximum length.

        /// <summary>
        /// Scan list exceeded maximum length.
        /// </summary>
        SCAN_CMD_LIST_OVERFLOW = 10, 

        // Cycle time of newly added command is impossible.

        /// <summary>
        /// Incorrect cycle time for command.
        /// </summary>
        SCAN_CMD_LIST_DISORDER = 11, 

        // Command is specified with non-existing channel number.

        /// <summary>
        /// Invalid channel.
        /// </summary>
        SCAN_CMD_INVALID_CHANNEL = 12, 

        // Too many nested loops.

        /// <summary>
        /// Too many nested loops.
        /// </summary>
        SCAN_CMD_LOOP_OVERFLOW = 13, 

        // Invalid number of loop, iterations (<0)

        /// <summary>
        /// Invalid number of loop iterations.
        /// </summary>
        SCAN_CMD_INVALID_ITERATIONS = 14, 

        // Trying to close a loop, which is already closed.

        /// <summary>
        /// Loop is already closed.
        /// </summary>
        SCAN_CMD_LOOP_IS_CLOSED = 15, 

        // Unknown command
        /// <summary>
        /// Unknown command.
        /// </summary>
        SCAN_CMD_UNKONWN_COMMAND = 16, 

        // No debug buffer available for the selected channel

        /// <summary>
        /// No buffer available for command.
        /// </summary>
        SCAN_CMD_NO_BUFFER = 17, 

        // Invalid syntax when adding a scan command:
        // not enough parameters or too many parameters.

        /// <summary>
        /// Invalid syntax.
        /// </summary>
        SCAN_CMD_INVALID_SYNTAX = 18, 

        // COM port is closed and DSP commands cannot be sent.

        /// <summary>
        /// Com port closed.
        /// </summary>
        SCAN_CMD_COM_PORT_CLOSED = 19, 

        // Galvo is OFF and DSP commands cannot be sent.

        /// <summary>
        /// Galvo is off.
        /// </summary>
        SCAN_CMD_COM_GALVO_IS_OFF = 20
    }
}