namespace SIS.Hardware.YanusIV
{
    using System;

    /// <summary>
    /// Structure that represents a DSC command line
    /// </summary>
    public struct DSCCommand
    {
        public ScanCommand ScanCmd;  // the scan command - defines the type of action to be performed
        public UInt64 Cycle;  // the cycle in which the command will be executed        
        public DSCChannel Channel;  // the channel number - the command will act on this channel
        public Int64 Value;  // channel value - the value to set for the channel value
        
        public DSCCommand(ScanCommand __scCommand, UInt64 __ui64Cycle, DSCChannel __dsccChannel, Int64 __int64Value)
        {
            this.ScanCmd = __scCommand;
            this.Cycle = __ui64Cycle;            
            this.Channel = __dsccChannel;
            this.Value = __int64Value;
        }        
    }


    /// <summary>
    /// Channel can be axes Channel 3 (X), Channel 4 (Y), Channel 5 (Polytrope) or
    /// digital outputs (Channel 7). Value is either the galvo position or the 
    /// digital outputs 3 bits pattern/marker.
    /// <summary>    
    public enum DSCChannel
    {
        None = 0,  // Channel 0 (dummy channel)
        X = 3,  // Channel 3 (X axis)
        Y = 4,  // Channel 4 (Y axis)
        Polytrope = 5,  // Channel 5 (Polytrope)
        DO = 7  // Channel 7 (digital outputs - 3 bits pattern/marker)
    }


    /// <summary> 
    /// Definition of DSC control commands for addressing the scan controller
    /// <summary>
    public enum DSPCommand
    {
        // Does nothing, but keeps the interface busy and signals. Break of protocol execution; can also serve as comment/marker.
        // No arguments.
        DSP_CMD_DO_NOTHING = '#',

        // Tells DSP to return null terminated information string (about YanusIV).
        // No arguments.
        DSP_CMD_RETURN_INFO = 'R',

        // Clears the scan control protocol.
        // No arguments.
        // Returns 0 on success.
        DSP_CMD_CLEAR_PROT = 'C',  

        // Adds a command to the scan command list.
        // Arguments:
        //  char <scanner control command (see below the ScanCommand enum struct)>
        //  cycles_t <cycle (current type long)>
        //  int <channel>
        //  int <value>
        // All arguments must be present. Even if they don't apply to the scan command.
        // Returns 0 on success or an error.
        DSP_CMD_ADD_PROT_CMD = 'A',

        // Initialize protocol: sort commands within blocks ...(?) // currently not functional.
        // No arguments.
        DSP_CMD_INIT_PROT = 'I',

        // Executes the current protocol.
        // No arguments.
        // Returns 0 on success or a numerical error code.
        DSP_CMD_EXECUTE_PROT = 'X',

        // List protocol loaded in YanusIV.
        // No arguments.
        DSP_CMD_LIST_PROT = 'L',

        // Shows the contents of the buffer for scan channel "channel" 
        // Argument:
        //  int <channel>
        DSP_CMD_SHOW_BUFFER = 'B',

        // Directly sets the value of a scan channel. Like the 'V' command in 
        // scan ScanCommand protocols, but the current protocol remains unchanged and the 
        // value is set immediately after the line is parsed.
        // Arguments:
        //  int <channel>
        //  long <value>
        // Returns 0 on success or a numerical error code.
        DSP_CMD_SET_VALUE = 'V'
    }


    /// <summary>
    /// The scan control commands have three parameters:
    ///  cycle (long; 48 bit; number of 10μs cycle, in which command is executed)
    ///  channel (integer; 24 bit; output channel, whose increment is changed)
    ///  value (long; 48 bit; value to set)
    /// The scan control commands are:
    /// <summary>
    public enum ScanCommand
    {
        // Dummy command - e.g. to extend execution of commands beyond last "real" command.
        scan_cmd_do_nothing = (int) '0',

        // Command to directly set the value of an output channel.
        //  cycle: 10 μs cycle, in which value is set
        //  channel: channel, which is set
        //  value: value, to which the channel is set
        scan_cmd_set_value = 'V',

        // Command to set value of an output channel relative to the current value.
        scan_cmd_set_value_relative = 'R',

        // Command to set value by which a channel is incremented every cycle (^= 1st derivative).
        //  cycle: 10 μs cycle, in which value is set
        //  channel: channel, whose 1st increment is set
        //  value: increment value
        scan_cmd_set_increment_1 = 'I',

        // Command to set value by which the increment of a channel is incremented every cycle (^= 2nd derivative).
        //  cycle: 10 μs cycle, in which value is set
        //  channel: channel, whose 2nd increment is set
        //  value: increment value
        scan_cmd_set_increment_2 = 'J',

        // Start of a loop; Up to 100 loops can be nested. 
        //  channel: not used
        //  value: number of iterations
        scan_cmd_loop_start = 'S',

        // End of a loop
        //  channel: not used 
        //  value: not used
        scan_cmd_loop_end = 'E',

        // Wait for a rising trigger.
        //  cycle: execution of commands behind this one is halted at this cycle until a rising trigger is detected
        //  channel: not used
        //  value: not used
        // While the protocol is waiting for a trigger, increments are not applied.
        scan_cmd_wait_trig_rising = 'U',

        // Wait for falling trigger.
        // Parameters and usage same as for scan_cmd_wait_trig_rising
        scan_cmd_wait_trig_falling = 'D'
    }
    
    
    /// <summary>
    /// An enumeration of all error codes that can be returned by the YanusIV DSC.
    /// Codes are returned by the DSC via RS232 as decimal numbers in ASCII representation.
    /// </summary>
    public enum DSPError
    {
        // Nothing wrong here!
        SCAN_CMD_NO_ERROR = 0,

        // Calculation took too long for 10 us frame
        SCAN_CMD_RUN_TIMEOUT = 1,

        // Run aborted by sending character on RS232
        SCAN_CMD_RUN_ABORTED = 2,

        // Trying to run an empty command list
        SCAN_CMD_LIST_EMPTY = 3,

        // Trying to run a command list with unclosed loops.
        SCAN_CMD_LIST_NOT_CLOSED = 4,

        // Tried to add command to a command-list that has already maximum length.
        SCAN_CMD_LIST_OVERFLOW = 10,

        // Cycle time of newly added command is impossible.
        SCAN_CMD_LIST_DISORDER = 11,

        // Command is specified with non-existing channel number.
        SCAN_CMD_INVALID_CHANNEL = 12,

        // Too many nested loops.
        SCAN_CMD_LOOP_OVERFLOW = 13,

        // Invalid number of loop, iterations (<0)
        SCAN_CMD_INVALID_ITERATIONS = 14,

        // Trying to close a loop, which is already closed.
        SCAN_CMD_LOOP_IS_CLOSED = 15,

        // Unknown command
        SCAN_CMD_UNKONWN_COMMAND = 16,

        // No debug buffer available for the selected channel
        SCAN_CMD_NO_BUFFER = 17,

        // Invalid syntax when adding a scan command:
        // not enough parameters or too many parameters.
        SCAN_CMD_INVALID_SYNTAX = 18,
                
        // COM port is closed and DSP commands cannot be sent.
        SCAN_CMD_COM_PORT_CLOSED = 19,

		// Galvo is OFF and DSP commands cannot be sent.
        SCAN_CMD_COM_GALVO_IS_OFF = 20
    }
}
